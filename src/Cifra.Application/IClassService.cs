using Cifra.Application.Models.Commands;
using Cifra.Application.Models.Results;
using System.Threading.Tasks;

namespace Cifra.Application
{
    public interface IClassService
    {
        Task<CreateClassResult> CreateClassAsync(CreateClassCommand model);
        Task<UpdateClassResult> UpdateClassAsync(UpdateClassCommand model);
        Task<GetAllClassesResult> GetClassesAsync();
        Task<GetClassResult> GetClassAsync(int id);
    }
}