using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using QuadControlApp.Data;

namespace QuadControlApp.Sensors
{
    public class Compass
    {
        public static double X_OFFSET = 145;
        public static double Y_OFFSET = 35;
        public static double Z_OFFSET = 0;

        public double process(double x, double y, double z)
        {
            Attitude levelFlight = new Attitude(0, 0, 0);
            return this.process(x, y, z, levelFlight);
        }

        public double process(double x, double y, double z, Attitude attitude)
        {
            double rollRad = attitude.getPitch() * (Math.PI / 180);
            double pitchRad = attitude.getRoll() * (Math.PI / 180);
            double rawHdg = (Math.Atan2(y - Y_OFFSET, x - X_OFFSET));
            if (rawHdg < 0)
            {
                rawHdg += 2 * Math.PI;
            }
            rawHdg *= (180 / Math.PI);
            y -= Y_OFFSET;
            x -= X_OFFSET;
            z -= Z_OFFSET;
            double xH = x * Math.Cos(pitchRad) + z * Math.Sin(pitchRad);
            double yH = x * Math.Sin(rollRad) * Math.Cos(pitchRad) + y * Math.Cos(rollRad) - z * Math.Sin(rollRad) * Math.Cos(pitchRad);
            double hdg = Math.Atan2(y, x);
            if (hdg < 0)
            {
                hdg += 2 * Math.PI;
            }
            return hdg * (180 / Math.PI);
        }
    }
}