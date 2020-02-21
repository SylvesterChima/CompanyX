using ReactiveUI;
using Splat;
using StudentTest.Models;
using StudentTest.Services;
using StudentTest.Views;
using Syncfusion.ListView.XForms;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using Xamarin.Forms;

namespace StudentTest.ViewModels
{
    public class EquipmentViewModel: ReactiveObject
    {
        private IEquipmentService _equipmentService;


        public EquipmentViewModel(IEquipmentService equipmentService = null)
        {
            _equipmentService = equipmentService ?? (IEquipmentService)Locator.Current.GetService(typeof(IEquipmentService));

            

            this.WhenAnyValue(vm => vm.Name)
                .Throttle(TimeSpan.FromSeconds(1))
                .Subscribe(name =>
            {
                if(name != "")
                {
                    Search(Name, Status, Type);
                }
                else
                {
                    GetAllEquipment();
                }
            });

            MessagingCenter.Subscribe<Equipment>(this, "NewEquipment", (item) =>
            {
                //var newItem = item as Equipment;
                Equipments.Add(item);
                //_equipmentService.Add(item);
            });

            MessagingCenter.Subscribe<Equipment>(this, "EditEquipment", (item) =>
            {
                var index = Equipments.ToList().FindIndex(c => c.Id == item.Id);
                Equipments[index].Id = item.Id;
                Equipments[index].Name = item.Name;
                Equipments[index].Type = item.Type;
                Equipments[index].TypeName = item.TypeName;
                Equipments[index].Quantity = item.Quantity;
            });


            //this.WhenAnyValue(vm => vm.Type).Subscribe(type =>
            //{
            //    Search(Name, Status, Type);
            //});
            //this.WhenAnyValue(vm => vm.Status).Subscribe(status =>
            //{
            //    Search(Name, Status, Type);
            //});
        }

        #region Propeties
        private bool _IsLoading = true;
        public bool IsLoading
        {
            get => _IsLoading;
            set { this.RaiseAndSetIfChanged(ref _IsLoading, value); }
        }

        private string _Name = "";
        public string Name
        {
            get => _Name;
            set { this.RaiseAndSetIfChanged(ref _Name, value); }
        }

        private int _Status = 0;
        public int Status
        {
            get => _Status;
            set { this.RaiseAndSetIfChanged(ref _Status, value); }
        }

        private int _Type = 0;
        public int Type
        {
            get => _Type;
            set { this.RaiseAndSetIfChanged(ref _Type, value); }
        }

        private ObservableCollection<Equipment> _Equipments;
        public ObservableCollection<Equipment> Equipments
        {
            get => _Equipments;
            set { this.RaiseAndSetIfChanged(ref _Equipments, value); }
        }
        #endregion

        public async void GetAllEquipment()
        {
            IsLoading = true;
            var equipments = await _equipmentService.GetAll();
            Equipments = new ObservableCollection<Equipment>(equipments);
            IsLoading = false;
        }

        private async void Search(string name, int status, int type)
        {
            IsLoading = true;
            var equipments = await _equipmentService.Search(name, status, type);
            Equipments = new ObservableCollection<Equipment>(equipments);
            IsLoading = false;
        }

    }
}
