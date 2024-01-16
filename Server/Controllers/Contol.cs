using Microsoft.AspNetCore.Mvc;
using FinalProject.Server;
using Microsoft.EntityFrameworkCore;
using FinalProject.Server.Data;
using FinalProject.Shared;

namespace Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {

        public AppDbContext Context = new AppDbContext(new DbContextOptions<AppDbContext>());

        // GET: api/<StudentController>
        [HttpGet("{Name}")]

        public async Task<IActionResult> GetApplication(string name)
        {
            // var add = await Context.Students.ToListAsync();
            // return StatusCode(200);
            //return Ok("Success");
            //   return Ok(new { Message = "jdbjskd" } );
            //  return new string[] { "value1", "value2" };
            var add = await Context.Students.Where(x => x.FirstName == name)
            .Select(x => new { aqs=x.FirstName,Queen=x.LastName })
            .FirstOrDefaultAsync();
            return Ok(add);
        }
      
       

        
        ////[HttpGet("{id}")]
        ////public async Task<IActionResult> GetStudents(int id)
        ////{
        ////    try
        ////    {
        ////   Student rr= await Context.Students.FirstOrDefaultAsync(x => x.Id == id);
        ////        if (rr != null)
        ////        {
        ////        return Ok(rr);
        ////        }
        ////        return NotFound(new { Message = "pgl" });
        ////    }
        ////    catch(Exception error)
        ////    {
        ////        return StatusCode(500, error.Message);
        ////    }
        ////}



        [HttpPost]
        [Route("Add")]
        public async Task<IActionResult> AddStudents([FromBody] DTO obj)
        {
            try
            {
            var student = await Context.Students.AddAsync(new Student
                {
                    Email = obj.Email,
                    Age = obj.Age,
                    FirstName = obj.FirstName,
                    Higheducation = obj.HighEducation,
                    LastName = obj.LastName
                });
                await Context.SaveChangesAsync();
                return Ok(student.Entity);
            }
            catch(Exception error)
            {
                return StatusCode(500, error.Message);
            }
        }
        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult>UpdateStudent(int id, [FromBody] DTO obj)
        {
            try
            {
                Student rr = await Context.Students.FirstOrDefaultAsync(x => x.Id == id);
                if (rr == null)
                {
                    return NotFound(new { Message = "pgl" });
                }
                rr.Age = obj.Age;
                rr.FirstName = obj.FirstName;
                rr.LastName = obj.LastName;
                rr.Higheducation = obj.HighEducation;
                rr.Email = obj.Email;
                await Context.SaveChangesAsync();
                rr = await Context.Students.FirstOrDefaultAsync(x => x.Id == id);
                return Ok(rr);
            }
            catch (Exception error)
            {
                return StatusCode(500, error.Message);
            }
        }
        
      
    }
}
