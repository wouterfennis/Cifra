using Cifra.Application.Models.Class.Requests;
using Cifra.Application.Models.Class.Results;
using System.Threading.Tasks;

namespace Cifra.Application.Interfaces
{
    public interface IClassService
    {
        Task<AddStudentResult> AddStudentAsync(AddStudentRequest model);
        Task<CreateClassResult> CreateClassAsync(CreateClassRequest model);
        Task<CreateMagisterClassResult> CreateMagisterClassAsync(CreateMagisterClassRequest model);
        Task<GetAllClassesResult> GetClassesAsync();
    }
}