using Tuto.Domain.Models;
using Tuto.Domain.Repositories;

namespace Tuto.API.Controllers
{
    public class RegionsController : ODataControllerBase<Region>
    {
        public RegionsController(IRepository<Region> entityRepository) : base(entityRepository)
        {
        }
    }
}
