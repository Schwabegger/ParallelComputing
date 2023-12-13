using OpenTK;
using OpenTK.Graphics.OpenGL;
using System.Drawing.Imaging;

namespace PandemicSimulator
{
    internal class MyGLControl : GLControl
    {
        private int texture;

        public MyGLControl()
        {
            Dock = DockStyle.Fill;
            Paint += GlControl_Paint;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
        }

        internal void InitializeOpenGLControl()
        {
            GL.ClearColor(Color.Black);
            GL.Enable(EnableCap.Texture2D);
            GL.GenTextures(1, out texture);
            GL.BindTexture(TextureTarget.Texture2D, texture);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);
        }

        private void GlControl_Paint(object? sender, PaintEventArgs e)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            GL.BindTexture(TextureTarget.Texture2D, texture);
            GL.Begin(PrimitiveType.Quads);
            GL.TexCoord2(0, 0); GL.Vertex2(-1, -1);
            GL.TexCoord2(1, 0); GL.Vertex2(1, -1);
            GL.TexCoord2(1, 1); GL.Vertex2(1, 1);
            GL.TexCoord2(0, 1); GL.Vertex2(-1, 1);
            /*
            GL.TexCoord2(0, 0); GL.Vertex2(0, 0);
            GL.TexCoord2(1, 0); GL.Vertex2(Width, 0);
            GL.TexCoord2(1, 1); GL.Vertex2(Width, Height);
            GL.TexCoord2(0, 1); GL.Vertex2(0, Height);
            */
            GL.End();
            SwapBuffers();
        }

        internal void UpdateTexture(Bitmap _worldBitmap)
        {
            BitmapData data = _worldBitmap.LockBits(new Rectangle(0, 0, _worldBitmap.Width, _worldBitmap.Height), ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            try
            {
                //GL.BindTexture(TextureTarget.Texture2D, texture);
                GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, data.Width, data.Height, 0, OpenTK.Graphics.OpenGL.PixelFormat.Bgra, PixelType.UnsignedByte, data.Scan0);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                _worldBitmap.UnlockBits(data);
                Invalidate();
            }
        }
    }
}
