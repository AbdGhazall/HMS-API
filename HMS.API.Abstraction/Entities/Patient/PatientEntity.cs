using System.Text.Json.Serialization;

namespace HMS.API.Abstraction.Entities.Patient
{
    public class PatientEntity
    {
        public int Id { get; set; }
        public int UserId { get; set; }

        public int GenderId { get; set; }
        public string FullName { get; set; } = string.Empty;

        [JsonPropertyName("dob")]
        public string DOB => DobDate.ToString("yyyy-MM-dd");

        [JsonIgnore]
        public DateTime DobDate { get; set; }

        public string Phone { get; set; } = string.Empty;
        public string MedicalHistory { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty; //from user table
    }
}