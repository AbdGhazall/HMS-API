namespace HMS.API.Abstraction.Entities.Appointment
{
    public class AppointmentEntity
    {
        public int Id { get; set; }
        public int DoctorId { get; set; }
        public int PatientId { get; set; }

        public int AppointmentStatusId { get; set; }
        public DateTime AppointmentDate { get; set; }

        public string Notes { get; set; } = string.Empty;
    }
}