using System;
using System.Collections.Generic;
using Todo.ViewModels;
using Xamarin.Forms;

namespace Todo.Views
{
    public partial class AddEditTodoPage : ContentPage
    {
        private AddEditTodoViewModel _viewModel;

        public AddEditTodoPage(Models.Todo todo = null)
        {
            InitializeComponent();
            
            _viewModel = new AddEditTodoViewModel(Navigation, todo);
            BindingContext = _viewModel;
        }
    }
}
