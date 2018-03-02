using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Todo.Services
{
    public static class TodoService
    {
        public static IList<Models.Todo> Todos = new ObservableCollection<Models.Todo>();
        public static IList<Models.Todo> AllTodos = new List<Models.Todo>();
        private static string _searchTerm;

        public static void Load()
        {
            AllTodos.Add(new Models.Todo()
            {
                Text = "Dit is een Todo",
                IsDone = true
            });
            AllTodos.Add(new Models.Todo()
            {
                Text = "Copy of Dit is een Todo",
                IsDone = true
            });
            Search(String.Empty);
        }

        public static void Save(Models.Todo todo) {
            if (!Todos.Contains(todo))
            {
                AllTodos.Add(todo);
                Search(_searchTerm);
            }
        }

        public static void Remove(Models.Todo todo)
        {
            AllTodos.Remove(todo);
            Search(_searchTerm);
        }

        public static void Search(string searchTerm)
        {
            _searchTerm = searchTerm;
            IList<Models.Todo> foundTodos;
            if (!String.IsNullOrWhiteSpace(searchTerm))
                foundTodos = AllTodos.Where(todo => todo.Text.ToLower().Contains(searchTerm.ToLower())).ToList();
            else
                foundTodos = AllTodos;

            Todos.Clear();
            foreach (var todo in foundTodos)
                Todos.Add(todo);
        }
    }
}
