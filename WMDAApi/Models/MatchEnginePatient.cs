using System;

namespace WMDAApi.Models
{
    abstract class MatchEnginePatient
    {
        public abstract string FirstName { get; set; }
        public abstract string LastName { get; set; }
        public abstract DateTime? DateOfBirth { get; set; }
        public abstract string DiseaseType { get; set; }
    }
}
