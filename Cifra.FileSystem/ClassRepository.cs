using Cifra.Application.Interfaces;
using Cifra.Application.Models.Class;
using Cifra.Application.Models.Test;
using Cifra.Application.Validation;
using System;

namespace Cifra.FileSystem
{
    public class ClassRepository : IClassRepository
    {
        public ClassRepository(IFileLocationProvider fileLocationProvider)
        {

        }

        public Guid Create(Class @class)
        {
            throw new NotImplementedException();
        }

        public Class Get(Guid id)
        {
            throw new NotImplementedException();
        }

        public ValidationMessage Update(Class @class)
        {
            throw new NotImplementedException();
        }
    }
}
