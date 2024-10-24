using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;

namespace ApiVersioning.Controllers.V1;

//[ValidateApiVersion]
[ApiVersion("1.0")]
//[ApiVersion("2.0")]
[Route("api/v{version:apiVersion}/[Controller]")]
[ApiController]
public class StudentController : ControllerBase
{
    public List<Student> Students { get; set; }

    public StudentController(List<Student> students1)
    {
        Students = students1;
        Students.Add(new Student(1, "Ibrahim", 85.5));
        Students.Add(new Student(2, "Aqil", 99.9));
        Students.Add(new Student(3, "Rizvan", 93.9));
        Students.Add(new Student(4, "Aslan", 94.9));

    }

    [MapToApiVersion("1.0")]
    [HttpGet("GetStudents")]
    public IActionResult GetStudentsV1()
    {
        return Ok(Students.Take(2));
    }
    //[MapToApiVersion("2.0")]
    //[HttpGet("GetStudents")]
    //public IActionResult GetStudentsV2()
    //{
    //    return Ok(Students.Skip(2).Take(2));
    //}

}
