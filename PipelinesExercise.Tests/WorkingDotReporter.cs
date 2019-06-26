using System;
using System.Collections.Generic;
using ApprovalTests.Reporters;
using Refactoring.Pipelines.Approvals;

namespace Tests
{
    public class WorkingDotReporter : GenericDiffReporter
        {
            public static readonly WorkingDotReporter INSTANCE = new WorkingDotReporter();

            public WorkingDotReporter()
                : base(new DiffInfo(@"{ProgramFiles}\Microsoft VS Code\Code.exe", "-r {0}", (Func<IEnumerable<string>>)(() => (IEnumerable<string>)new string[1]
                {
                    "dot"
                })))
            {
            }
        }
}