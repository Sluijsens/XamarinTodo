using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Todo.Services;
using Xamarin.Forms;

namespace Todo.ViewModels
{
    public class AddEditTodoViewModel : INotifyPropertyChanged
    {
        private INavigation _navigation;
        private Models.Todo _todo;

        public Models.Todo Todo {
            get => _todo;
            set {
                _todo = value;
                OnPropertyChanged();
            }
        }

        public AddEditTodoViewModel(INavigation navigation, Models.Todo todo = null)
        {
            _navigation = navigation;
            
            _todo = todo ?? new Models.Todo();
        }

        public Command AddCommand {
            get => new Command(async () =>
            {
                await TodoService.AddOrUpdate(_todo);
                await _navigation.PopAsync();
            });
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName]string propertyName = "") =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
