using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoListApp.Models;

namespace ToDoListApp.Interfaces
{
    public interface IToDoService
    {
        public List<ToDoItem> GetAll();
        public ToDoItem GetById(string id);
        public ToDoItem Create(ToDoItem model);
        public bool Update(string id, ToDoItem model);
        public bool Delete(string id);
        public bool Clear();
    }
}
