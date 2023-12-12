using OpenTK;
using OpenTK.Graphics.OpenGL;
using Simulator;
using System.Diagnostics;
using System.Drawing.Imaging;

namespace PandemicSimulator
{
    public partial class Form1 : Form
    {
        static CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
        private CancellationToken simulationCancellationToken = cancellationTokenSource.Token;
        private Bitmap _worldBitmap;
        private Simulation? _simulation;
        private SimulationConfig? _config;
        private Stopwatch _stopwatch = new Stopwatch();
        private int _frameCount = 0;
        private decimal _fps = 0;

        private int texture;

        private GLControl glControl;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }

            // Stop yourThread before the form gets disposed.
            if (simulationThread is not null && simulationThread.IsAlive)
            {
                simulationThread.Abort();
            }

            base.Dispose(disposing);
        }

        public Form1()
        {
            InitializeComponent();
            // In your form constructor or initialization code
            //SetStyle(ControlStyles.DoubleBuffer | ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint, true);
            //UpdateStyles();
            Size = new Size(1000, 1000);
            glControl = new GLControl();
            glControl.Dock = DockStyle.Fill;
            glControl.Paint += GlControl_Paint;
            Controls.Add(glControl);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            InitializeOpenGLControl();
        }

        internal void InitializeOpenGLControl()
        {
            GL.Enable(EnableCap.Texture2D);
            GL.GenTextures(1, out texture);
            GL.BindTexture(TextureTarget.Texture2D, texture);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);
        }

        private void GlControl_Paint(object sender, PaintEventArgs e)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            GL.BindTexture(TextureTarget.Texture2D, texture);
            GL.Begin(PrimitiveType.Quads);
            GL.TexCoord2(0, 0); GL.Vertex2(-1, -1);
            GL.TexCoord2(1, 0); GL.Vertex2(1, -1);
            GL.TexCoord2(1, 1); GL.Vertex2(1, 1);
            GL.TexCoord2(0, 1); GL.Vertex2(-1, 1);
            GL.End();
            glControl.SwapBuffers();
        }

        internal void UpdateTexture()
        {
            BitmapData data = _worldBitmap.LockBits(new Rectangle(0, 0, _worldBitmap.Width, _worldBitmap.Height), ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            try
            {
                GL.BindTexture(TextureTarget.Texture2D, texture);
                GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, data.Width, data.Height, 0, OpenTK.Graphics.OpenGL.PixelFormat.Bgra, PixelType.UnsignedByte, data.Scan0);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                _worldBitmap.UnlockBits(data);
                glControl.Invalidate();
            }
        }

        private void tsmiStart_Click(object sender, EventArgs e)
        {
            if (_config is not null && _config.IsValid())
            {
                MessageBox.Show("Please configure the simulation first!");
                return;
            }
            _simulation = new Simulation(_config, simulationCancellationToken);
            _simulation.Initialize();
            _worldBitmap = new Bitmap(_config.Width, _config.Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            // Simulation Events
            _simulation.OnSimulationUpdated += Simulation_OnSimulationUpdated;
            _simulation.OnSimulationFinished += Simulation_OnSimulationFinished;

            _simulation.Run();
        }

        private void Simulation_OnSimulationFinished(object? sender, EventArgs e)
        {
            MessageBox.Show("Simulation finished!");
        }

        private void Simulation_OnSimulationUpdated(object? sender, SimulationUpdateEventArgs e)
        {
            UpdateImg();
            UpdateTexture();
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
                _autoResetEvent.Set();
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

        int iterations = 0;
        AutoResetEvent _autoResetEvent = new AutoResetEvent(false);
        Thread simulationThread;
        private async void tsmiTest_Click(object sender, EventArgs e)
        {
            _config ??= new SimulationConfig()
            {
                Height = 100,
                Width = 100,
                InitialInfectionRate = 10,
                PersonHealth = 100,
                PopulationSize = 100,
                Virus = new Virus(Name: "Test", MortalityRate: 0.1f, InfectionRate: 0.1f)
            };
            _worldBitmap = new Bitmap(_config.Width, _config.Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            _stopwatch.Start();
            timer1.Start();

            simulationThread = new Thread(() =>
            {
                while (!simulationCancellationToken.IsCancellationRequested && !this.IsDisposed)
                {
                    Interlocked.Increment(ref _frameCount);
                    iterations++;
                    UpdateImg();
                    UpdateUI();
                    //_autoResetEvent.WaitOne();
                }
                MessageBox.Show("Simulation finished!");
            });
            simulationThread.Start();
        }

        private void UpdateUI()
        {
            //glControl.UpdateTexture(_worldBitmap);
            if (InvokeRequired)
            {
                Invoke(UpdateTexture);
                Invoke(() => lblIterations.Text = $"Iterations: {iterations}");
            }
            else
            {
                if (this.IsDisposed || !this.IsHandleCreated) return;
                UpdateTexture();
            }
            //UpdateTexture();
            //Application.DoEvents();
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