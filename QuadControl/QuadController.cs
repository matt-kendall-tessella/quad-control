using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using QuadControlApp.Imu;
using QuadControlApp.Data;
using QuadControlApp.Gauges;
using System.Reflection;

namespace QuadControlApp
{
    // Manages the current attitude information
    public class QuadController
    {

        // Communication class for IMU
        private readonly ImuComms imuComms;
        
        //private App app;

        // These are readonly so they are only created once (registered observers can't be lost)
        private readonly AttitudeData attitudeData;
        private readonly EngineData engineData;

        public QuadController(MainWindow mainWindow)
        {         
           // this.app = app;
            attitudeData = new AttitudeData();
            engineData = new EngineData();
            //attitudeData.registerObserver(mainWindow.ai);
            List<IGauge> gauges = identifyGauges(mainWindow);
            subscribeGaugesToData(gauges);
        }

        // Change MainWindow to Window??
        // Here we find all the gauges that are declared on a window        
        private List<IGauge> identifyGauges(MainWindow mainWindow)
        {
            List<IGauge> gauges = new List<IGauge>();
            foreach (FieldInfo field in typeof(MainWindow).GetFields())
            {
                IGauge gauge = field.GetValue(mainWindow) as IGauge;
                if (gauge != null)
                {
                    gauges.Add(gauge);
                }
            }
            return gauges;
        }


        private void subscribeGaugesToData(List<IGauge> gauges) 
        {
            foreach (IGauge gauge in gauges)
            {
                HashSet<Type> dataTypes = getDataTypesForGauge(gauge);
                foreach (Type dataType in dataTypes)
                {
                    subscribeGaugeToDataType(gauge, dataType);
                }
            }
        }


        // Now we need to know what data types that gauge wants to subscribe to.
        // We do this by finding all the classFields which implement IData
        private HashSet<Type> getDataTypesForGauge(IGauge gauge)
        {
            HashSet<Type> dataTypes = new HashSet<Type>();
            Type gaugeType = gauge.GetType();
            FieldInfo[] fields = gaugeType.GetFields(BindingFlags.NonPublic | BindingFlags.Instance);
            foreach (FieldInfo field in fields)
            {

                Type fieldType = field.FieldType;                
                if (typeof(IData).IsAssignableFrom(fieldType))
                {
                    dataTypes.Add(fieldType);
                }

            }
            return dataTypes;
        }

        // Now we have a gauge to subscribe and a Data Type to subscribe to,
        // we just need to find the matching instance of that type on this class and then 
        // subscribe it!
        private void subscribeGaugeToDataType(IGauge gauge, Type dataType)
        {
            // Get all the data sources in this class
            //List<BaseData> dataSources = new List<BaseData>();
            FieldInfo[] classFields = this.GetType().GetFields(BindingFlags.Instance | BindingFlags.NonPublic);
            foreach (var classField in classFields)
            {
                // If the classField matches the requested type then register the gauge
                if (classField.FieldType == dataType)
                {
                    IData dataSource = (IData) classField.GetValue(this);
                    if (dataSource != null)
                    {
                        dataSource.registerObserver(gauge);
                    }
                }
            }                 
        }


        // Start the imuComms communicating with this
        public void beginImuComms()
        {
            if (imuComms == null)
            {
                //imuComms = new ImuComms();
                imuComms.start(this);
            }
        }

        //public AttitudeData getCurrentAttitude()
        //{
        //    return currentAttitude;
        //}
        
        //// Do any processing then set the current attitude
        //public void processNewCurrentAttitude(AttitudeData rawAttitude)
        //{
        //    this.currentRawAttitude = rawAttitude;
        //    this.currentAttitude = new AttitudeData(rawAttitude.getRoll() - zeroRefAttitude.getRoll(),
        //        rawAttitude.getPitch() - zeroRefAttitude.getPitch(),
        //        rawAttitude.getHeading() - zeroRefAttitude.getHeading());           
        //    // more processing if needed
        //    passUpdateToUI();
        //}

        //public void processNewData(ImuData imuData)
        //{
        //    this.currentAttitude = accel.process(imuData.xa, imuData.ya, imuData.za);
        //    double hdg = compass.process(imuData.xm, imuData.ym, imuData.zm, this.currentAttitude);
        //    this.currentAttitude = new AttitudeData(this.currentAttitude.getRoll(), this.currentAttitude.getPitch(), hdg);
        //    passUpdateToUI();
        //}

        //// Set the current attitude as zero
        //public void zeroAttitude()
        //{
        //    accel.zeroAttitude(this.currentAttitude);
        //    //this.zeroRefAttitude = new Attitude(currentRawAttitude.getRoll(), currentRawAttitude.getPitch(), currentRawAttitude.getHeading());
        //}

        //private void passUpdateToUI()
        //{
        //    app.mainWindow.setNewAttitude(currentAttitude);
        //}
    }
}
