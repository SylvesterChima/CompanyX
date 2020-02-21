using Newtonsoft.Json;
using Splat;
using StudentTest.Models;
using StudentTest.Services;
using StudentTest.ViewModels;
using Syncfusion.ListView.XForms;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;


using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace StudentTest.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EquipmentPage : ContentPage
    {
        EquipmentViewModel vm;
        //IEquipmentService _equipmentService;
        public EquipmentPage()
        {

            InitializeComponent();
            //_equipmentService = equipmentService ?? (IEquipmentService)Locator.Current.GetService(typeof(IEquipmentService));
            
            vm = new EquipmentViewModel();
            BindingContext = vm;
            listView.SwipeStarted += ListView_SwipeStarted;
            listView.SwipeEnded += ListView_SwipeEnded;
            //pullToRefresh.Refreshing += PullToRefresh_Refreshing;
        }

        Image leftImage;
        Image rightImage;
        int itemIndex = -1;

        //private async void PullToRefresh_Refreshing(object sender, EventArgs args)
        //{
        //    pullToRefresh.IsRefreshing = true;
        //    await Task.Delay(2000);
        //    vm.GetAllEquipment();
        //    //var _equipmentService = (IEquipmentService)Locator.Current.GetService(typeof(IEquipmentService));
        //    //var equipments = await _equipmentService.GetAll();
        //    //vm.Equipments = new ObservableCollection<Equipment>(equipments);

        //    pullToRefresh.IsRefreshing = false;
        //}

        private async void SetFavorites()
        {
            if (itemIndex >= 0)
            {
                var item = vm.Equipments[itemIndex];
                var jsonStr = JsonConvert.SerializeObject(item);
                Preferences.Set("mItem", jsonStr);
            }
            listView.ResetSwipe();
            await Navigation.PushModalAsync(new NavigationPage(new EditEquipmentPage()));
        }

        private async void Delete()
        {
            try
            {
                if (itemIndex >= 0)
                {
                    var _equipmentService = (IEquipmentService)Locator.Current.GetService(typeof(IEquipmentService));
                    bool answer = await DisplayAlert("Question?", "Would you like to delete this equipment", "Yes", "No");
                    if (answer)
                    {
                        _equipmentService.Delete(vm.Equipments[itemIndex].Id);
                        vm.Equipments.RemoveAt(itemIndex);
                        await DisplayAlert("Alert", "Deleted successfully!", "OK");
                    }
                }
                listView.ResetSwipe();
            }
            catch (Exception)
            {
                listView.ResetSwipe();
            }
        }

        private void ListView_SwipeStarted(object sender, SwipeStartedEventArgs e)
        {
            itemIndex = -1;
        }

        private void ListView_SwipeEnded(object sender, SwipeEndedEventArgs e)
        {
            itemIndex = e.ItemIndex;
        }

        private void leftImage_BindingContextChanged(object sender, EventArgs e)
        {
            if (leftImage == null)
            {
                leftImage = sender as Image;
                (leftImage.Parent as View).GestureRecognizers.Add(new TapGestureRecognizer() { Command = new Command(SetFavorites) });
                //leftImage.Source = ImageSource.FromResource("StuentTest.Images.Favorites.png");
            }
        }

        private void rightImage_BindingContextChanged(object sender, EventArgs e)
        {
            if (rightImage == null)
            {
                rightImage = sender as Image;
                (rightImage.Parent as View).GestureRecognizers.Add(new TapGestureRecognizer() { Command = new Command(Delete) });
                //rightImage.Source = ImageSource.FromResource("StuentTest.Images.Delete.png");
            }
        }

        async void OnItemAdd(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new NewEquipmentPage()));
        }

        //protected async override void OnAppearing()
        //{
        //    base.OnAppearing();

        //    new EquipmentViewModel();
        //}

    }
}