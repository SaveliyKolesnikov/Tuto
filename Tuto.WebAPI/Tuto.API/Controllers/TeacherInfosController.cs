using Tuto.Domain.Models;
using Tuto.Domain.Repositories;

namespace Tuto.API.Controllers
{
    public class TeacherInfosController : ODataControllerBase<TeacherInfo>
    {
        public TeacherInfosController(IRepository<TeacherInfo> entityRepository) : base(entityRepository)
        {
        }
    }
}
