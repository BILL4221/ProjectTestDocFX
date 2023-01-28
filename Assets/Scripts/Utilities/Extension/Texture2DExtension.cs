using UnityEngine;

namespace Runnex.Classifier
{
	public class TextureScaler
	{
		public static Texture2D scaled(Texture2D src, int width, int height, FilterMode mode = FilterMode.Trilinear)
		{
			Rect texR = new Rect(0, 0, width, height);
			_gpu_scale(src, width, height, mode);
			Texture2D result = new Texture2D(width, height, TextureFormat.ARGB32, true);
			result.Resize(width, height);
			result.ReadPixels(texR, 0, 0, true);
			return result;
		}

		public static void scale(Texture2D tex, int width, int height, FilterMode mode = FilterMode.Trilinear)
		{
			Rect texR = new Rect(0, 0, width, height);
			// The data is rendered to GPU
			_gpu_scale(tex, width, height, mode);

			tex.Resize(width, height);
			// GPU->CPU and already need to sync both = slow
			tex.ReadPixels(texR, 0, 0, true);
			tex.Apply(true);
			
		}

		private static void _gpu_scale(Texture2D src, int width, int height, FilterMode fmode)
		{
			src.filterMode = fmode;
			src.Apply(true);

			// Fix Depth later after dealing with Memory Leak
			RenderTexture rtt = new RenderTexture(width, height, 32);

			Graphics.SetRenderTarget(rtt);

			GL.LoadPixelMatrix(0, 1, 1, 0);

			GL.Clear(true, true, new Color(0, 0, 0, 0));
			Graphics.DrawTexture(new Rect(0, 0, 1, 1), src);
		}
	}
}