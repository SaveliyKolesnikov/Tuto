using System;
using System.Threading.Tasks;

namespace Tuto.Services.Interfaces
{
    public interface IChatManager
    {
        Task<bool> HaveAnyCommonMessages(Guid user1, Guid user2);
    }
}
