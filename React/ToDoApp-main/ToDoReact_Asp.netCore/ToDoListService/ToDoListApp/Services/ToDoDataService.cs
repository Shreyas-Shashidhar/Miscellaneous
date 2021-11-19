using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoListApp.Interfaces;
using ToDoListApp.Models;

namespace ToDoListApp.Services
{
    public class ToDoDataService : IToDoService
    {
        ConcurrentDictionary<string, ToDoItem> _toDoList = new ConcurrentDictionary<string, ToDoItem>();

        public bool Clear()
        {
            _toDoList?.Clear();
            return true;
        }

        public ToDoItem Create(ToDoItem model)
        {
            if (model == null)
                return null;

            _toDoList.AddOrUpdate(model?.Id, model, (k, v) => model);
            return model;
        }

        public bool Delete(string id)
        {
            if (_toDoList.ContainsKey(id))
            {
                return _toDoList.TryRemove(id, out _);
            }
            return false;
        }

        public List<ToDoItem> GetAll()
        {
            return _toDoList?.Values?.OrderBy(item =>item.Priority).ToList();
        }

        public ToDoItem GetById(string id)
        {
            if (_toDoList.ContainsKey(id))
            {
                return _toDoList[id];
            }
            return null;
        }

        public bool Update(string id, ToDoItem model)
        {
            if (_toDoList.ContainsKey(id))
            {
                _toDoList[id] = model;
                return true;
            }
            return false;
        }
    }
}
