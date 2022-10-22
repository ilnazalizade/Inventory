using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using IMSBeta.window;

namespace IMSBeta
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void App_StartUp(object sender, StartupEventArgs e)
        {
            win_login win_Login = new win_login();
            win_Login.ShowDialog();

            win_main win_Main = new win_main(); 
            win_Main.ShowDialog();
        }
    }
}
