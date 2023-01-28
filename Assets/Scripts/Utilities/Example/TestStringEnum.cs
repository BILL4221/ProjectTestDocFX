using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Runnex.Utilities.Example
{
    public class TestStringEnum : MonoBehaviour
    {
        private Dictionary<PoseStyle, int> enumStringDict;

        private void Start()
        {
            enumStringDict = new Dictionary<PoseStyle, int>();
            enumStringDict.Add(PoseStyle.SideJump, 0);
            enumStringDict.Add(PoseStyle.IceSkate, 1);
        }

        private PoseStyle GetPoseStyle(string style)
        {
            return PoseStyle.FromString(style);
        }
    }
}
