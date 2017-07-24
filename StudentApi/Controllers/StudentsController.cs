using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StudentApi.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace StudentApi.Controllers
{
    [Route("api/student")]
    public class StudentsController : Controller
    {
        private readonly StudentContext _context;

        public StudentsController(StudentContext context)
        {
            _context = context;

            if (_context.Student.Count() == 0)
            {
                _context.Student.Add(new Student {
                    FullName = "Suheri Marpaung",
                    Gender = "Male",
                    Email = "suherimarpaung@gmail.com",
                    Address = "South Jakarta",
                    Age = 23,
                    AcademicAdvisor = "Handoko Mulyani"
                });
                _context.SaveChanges();
            }
        }


        // GET: api/values
        [HttpGet]
        public IEnumerable<Student> GetAllStudents()
        {
            return _context.Student.ToList();
        }

        // GET api/values/5
        [HttpGet("{id}", Name = "StudentDetail")]
        public IActionResult GetStudentDetail(long id)
        {
            var student = _context.Student.FirstOrDefault(t => t.Id == id);
            if (student == null)
            {
                return NotFound();
            }
            return new ObjectResult(student);
        }

        // POST api/values
        [HttpPost]
        public IActionResult Create([FromBody] Student student)
        {
            if (student == null)
            {
                return BadRequest();
            }

            _context.Student.Add(student);
            _context.SaveChanges();

            return CreatedAtRoute("StudentDetail", new { id = student.Id }, student);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public IActionResult Update(long id, [FromBody] Student student)
        {
            if (student == null || student.Id != id)
            {
                return BadRequest();
            }

            var studentFromDb = _context.Student.FirstOrDefault(t => t.Id == id);
            if (studentFromDb == null)
            {
                return NotFound();
            }

            studentFromDb.FullName = student.FullName;
            studentFromDb.Age = student.Age;
            studentFromDb.Email = student.Email;
            studentFromDb.Age = student.Age;
            studentFromDb.AcademicAdvisor = student.AcademicAdvisor;
            studentFromDb.Address = student.Address;

            _context.Student.Update(studentFromDb);
            _context.SaveChanges();
            return new NoContentResult();
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var todo = _context.Student.First(t => t.Id == id);
            if (todo == null)
            {
                return NotFound();
            }

            _context.Student.Remove(todo);
            _context.SaveChanges();
            return new NoContentResult();
        }
    }
}
