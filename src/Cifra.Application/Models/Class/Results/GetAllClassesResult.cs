﻿using System;
using System.Collections.Generic;

namespace Cifra.Application.Models.Class.Results
{
    /// <summary>
    /// The result of the Get All Classes operation
    /// </summary>
    public sealed class GetAllClassesResult
    {
        /// <summary>
        /// The classes
        /// </summary>
        public IEnumerable<Domain.Class> Classes { get; }

        /// <summary>
        /// Ctor
        /// </summary>
        internal GetAllClassesResult(IEnumerable<Domain.Class> classes)
        {
            Classes = classes ?? throw new ArgumentNullException(nameof(classes));
        }
    }
}
