using Tuto.Domain.Models;
using Tuto.Domain.Repositories;

namespace Tuto.API.Controllers
{
    public class SubjectsController : ODataControllerBase<Subject>
    {
        public SubjectsController(IRepository<Subject> entityRepository) : base(entityRepository)
        {
        }
    }
}
