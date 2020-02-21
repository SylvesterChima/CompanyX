using System;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace StudentTest
{
    public partial class App : Application
    {
        [Obsolete]
        public App()
        {
            //Register Syncfusion license
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("MjExMDUyQDMxMzcyZTM0MmUzMFd4eENBY1lwRGpGZ1Q0T3RYb1Vxckt6UkZBSW81aEh4SDVTSmpOQTBIY1E9");
            InitializeComponent();
            new Startup();
            var userJson = Preferences.Get("userData", "");
            if (userJson == "")
            {
                MainPage = new NavigationPage(new LoginPage());
            }
            else
            {
                MainPage = new AppShell();
            }
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
