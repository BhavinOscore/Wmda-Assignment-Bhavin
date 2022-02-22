using Newtonsoft.Json;
using System;
using WMDAApi.Utils;

namespace WMDAApi.Models
{
    class MatchEngineOnePatient : MatchEnginePatient
    {
        private string _forename;
        private string _surname;
        private DateTime? _dateOfBirth;
        private string _diseaseType;
        public MatchEngineOnePatient(Patient patient)
        {
            _forename = patient.FirstName;
            _surname = patient.LastName;
            _dateOfBirth = patient.DateOfBirth;
            _diseaseType = patient.DiseaseType;
        }
        [JsonProperty("forename")]
        public override string FirstName
        {
            get { return _forename; }
            set { _forename = value; }
        }
        [JsonProperty("surname")]
        public override string LastName
        {
            get { return _surname; }
            set { _surname = value; }
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
