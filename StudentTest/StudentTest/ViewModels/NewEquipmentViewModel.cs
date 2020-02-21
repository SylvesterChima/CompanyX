using ReactiveUI;
using Splat;
using StudentTest.Models;
using StudentTest.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace StudentTest.ViewModels
{
    public class NewEquipmentViewModel : ReactiveObject
    {
        IEquipmentService _equipmentService;
        public NewEquipmentViewModel(IEquipmentService equipmentService = null)
        {
            _equipmentService = equipmentService ?? (IEquipmentService)Locator.Current.GetService(typeof(IEquipmentService));
            var ts = new List<Types>
            {
                new Types{Type=0, TypeName = "Outdoor"},
                new Types{Type=1, TypeName = "Indoor"},
            };
            EquipTypes = ts;
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
           

            //var equipment = await _equipmentService.Add(eqm);
            //MessagingCenter.Send(equipment, "NewEquipment");
            //await Shell.Current.DisplayAlert("Alert", "Saved successfully!", "OK");
            //await Shell.Current.Navigation.PopModalAsync();

            if (Quantity > 0)
            {
                if (EquipType != null && Name !="")
                {
                    var eqm = new Equipment
                    {
                        Id = Guid.NewGuid().ToString(),
                        Name = Name,
                        Status = 0,
                        StatusName = "Inactive",
                        Quantity = Quantity,
                        Type = EquipType.Type,
                        TypeName = EquipType.TypeName
                    };

                    var equipment = await _equipmentService.Add(eqm);
                    MessagingCenter.Send(equipment, "NewEquipment");
                    await Shell.Current.DisplayAlert("Alert", "Saved successfully!", "OK");
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
