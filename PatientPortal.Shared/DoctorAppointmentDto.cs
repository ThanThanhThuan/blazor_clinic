namespace PatientPortal.Shared
{
    public class DoctorAppointmentDto
    {
        public int Id { get; set; }
        public DateTime AppointmentDate { get; set; }
        public string PatientName { get; set; } // We join this data in the API
        public string Reason { get; set; }
        public bool IsConfirmed { get; set; }
    }
}