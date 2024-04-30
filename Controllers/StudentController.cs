using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MyFullStackBackendDemo1;

[ApiController]
[Route("ACET/[controller]")]
public class StudentController:ControllerBase
{
    private readonly StudentContext studentContext;

    public StudentController(StudentContext myContext){
        studentContext=myContext;
    }

    [HttpGet("GetAllStudentData")]
    public async Task<ActionResult<IEnumerable<Student>>> GetAllStudentDetails(){
        return await studentContext.Student.ToListAsync();
    }
   
    [HttpGet("GetStudentDataByID/{id}")]
    public async Task<ActionResult<Student>> GetParticularStudent(int id){
        return await studentContext.Student.FindAsync(id);
    }

    [HttpPost("AddNewStudent")]
    public async Task<ActionResult<Student>> AddStudent(Student myStudent){
        studentContext.Student.Add(myStudent);
        await studentContext.SaveChangesAsync();
        return CreatedAtAction("GetParticularStudent",new {id=myStudent.Student_Id},myStudent);
    }

    //[HttpDelete("RemoveStudent/{id}")]
    //public async Task<ActionResult<Student>> DeleteStudent(int id){
[HttpDelete("RemoveStudent/{id}")]
public async Task<ActionResult<IEnumerable<Student>>> DeleteStudent(int id){        
    var studentData = await studentContext.Student.FindAsync(id);
        if(studentData==null){
            return NotFound();
        }
        studentContext.Student.Remove(studentData);
        await studentContext.SaveChangesAsync();
        return await studentContext.Student.ToListAsync();
        //return NoContent();
    }
    [HttpPut("ModifyStudentData/{id}")]
    public async Task<IActionResult> UpdateStudentData(int id,Student myStudent){
        if(id!=myStudent.Student_Id){
            return BadRequest();
        }
        studentContext.Entry(myStudent).State = EntityState.Modified;
        try{
            await studentContext.SaveChangesAsync();
        }catch (DbUpdateConcurrencyException)
            {
                if (!StudentExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return NoContent();
    }
     private bool StudentExists(int id)
        {
            return studentContext.Student.Any(e => e.Student_Id == id);
        }

}
