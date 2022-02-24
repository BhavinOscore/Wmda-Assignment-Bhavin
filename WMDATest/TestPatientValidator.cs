using FluentValidation;
using FluentValidation.TestHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WMDAApi.Models;
using Xunit;

namespace WMDATest
{
    public class TestPatientValidator
    {
        List<Patient> listPatients;
        private readonly IValidator<Patient> _validator;

        public TestPatientValidator()
        {
            listPatients = MockDataUtils.PatientsMock();
            _validator = new PatientValidator();
        }

        [Fact]
        public void When_Validated_WithoutFirstName_Should_ShouldFailValidationWithFirstNameIsRequired()
        {
            // Arrange
            Patient patient = new Patient() { LastName = "Thomas", DateOfBirth = new DateTime(2000, 01, 01), DiseaseType = "alm" };

            // Act
            var result = _validator.TestValidate(patient);

            // Assert
            result.ShouldHaveValidationErrorFor(patient => patient.FirstName);
        }

        [Fact]
        public void When_Validated_WithoutDateOfBirth_Should_ShouldFailValidationWithDateOfBirthIsRequired()
        {
            // Arrange
            Patient patient = new Patient() { FirstName = "MILLIE", LastName = "Thomas", DiseaseType = "alm" };

            // Act
            var result = _validator.TestValidate(patient);

            // Assert
            result.ShouldHaveValidationErrorFor(patient => patient.DateOfBirth);
        }

        [Fact]
        public void When_Validated_WithAlphaNumericFirstname_Should_ShouldFailValidationWithFirstNameIsInvalid()
        {
            // Arrange
            Patient patient = new Patient() { FirstName = "MILLIE1", LastName = "Thomas", DateOfBirth = new DateTime(2000, 01, 01), DiseaseType = "alm" };

            // Act
            var result = _validator.TestValidate(patient);

            // Assert
            result.ShouldHaveValidationErrorFor(patient => patient.FirstName);
        }

        [Fact]
        public void When_Validated_WithLessThan5CharacterFirstname_Should_ShouldFailValidationWithFirstNameAtleast5CharactersLong()
        {
            // Arrange
            Patient patient = new Patient() { FirstName = "MIL", LastName = "Thomas", DateOfBirth = new DateTime(2000, 01, 01), DiseaseType = "alm" };

            // Act
            var result = _validator.TestValidate(patient);

            // Assert
            result.ShouldHaveValidationErrorFor(patient => patient.FirstName);
        }
    }
}
