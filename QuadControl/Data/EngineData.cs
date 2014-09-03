using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QuadControlApp.Data
{
    class EngineData : BaseData
    {
        private string _someEngineData;
        public string someEngineData
        {
            get { return this._someEngineData; }
            set
            {
                this._someEngineData = value;
                notifyObservers();
            }
        }

    }
}
