using StudentTest.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace StudentTest.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditEquipmentPage : ContentPage
    {
        public EditEquipmentPage()
        {
            InitializeComponent();
            BindingContext = new EditEquipmentViewModel();
            picker.SelectedIndex = 0;
        }
        async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    }
}