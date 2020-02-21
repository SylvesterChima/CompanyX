using Newtonsoft.Json;
using StudentTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;

namespace StudentTest
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AppShell : Shell   
    {
        public AppShell()
        {
            InitializeComponent();
            var userJson = Preferences.Get("userData", "{}");
            var user = JsonConvert.DeserializeObject<User>(userJson);
            BindingContext = user;
        }

        [Obsolete]
        private void MenuItem_Clicked(object sender, EventArgs e)
        {
            Preferences.Clear();
            Application.Current.MainPage = new NavigationPage(new LoginPage());
        }
    }
}