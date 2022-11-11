using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Admin.Common.Interfaces
{
    public interface ISeeder
    {
        Task Start(bool isForceUpdate = false);
        Dictionary<string, string> GetMessages();
    }
}