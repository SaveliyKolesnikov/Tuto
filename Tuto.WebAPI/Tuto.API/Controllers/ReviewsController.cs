using Tuto.Domain.Models;
using Tuto.Domain.Repositories;

namespace Tuto.API.Controllers
{
    public class ReviewsController : ODataControllerBase<Review>
    {
        public ReviewsController(IRepository<Review> entityRepository) : base(entityRepository)
        {
        }
    }
}
