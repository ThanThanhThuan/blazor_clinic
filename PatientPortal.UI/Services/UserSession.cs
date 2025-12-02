namespace PatientPortal.UI.Services
{
    public class UserSession
    {
        public string Role { get; set; } = "Patient"; // Default role
        public int UserId { get; set; } = 1; // Default Patient ID

        public void SetDoctorMode()
        {
            Role = "Doctor";
            UserId = 101; // ID for Dr. Sarah Smith from our seed data
        }

        public void SetPatientMode()
        {
            Role = "Patient";
            UserId = 1; // ID for the default patient
        }

        public bool IsDoctor => Role == "Doctor";
    }
}