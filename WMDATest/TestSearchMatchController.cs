using Microsoft.AspNetCore.Mvc;
using MockQueryable.Moq;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WMDAApi.Controllers;
using WMDAApi.Models;
using WMDAApi.Services;
using WMDAApi.Utils;
using WMDAApi.ViewModels;
using Xunit;

namespace WMDATest
{
    public class TestSearchMatchController
    {
        SearchMatchController _controller;
        ISearchMatchService _service;
        List<Patient> listPatients;

        public TestSearchMatchController()
        {
            listPatients = MockDataUtils.PatientsMock();
        }
        [Fact]
        public async Task When_CreateSearch_Called_WithValidData_Should_ReturnIsSuccessTrue()
        {
            // Arrange
            var mockSetPatients = listPatients.AsQueryable().BuildMockDbSet();
            var mockContext = new Mock<IAppDbContext>();
            mockContext.Setup(c => c.Patients).Returns(mockSetPatients.Object);
            _service = new SearchMatchService(mockContext.Object);
            _controller = new SearchMatchController(_service);

            // Act
            var result = await _controller.CreateSearch(1, 1);

            // Assert
            var actionResult = Assert.IsType<OkObjectResult>(result);
            var responseModel = Assert.IsType<ResponseModel>(actionResult.Value);
            Assert.True(responseModel.IsSuccess);
        }
    }
}
