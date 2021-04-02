﻿using System.Collections.Generic;

namespace Cifra.Application.Models.Test.Results
{
    /// <summary>
    /// The result of the Get All Tests operation
    /// </summary>
    public sealed class GetAllTestsResult
    {
        /// <summary>
        /// The tests
        /// </summary>
        public IEnumerable<Test> Tests { get; }

        /// <summary>
        /// Ctor
        /// </summary>
        public GetAllTestsResult(IEnumerable<Test> tests)
        {
            Tests = tests;
        }
    }
}