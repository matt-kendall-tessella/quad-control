using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QuadControlApp.Data
{
    public class Attitude : IQuadData
    {
        private double roll;
        private double pitch;
        private double heading;

        public double getRoll()
        {
            return roll;
        }

        public double getPitch()
        {
            return pitch;
        }

        public double getHeading()
        {
            return heading;
        }

        public Attitude(double roll, double pitch, double heading)
        {
            this.roll = roll;
            this.heading = heading;
            this.pitch = pitch;
        }
    }
}
