using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Uluru.Scheduling;
using GeneticSharp.Domain.Chromosomes;
using GeneticSharp.Domain.Randomizations;
using GeneticSharp.Domain.Crossovers;

namespace Uluru.Scheduling
{
    [DisplayName("Ordered (OX1)")]
    public sealed class ScheduleCrossover : CrossoverBase
    {
        #region Constructors
        public ScheduleCrossover()
            : base(2, 1)
        {
            IsOrdered = true;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Performs the cross with specified parents generating the children.
        /// </summary>
        /// <param name="parents">The parents chromosomes.</param>
        /// <returns>The offspring (children) of the parents.</returns>
        protected override IList<IChromosome> PerformCross(IList<IChromosome> parents)
        {
            var baseParent = parents[RandomizationProvider.Current.GetInt(0, 1)];
            var parentToProvideGenesToChange = parents.Except(new List<IChromosome>() { baseParent }, new ScheduleChromosomeEqualityComparer()).First();
            var genesIndexesToChange = RandomizationProvider.Current.GetUniqueInts(parents[0].Length / 2, 0, parents[0].Length);

            var child = new ScheduleChromosome(baseParent.GetWorkEntries().ToList(), 
                                               ((ScheduleChromosome)baseParent).Availabilities);

            var genes = parentToProvideGenesToChange.GetGenes();

            foreach (int geneIndex in genesIndexesToChange)
            {
                var newGene = genes[geneIndex];

                bool colides = child.GetWorkEntries().ToList().IsAvailabilityColision(((GAWorkEntry)newGene.Value).Availability);
                if (!colides)
                {
                    child.ReplaceGene(geneIndex, newGene);
                }
            }

            child.AssignAvailabilityToWorkEntry();

            if (parents.AnyHasRepeatedGene())
            {
                throw new CrossoverException(this, "The Ordered Crossover (OX1) can be only used with ordered chromosomes. The specified chromosome has repeated genes.");
            }

            return new List<IChromosome>() { child };
        }
        #endregion
    }
}