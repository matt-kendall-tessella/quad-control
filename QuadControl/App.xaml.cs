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
        public MainWindow mainWindow;
        public QuadController controller;

        private void applicationStartup(object sender, StartupEventArgs e)
        {
            mainWindow = new MainWindow();
            controller = new QuadController(this.mainWindow);
            mainWindow.Show();
        }
    }
}
