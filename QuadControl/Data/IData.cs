using QuadControlApp.Gauges;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QuadControlApp.Data
{
    public interface IData
    {
        void registerObserver(IGauge observer);

        void notifyObservers();
    }
}
