using Tuto.Domain.Models;
using Tuto.Domain.Repositories;

namespace Tuto.API.Controllers
{
    public class CitiesController : ODataControllerBase<City>
    {
        public CitiesController(IRepository<City> entityRepository) : base(entityRepository)
        {
        }
    }
}
