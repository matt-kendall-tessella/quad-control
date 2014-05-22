using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AttitudeIndicatorApp.Imu
{
    public class ImuData
    {
        public static String DATA_START = "A";
        public static String DATA_END = "Z";

        // Accelerometer data
        public double xa;
        public double ya;
        public double za;

        // Gyro data
        public double xg;
        public double yg;
        public double zg;

        // Magnetometer data
        public double xm;
        public double ym;
        public double zm;

        // Barometer data
        //public double p;
        //public double t;
    }
}
