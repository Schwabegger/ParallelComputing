using OpenTK;
using OpenTK.Graphics.OpenGL;
using System.Drawing.Imaging;

namespace PandemicSimulator
{
    internal sealed class MyGLControl : GLControl
    {
        private int texture;
        public volatile Bitmap? WorldBitmap;

        public MyGLControl()
        {
            this.VSync = false;
            this.Dock = DockStyle.Fill;
            this.Paint += GlControl_Paint;
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
            GL.End();
            this.SwapBuffers();
        }

        internal void UpdateTexture()
        {
            BitmapData data = WorldBitmap.LockBits(new Rectangle(0, 0, WorldBitmap.Width, WorldBitmap.Height), ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
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
                WorldBitmap.UnlockBits(data);
                this.Invalidate();
            }
        }
    }
}
