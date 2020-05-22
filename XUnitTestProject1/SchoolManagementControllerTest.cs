using System;
using System.Collections.Generic;
using BusinessService.Api.Controllers;
using BusinessService.Domain.Services;
using Microsoft.AspNetCore.Mvc;
using BusinessService.Data.DBModel;
using Xunit;
using System.Linq;

namespace XUnitTestProject1
{
    public class SchoolManagementControllerTest
    {
        SchoolManagementController _controller;
        ISchoolManagementService _service;

        public SchoolManagementControllerTest()
        {
            _service = new SchoolManagementServiceFake();
            _controller = new SchoolManagementController(_service);
        }

        [Fact]
        public void Get_WhenCalled_ReturnsOkResult()
        {
            // Act
            var okResult = _controller.Get();

            // Assert
            Assert.IsType<OkObjectResult>(okResult.Result);
        }

        [Fact]
        public void Get_WhenCalled_ReturnsAllItems()
        {
            // Act
            var okResult = _controller.Get().Result as OkObjectResult;

            // Assert
            var items = Assert.IsType<List<SchoolItem>>(okResult.Value);
            Assert.Equal(3, items.Count);
        }
        [Fact]
        public void GetById_UnknownGuidPassed_ReturnsNotFoundResult()
        {
            // Act
            var notFoundResult = _controller.Get(Guid.NewGuid());

            // Assert
            Assert.IsType<NotFoundResult>(notFoundResult.Result);
        }

        [Fact]
        public void GetById_ExistingGuidPassed_ReturnsOkResult()
        {
            // Arrange
            var testGuid = new Guid("ab2bd817-98cd-4cf3-a80a-53ea0cd9c200");

            // Act
            var okResult = _controller.Get(testGuid);

            // Assert
            Assert.IsType<OkObjectResult>(okResult.Result);
        }

        [Fact]
        public void GetById_ExistingGuidPassed_ReturnsRightItem()
        {
            // Arrange
            var testGuid = new Guid("ab2bd817-98cd-4cf3-a80a-53ea0cd9c200");

            // Act
            var okResult = _controller.Get(testGuid).Result as OkObjectResult;

            // Assert
            Assert.IsType<SchoolItem>(okResult.Value);
            Assert.Equal(testGuid, (okResult.Value as SchoolItem).Id);
        }

        [Fact]
        public void Add_InvalidObjectPassed_ReturnsBadRequest()
        {
            // Arrange
            var nameMissingItem = new SchoolItem()
            {
                Name = "Eshwar",
                Principal = "Ramesh",
                Fees = 6.00M
            };
            _controller.ModelState.AddModelError("Name", "Required");

            // Act
            var badResponse = _controller.Post(nameMissingItem);

            // Assert
            Assert.IsType<BadRequestObjectResult>(badResponse);
        }


        [Fact]
        public void Add_ValidObjectPassed_ReturnsCreatedResponse()
        {
            // Arrange
            SchoolItem testItem = new SchoolItem()
            {
                Name = "Rahul",
                Principal = "Balakumar",
                Fees = 7.00M
            };

            // Act
            var createdResponse = _controller.Post(testItem);

            // Assert
            Assert.IsType<CreatedAtActionResult>(createdResponse);
        }


        [Fact]
        public void Add_ValidObjectPassed_ReturnedResponseHasCreatedItem()
        {
            // Arrange
            var testItem = new SchoolItem()
            {
                Name = "Rahul",
                Principal = "Balakumar",
                Fees = 7.00M
            };

            // Act
            var createdResponse = _controller.Post(testItem) as CreatedAtActionResult;
            var item = createdResponse.Value as SchoolItem;

            // Assert
            Assert.IsType<SchoolItem>(item);
            Assert.Equal("Rahul", item.Name);
        }

        [Fact]
        public void Remove_NotExistingGuidPassed_ReturnsNotFoundResponse()
        {
            // Arrange
            var notExistingGuid = Guid.NewGuid();

            // Act
            var badResponse = _controller.Remove(notExistingGuid);

            // Assert
            Assert.IsType<NotFoundResult>(badResponse);
        }

        [Fact]
        public void Remove_ExistingGuidPassed_ReturnsOkResult()
        {
            // Arrange
            var existingGuid = new Guid("ab2bd817-98cd-4cf3-a80a-53ea0cd9c200");

            // Act
            var okResponse = _controller.Remove(existingGuid);

            // Assert
            Assert.IsType<OkResult>(okResponse);
        }
        [Fact]
        public void Remove_ExistingGuidPassed_RemovesOneItem()
        {
            // Arrange
            var existingGuid = new Guid("ab2bd817-98cd-4cf3-a80a-53ea0cd9c200");

            // Act
            var okResponse = _controller.Remove(existingGuid);

            // Assert
            Assert.Equal(2, _service.GetAllItems().Count());
        }

    }
}
