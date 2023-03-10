using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Runnex.Utilities
{
    public static class GameObjectExtension
    {
        public static void SetLayerRecursively(this GameObject gameObject, int layer)
        {
            gameObject.layer = layer;
            foreach (Transform child in gameObject.transform)
            {
                child.gameObject.SetLayerRecursively(layer);
            }
        }
    }
}
