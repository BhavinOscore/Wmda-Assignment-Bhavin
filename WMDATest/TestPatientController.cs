using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using MockQueryable.Moq;
using Moq;
using System;
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
    public class TestPatientController
    {
        PatientController _controller;
        IPatientService _service;
        List<Patient> listPatients;
        private readonly Mock<IValidator<Patient>> _validator;

        public TestPatientController()
        {
            listPatients = MockDataUtils.PatientsMock();
            _validator = new Mock<IValidator<Patient>>();
        }
        [Fact]
        public async Task When_CreatePatient_Called_WithValidData_Should_ReturnIsSuccessTrue()
        {
            // Arrange
            var mockSetPatients = listPatients.AsQueryable().BuildMockDbSet();
            var mockContext = new Mock<AppDbContext>();
            mockContext.Setup(c => c.Patients).Returns(mockSetPatients.Object);
            _service = new PatientService(mockContext.Object, _validator.Object);
            _controller = new PatientController(_service);
            Patient patient = new Patient() { FirstName = "MILLIE", LastName = "Thomas", DateOfBirth = new DateTime(2000, 01, 01), DiseaseType = "alm" };
            _validator.Setup(t => t.Validate(patient)).Returns(new ValidationResult());
            // Act
            var result = await _controller.CreatePatient(patient);

            // Assert
            var actionResult = Assert.IsType<OkObjectResult>(result);
            var responseModel = Assert.IsType<ResponseModel>(actionResult.Value);
            Assert.True(responseModel.IsSuccess);
        }
    }
}