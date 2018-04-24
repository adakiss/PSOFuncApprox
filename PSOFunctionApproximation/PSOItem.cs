using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSOFunctionApproximation
{
    class PSOItem
    {
        public double[] Position { get; set; }
        public double[] LocalOptimum { get; set; }

        public double[] Velocity { get; set; }

    }
}
