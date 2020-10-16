using Cifra.Application.Interfaces;
using Cifra.Application.Models.Test;
using Cifra.Application.Validation;
using System;

namespace Cifra.FileSystem
{
    public class TestRepository : ITestRepository
    {
        public TestRepository(IFileLocationProvider fileLocationProvider)
        {

        }

        public Guid Create(Test test)
        {
            throw new NotImplementedException();
        }

        public Test Get(Guid id)
        {
            throw new NotImplementedException();
        }

        public ValidationMessage Update(Test test)
        {
            throw new NotImplementedException();
        }
    }
}
