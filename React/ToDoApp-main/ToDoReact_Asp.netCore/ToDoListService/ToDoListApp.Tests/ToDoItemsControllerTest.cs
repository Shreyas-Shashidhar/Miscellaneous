using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using ToDoListApp.Controllers;
using ToDoListApp.Interfaces;
using ToDoListApp.Models;
using ToDoListApp.Services;
using Xunit;

namespace ToDoListApp.Tests
{
    public class ToDoItemsControllerTest
    {
        #region Members
        IToDoService _toDoDataService;
        ToDoItemsController _toDoItemsController;
        #endregion

        public ToDoItemsControllerTest()
        {
            _toDoDataService = new ToDoDataService();
            _toDoItemsController = new ToDoItemsController(_toDoDataService);
        }

        private void PopulateData()
        {
            _toDoDataService.Create(new ToDoItem() { Title = "Task1" });
            _toDoDataService.Create(new ToDoItem() { Title = "Task2" });
            _toDoDataService.Create(new ToDoItem() { Title = "Task3" });
            _toDoDataService.Create(new ToDoItem() { Title = "Task4" });
            _toDoDataService.Create(new ToDoItem() { Title = "Task5" });
        }

        [Fact]
        public void GetAllItems_ReturnsAllPopulatedData()
        {
            // Arrange
            PopulateData();

            //Act 
            var result = _toDoItemsController.Get();

            // Assert
            var actionResult = Assert.IsType<OkObjectResult>(result);
            var model = Assert.IsAssignableFrom<List<ToDoItem>>(
                actionResult.Value);
            Assert.Equal(5, model.Count);
        }


        [Fact]
        public void GetItemById_ValidItem_ReturnsSpecificItem()
        {
            // Arrange
            var toDoItem = new ToDoItem() { Title = "Task1" };
            _toDoDataService.Create(toDoItem);

            //Act 
            var result = _toDoItemsController.Get(toDoItem.Id);

            // Assert
            var actionResult = Assert.IsType<OkObjectResult>(result);
            var model = Assert.IsAssignableFrom<ToDoItem>(
                actionResult.Value);
            Assert.Equal(toDoItem.Id, model.Id);
        }

        [Fact]
        public void GetItemById_NotPresent_ReturnsNotFoundResult()
        {
            //Act 
            var result = _toDoItemsController.Get("1");

            // Assert
            Assert.IsType<Microsoft.AspNetCore.Mvc.NotFoundResult>(result);
           
        }

        [Fact]
        public void GetItemById_InvalidInput_ReturnsBadRequestResult()
        {
            //Act 
            var result = _toDoItemsController.Get(null);

            // Assert
            Assert.IsType<Microsoft.AspNetCore.Mvc.BadRequestResult>(result);

        }

        [Fact]
        public void CreateItem_EmptyData_ReturnsBadRequestResult()
        {
            //Act 
            var result = _toDoItemsController.Post(null);

            // Assert
            Assert.IsType<Microsoft.AspNetCore.Mvc.BadRequestResult>(result);

        }

        [Fact]
        public void CreateItem_ValidData_ReturnsSpecificItem()
        {
            // Arrange
            var toDoItem = new ToDoItem() { Title = "Task1" };

            //Act 
            var result = _toDoItemsController.Post(toDoItem);

            // Assert
            var actionResult = Assert.IsType<CreatedAtActionResult>(result);
            var model = Assert.IsAssignableFrom<ToDoItem>(
                actionResult.Value);
            Assert.Equal(toDoItem.Id, model.Id);
        }

        [Fact]
        public void UpdateItem_ValidData_ReturnsSpecificItem()
        {
            // Arrange
            var toDoItem = new ToDoItem() { Title = "Task1" };

            //Act 
            _toDoItemsController.Post(toDoItem);
            toDoItem.Title = "Task2";
            var result = _toDoItemsController.Put(toDoItem.Id, toDoItem);

            // Assert
            var actionResult = Assert.IsType<OkObjectResult>(result);
            var model = Assert.IsAssignableFrom<ToDoItem>(
                actionResult.Value);
            Assert.Equal(toDoItem.Id, model.Id);
            Assert.Equal("Task2", toDoItem.Title);
        }

        [Fact]
        public void UpdateItem_NotPresentItem_ReturnsNotFoundResult()
        {
            // Arrange
            var toDoItem = new ToDoItem() { Title = "Task1" };

            //Act 
            var result = _toDoItemsController.Put(toDoItem.Id, toDoItem);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }


        [Fact]
        public void UpdateItem_EmptyData_ReturnsBadResult()
        {
            //Act 
            var result = _toDoItemsController.Put(null, null);

            // Assert
            Assert.IsType<Microsoft.AspNetCore.Mvc.BadRequestResult>(result);

        }

        [Fact]
        public void DeleteItem_EmptyData_ReturnsBadRequestResult()
        {
            //Act 
            var result = _toDoItemsController.Delete(null);

            // Assert
            Assert.IsType<Microsoft.AspNetCore.Mvc.BadRequestResult>(result);

        }

        [Fact]
        public void DeleteItem_ReturnsSuccessResult()
        {
            // Arrange
            var toDoItem = new ToDoItem() { Title = "Task1" };

            //Act s
            _toDoItemsController.Post(toDoItem);
            var deleteResult = _toDoItemsController.Delete(toDoItem.Id);
            var result = _toDoItemsController.Get("1");

            // Assert
            Assert.IsType<NoContentResult>(deleteResult);
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void DeleteItem_NotPresentItem_ReturnsNotFoundResult()
        {
            // Arrange
            var toDoItem = new ToDoItem() { Title = "Task1" };

            //Act 
            var result = _toDoItemsController.Delete(toDoItem.Id);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }


    }
}
