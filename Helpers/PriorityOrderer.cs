using Xunit.Abstractions;
using Xunit.Sdk;
using System.Collections.Generic;
using System.Linq;

namespace TestCheck.Helpers
{
    public class PriorityOrderer : ITestCaseOrderer
    {
        public IEnumerable<TTestCase> OrderTestCases<TTestCase>(
            IEnumerable<TTestCase> testCases)
            where TTestCase : ITestCase
        {
            var sorted = new SortedDictionary<int, List<TTestCase>>();

            foreach (var testCase in testCases)
            {
                var priorityAttr = testCase.TestMethod.Method
                    .GetCustomAttributes(typeof(PriorityAttribute).AssemblyQualifiedName)
                    .FirstOrDefault();

                var priority = priorityAttr == null
                    ? 0
                    : priorityAttr.GetNamedArgument<int>("Value");

                if (!sorted.ContainsKey(priority))
                    sorted[priority] = new List<TTestCase>();

                sorted[priority].Add(testCase);
            }

            foreach (var list in sorted.Values)
            {
                foreach (var testCase in list)
                    yield return testCase;
            }
        }
    }
}