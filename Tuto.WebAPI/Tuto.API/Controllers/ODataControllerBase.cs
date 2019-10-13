using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tuto.Domain.Models.Interfaces;
using Tuto.Domain.Repositories;

namespace Tuto.API.Controllers
{
    public class ODataControllerBase<T> : ODataController where T: class, IODataEntity
    {
        protected readonly IRepository<T> _entityRepository;
        public ODataControllerBase(IRepository<T> entityRepository)
        {
            _entityRepository = entityRepository;
        }

        [EnableQuery]
        public virtual IActionResult Get()
        {
            return Ok(_entityRepository.Read());
        }

        [EnableQuery]
        public virtual async Task<IActionResult> Get(Guid key)
        {
            return Ok(await _entityRepository.Read().FirstOrDefaultAsync(e => e.Id == key));
        }

        [EnableQuery]
        public virtual async Task<IActionResult> Post([FromBody]T entity)
        {
            _entityRepository.Create(entity);
            await _entityRepository.Commit();
            return Created(entity);
        }

        public virtual async Task<IActionResult> Put([FromODataUri]Guid key, [FromBody]T entity)
        {
            var entityExists = await _entityRepository.Read().AnyAsync(e => e.Id == key);
            if (!entityExists)
            {
                return NotFound();
            }
            entity.Id = key;
            _entityRepository.Update(entity);
            await _entityRepository.Commit();
            return Updated(entity);
        }

        public virtual async Task<IActionResult> Delete([FromODataUri]Guid key)
        {
            var entityFromStorage = await _entityRepository.Read().FirstOrDefaultAsync(l => l.Id == key);
            if (entityFromStorage is null)
            {
                return NotFound();
            }
            _entityRepository.Delete(entityFromStorage);
            await _entityRepository.Commit();
            return NoContent();
        }
    }
}
