using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using QuadControlApp.Data;

namespace QuadControlApp.Gauges
{
    interface IAttitudeIndicator : IGauge
    {
        void updateAttitude(Attitude attitude);
    }
}
