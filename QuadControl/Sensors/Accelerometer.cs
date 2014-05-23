using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using QuadControlApp.Data;

namespace QuadControlApp.Sensors
{
    public class Accelerometer
    {
        private double rollOffset = 0;
        private double pitchOffset = 0;

        public void zeroAttitude(Attitude attitude)
        {
            rollOffset += attitude.getRoll();
            pitchOffset += attitude.getPitch();
        }

        public Attitude process(double x, double y, double z)
        {
            Attitude attitude = preProcess(x, y, z);
            return new Attitude(attitude.getRoll() - rollOffset, attitude.getPitch() - pitchOffset, 360);
        }

        private Attitude preProcess(double x, double y, double z)
        {
            double pitch;
            double roll;

            roll = Math.Atan2(-y, z) * (180 / Math.PI);
            pitch = Math.Atan2(x, Math.Sqrt(y * y + z * z)) * (180 / Math.PI);

            return new Attitude(roll, pitch, 360);
        }
    }
}
