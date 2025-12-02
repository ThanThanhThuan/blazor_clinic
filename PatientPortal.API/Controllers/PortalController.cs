using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PatientPortal.API.Data;
using PatientPortal.Shared;

namespace PatientPortal.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PortalController : ControllerBase
    {
        private readonly PortalDbContext _context;

        public PortalController(PortalDbContext context)
        {
            _context = context;
        }

        // GET: api/Portal/records/patient/1
        [HttpGet("records/patient/{patientId}")]
        public async Task<ActionResult<List<MedicalRecord>>> GetRecords(int patientId)
        {
            return await _context.MedicalRecords
                                 .Where(r => r.PatientId == patientId)
                                 .OrderByDescending(r => r.DateCreated)
                                 .ToListAsync();
        }

        // GET: api/Portal/appointments/patient/1
        [HttpGet("appointments/patient/{patientId}")]
        public async Task<ActionResult<List<Appointment>>> GetAppointments(int patientId)
        {
            return await _context.Appointments
                                 .Where(a => a.PatientId == patientId && a.AppointmentDate > DateTime.Now)
                                 .OrderBy(a => a.AppointmentDate)
                                 .ToListAsync();
        }

        // POST: api/Portal/appointment
        [HttpPost("appointment")]
        public async Task<ActionResult<Appointment>> CreateAppointment(Appointment appt)
        {
            _context.Appointments.Add(appt);
            await _context.SaveChangesAsync();
            return Ok(appt);
        }

        // GET: api/Portal/doctors
        [HttpGet("doctors")]
        public ActionResult<List<DoctorDto>> GetDoctors()
        {
            // In a real app, you would query the Users table where Role == Doctor
            // For this demo, we return a hardcoded list of available doctors
            var doctors = new List<DoctorDto>
            {
                new DoctorDto { Id = 101, Name = "Dr. Sarah Smith", Specialty = "General Practitioner" },
                new DoctorDto { Id = 102, Name = "Dr. John Watson", Specialty = "Cardiologist" },
                new DoctorDto { Id = 103, Name = "Dr. Gregory House", Specialty = "Diagnostician" }
            };

            return Ok(doctors);
        }

        // GET: api/Portal/patients
        [HttpGet("patients")]
        public ActionResult<List<PatientDto>> GetPatients()
        {
            // Hardcoded list for demo. In real app: _context.Users.Where(u => u.Role == Patient)
            var patients = new List<PatientDto>
            {
                new PatientDto { Id = 1, Name = "John Doe" },
                new PatientDto { Id = 2, Name = "Jane Roe" }
            };
            return Ok(patients);
        }

        // POST: api/Portal/record
        [HttpPost("record")]
        public async Task<ActionResult<MedicalRecord>> CreateRecord(MedicalRecord record)
        {
            record.DateCreated = DateTime.Now;
            _context.MedicalRecords.Add(record);
            await _context.SaveChangesAsync();
            return Ok(record);
        }
        // GET: api/Portal/appointments/doctor/{doctorId}
        [HttpGet("appointments/doctor/{doctorId}")]
        public async Task<ActionResult<List<DoctorAppointmentDto>>> GetDoctorAppointments(int doctorId)
        {
            // 1. Get raw appointments from SQL
            var appointments = await _context.Appointments
                                     .Where(a => a.DoctorId == doctorId && a.AppointmentDate >= DateTime.Today)
                                     .OrderBy(a => a.AppointmentDate)
                                     .ToListAsync();

            // 2. Mock a Patient Directory (In a real app, this comes from the Users table, use .Include)
            var patientDirectory = new Dictionary<int, string>
            {
                { 1, "John Doe" },
                { 2, "Jane Roe" }
            };

            // 3. Map to DTO
            var result = appointments.Select(a => new DoctorAppointmentDto
            {
                Id = a.Id,
                AppointmentDate = a.AppointmentDate,
                Reason = a.Reason,
                IsConfirmed = a.IsConfirmed,
                // Try to find name, otherwise default to ID
                PatientName = patientDirectory.ContainsKey(a.PatientId)
                              ? patientDirectory[a.PatientId]
                              : $"Patient #{a.PatientId}"
            }).ToList();

            return Ok(result);
        }
    }
}