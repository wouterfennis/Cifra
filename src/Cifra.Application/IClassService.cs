using Cifra.Application.Models.Class.Commands;
using Cifra.Application.Models.Class.Results;
using System.Threading.Tasks;

namespace Cifra.Application
{
    public interface IClassService
    {
        Task<AddStudentResult> AddStudentAsync(AddStudentCommand model);
        Task<CreateClassResult> CreateClassAsync(CreateClassCommand model);
        Task<GetAllClassesResult> GetClassesAsync();
        Task<GetClassResult> GetClassAsync(int id);
    }
}