﻿using Cifra.Application.Models.Class.Commands;
using Cifra.Application.Models.Class.Results;
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