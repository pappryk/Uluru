﻿using GeneticSharp.Domain;
using GeneticSharp.Domain.Crossovers;
using GeneticSharp.Domain.Mutations;
using GeneticSharp.Domain.Populations;
using GeneticSharp.Domain.Selections;
using GeneticSharp.Domain.Terminations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Uluru.Scheduling
{
    class ScheduleGenerator : IScheduleGenerator
    {
        private List<GAWorkEntry> _workEntries;
        private List<GAAvailability> _availabilities;

        public GeneticAlgorithm GeneticAlgorithm { get; set; }
        public ScheduleChromosome BestChromosome => GeneticAlgorithm?.BestChromosome as ScheduleChromosome;
        public ScheduleFitness Fitness { get; set; }
        
        public IEnumerable<GAWorkEntry> Generate(IList<GAWorkEntry> workEntries, IList<GAAvailability> availabilities)
        {
            _workEntries = new List<GAWorkEntry>(workEntries);
            _availabilities = new List<GAAvailability>(availabilities);
            Initialize();
            GeneticAlgorithm.Start();

            return BestChromosome.GetWorkEntries();
        }
        public ScheduleGenerator()
        {
        }

        public ScheduleGenerator(IList<GAWorkEntry> workEntries, IList<GAAvailability> availabilities)
        {
            _workEntries = new List<GAWorkEntry>(workEntries);
            _availabilities = new List<GAAvailability>(availabilities);

            Initialize();
        }

        public void Initialize()
        {
            var chromosome = new ScheduleChromosome(_workEntries.DeepClone(), _availabilities.DeepClone());

            var population = new Population(5, 10, chromosome);
            Fitness = new ScheduleFitness();

            var crossover = new ScheduleCrossover();
            var selection = new EliteSelection();
            var mutation = new ReverseSequenceMutation();

            GeneticAlgorithm = new GeneticAlgorithm(population, Fitness, selection, crossover, mutation);
            GeneticAlgorithm.Termination = new GenerationNumberTermination(5);

            GeneticAlgorithm.MutationProbability = 0.0f;
        }

        public void Run()
        {
            GeneticAlgorithm.Start();
            var best = ((ScheduleChromosome)GeneticAlgorithm.BestChromosome);
            best.PrintWorkEntries();
        }
    }
}
