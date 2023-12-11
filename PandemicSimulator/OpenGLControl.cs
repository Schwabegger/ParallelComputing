using OpenTK;
using OpenTK.Graphics.OpenGL;
using System.Drawing.Imaging;


namespace PandemicSimulator;

public class OpenGLControl : GLControl
{
    private int _textureId;
    private Bitmap _bitmapToRender; // Add this variable

    public OpenGLControl()
    {
        DoubleBuffered = true;
        Load += (sender, e) => { SetupViewport(); };
        Paint += (sender, e) => { Render(); };
    }

    public void SetBitmapToRender(Bitmap bitmap)
    {
        _bitmapToRender = bitmap;
        LoadTexture();
    }

    private void SetupViewport()
    {
        GL.ClearColor(Color.Black);
        GL.Enable(EnableCap.Texture2D);
        GL.Enable(EnableCap.Blend);
        GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);
    }

    private void LoadTexture()
    {
        if (_bitmapToRender is not null)
        {
            GL.DeleteTextures(1, ref _textureId); // Delete existing texture if it exists

            GL.GenTextures(1, out _textureId);
            GL.BindTexture(TextureTarget.Texture2D, _textureId);

            BitmapData data = _bitmapToRender.LockBits(
                new Rectangle(0, 0, _bitmapToRender.Width, _bitmapToRender.Height),
                ImageLockMode.ReadOnly,
                System.Drawing.Imaging.PixelFormat.Format32bppArgb
            );

            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, data.Width, data.Height, 0,
                OpenTK.Graphics.OpenGL.PixelFormat.Bgra, PixelType.UnsignedByte, data.Scan0);

            _bitmapToRender.UnlockBits(data);

            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);
        }
    }

    private void Render()
    {
        GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
        GL.MatrixMode(MatrixMode.Projection);
        GL.LoadIdentity();
        GL.Ortho(0, Width, Height, 0, -1, 1); // Set up an orthographic projection

        GL.MatrixMode(MatrixMode.Modelview);
        GL.LoadIdentity();

        if (_textureId != 0)
        {
            GL.BindTexture(TextureTarget.Texture2D, _textureId);

            GL.Begin(PrimitiveType.Quads);
            GL.TexCoord2(0, 0); GL.Vertex2(0, 0);
            GL.TexCoord2(1, 0); GL.Vertex2(Width, 0);
            GL.TexCoord2(1, 1); GL.Vertex2(Width, Height);
            GL.TexCoord2(0, 1); GL.Vertex2(0, Height);
            GL.End();
        }

        SwapBuffers();
    }
}