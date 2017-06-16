using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace ThroneRooms
{
    public class InfoPageViewModel : INotifyPropertyChanged
    {
        public InfoPageViewModel(INavigation navigation)
        {
            Info = "This app uses Azure Cosmos DB as a backend";
            _closeCommand = new Command(async () => await navigation.PopModalAsync());
        }

        private string _info;

        public string Info
        {
            get => _info;
            set
            {
                _info = value;
                OnPropertyChanged();
            }
        }

        private ICommand _closeCommand;

        public ICommand CloseCommand
        {
            get => _closeCommand;
            set
            {
                CloseCommand = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
