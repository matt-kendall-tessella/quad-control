using QuadControlApp.Data;
using QuadControlApp.Imu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QuadControlApp.Connectors
{
    class AttitudeConnector : IConnector
    {
        private AttitudeData attitudeData;

        public AttitudeConnector(AttitudeData attitudeData)
        {
            this.attitudeData = attitudeData;
        }

        public void updateData(ImuData imuData)
        {
            // Don't do anything complex at this stage: just use precalculated values from onboard
            this.attitudeData.pitch = imuData.ap;
            this.attitudeData.roll = imuData.ar;
            this.attitudeData.heading = imuData.ay;
        }
    }
}
