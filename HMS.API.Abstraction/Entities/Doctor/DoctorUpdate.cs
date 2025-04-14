namespace HMS.API.Abstraction.Entities.Doctor
{
    public class DoctorUpdate
    {
        public string Specialty { get; set; }

        public string FullName { get; set; } = string.Empty;

        public string Phone { get; set; } = string.Empty;
    }
}