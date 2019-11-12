using Tuto.Domain.Models;
using Tuto.Domain.Repositories;

namespace Tuto.API.Controllers
{
    public class UsersController : ODataControllerBase<User>
    {
        public UsersController(IRepository<User> entityRepository) : base(entityRepository)
        {
        }
    }
}
