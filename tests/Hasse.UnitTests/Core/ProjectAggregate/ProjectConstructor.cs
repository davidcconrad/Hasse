﻿using Hasse.Core.ProjectAggregate;
using Xunit;

namespace Hasse.UnitTests.Core.ProjectAggregate
{
    public class ProjectConstructor
    {
        private readonly string _testName = "test name";
        private Project _testProject;

        private Project CreateProject()
        {
            return new(_testName);
        }

        [Fact]
        public void InitializesName()
        {
            _testProject = CreateProject();

            Assert.Equal(_testName, _testProject.Name);
        }

        [Fact]
        public void InitializesTaskListToEmptyList()
        {
            _testProject = CreateProject();

            Assert.NotNull(_testProject.Items);
        }

        [Fact]
        public void InitializesStatusToInProgress()
        {
            _testProject = CreateProject();

            Assert.Equal(ProjectStatus.Complete, _testProject.Status);
        }
    }
}