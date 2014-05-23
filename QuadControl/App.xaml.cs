using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;

namespace QuadControlApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public AiLogic aiLogic;
        public MainWindow mainWindow;

        private void applicationStartup(object sender, StartupEventArgs e)
        {
           mainWindow = new MainWindow(this);
           //aiLogic = new AiLogic(this);
           //aiLogic.beginImuComms();
           mainWindow.Show();
        }
    }
}
