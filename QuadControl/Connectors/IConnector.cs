using QuadControlApp.Imu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QuadControlApp.Connectors
{
    public interface IConnector
    {
        void updateData(ImuData imuData);
    }
}
