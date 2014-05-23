using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;


using QuadControlApp.Data;
using QuadControlApp.Gauges;

namespace QuadControlApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        private double roll;
        private double pitch;
        private double heading;

        //private int V_PX = 800;
        //private int V_PX_PER_DEGREE = 6;
        //private double V_OFFSET;

        private App app;

        private IAttitudeIndicator ai;

        /**
        private double aiBaseHeight;

        public void setRoll(double roll)
        {
            this.roll = roll;
        }

        public void setPitch(double pitch)
        {
            this.pitch = pitch;
        }
        */

        public MainWindow(App app)
        {
            this.app = app;
            InitializeComponent();
            ai = new BasicAttitudeIndicator(ai_canvas);
            slider_pitch.Value = (slider_pitch.Maximum - slider_pitch.Minimum) / 2;
            slider_roll.Value = (slider_roll.Maximum - slider_roll.Minimum) / 2;
            //V_OFFSET = (ai_base.Height - canvas1.Height) / 2;
            //updateAi();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

        //public void updateAi()
        //{
        //    Dispatcher.Invoke((Action)(() =>
        //    {

        //        //Canvas.SetTop(ai_base, 10);
        //        Canvas.SetTop(ai_base, mapPitchToAI() - V_OFFSET);
        //        var transform = ai_base.RenderTransform as RotateTransform;
        //        ai_base.RenderTransformOrigin = new Point(0.5, 0.5 - mapPitchToAI() / ai_base.Height);
        //        transform.Angle = roll;
        //        labelRoll.Content = Math.Round(roll);
        //        labelPitch.Content = Math.Round(pitch);
        //    }));
        //}

        //private double mapPitchToAI()
        //{
        //    return pitch * (ai_base.Height / V_PX) * V_PX_PER_DEGREE;
        //}
       private void updateHsi()
        {
            var transform = hsi_base.RenderTransform as RotateTransform;
            transform.Angle = 360 - heading;
            labelHeading.Content = Math.Round(heading);
        }


       private void slider_pitch_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
       {
           pitch = (slider_pitch.Value - 90);
           ai.updateAttitude(new Attitude(roll,pitch,heading));
           labelPitch.Content = Math.Round(pitch);
       }

       private void slider_roll_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
       {
           roll = slider_roll.Value - 90;
           ai.updateAttitude(new Attitude(roll,pitch,heading));
           labelRoll.Content = Math.Round(roll);
       }

        public void setNewAttitude(Attitude attitude) 
        {
            pitch = attitude.getPitch();
            roll = attitude.getRoll();
            heading = attitude.getHeading();
            //updateAi();
            updateHsi();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
           // app.aiLogic.zeroAttitude();
            //roll = 0;
            //pitch = 0;
            //updateAi();
            //slider_roll.Value = roll + 90;
            //slider_pitch.Value = pitch + 90;


        }
        
        private void border1_MouseMove(object sender, MouseEventArgs e)
        {
        //    Point pos = e.GetPosition(canvas1);
        //    roll = 90 * ((pos.X / canvas1.Width) - 0.5);
        //    pitch = 90 * (0.5 - (pos.Y / canvas1.Height));
        //    updateAi();


        }

        private void slider_heading_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
        //    heading = slider_heading.Value;
        //    updateHsi();
        }
         

 
        /*
        private void button2_Click(object sender, RoutedEventArgs e)
        {
            //gotData;

         //   System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
          //  dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
           // dispatcherTimer.Interval = new TimeSpan(0, 0, 0,0,10);
           // dispatcherTimer.Start();




            //while (true)
            {
                
            }

        }



        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            

            // code goes here
        }
        */
       // private void gotData(RoutedEventHandler r){}


    }
}