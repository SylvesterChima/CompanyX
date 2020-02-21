using Newtonsoft.Json;
using ReactiveUI;
using Splat;
using StudentTest.Models;
using StudentTest.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace StudentTest.ViewModels
{
    public class EditEquipmentViewModel : ReactiveObject
    {
        IEquipmentService _equipmentService;
        public EditEquipmentViewModel(IEquipmentService equipmentService = null)
        {
            _equipmentService = equipmentService ?? (IEquipmentService)Locator.Current.GetService(typeof(IEquipmentService));
            var jsonStr = Preferences.Get("mItem", "{}");
            Equipment = JsonConvert.DeserializeObject<Equipment>(jsonStr);
            var tn = new List<string> { "Outdoor", "Indoor" };
            var ts = new List<Types>();
            ts.Add(new Types { Type = Equipment.Type, TypeName = Equipment.TypeName });
            foreach (var item in tn)
            {
                if (item.ToLower() != Equipment.TypeName.ToLower())
                {
                    ts.Add(new Types { Type = Equipment.Type == 0 ? 1 : 0, TypeName = item });
                }
            }
            EquipTypes = ts;
            Name = Equipment.Name;
            Quantity = Equipment.Quantity;
            EquipType = EquipTypes[0];
            TitleName = $"Edit {Equipment.Name}";
        }

        public string TitleName { get; set; }

        private Equipment _Equipment;
        public Equipment Equipment
        {
            get => _Equipment;
            set { this.RaiseAndSetIfChanged(ref _Equipment, value); }
        }

        private List<Types> _EquipTypes;
        public List<Types> EquipTypes
        {
            get => _EquipTypes;
            set { this.RaiseAndSetIfChanged(ref _EquipTypes, value); }
        }

        private Types _EquipType;
        public Types EquipType
        {
            get => _EquipType;
            set { this.RaiseAndSetIfChanged(ref _EquipType, value); }
        }

        private string _Name = "";
        public string Name
        {
            get => _Name;
            set { this.RaiseAndSetIfChanged(ref _Name, value); }
        }

        private string _TypeName = "";
        public string TypeName
        {
            get => _TypeName;
            set { this.RaiseAndSetIfChanged(ref _TypeName, value); }
        }

        private int _Quantity = 0;
        public int Quantity
        {
            get => _Quantity;
            set { this.RaiseAndSetIfChanged(ref _Quantity, value); }
        }


        private ICommand _OnSave;
        public ICommand OnSave
        {
            get
            {
                return _OnSave ?? (_OnSave = new CommandHandler(() => MyAction(), true));
            }
        }

        public async void MyAction()
        {
            

            //var equipment = await _equipmentService.Update(eqm);
            //MessagingCenter.Send(eqm, "EditEquipment");
            //await Shell.Current.DisplayAlert("Alert", "Updated successfully!", "OK");
            //await Shell.Current.Navigation.PopModalAsync();

            if (Quantity > 0)
            {
                if (EquipType != null && Name != "")
                {

                    var eqm = new Equipment
                    {
                        Id = Equipment.Id,
                        Name = Name,
                        Status = 1,
                        StatusName = "Active",
                        Quantity = Quantity,
                        Type = EquipType.Type,
                        TypeName = EquipType.TypeName
                    };

                    var equipment = await _equipmentService.Update(eqm);
                    MessagingCenter.Send(eqm, "EditEquipment");
                    await Shell.Current.DisplayAlert("Alert", "Updated successfully!", "OK");
                    await Shell.Current.Navigation.PopModalAsync();
                }
                else
                {
                    await Shell.Current.DisplayAlert("Alert", "Please fill the form correctly!", "OK");
                }
            }
            else
            {
                await Shell.Current.DisplayAlert("Alert", "Quantity must be greater than 0", "OK");
            }

        }

        public class CommandHandler : ICommand
        {
            private Action _action;
            private bool _canExecute;
            public CommandHandler(Action action, bool canExecute)
            {
                _action = action;
                _canExecute = canExecute;
            }

            public bool CanExecute(object parameter)
            {
                return _canExecute;
            }

            public event EventHandler CanExecuteChanged;

            public void Execute(object parameter)
            {
                _action();
            }
        }
    }
}
