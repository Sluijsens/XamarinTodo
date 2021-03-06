﻿using System.Threading.Tasks;
using Todo.Services;
using Todo.Views;
using Xamarin.Forms;

namespace Todo
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            Task.Run(async () =>
            {
                await TodoService.Load();
            }).Wait();

            MainPage = new NavigationPage(new TodosPage())
            {
                // Volgende kleuren komen uit de ResourceDictionary van App.xaml
                BarBackgroundColor = (Color) Current.Resources["navBarBackgroundColor"],
                BarTextColor = (Color) Current.Resources["navBarTextColor"]
            };
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
