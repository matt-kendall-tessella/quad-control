using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using QuadControlApp.Imu;
using QuadControlApp.Sensors;
using QuadControlApp.Data;

namespace QuadControlApp
{
    // Manages the current attitude information
    public class AiLogic
    {
        private Attitude zeroRefAttitude;
        private Attitude currentAttitude;
        private Attitude currentRawAttitude;

        // Communication class for IMU
        private ImuComms imuComms;
        private App app;

        private Compass compass;
        private Accelerometer accel;

        public AiLogic(App app)
        {
            this.app = app;
            currentAttitude = new Attitude(0, 0, 360);
            zeroRefAttitude = new Attitude(0, 0, 360);
            compass = new Compass();
            accel = new Accelerometer();
        }

        // Start the imuComms communicating with this
        public void beginImuComms()
        {
            if (imuComms == null)
            {
                imuComms = new ImuComms();
                imuComms.start(this);
            }
        }

        public Attitude getCurrentAttitude()
        {
            return currentAttitude;
        }
        
        // Do any processing then set the current attitude
        public void processNewCurrentAttitude(Attitude rawAttitude)
        {
            this.currentRawAttitude = rawAttitude;
            this.currentAttitude = new Attitude(rawAttitude.getRoll() - zeroRefAttitude.getRoll(),
                rawAttitude.getPitch() - zeroRefAttitude.getPitch(),
                rawAttitude.getHeading() - zeroRefAttitude.getHeading());           
            // more processing if needed
            passUpdateToUI();
        }

        public void processNewData(ImuData imuData)
        {
            this.currentAttitude = accel.process(imuData.xa, imuData.ya, imuData.za);
            double hdg = compass.process(imuData.xm, imuData.ym, imuData.zm, this.currentAttitude);
            this.currentAttitude = new Attitude(this.currentAttitude.getRoll(), this.currentAttitude.getPitch(), hdg);
            passUpdateToUI();
        }

        // Set the current attitude as zero
        public void zeroAttitude()
        {
            accel.zeroAttitude(this.currentAttitude);
            //this.zeroRefAttitude = new Attitude(currentRawAttitude.getRoll(), currentRawAttitude.getPitch(), currentRawAttitude.getHeading());
        }

        private void passUpdateToUI()
        {
            app.mainWindow.setNewAttitude(currentAttitude);
        }
    }
}
