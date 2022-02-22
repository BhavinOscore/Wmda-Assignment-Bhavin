using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace WMDAApi.Models
{
    public class Patient
    {
        [Key]
        [JsonIgnore]
        public int PatientId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string DiseaseType { get; set; }
    }
}
