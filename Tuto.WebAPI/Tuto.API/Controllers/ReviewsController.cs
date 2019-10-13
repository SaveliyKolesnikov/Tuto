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
    public class ReviewsController : ODataController
    {
        private readonly IRepository<Review> _reviewsRepository;
        public ReviewsController(IRepository<Review> reviewsRepository)
        {
            _reviewsRepository = reviewsRepository;
        }

        [EnableQuery]
        public IActionResult Get()
        {
            return Ok(_reviewsRepository.Read());
        }

        [EnableQuery]
        public async Task<IActionResult> Get(Guid key)
        {
            return Ok(await _reviewsRepository.Read().FirstOrDefaultAsync(c => c.ReviewId == key));
        }

        [EnableQuery]
        public async Task<IActionResult> Post([FromBody]Review review)
        {
            _reviewsRepository.Create(review);
            await _reviewsRepository.Commit();
            return Created(review);
        }

        public async Task<IActionResult> Put([FromODataUri]Guid key, [FromBody]Review review)
        {
            var reviewExists = await _reviewsRepository.Read().AnyAsync(l => l.ReviewId == key);
            if (!reviewExists)
            {
                return NotFound();
            }
            review.ReviewId = key;
            _reviewsRepository.Update(review);
            await _reviewsRepository.Commit();
            return Updated(review);
        }

        public async Task<IActionResult> Delete([FromODataUri]Guid key)
        {
            var reviewFromDb = await _reviewsRepository.Read().FirstOrDefaultAsync(l => l.ReviewId == key);
            if (reviewFromDb is null)
            {
                return NotFound();
            }
            _reviewsRepository.Delete(reviewFromDb);
            await _reviewsRepository.Commit();
            return NoContent();
        }
    }
}
