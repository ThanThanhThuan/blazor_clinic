using System.ComponentModel.DataAnnotations;

namespace PatientPortal.Shared
{
    public class Appointment
    {
        public int Id { get; set; }
        public int PatientId { get; set; }

        [Required(ErrorMessage = "Please select a doctor.")]
        [Range(1, int.MaxValue, ErrorMessage = "Please select a doctor.")]
        public int DoctorId { get; set; }

        [Required]
        public DateTime AppointmentDate { get; set; } = DateTime.Now.AddDays(1);

        [Required(ErrorMessage = "Please state the reason for the visit.")]
        [StringLength(100, MinimumLength = 3)]
        public string Reason { get; set; } = string.Empty;

        public bool IsConfirmed { get; set; }
    }

    // Simple DTO for populating the dropdown
    public class DoctorDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Specialty { get; set; }
    }
    public class PatientDto { public int Id { get; set; } public string Name { get; set; } }
}