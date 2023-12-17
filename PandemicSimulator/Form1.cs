using Simulator;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Drawing.Imaging;

namespace PandemicSimulator
{
    public partial class Form1 : Form
    {
        #region Fields
        static CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
        private CancellationToken simulationCancellationToken = cancellationTokenSource.Token;
        private Bitmap _worldBitmap = null!;
        private Simulation? _simulation;
        private SimulationConfig? _config;
        private Stopwatch _stopwatch = new Stopwatch();
        private int _frameCount = 0;
        private decimal _fps = 0;
        private int _iterations = 0;
        private Thread? _simulationThread;

        private int texture;
        private MyGLControl glControl;
        #endregion

        #region Form
        public Form1()
        {
            InitializeComponent();
            // In your form constructor or initialization code
            //SetStyle(ControlStyles.DoubleBuffer | ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint, true);
            //UpdateStyles();

            Screen screen = Screen.PrimaryScreen;
            int width = screen.Bounds.Width;
            int height = screen.Bounds.Height;

            width = 1920;
            height = 1080;

            Size = new Size(width, height);
            this.CenterToScreen();
            glControl = new MyGLControl();
            Controls.Add(glControl);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            glControl.InitializeOpenGLControl();
        }
        #endregion

        #region Control events
        private void tsmiStart_Click(object sender, EventArgs e)
        {
            if (_config is null || !_config.IsValid())
            {
                MessageBox.Show("Please configure the simulation first!");
                return;
            }

            tsmiStart.Enabled = false;
            tsmiCancle.Enabled = true;
            _simulation = new Simulation(_config, simulationCancellationToken);
            //_simulation.Initialize();
            _worldBitmap = new Bitmap(_config.Width, _config.Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            // Simulation Events
            _simulation.OnSimulationUpdated += Simulation_OnSimulationUpdated;
            _simulation.OnSimulationFinished += Simulation_OnSimulationFinished;

            _simulationThread = new Thread(() => _simulation!.Run());
            _simulationThread.IsBackground = true;
            _simulationThread.Start();
            //Task simulationTask = Task.Run(() => _simulation!.Run());
            //_simulation.Run();
        }

        private void tsmiCancle_Click(object sender, EventArgs e)
        {
            cancellationTokenSource.Cancel();
        }

        private void tsmiConfig_Click(object sender, EventArgs e)
        {
            using (var configurationForm = new ConfigurationForm())
            {
                var dialogResult = configurationForm.ShowDialog();
                if (dialogResult is DialogResult.OK or DialogResult.Cancel)
                {
                    _config = configurationForm.Configuration;
                    if (_config.IsValid())
                    {
                        tsmiStart.Enabled = true;
                    }
                }
            }
        }
        #endregion

        #region Handle events
        private void Simulation_OnSimulationFinished(object? sender, EventArgs e)
        {
            MessageBox.Show("Simulation finished!");
        }

        private void Simulation_OnSimulationUpdated(object? sender, SimulationUpdateEventArgs e)
        {
            UpdateImgPartially(e.MovedPeople, e.PeopleDied);
            //UpdateTexture();
            glControl.WorldBitmap = _worldBitmap;
            glControl.UpdateTexture();
            UpdateUI();
        }
        #endregion

        private enum PersonColor
        {
            Healthy,
            Infected,
            Contagious,
            LowHealth,
            InfectedAndLowHealth,
            ContagiousAndLowHealth
        }

        readonly Dictionary<PersonColor, Color> _personColors = new()
        {
            [PersonColor.Healthy] = Color.FromArgb(255, 0, 255, 0),
            [PersonColor.Infected] = Color.FromArgb(255, 255, 0, 0),
            [PersonColor.Contagious] = Color.FromArgb(255, 255, 255, 0),
            [PersonColor.LowHealth] = Color.FromArgb(255, 0, 0, 255),
            [PersonColor.InfectedAndLowHealth] = Color.FromArgb(255, 255, 0, 255),
            [PersonColor.ContagiousAndLowHealth] = Color.FromArgb(255, 255, 255, 255)
        };

        private void UpdateImgPartially(IEnumerable<MovedPerson> movedPeople, IEnumerable<Point> died)
        {
            BitmapData bmpData = _worldBitmap.LockBits(new Rectangle(0, 0, _worldBitmap.Width, _worldBitmap.Height), ImageLockMode.WriteOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            try
            {
                // Remove dead people from the bitmap
                Parallel.ForEach(died, (point) =>
                {
                    int index = point.Y * bmpData.Stride + point.X * 4; // Assuming 32bppArgb format
                    unsafe
                    {
                        byte* ptr = (byte*)bmpData.Scan0;
                        ptr[index] = 0;
                        ptr[index + 1] = 0;
                        ptr[index + 2] = 0;
                        ptr[index + 3] = 255;
                    }
                });

                // Remove moved peoples previous position from the bitmap
                Parallel.ForEach(movedPeople, (person) =>
                {
                    int index = person.PreviousPosition.Y * bmpData.Stride + person.PreviousPosition.X * 4; // Assuming 32bppArgb format
                    unsafe
                    {
                        byte* ptr = (byte*)bmpData.Scan0;
                        ptr[index] = 0;
                        ptr[index + 1] = 0;
                        ptr[index + 2] = 0;
                        ptr[index + 3] = 255;
                    }
                });

                // Draw moved peoples current position on the bitmap
                Parallel.ForEach(movedPeople, (person) =>
                {
                    int index = person.CurrentPosition.Y * bmpData.Stride + person.CurrentPosition.X * 4; // Assuming 32bppArgb format

                    Color color = GetPixelColorBasedOnPersonCondition(person);

                    unsafe
                    {
                        byte* ptr = (byte*)bmpData.Scan0;
                        ptr[index] = color.A;
                        ptr[index + 1] = color.R;
                        ptr[index + 2] = color.G;
                        ptr[index + 3] = color.B;
                    }
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                _worldBitmap.UnlockBits(bmpData);
            }
        }

        private Color GetPixelColorBasedOnPersonCondition(MovedPerson person)
        {
            Color color;
            if (person.IsInfected && person.IsContagious)
                color = _personColors[PersonColor.Contagious];
            else if (person.IsInfected)
                color = _personColors[PersonColor.Infected];
            else if (person.IsContagious)
                color = _personColors[PersonColor.Contagious];
            else if (person.Health < 50)
                color = _personColors[PersonColor.LowHealth];
            else if (person.IsInfected && person.Health < 50)
                color = _personColors[PersonColor.InfectedAndLowHealth];
            else if (person.IsContagious && person.Health < 50)
                color = _personColors[PersonColor.ContagiousAndLowHealth];
            else
                color = Color.Azure;
            return color;
        }

        private void UpdateImg()
        {
            BitmapData bmpData = _worldBitmap.LockBits(new Rectangle(0, 0, _worldBitmap.Width, _worldBitmap.Height), ImageLockMode.WriteOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            try
            {
                unsafe
                {
                    byte* ptr = (byte*)bmpData.Scan0;
                    int stride = bmpData.Stride;

                    Parallel.For(0, _worldBitmap.Height, y =>
                    {
                        for (int x = 0; x < GetBitmapWidth(); x++)
                        {
                            int index = y * stride + x * 4; // Assuming 32bppArgb format
                            ptr[index] = (byte)GetRandomValue(0, 255);
                            ptr[index + 1] = (byte)GetRandomValue(0, 255);
                            ptr[index + 2] = (byte)GetRandomValue(0, 255);
                            ptr[index + 3] = 255;
                        }
                    });
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                _worldBitmap.UnlockBits(bmpData);
            }
        }

        int GetBitmapWidth()
        {
            return _config!.Width;
        }

        private static readonly Random _random = new();
        static int GetRandomValue(int min, int max)
        {
            //lock (_random)
            return _random.Next(min, max);
        }

        private void tsmiTest_Click(object sender, EventArgs e)
        {
            _config ??= new SimulationConfig()
            {
                Height = 300,
                Width = 300,
                InitialInfected = 10,
                Population = 100
            };
            _worldBitmap = new Bitmap(_config.Width, _config.Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            _stopwatch.Start();
            timer1.Start();

            _simulationThread = new Thread(TestLoop);
            _simulationThread.IsBackground = true;
            _simulationThread.Start();
        }

        private void TestLoop()
        {
            while (!simulationCancellationToken.IsCancellationRequested && !this.IsDisposed)
            {
                Interlocked.Increment(ref _frameCount);
                _iterations++;
                UpdateImg();
                UpdateUI();
            }
            Console.WriteLine();
        }

        private void UpdateUI()
        {
            try
            {
                glControl.WorldBitmap = _worldBitmap;
                if (InvokeRequired)
                {
                    Invoke(glControl.UpdateTexture);
                    Invoke(() => lblIterations.Text = $"Iterations: {_iterations}");
                }
                else
                {
                    glControl.UpdateTexture();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (_stopwatch.ElapsedMilliseconds >= 1000)
            {
                _fps = Math.Round(((decimal)_frameCount / (_stopwatch.ElapsedMilliseconds / 1000)), 2);
                Invoke(() => lblFps.Text = $"FPS: {_fps}");
                _frameCount = 0;
                _stopwatch.Restart();
            }
        }
    }
}