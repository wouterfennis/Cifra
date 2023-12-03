using Cifra.Application.Models.Results;
using Cifra.Commands;
using System.Threading.Tasks;

namespace Cifra.Application
{
    public interface IClassService
    {
        Task<CreateClassResult> CreateClassAsync(CreateClassCommand model);
        Task<UpdateClassResult> UpdateClassAsync(UpdateClassCommand model);
        Task<DeleteClassResult> DeleteClassAsync(DeleteClassCommand model);
        Task<GetAllClassesResult> GetClassesAsync();
        Task<GetClassResult> GetClassAsync(uint id);
    }
}