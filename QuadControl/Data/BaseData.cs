using QuadControlApp.Gauges;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QuadControlApp.Data
{
    public abstract class BaseData : IData
    {
        List<IGauge> observers = new List<IGauge>();

        public void registerObserver(IGauge observer)
        {
            observers.Add(observer);
        }

        public void notifyObservers()
        {
            foreach (IGauge observer in observers)
            {
                observer.notify(this);
            }
        }
    }
}
