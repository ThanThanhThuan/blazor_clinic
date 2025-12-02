using System.ComponentModel.DataAnnotations;

namespace PatientPortal.Shared
{
    public class MedicalRecord
    {
        public int Id { get; set; }
        public int PatientId { get; set; } // Simplified: In real app link to User Table
        public int DoctorId { get; set; }
        public DateTime DateCreated { get; set; }
        public RecordType Type { get; set; } // Distinguish between Labs, Rx, History

        [Required]
        public string Title { get; set; } = string.Empty; // e.g., "Blood Test", "Amoxicillin"
        public string Description { get; set; } = string.Empty; // Details or Dosage
        public string? AttachmentUrl { get; set; } // For lab PDFs
    }
}