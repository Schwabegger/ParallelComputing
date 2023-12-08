using Simulator;
using System;
using System.Diagnostics;
using System.Drawing.Imaging;

namespace PandemicSimulator
{
    public partial class Form1 : Form
    {
        private Bitmap? _bitmap;
        private Simulation? _simulation;
        private SimulationConfig? _config;
        private Stopwatch _stopwatch = new Stopwatch();
        private int _frameCount = 0;
        private decimal _fps = 0;

        public Form1()
        {
            InitializeComponent();
            // In your form constructor or initialization code
            //SetStyle(ControlStyles.DoubleBuffer | ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint, true);
            //UpdateStyles();
        }

        private void tsmiStart_Click(object sender, EventArgs e)
        {
            _simulation = new Simulation(_config);
            _simulation.Initialize();
            _bitmap = new Bitmap(_config.Width, _config.Height, PixelFormat.Format32bppArgb);
            // Simulation Events

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

        private void UpdateImg()
        {
            BitmapData bmpData = _bitmap.LockBits(new Rectangle(0, 0, _bitmap.Width, _bitmap.Height), ImageLockMode.ReadWrite, _bitmap.PixelFormat);

            try
            {
                unsafe
                {
                    byte* ptr = (byte*)bmpData.Scan0;
                    int stride = bmpData.Stride;

                    // Use Parallel.For for parallelization
                    Parallel.For(0, _config!.Height, y =>
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
            finally
            {
                _bitmap.UnlockBits(bmpData);
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

        AutoResetEvent _autoResetEvent = new AutoResetEvent(false);
        private void tsmiTest_Click(object sender, EventArgs e)
        {
            _config ??= new SimulationConfig()
            {
                Height = 2,
                Width = 2,
                InitialInfectionRate = 10,
                PersonHealth = 100,
                PopulationSize = 100,
                Virus = new Virus(Name: "Test", MortalityRate: 0.1f, InfectionRate: 0.1f)
            };
            _bitmap ??= new Bitmap(_config.Width, _config.Height, PixelFormat.Format32bppArgb);
            _stopwatch.Start();
            timer1.Start();

            int iterations = 0;

            while (true)
            {
                _frameCount++;
                iterations++;
                lblIterations.Text = $"Iterations: {iterations}";
                UpdateImg();
                pbWorld.Image = _bitmap;
                //Application.DoEvents();
                switch (_fps)
                {
                    case > 1000:
                        if (iterations % 50 == 0)
                                Application.DoEvents();
                        break;
                    case > 500:
                        if (iterations % 20 == 0)
                            Application.DoEvents();
                        break;
                    case > 240:
                        if (iterations % 10 == 0)
                            Application.DoEvents();
                        break;
                    case > 120:
                        if (iterations % 3 == 0)
                            Application.DoEvents();
                        break;
                    case > 60:
                        if (iterations % 2 == 0)
                            Application.DoEvents();
                        break;
                    default:
                            Application.DoEvents();
                        break;
                }
                _autoResetEvent.WaitOne();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (_stopwatch.ElapsedMilliseconds >= 1000)
            {
                _fps = Math.Round(((decimal)_frameCount / (_stopwatch.ElapsedMilliseconds / 1000)), 2);
                lblFps.Text = $"FPS: {_fps}";
                _frameCount = 0;
                _stopwatch.Restart();
            }
        }
    }
}