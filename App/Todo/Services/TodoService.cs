using Newtonsoft.Json;
using PCLStorage;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace Todo.Services
{
    public static class TodoService
    {
        public static IList<Models.Todo> Todos = new ObservableCollection<Models.Todo>();
        public static IList<Models.Todo> AllTodos = new List<Models.Todo>();
        private static string _searchTerm;

        public static async Task Load()
        {
            var file = await OpenFile();
            var json = await file.ReadAllTextAsync();

            if(!String.IsNullOrWhiteSpace(json))
                AllTodos = JsonConvert.DeserializeObject<IList<Models.Todo>>(json);

            Search(String.Empty);
        }

        public static async Task AddOrUpdate(Models.Todo todo) {
            if (!Todos.Contains(todo))
            {
                AllTodos.Add(todo);
                await Save();

                Search(_searchTerm);
            }
        }

        public static async Task Remove(Models.Todo todo)
        {
            AllTodos.Remove(todo);
            await Save();

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

            if (!foundTodos.SequenceEqual(Todos))
            {
                Todos.Clear();
                foreach (var todo in foundTodos)
                    Todos.Add(todo);
            }
        }

        /* Helper Methods */
        private static async Task<IFile> OpenFile()
        {
            IFolder rootFolder = FileSystem.Current.LocalStorage;
            IFolder folder = await rootFolder.CreateFolderAsync("Todos", CreationCollisionOption.OpenIfExists);
            IFile file = await folder.CreateFileAsync("todos.qwerty", CreationCollisionOption.OpenIfExists);

            return file;
        }
        public static async Task Save()
        {
            var file = await OpenFile();

            var json = JsonConvert.SerializeObject(AllTodos);
            await file.WriteAllTextAsync(json);
        }
    }
}
