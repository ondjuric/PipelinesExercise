using ApprovalTests.Reporters;
using ApprovalTests.Reporters.Linux;
using NUnit.Framework;
using PipelinesExercise;
using Refactoring.Pipelines.Approvals;

namespace Tests
{
    [UseReporter(typeof(DiffMergeReporter))]
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            var pipes = new DoEverything().SetUpPipeline();

            PipelineApprovals.Verify(pipes.Item4);
        }
    }
}