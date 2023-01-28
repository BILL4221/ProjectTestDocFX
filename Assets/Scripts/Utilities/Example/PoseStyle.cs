using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Runnex.Utilities
{
    public class PoseStyle : StringEnum<PoseStyle>
    {
        public static PoseStyle SideJump = new PoseStyle("Side Jump");
        public static PoseStyle IceSkate = new PoseStyle("Ice Skate");

        public PoseStyle(string id) : base(id)
        {
        }
    }
}
