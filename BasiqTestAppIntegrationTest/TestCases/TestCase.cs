using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BasiqTestAppIntegrationTest.TestCases
{
    public class TestCase
    {
        public string Name { get; set; }
        public string InputCode { get; set; }
        public string JsonData { get; set; }
        public string ExpectedAverageValue { get; set; }
        public bool Passed { get; set; }
    }
}
