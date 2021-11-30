using BasiqTestAppIntegrationTest.TestCases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace BasiqTestAppIntegrationTest
{
    public class TestCaseRunner
    {
        public static TestCase CurrentTestCase { get; set; }

        private static List<TestCase> testCases = new List<TestCase>(1)
        {
            new TestCase()
            {
                Name = "InitialTest",
                InputCode = "411",
                JsonData = "{\"type\":\"list\",\"count\":500,\"size\":1494,\"data\":[{\"amount\":\"-19.20\",\"subClass\":{\"title\":\"Cafes, Restaurants and Takeaway Food Services\",\"code\":\"451\"}},{\"amount\":\"-144.35\",\"subClass\":{\"title\":\"Supermarket and Grocery Stores\",\"code\":\"411\"}},{\"amount\":\"-17.30\",\"subClass\":{\"title\":\"Cafes, Restaurants and Takeaway Food Services\",\"code\":\"451\"}}],\"links\":{\"next\":\"\"}}",
                ExpectedAverageValue = "144.35"
            }
        };

        public void RunTests()
        {
            HttpClient httpClient = new HttpClient();
            foreach(TestCase testCase in testCases)
            {
                CurrentTestCase = testCase;
                var response = httpClient.GetAsync("https://localhost:5051/api/average"+"/"+testCase.InputCode).Result;
                string average = response.Content.ReadAsStringAsync().Result;
                Console.WriteLine($"Expected: {testCase.ExpectedAverageValue}, Received: {average}");
                testCase.Passed = testCase.ExpectedAverageValue == average;
            }
        }

        public void PrintResults()
        {
            HttpClient httpClient = new HttpClient();
            foreach (TestCase testCase in testCases)
            {
                string status = testCase.Passed ? "PASSED" : "FAILED";
                Console.WriteLine($"Test: {testCase.Name} - {status}");
            }
        }
    }
}
