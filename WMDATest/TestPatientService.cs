using FluentValidation;
using FluentValidation.Results;
using MockQueryable.Moq;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WMDAApi.Models;
using WMDAApi.Services;
using WMDAApi.Utils;
using Xunit;

namespace WMDATest
{
    public class TestPatientService
    {
        IPatientService _service;
        List<Patient> listPatients;
        private readonly Mock<IValidator<Patient>> _validator;
        public TestPatientService()
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
            Patient patient = new Patient();
            _validator.Setup(t => t.Validate(patient)).Returns(new ValidationResult());

            // Act
            var result = await _service.CreatePatientAsync(patient);
            
            // Assert
            Assert.True(result.IsSuccess);
        }
        [Fact]
        public async Task When_CreatePatient_Called_WithoutFirstName_Should_ReturnFirstNameIsRequired()
        {
            // Arrange
            var mockSetPatients = listPatients.AsQueryable().BuildMockDbSet();
            var mockContext = new Mock<AppDbContext>();
            mockContext.Setup(c => c.Patients).Returns(mockSetPatients.Object);
            _service = new PatientService(mockContext.Object, _validator.Object);
            Patient patient = new Patient();
            var expectedError = new List<ValidationFailure> { new("FirstName", "First name is required.") };
            _validator.Setup(t => t.Validate(patient)).Returns(new ValidationResult(expectedError));

            // Act
            var exception = await Assert.ThrowsAsync<CustomException>(async () => await _service.CreatePatientAsync(patient));

            // Assert
            Assert.Contains("First name is required.", exception.Errors.Select(t => t.Message));
        }
        [Fact]
        public async Task When_CreatePatient_Called_WithoutDateOfBirth_Should_ReturnDateOfBirthIsRequired()
        {
            // Arrange
            var mockSetPatients = listPatients.AsQueryable().BuildMockDbSet();
            var mockContext = new Mock<AppDbContext>();
            mockContext.Setup(c => c.Patients).Returns(mockSetPatients.Object);
            _service = new PatientService(mockContext.Object, _validator.Object);
            Patient patient = new Patient();
            var expectedError = new List<ValidationFailure> { new("DateOfBirth", "Date of birth is required.") };
            _validator.Setup(t => t.Validate(patient)).Returns(new ValidationResult(expectedError));

            // Act
            var exception = await Assert.ThrowsAsync<CustomException>(async () => await _service.CreatePatientAsync(patient));

            // Assert
            Assert.Contains("Date of birth is required.", exception.Errors.Select(t => t.Message));
        }
        [Fact]
        public async Task When_CreatePatient_Called_WithAlphaNumericFirstname_Should_ReturnFirstNameIsInvalid()
        {
            // Arrange
            var mockSetPatients = listPatients.AsQueryable().BuildMockDbSet();
            var mockContext = new Mock<AppDbContext>();
            mockContext.Setup(c => c.Patients).Returns(mockSetPatients.Object);
            _service = new PatientService(mockContext.Object, _validator.Object);
            Patient patient = new Patient();
            var expectedError = new List<ValidationFailure> { new("FirstName", "First name allows only alphabet characters.") };
            _validator.Setup(t => t.Validate(patient)).Returns(new ValidationResult(expectedError));

            // Act
            var exception = await Assert.ThrowsAsync<CustomException>(async () => await _service.CreatePatientAsync(patient));

            // Assert
            Assert.Contains("First name allows only alphabet characters.", exception.Errors.Select(t => t.Message));
        }
        [Fact]
        public async Task When_CreatePatient_Called_WithLessThan5CharacterFirstname_Should_ReturnFirstNameAtleast5CharactersLong()
        {
            // Arrange
            var mockSetPatients = listPatients.AsQueryable().BuildMockDbSet();
            var mockContext = new Mock<AppDbContext>();
            mockContext.Setup(c => c.Patients).Returns(mockSetPatients.Object);
            _service = new PatientService(mockContext.Object, _validator.Object);
            Patient patient = new Patient();
            var expectedError = new List<ValidationFailure> { new("FirstName", "First name should be at least 5 characters long.") };
            _validator.Setup(t => t.Validate(patient)).Returns(new ValidationResult(expectedError));

            // Act
            var exception = await Assert.ThrowsAsync<CustomException>(async () => await _service.CreatePatientAsync(patient));

            // Assert
            Assert.Contains("First name should be at least 5 characters long.", exception.Errors.Select(t => t.Message));
        }
        [Fact]
        public async Task When_CreatePatient_Called_WithLessThan5CharacterAndAlphaNumericFirstname_Should_ReturnFirstNameIsInvalidWithTwoViolations()
        {
            // Arrange
            var mockSetPatients = listPatients.AsQueryable().BuildMockDbSet();
            var mockContext = new Mock<AppDbContext>();
            mockContext.Setup(c => c.Patients).Returns(mockSetPatients.Object);
            _service = new PatientService(mockContext.Object, _validator.Object);
            Patient patient = new Patient();
            var expectedError = new List<ValidationFailure>
            {
                new("FirstName", "First name allows only alphabet characters."),
                new("FirstName", "First name should be at least 5 characters long.")
            };
            _validator.Setup(t => t.Validate(patient)).Returns(new ValidationResult(expectedError));

            // Act
            var exception = await Assert.ThrowsAsync<CustomException>(async () => await _service.CreatePatientAsync(patient));

            // Assert
            Assert.Contains("First name should be at least 5 characters long.", exception.Errors.Select(t => t.Message));
            Assert.Contains("First name allows only alphabet characters.", exception.Errors.Select(t => t.Message));
        }

    }
}
