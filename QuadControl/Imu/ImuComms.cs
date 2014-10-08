using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.IO.Ports;
using System.Reflection;
using QuadControlApp.Connectors;

namespace QuadControlApp.Imu
{
    /// <summary>
    /// This class communicates with the IMU via Serial and parses and outputs raw attitude data. 
    /// </summary>
    class ImuComms
    {
        private SerialPort port;
        public static int BAUD_RATE = 115200;
        public static int DELAY = 100;
        private IConnector[] dataConnectors = new IConnector[0];


        public void start(IConnector[] dataConnectors) 
        {
            this.dataConnectors = dataConnectors;
            port = new SerialPort("COM6", BAUD_RATE);
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
                foreach (IConnector connector in this.dataConnectors)
                {
                    connector.updateData(imuData);    
                }
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

            String dataPattern = generateDataPattern(dataFields);

            List<String> dataStrings = new List<string>();
            foreach (Match m in Regex.Matches(input, dataPattern))
            {
                dataStrings.Add(m.Value);
            }
            return dataStrings;
        }

        private static string generateDataPattern(FieldInfo[] dataFields)
        {
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
            return dataPattern;
        }

        /// <summary>
        /// Take a correctly formatted string and return an ImuData object
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private ImuData getDataFromString(String data)
        {
            // Data container
            ImuData imuData = new ImuData();

            // Get a list of the data classFields
            Type type = typeof(ImuData);
            FieldInfo[] dataFields = type.GetFields();
            List<FieldInfo> dataPrefixes = dataFields.ToList();

            //Dictionary<String, FieldInfo> dataPatterns = new Dictionary<String,FieldInfo>();
            
            // For each of the classFields, create a regex and parse the result into the classField
            foreach (FieldInfo datumField in dataFields)
            {
                // Get the regex for this datum
                String datumName = datumField.Name.ToUpper();
                String datumPattern = datumName + "[0-9-.]+";

                // Get the string, trim and parse;
                String datumString = Regex.Match(data, datumPattern).Value;
                String datumDoubleString = datumString.TrimStart(datumField.Name.ToUpper().ToCharArray());
                if (datumDoubleString.Length > 0)
                {
                    double datum;
                    double.TryParse(datumDoubleString, out datum);
                    datumField.SetValue(imuData, datum);     
                }           
            }
            return imuData;
        }
    }
}