using System.Collections.Generic;

namespace Cifra.Application.Models.Class.Results
{
    public class GetAllClassesResult
    {
        public IEnumerable<Class> Classes { get; }

        public GetAllClassesResult(IEnumerable<Class> classes)
        {
            Classes = classes;
        }
    }
}
