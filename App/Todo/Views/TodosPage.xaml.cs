using System;
using Todo.Services;
using Todo.ViewModels;
using Xamarin.Forms;

namespace Todo.Views
{
    public partial class TodosPage : ContentPage
    {
        private TodosViewModel _viewModel;
        
        public TodosPage()
        {
            InitializeComponent();

            _viewModel = new TodosViewModel(Navigation);
            BindingContext = _viewModel;
        }

        private async void Remove_Clicked(object sender, EventArgs e)
        {
            var menuItem = (MenuItem)sender;
            var todo = menuItem.CommandParameter as Models.Todo;

            var deleteItem = await DisplayAlert("Remove", $"Remove the todo {todo.Text}", "OK", "Never mind");
            if(deleteItem)
                await TodoService.Remove(todo);
        }

        private async void Edit_Clicked(object sender, EventArgs e)
        {
            var menuItem = (MenuItem)sender;
            var todo = menuItem.CommandParameter as Models.Todo;

            await Navigation.PushAsync(new AddEditTodoPage(todo));
        }

        private async void ItemTapped(object sender, ItemTappedEventArgs e)
        {
            TodosListView.SelectedItem = null;

            if (e.Item is Models.Todo todo)
            {
                todo.IsDone = !todo.IsDone;
                await TodoService.Save();
            }
        }
    }
}
