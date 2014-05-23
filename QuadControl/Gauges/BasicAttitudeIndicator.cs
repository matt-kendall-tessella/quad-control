using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Windows.Media;

using QuadControlApp.Data;

namespace QuadControlApp.Gauges
{
    class BasicAttitudeIndicator : IAttitudeIndicator
    {
        Canvas canvas;
        Image aiBackground;
        Image aiNeedle;

        // This is the number of pixels which make one degree in the background image;
        // it is an intrinsic property of the image when created.
        private int V_PX_PER_DEGREE = 6;

        public BasicAttitudeIndicator(Canvas canvas)
        {
            this.canvas = canvas;
            canvas.ClipToBounds = true;
            canvas.Children.Clear();
            createAiBackground();
            createAiNeedle();
            canvas.Children.Add(aiBackground);
            canvas.Children.Add(aiNeedle);
            
        }

        private void createAiBackground()
        {
            aiBackground = new Image();
            // The ai background outstretches the view canvas by 1.5; e.g. if the canvas is 200px by 200px the 
            // background should be 300px by 300px.
            aiBackground.Height = canvas.Height * 1.5;
            aiBackground.Width = canvas.Width * 1.5;
            aiBackground.Stretch = System.Windows.Media.Stretch.Fill;
            aiBackground.RenderTransformOrigin = new Point(0.5, 0.5);
            aiBackground.ClipToBounds = false;
            aiBackground.OverridesDefaultStyle = true;
            aiBackground.Source = new BitmapImage(new Uri(@"../../Images/BasicAttitudeIndicator/attitude_base.jpg", UriKind.Relative));
            // center the image in the canvas
            var l = ((canvas.Width - aiBackground.Width) / 2);
            Canvas.SetLeft(aiBackground, l);
            Canvas.SetTop(aiBackground, (aiBackground.Height - canvas.Height) / 2);
            
            // Create the render transform for the moving background
            Transform rotate = new RotateTransform();
            aiBackground.RenderTransform = rotate;
        }

        private void createAiNeedle()
        {
            aiNeedle = new Image();
            aiNeedle.Height = canvas.Height * 0.10;
            aiNeedle.Width = canvas.Width * 0.20;
            aiNeedle.Stretch = System.Windows.Media.Stretch.Fill;
            aiNeedle.Source = new BitmapImage(new Uri(@"../../Images/BasicAttitudeIndicator/attitude_pointer_trans.png", UriKind.Relative));          
            Canvas.SetLeft(aiNeedle, (canvas.Width - aiNeedle.Width) / 2);
            Canvas.SetTop(aiNeedle, canvas.Height / 2 );//- 3);
        }

        public void updateAttitude(Attitude attitude)
        {
            /* May need to wrap this in:
            Dispatcher.Invoke((Action)(() => {         
             */

            // First we set the pitch and the new rotate point
            var top = ((canvas.Height - aiBackground.Height) / 2) + mapPitchToAiHeight(attitude.getPitch());
            Canvas.SetTop(aiBackground, top); // - V_OFFSET??
            RotateTransform rotate = (RotateTransform)aiBackground.RenderTransform;
            aiBackground.RenderTransformOrigin = new Point(0.5, 0.5 - mapPitchToAiHeight(attitude.getPitch()) / aiBackground.Height);
            
            // Now apply a roll transform about the new axis
            rotate.Angle = attitude.getRoll();
        }


        private double mapPitchToAiHeight(double pitch)
        {
            // The number of points to move the AI background is the pitch angle * a gauge scaling factor * pixels per degree in the bg image            
            return pitch * (aiBackground.Height / ((BitmapSource)aiBackground.Source).PixelHeight) * V_PX_PER_DEGREE;
        }
    
    }
}
