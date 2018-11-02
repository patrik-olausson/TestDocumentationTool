using System;
using System.Collections.Generic;
using System.Linq;

namespace TestDocumentationTool.Api
{
    public class TestReport
    {
        private readonly List<ProjectInfo> _projects;

        public DateTimeOffset DateCreated { get; }
        public IReadOnlyCollection<ProjectInfo> Projects => _projects.ToList();

        public TestReport()
        {
            DateCreated = DateTimeOffset.Now;
            _projects = new List<ProjectInfo>();
        }

        public void AddProject(ProjectInfo projectInfo) => _projects.AddIfNotNull(projectInfo);
    }
}