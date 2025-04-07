namespace AppointmentScheduler.Models.ViewModels
{
    public class AppointmentVM
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public int? Duration { get; set; }
        public string DoctorId { get; set; }
        public string PatientId { get; set; }
        public bool IsDoctorApproved { get; set; }
        public bool IsForClient { get; set; }
    }
}
