using QuadControlApp.Data;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QuadControlApp.Gauges
{
    public interface IGauge
    {
        void notify(IData changed);
    }
}
