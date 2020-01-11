using GeneticSharp.Domain.Chromosomes;
using GeneticSharp.Domain.Randomizations;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace Uluru.Scheduling
{
    public static class ExtensionMethods
    {
        /// <summary>
        /// Returns shuffled collection
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <returns></returns>
        public static List<T> Shuffled<T>(this List<T> collection)
        {
            List<T> newCollection = new List<T>();
            int count = collection.Count;
            var indexes = RandomizationProvider.Current.GetUniqueInts(count, 0, count);

            foreach (var index in indexes)
            {
                newCollection.Add(collection[index]);
            }

            return newCollection;
        }

        public static T DeepClone<T>(this T obj)
        {
            using (var ms = new MemoryStream())
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(ms, obj);
                ms.Position = 0;

                return (T)formatter.Deserialize(ms);
            }
        }

        public static IList<GAAvailability> GetAvailabilities(this IList<GAWorkEntry> workEntries)
        {
            return workEntries.Select(w => w.Availability).ToList();
        }

        public static bool IsAvailabilityColision(this IList<GAWorkEntry> workEntries, GAAvailability availability)
        {
            if (availability == null)
                return true;

            var availabilities = workEntries.GetAvailabilities().Where(a => a != null).ToList();
            if (availabilities.Count > 0)
            {
                if (availabilities.FirstOrDefault(a => a.UserId == availability.UserId && a.Date == availability.Date) != null)
                {
                    return true;
                }
            }
            //return workEntries.GetAvailabilities().Contains(availability);
            return false;
        }

        public static IEnumerable<GAWorkEntry> GetWorkEntries(this IChromosome chromosome)
        {
            return chromosome.GetGenes().Select(g => (GAWorkEntry)g.Value);
        }

    }
}
