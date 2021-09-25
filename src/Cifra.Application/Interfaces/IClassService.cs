using Cifra.Application.Models.Class.Commands;
using Cifra.Application.Models.Class.Results;
using System.Threading.Tasks;

namespace Cifra.Application.Interfaces
{
    public interface IClassService
    {
        Task<AddStudentResult> AddStudentAsync(AddStudentCommand model);
        Task<CreateClassResult> CreateClassAsync(CreateClassCommand model);
        Task<CreateMagisterClassResult> CreateMagisterClassAsync(CreateMagisterClassCommand model);
        Task<GetAllClassesResult> GetClassesAsync();
    }
}