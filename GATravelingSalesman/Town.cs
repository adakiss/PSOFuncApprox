using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GATravelingSalesman
{
    class Town : IEquatable<Town>
    {
        public int X { get; set; }
        public int Y { get; set; }

        public bool Equals(Town other)
        {
            return X == other.X && Y == other.Y;
        }
    }
}