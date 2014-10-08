using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QuadControlApp.Imu
{
    public class ImuData
    {
        public static String DATA_START = "AA";
        public static String DATA_END = "ZZ";

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

        // Combined attitude reference data
        public double ap; // Pitch
        public double ar; // Roll        
        public double ay; // Yaw        

        // Barometer data
        //public double p;
        //public double t;
    }
}
