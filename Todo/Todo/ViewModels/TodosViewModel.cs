using System;
using Todo.Services;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamarin.Forms;
using Todo.Views;

namespace Todo.ViewModels
{
    public class TodosViewModel : INotifyPropertyChanged
    {
        private INavigation _navigation;

        public IList<Models.Todo> Todos {
            get => TodoService.Todos;
            set {
                TodoService.Todos = value;
                OnPropertyChanged();
            }
        }

        public string SearchTerm { get; set; } = "U wot m8";

        public TodosViewModel(INavigation navigation)
        {
            _navigation = navigation;
        }

        #region Commands
        public Command AddTodoCommand {
            get => new Command(async () => {
                await _navigation.PushAsync(new AddEditTodoPage());
            });
        }

        public Command SearchCommand
        {
            get
            {
                return new Command<string>((searchTerm) =>
                {
                    TodoService.Search(searchTerm);
                });
            }
        }

        #endregion

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "") =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
