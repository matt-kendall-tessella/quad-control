using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using AttitudeIndicatorApp.Data;

namespace AttitudeIndicatorApp.Gauges
{
    interface IAttitudeIndicator : IGauge
    {
        void updateAttitude(Attitude attitude);
    }
}
