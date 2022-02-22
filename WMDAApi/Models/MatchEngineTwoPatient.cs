using Newtonsoft.Json;
using System;
using WMDAApi.Utils;

namespace WMDAApi.Models
{
    class MatchEngineTwoPatient : MatchEnginePatient
    {
        private string _firstName;
        private string _lastName;
        private DateTime? _dateOfBirth;
        private string _diseaseType;
        public MatchEngineTwoPatient(Patient patient)
        {
            _firstName = patient.FirstName;
            _lastName = patient.LastName;
            _dateOfBirth = patient.DateOfBirth;
            _diseaseType = patient.DiseaseType;
        }
        [JsonProperty("firstName")]
        public override string FirstName
        {
            get { return _firstName; }
            set { _firstName = value; }
        }
        [JsonProperty("lastName")]
        public override string LastName
        {
            get { return _lastName; }
            set { _lastName = value; }
        }
        [JsonProperty("dateOfBirth")]
        [JsonConverter(typeof(JsonDateFormatConverter), "yyyy-MM-dd")]
        public override DateTime? DateOfBirth
        {
            get { return _dateOfBirth; }
            set { _dateOfBirth = value; }
        }
        [JsonProperty("diseaseType")]
        public override string DiseaseType
        {
            get { return _diseaseType; }
            set { _diseaseType = value; }
        }
    }
}
