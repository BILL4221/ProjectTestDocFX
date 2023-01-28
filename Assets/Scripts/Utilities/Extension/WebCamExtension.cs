using UnityEngine;

namespace Runnex.Utilities.Extensions
{
    public static class WebCamExtension
    {
        public static Texture2D CamToTexture2D(WebCamTexture webCamTexture)
        {
            Texture2D tx2d = new Texture2D(webCamTexture.width, webCamTexture.height);
            tx2d.SetPixels(webCamTexture.GetPixels());
            tx2d.Apply();
            return tx2d;
        }
    }
}
