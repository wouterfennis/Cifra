using AutoMapper;
using Cifra.Application.Models.Test;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cifra.Database.Mapping.TypeConverters
{
    class TestConverter : ITypeConverter<Schema.Test, Test>
    {
        public Test Convert(Schema.Test source, Test destination, ResolutionContext context)
        {
            throw new NotImplementedException();
        }
    }
}
