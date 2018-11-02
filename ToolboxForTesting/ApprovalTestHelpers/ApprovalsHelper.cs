using System;
using System.Text.RegularExpressions;
using ApprovalTests;
using ApprovalTests.Core;

namespace ToolboxForTesting.ApprovalTestHelpers
{
    /// <summary>
    /// Helper mehtods that simplifies the usage of the Approvals test framework, especially
    /// by making it possible to override the naming of the approval files. 
    /// </summary>
    public static class ApprovalsHelper
    {
        /// <summary>
        /// A straight call to Approvals.Verify that could be used if you don't
        /// want to provide a file name.
        /// </summary>
        public static void Verify(string text)
        {
            Approvals.Verify(text);
        }

        /// <summary>
        /// Allows you to verify a text in a file with a name of your own selection.
        /// </summary>
        /// <param name="text">The text to verify.</param>
        /// <param name="fileName">The name of the approvals file (instead of the test name).</param>
        public static void Verify(string text, string fileName)
        {
            Approvals.Verify(new ApprovalTextWriter(text), new ApprovalFileNamer(fileName), Approvals.GetReporter());
        }

        /// <summary>
        /// Allows you to verify an object (that will be serialized to a Json string) in a file with a name of your own selection.
        /// </summary>
        /// <param name="objectToSerialize">The object you want to verify. The object will be serialized into a
        /// Json string (that is indented)</param>
        /// <param name="fileName">The name of the approvals file (instead of the test name).</param>
        public static void Verify(object objectToSerialize, string fileName)
        {
            Approvals.Verify(new ApprovalTextWriter(objectToSerialize.ToJsonString()), new ApprovalFileNamer(fileName), Approvals.GetReporter());
        }


        /// <summary>
        /// Helper mehtod that searches a text for all guids (with dashes)
        /// and replaces them with the provided replacement value to make it testable.
        /// </summary>
        public static string ReplaceGuids(this string value, string replacementValue = "ReplacedGuid")
        {
            return new Regex("........-....-....-....-............").Replace(value, replacementValue);
        }

        /// <summary>
        /// Helper method that searches a text for all dates and times and 
        /// replaces them with the provided replacement value to make it testable.
        /// </summary>
        public static string ReplaceDateTime(this string value, string replacementValue = "ReplacedDateTime")
        {
            return new Regex(@"""....-..-...*""|>....-..-...*<").Replace(value, replacementValue);
        }

        /// <summary>
        /// Helper method that searches a text for a specified value (pattern) and replaces 
        /// them with the provided replacement value to make it testable.
        /// </summary>
        public static string ReplaceExactMatch(this string value, string valueToReplace, string replacementValue = "ReplacedValue")
        {
            return new Regex(valueToReplace).Replace(value, replacementValue);
        }

        /// <summary>
        /// The "magic" class that is injected into approval tests to override the file naming.
        /// </summary>
        private class ApprovalFileNamer : IApprovalNamer
        {
            public string SourcePath { get; private set; }
            public string Name { get; private set; }

            public ApprovalFileNamer(string name)
            {
                if (String.IsNullOrWhiteSpace(name)) throw new ArgumentException("A file name for the approvals file must be provided");

                Name = name;
                SourcePath = Approvals.GetDefaultNamer().SourcePath;
            }
        }
    }
}