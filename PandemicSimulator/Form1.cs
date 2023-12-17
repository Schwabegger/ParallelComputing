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

        private int texture;

        private MyGLControl glControl;
        BoolLock[,] _locks = null!;
        private sealed record BoolLock { public bool IsNewPerson { get; set; } = false; };
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
            if (_config is not null && _config.IsValid())
            {
                MessageBox.Show("Please configure the simulation first!");
                return;
            }

            tsmiStart.Enabled = false;
            tsmiCancle.Enabled = true;

            _locks = new BoolLock[_config!.Width, _config!.Height];
            for (int x = 0; x < _config.Width; x++)
            {
                for (int y = 0; y < _config.Height; y++)
                {
                    _locks[x, y] = new BoolLock();
                }
            }
            _simulation = new Simulation(_config, simulationCancellationToken);
            //_simulation.Initialize();
            _worldBitmap = new Bitmap(_config.Width, _config.Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            // Simulation Events
#warning Simulation_OnSimulationUpdated is async void
            _simulation.OnSimulationUpdated += Simulation_OnSimulationUpdated;
            _simulation.OnSimulationFinished += Simulation_OnSimulationFinished;

            _simulation.Run();
        }

        private void tsmiCancle_Click(object sender, EventArgs e)
        {

        }

        private void tsmiConfig_Click(object sender, EventArgs e)
        {
            using (var configurationForm = new ConfigurationForm())
            {
                if (configurationForm.ShowDialog() == DialogResult.OK)
                {
                    _config = configurationForm.Configuration;
                }
            }
        }
        #endregion

        #region Handle events
        private void Simulation_OnSimulationFinished(object? sender, EventArgs e)
        {
            MessageBox.Show("Simulation finished!");
        }

        private async void Simulation_OnSimulationUpdated(object? sender, SimulationUpdateEventArgs e)
        {
            await UpdateImgPartially(e.MovedPeople);
            //UpdateTexture();
            glControl.WorldBitmap = _worldBitmap;
            glControl.UpdateTexture();
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

        private async Task UpdateImgPartially(IEnumerable<MovedPerson> movedPeople)
        {
            BitmapData bmpData = _worldBitmap.LockBits(new Rectangle(0, 0, _worldBitmap.Width, _worldBitmap.Height), ImageLockMode.WriteOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            ConcurrentDictionary<int, Task> tasks = new ConcurrentDictionary<int, Task>();

            try
            {
                //Parallel.ForEach(movedPeople, (person) =>
                //{
                //    int index = person.PreviousPosition.Y * bmpData.Stride + person.PreviousPosition.X * 4; // Assuming 32bppArgb format
                //    lock (_locks[person.PreviousPosition.X, person.PreviousPosition.Y])
                //    {
                //        if (_locks[person.PreviousPosition.X, person.PreviousPosition.Y].IsNewPerson)
                //            return;
                //        unsafe
                //        {
                //            byte* ptr = (byte*)bmpData.Scan0;
                //            ptr[index] = 0;
                //            ptr[index + 1] = 0;
                //            ptr[index + 2] = 0;
                //            ptr[index + 3] = 255;
                //        }
                //    }
                //});

                Parallel.ForEach(movedPeople, (person) =>
                {
                    Task t1 = new(() =>
                    {
                        int index = person.PreviousPosition.Y * bmpData.Stride + person.PreviousPosition.X * 4; // Assuming 32bppArgb format
                        lock (_locks[person.PreviousPosition.X, person.PreviousPosition.Y])
                        {
                            if (_locks[person.PreviousPosition.X, person.PreviousPosition.Y].IsNewPerson)
                                return;
                            unsafe
                            {
                                byte* ptr = (byte*)bmpData.Scan0;
                                ptr[index] = 0;
                                ptr[index + 1] = 0;
                                ptr[index + 2] = 0;
                                ptr[index + 3] = 255;
                            }
                        }
                    });

                    Task t2 = new(() =>
                    {
                        int index = person.CurrentPosition.Y * bmpData.Stride + person.CurrentPosition.X * 4; // Assuming 32bppArgb format
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

                        lock (_locks[person.CurrentPosition.X, person.CurrentPosition.Y])
                        {
                            unsafe
                            {
                                byte* ptr = (byte*)bmpData.Scan0;
                                ptr[index] = color.A;
                                ptr[index + 1] = color.R;
                                ptr[index + 2] = color.G;
                                ptr[index + 3] = color.B;
                            }
                            _locks[person.CurrentPosition.X, person.CurrentPosition.Y].IsNewPerson = true;
                        }
                    });

                    tasks.TryAdd(t1.Id, t1);
                    tasks.TryAdd(t2.Id, t2);

                    t1.Start();
                    t2.Start();
                });
                await Task.WhenAll(tasks.Values.AsEnumerable());
                Parallel.ForEach(movedPeople, (person, _) =>
                {
                    _locks[person.CurrentPosition.X, person.CurrentPosition.Y].IsNewPerson = false;
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

        int _iterations = 0;
        Thread? _simulationThread;
        private void tsmiTest_Click(object sender, EventArgs e)
        {
            _config ??= new SimulationConfig()
            {
                Height = 300,
                Width = 300,
                InitialInfectionRate = 10,
                PopulationSize = 100
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