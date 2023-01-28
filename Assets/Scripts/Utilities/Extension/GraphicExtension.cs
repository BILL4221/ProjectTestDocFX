using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Runnex.Utilities.Extensions
{
    public static class GraphicExtension 
    {
        public static IEnumerator Fade(this Graphic graphic, float duration, float from = 0f, float to = 1.0f)
        {
            var textColor = graphic.color;
            float currentTime = 0;
            float alpha = 0;
            while (currentTime < duration)
            {
                alpha = Mathf.Lerp(from, to, currentTime / duration);
                textColor.a = alpha;
                graphic.color = textColor;
                currentTime += Time.deltaTime;
                yield return null;
            }
            textColor.a = to;
            graphic.color = textColor;
        }
    }
}
