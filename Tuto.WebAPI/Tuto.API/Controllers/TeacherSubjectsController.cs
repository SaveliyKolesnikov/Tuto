using Tuto.Domain.Models;
using Tuto.Domain.Repositories;

namespace Tuto.API.Controllers
{
    public class TeacherSubjectsController : ODataControllerBase<TeacherSubject>
    {
        public TeacherSubjectsController(IRepository<TeacherSubject> entityRepository) : base(entityRepository)
        {
        }
    }
}
