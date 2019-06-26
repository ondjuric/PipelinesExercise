using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Refactoring.Pipelines;

namespace PipelinesExercise
{
    public static class PipelineHelpers
    {
        public static FunctionPipe<IEnumerable<TInput>, List<TOutput>> ProcessForEach<TInput, TOutput>(
            this Sender<IEnumerable<TInput>> sender, Func<TInput, TOutput> func)
        {
            return new FunctionPipe<IEnumerable<TInput>, List<TOutput>>(p => p.Select(func).ToList(), sender);
        }
    }
}
