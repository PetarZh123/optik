using AppointmentScheduler.Models.ViewModels;
using AppointmentScheduler.Services;
using AppointmentScheduler.Utility;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Optik.Models;
using System.Security.Claims;

namespace AppointmentScheduler.Controllers.Api
{
    [Route("api/Appointment")]
    [ApiController]
    public class AppointmentApiController : Controller
    {
        private readonly IAppointmentService _appointmentService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ApplicationDbContext dbContext;
        private readonly string loginUserId;
        private readonly string role;

        public AppointmentApiController(IAppointmentService appointmentService, IHttpContextAccessor httpContextAccessor, ApplicationDbContext dbContext)
        {
            _appointmentService = appointmentService;
            _httpContextAccessor = httpContextAccessor;
            this.dbContext = dbContext;
            loginUserId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            role = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Role);
        }
        [HttpPost]
        [Route("SaveCalendarData")]
        public IActionResult SaveCalendarData(AppointmentVM data)
        {
            CommonResponse<int> commonResponse = new CommonResponse<int>();
            try
            {
                commonResponse.status = _appointmentService.AddUpdate(data).Result;
                if(commonResponse.status == 1)
                {
                    commonResponse.message = Helper.appointmentUpdated;
                }
                if(commonResponse.status == 2)
                {
                    commonResponse.message = Helper.appointmentAdded;
                }
            }   
            catch(Exception e)
            {
                commonResponse.message = e.Message;
                commonResponse.status = Helper.failure_code;
            }
            return Ok(commonResponse);
        }

        [HttpGet("GetAppointments")]
        public IActionResult GetAppointments()
        {
            try
            {
                var appointments = dbContext.Appointments
                    .Select(a => new
                    {
                        id = a.Id,
                        title = a.Title,
                        start = a.StartDate.ToString("yyyy-MM-ddTHH:mm:ss"), // ISO format
                        end = a.EndDate.ToString("yyyy-MM-ddTHH:mm:ss"),
                        description = a.Description
                    })
                    .ToList();

                return Ok(appointments);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error retrieving appointments", error = ex.Message });
            }
        }
    }
}
