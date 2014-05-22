using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.IO.Ports;
using System.Reflection;

namespace AttitudeIndicatorApp.Imu
{
    /// <summary>
    /// This class communicates with the IMU via Serial and parses and outputs raw attitude data. 
    /// </summary>
    class ImuComms
    {
        private SerialPort port;
        private AiLogic aiLogic;
        public static int BAUD_RATE = 115200;
        public static int DELAY = 100;

        //static String dataStart = "A";
        //static String dataEnd = "Z";
        //static List<String> dataPrefixes = new List<string> { "XA", "YA", "ZA", "XG", "YG", "ZG", "XM", "YM", "P", "T" };

        public void start(AiLogic aiLogic) 
        {
            this.aiLogic = aiLogic;
            port = new SerialPort("COM4", BAUD_RATE);
            port.Open();
            port.DtrEnable = true;
            port.RtsEnable = true;
            System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(gotData);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 0,0, DELAY);
            dispatcherTimer.Start();
            //port.DataReceived += new SerialDataReceivedEventHandler(gotData);    
        }

        /// <summary>
        /// Method to call whenever data is received.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gotData(object sender, EventArgs e)
        {
            String input = port.ReadExisting();
            List<String> dataStrings = getDataStringsFromInput(input);
            if (dataStrings.Count > 0)
            {
                String dataString = dataStrings.Last();
                ImuData imuData = getDataFromString(dataString);
                this.aiLogic.processNewData(imuData);
            }
        }

        /// <summary>
        /// Take the raw input string from Serial and extracts the strings which match the data format pattern.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private List<String> getDataStringsFromInput(String input)
        {
            Type type = typeof(ImuData);
            FieldInfo[] dataFields = type.GetFields();
            List<FieldInfo> dataPrefixes = dataFields.ToList();

            String dataPattern = ImuData.DATA_START;
            foreach (FieldInfo dF in dataFields)
            {
                if (dF.Name != "DATA_START" && dF.Name != "DATA_END")
                {
                    dataPattern += dF.Name.ToUpper();
                    dataPattern += "[0-9-.]*";
                }
            }
            dataPattern += ImuData.DATA_END;

            List<String> dataStrings = new List<string>();
            foreach (Match m in Regex.Matches(input, dataPattern))
            {
                dataStrings.Add(m.Value);
            }
            return dataStrings;
        }

        /// <summary>
        /// Take a correctly formatted string and return {roll, pitch}
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private ImuData getDataFromString(String data)
        {
            // Data container
            ImuData imuData = new ImuData();

            // Get a list of the data fields
            Type type = typeof(ImuData);
            FieldInfo[] dataFields = type.GetFields();
            List<FieldInfo> dataPrefixes = dataFields.ToList();

            //Dictionary<String, FieldInfo> dataPatterns = new Dictionary<String,FieldInfo>();
            
            // For each of the fields, create a regex and parse the result into the field
            foreach (FieldInfo datumField in dataFields)
            {
                if (datumField.Name != "DATA_START" && datumField.Name != "DATA_END")
                {
                    // Get the regex for this datum
                    String datumPattern = datumField.Name.ToUpper();
                    datumPattern += "[0-9-.]*";

                    // Get the string, trim and parse;
                    String datumString = Regex.Match(data, datumPattern).Value;
                    String datumDoubleString = datumString.TrimStart(datumField.Name.ToUpper().ToCharArray());
                    double datum;
                    double.TryParse(datumDoubleString, out datum);

                    datumField.SetValue(imuData, datum);
                }
            }
            return imuData;
        }
    }
}