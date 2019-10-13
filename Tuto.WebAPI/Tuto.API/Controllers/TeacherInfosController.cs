using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using Tuto.Domain.Models;
using Tuto.Domain.Repositories;

namespace Tuto.API.Controllers
{
    public class TeacherInfosController : ODataController
    {
        private readonly IRepository<TeacherInfo> _teacherInfosRepository;
        public TeacherInfosController(IRepository<TeacherInfo> lessonsRepository)
        {
            _teacherInfosRepository = lessonsRepository;
        }

        [EnableQuery]
        public IActionResult Get()
        {
            return Ok(_teacherInfosRepository.Read());
        }

        [EnableQuery]
        public async Task<IActionResult> Get(Guid key)
        {
            return Ok(await _teacherInfosRepository.Read().FirstOrDefaultAsync(c => c.TeacherInfoId == key));
        }

        [EnableQuery]
        public async Task<IActionResult> Post([FromBody]TeacherInfo teacherInfo)
        {
            _teacherInfosRepository.Create(teacherInfo);
            await _teacherInfosRepository.Commit();
            return Created(teacherInfo);
        }

        public async Task<IActionResult> Put([FromODataUri]Guid key, [FromBody]TeacherInfo teacherInfo)
        {
            var lessonExists = await _teacherInfosRepository.Read().AnyAsync(l => l.TeacherInfoId == key);
            if (!lessonExists)
            {
                return NotFound();
            }
            teacherInfo.TeacherInfoId = key;
            _teacherInfosRepository.Update(teacherInfo);
            await _teacherInfosRepository.Commit();
            return Updated(teacherInfo);
        }

        public async Task<IActionResult> Delete([FromODataUri]Guid key)
        {
            var teacherInfoFromDb = await _teacherInfosRepository.Read().FirstOrDefaultAsync(l => l.TeacherInfoId == key);
            if (teacherInfoFromDb is null)
            {
                return NotFound();
            }
            _teacherInfosRepository.Delete(teacherInfoFromDb);
            await _teacherInfosRepository.Commit();
            return NoContent();
        }
    }
}
