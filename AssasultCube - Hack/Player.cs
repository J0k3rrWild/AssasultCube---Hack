using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssasultCube___Hack
{
    public class Player
    {
        internal float walkX;
        internal float walkY;
        internal float flyY;
        internal float fY;

        public int Health { get; set; }

        public float x { get; set; }
        public float X { get; internal set; }
        public float y { get; set; }
        public float Y { get; internal set; }
        public float z { get; set; }
        public float Z { get; internal set; }
        public float Magnitude { get; set; }
    }
}
