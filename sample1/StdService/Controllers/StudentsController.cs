using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StdService.Models;

namespace StdService.Controllers
{
    [Route("api/[controller]")]
    public class StudentsController : Controller
    {
        private readonly StdServiceContext _context;

        public StudentsController(StdServiceContext context){
            _context = context;
            if(_context.Students.Count()==0){
                _context.Students.Add(new Student{ fname = "Test1", lname = "TestTest" });
                _context.SaveChanges();
            }
        }

        [HttpGet]
        public IEnumerable<Student> GetAll(){
            return _context.Students.ToList();
        }
        [HttpGet("{id}", Name = "GetStudent")]
        public IActionResult GetById(long id){
            var item = _context.Students.FirstOrDefault(s => s.id == id);
            if(item== null){
                return NotFound();
            }
            return new ObjectResult(item);
        }

        [HttpPost]
        public IActionResult Create([FromBody] Student item){
            if(item == null){
                return BadRequest();
            }
            _context.Students.Add(item);
            _context.SaveChanges();

            return CreatedAtRoute("GetStudent", new {id = item.id} , item);
        }

        [HttpPut("{id}")]
        public IActionResult Update(long id, [FromBody] Student item)
        {
            if (item == null || item.id != id)
            {
                return BadRequest();
            }

            var std = _context.Students.FirstOrDefault(t => t.id == id);
            if (std == null)
            {
                return NotFound();
            }
            std.fname = item.fname;
            std.lname = item.lname;
            _context.Students.Update(std);
            _context.SaveChanges();
            return new NoContentResult();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var todo = _context.Students.FirstOrDefault(t => t.id == id);
            if (todo == null)
            {
                return NotFound();
            }

            _context.Students.Remove(todo);
            _context.SaveChanges();
            return new NoContentResult();
        }
    }
}