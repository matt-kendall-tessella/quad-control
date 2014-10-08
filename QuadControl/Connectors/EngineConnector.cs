using QuadControlApp.Data;
using QuadControlApp.Imu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QuadControlApp.Connectors
{
    class EngineConnector : IConnector
    {
        private EngineData engineData;

        public EngineConnector(EngineData engineData)
        {
            this.engineData = engineData;
        }

        public void updateData(ImuData imuData)
        {
            //attitudeData.heading = imuData;
        }
    }
}
