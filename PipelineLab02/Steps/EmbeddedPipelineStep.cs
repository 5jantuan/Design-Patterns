using System;
using System.Text;
using Pipeline.Core;
using Pipeline.Interfaces;

namespace Pipeline.Steps
{
    public sealed class EmbeddedPipelineStep<TContext> : IPipelineStep<TContext>
    {
        private readonly Pipeline.Core.Pipeline<TContext> _inner;

        public EmbeddedPipelineStep(Pipeline.Core.Pipeline<TContext> inner)
        {
            _inner = inner ?? throw new ArgumentNullException(nameof(inner));
        }

        public void Execute(TContext context)
        {
            Console.WriteLine("EmbeddedPipelineStep: executing inner pipeline...");
            _inner.Execute(context);
        }

        public void Introspect(StringBuilder sb, int indent = 0)
        {
            sb.AppendLine($"{new string(' ', indent)}EmbeddedPipelineStep -> inner pipeline:");
            _inner.Describe(sb);
        }
    }
}
