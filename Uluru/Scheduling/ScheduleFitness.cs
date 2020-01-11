using GeneticSharp.Domain.Chromosomes;
using GeneticSharp.Domain.Fitnesses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Uluru.Scheduling
{
    class ScheduleFitness : IFitness
    {
        public double Evaluate(IChromosome chromosome)
        {
            var genes = chromosome.GetGenes();

            int notFilledWorkEntries = genes.Where(g => 
                ((g.Value as GAWorkEntry).Availability == null)
            ).ToList().Count;

            double fitness = ((double)genes.Length - notFilledWorkEntries) / genes.Length;
            Console.WriteLine(fitness);
            return fitness;
        }
    }
}
