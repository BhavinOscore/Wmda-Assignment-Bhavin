using MockQueryable.Moq;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WMDAApi.Models;
using WMDAApi.Services;
using WMDAApi.Utils;
using Xunit;


namespace WMDATest
{
    public class TestSearchMatchService
    {
        ISearchMatchService _service;
        List<Patient> listPatients;
        public TestSearchMatchService()
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

            // Act
            var result = await _service.CreateSearchAsync(1, 1);

            // Assert
            Assert.True(result.IsSuccess);
        }
        [Fact]
        public async Task When_CreateSearch_Called_WithInvalidMatchingEngine_Should_ReturnMatchEngineNotFound()
        {
            // Arrange
            var mockSetPatients = listPatients.AsQueryable().BuildMockDbSet();
            var mockContext = new Mock<IAppDbContext>();
            mockContext.Setup(c => c.Patients).Returns(mockSetPatients.Object);
            _service = new SearchMatchService(mockContext.Object);

            // Act
            var exception = await Assert.ThrowsAsync<CustomException>(async () => await _service.CreateSearchAsync(1, 3));

            // Assert
            Assert.Contains("Match Engine Not Found.", exception.Errors.Select(t => t.Message));
        }
        [Fact]
        public async Task When_CreateSearch_Called_WithInvalidPatient_Should_ReturnPatientNotFound()
        {
            // Arrange
            var mockSetPatients = listPatients.AsQueryable().BuildMockDbSet();
            var mockContext = new Mock<IAppDbContext>();
            mockContext.Setup(c => c.Patients).Returns(mockSetPatients.Object);
            _service = new SearchMatchService(mockContext.Object);

            // Act
            var exception = await Assert.ThrowsAsync<CustomException>(async () => await _service.CreateSearchAsync(3, 1));

            // Assert
            Assert.Contains("Patient Not Found.", exception.Errors.Select(t => t.Message));
        }
        [Fact]
        public async Task When_CreateSearch_Called_WithInvalidMatchingEngineAndInvalidPatient_Should_ReturnEngineNotFoundAndPatientNotFound()
        {
            // Arrange
            var mockSetPatients = listPatients.AsQueryable().BuildMockDbSet();
            var mockContext = new Mock<IAppDbContext>();
            mockContext.Setup(c => c.Patients).Returns(mockSetPatients.Object);
            _service = new SearchMatchService(mockContext.Object);

            // Act
            var exception = await Assert.ThrowsAsync<CustomException>(async () => await _service.CreateSearchAsync(3, 3));

            // Assert
            Assert.Contains("Match Engine Not Found.", exception.Errors.Select(t => t.Message));
            Assert.Contains("Patient Not Found.", exception.Errors.Select(t => t.Message));
        }

    }
}
