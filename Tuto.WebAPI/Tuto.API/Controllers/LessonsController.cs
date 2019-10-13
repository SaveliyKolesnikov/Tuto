using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using Tuto.Domain.Models;
using Tuto.Domain.Repositories;

namespace Tuto.API.Controllers
{
    public class LessonsController : ODataController
    {
        private readonly IRepository<Lesson> _lessonsRepository;
        public LessonsController(IRepository<Lesson> lessonsRepository)
        {
            _lessonsRepository = lessonsRepository;
        }

        [EnableQuery]
        public IActionResult Get()
        {
            return Ok(_lessonsRepository.Read());
        }

        [EnableQuery]
        public async Task<IActionResult> Get(Guid key)
        {
            return Ok(await _lessonsRepository.Read().FirstOrDefaultAsync(c => c.LessonId == key));
        }

        [EnableQuery]
        public async Task<IActionResult> Post([FromBody]Lesson lesson)
        {
            _lessonsRepository.Create(lesson);
            await _lessonsRepository.Commit();
            return Created(lesson);
        }

        public async Task<IActionResult> Put([FromODataUri]Guid key, [FromBody]Lesson lesson)
        {
            var lessonExists = await _lessonsRepository.Read().AnyAsync(l => l.LessonId == key);
            if (!lessonExists)
            {
                return NotFound();
            }
            lesson.LessonId = key;
            _lessonsRepository.Update(lesson);
            await _lessonsRepository.Commit();
            return Updated(lesson);
        }

        public async Task<IActionResult> Delete([FromODataUri]Guid key)
        {
            var lessonFromDb = await _lessonsRepository.Read().FirstOrDefaultAsync(l => l.LessonId == key);
            if (lessonFromDb is null)
            {
                return NotFound();
            }
            _lessonsRepository.Delete(lessonFromDb);
            await _lessonsRepository.Commit();
            return NoContent();
        }
    }
}
