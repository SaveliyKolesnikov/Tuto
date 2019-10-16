using Tuto.Domain.Models;
using Tuto.Domain.Repositories;

namespace Tuto.API.Controllers
{
    public class LessonsController : ODataControllerBase<Lesson>
    {
        public LessonsController(IRepository<Lesson> entityRepository) : base(entityRepository)
        {
        }
    }
}
