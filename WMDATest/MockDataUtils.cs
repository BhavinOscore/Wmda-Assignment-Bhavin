using System;
using System.Collections.Generic;
using WMDAApi.Models;

namespace WMDATest
{
    public class MockDataUtils
    {
        public static List<Patient> PatientsMock()
        {
            return new List<Patient>
            {
                new Patient(){ PatientId = 1, FirstName = "Harry", LastName = "Thomas", DateOfBirth = new DateTime(2000, 01, 01), DiseaseType ="alm"},
                new Patient(){ PatientId = 2, FirstName = "Jacob", LastName = "Thomas", DateOfBirth = new DateTime(2000, 01, 01), DiseaseType ="alm"}
            };
        }
    }
}
