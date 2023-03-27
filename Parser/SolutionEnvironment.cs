using AbstractLL;

using CleanArc.Models;

namespace CleanArc.Parser
{
    internal class SolutionEnvironment : AbstractEnvironment<SolutionModel>
    {
        public SolutionModel Solution { get; set; } = default!;

        public override SolutionModel? Result => Solution;

        public override void Inicializa()
        {
            Solution = new();
        }
    }
}
