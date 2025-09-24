using EmployeeAttendanceAPI.Data;
using EmployeeAttendanceAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmployeeAttendanceAPI.Controllers// define the namespace
{
    [Route("api/[controller]")]// define the route for the controller
    [ApiController]// specify that this is an API controller
    public class AttendanceController : ControllerBase// inherit from ControllerBase
    {
        private readonly AppDbContext _context;// database context efcore field
        public AttendanceController(AppDbContext context) => _context = context;// constructor to initialize the database context to field

        // GET: api/attendance
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Attendance>>> GetAttendances()// get all attendance records
        {
            return await _context.Attendances.ToListAsync();// return the list of attendance records
        }

        // GET: api/attendance/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Attendance>> GetAttendance(int id)// get a specific attendance record by id
        {
            var attendance = await _context.Attendances.FindAsync(id);// find the attendance record by id
            if (attendance == null) return NotFound();// if not found return 404
            return attendance;// return the attendance record
        }

        // POST: api/attendance
        [HttpPost]
        public async Task<ActionResult<Attendance>> PostAttendance(Attendance attendance)// create a new attendance record
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);// if model state is not valid return 400 checks validations

            _context.Attendances.Add(attendance);// add the attendance record to the context or dbset
            try
            {
                await _context.SaveChangesAsync();// save changes to the database
            }
            catch (DbUpdateException)// catch database update exceptions
            {
                return Conflict("EmployeeId must be unique per day.");// return 409 if there is a conflict
            }

            return CreatedAtAction(nameof(GetAttendance), new { id = attendance.Id }, attendance);// return 201 with the location of the new resource
        }

        // PUT: api/attendance/5
        [HttpPut("{id}")]// update an existing attendance record
        public async Task<IActionResult> PutAttendance(int id, Attendance attendance)// update an existing attendance record
        {
            if (id != attendance.Id) return BadRequest();// if id does not match return 400

            _context.Entry(attendance).State = EntityState.Modified;// mark the attendance record as modified

            try
            {
                await _context.SaveChangesAsync();// save changes to the database
            }
            catch (DbUpdateException)// catch database update exceptions
            {
                return Conflict("EmployeeId must be unique per day.");// return 409 if there is a conflict
            }

            return NoContent();
        }

        // DELETE: api/attendance/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAttendance(int id)// delete an attendance record by id
        {
            var attendance = await _context.Attendances.FindAsync(id);// find the attendance record by id
            if (attendance == null) return NotFound();// if not found return 404

            _context.Attendances.Remove(attendance);// remove the attendance record from the context or dbset
            await _context.SaveChangesAsync();// save changes to the database

            return NoContent();// return 204
        }

        // GET: api/attendance/search?employeeId=E001
        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<Attendance>>> SearchByEmployee(string employeeId)// search attendance records by employee id
        {
            return await _context.Attendances// query the attendance records
                .Where(a => a.EmployeeId == employeeId)// filter by employee id
                .ToListAsync();// return the list of attendance records
        }

        // GET: api/attendance/date?from=2025-01-01&to=2025-12-31
        [HttpGet("date")]
        public async Task<ActionResult<IEnumerable<Attendance>>> GetByDateRange(DateTime from, DateTime to)// get attendance records within a date range
        {
            return await _context.Attendances// query the attendance records
                .Where(a => a.Date >= from && a.Date <= to)// filter by date range
                .ToListAsync();// return the list of attendance records
        }
    }
}
