using Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services
{
    public class RandomizerService : IRandomizerService
    {
        private static Random rng = new Random();

        public T PickRandom<T>(IList<T> list)
        {
            int index = rng.Next(list.Count);

            return list[index];
        }

        public ICollection<T> Shuffle<T>(IList<T> list)
        {
            IList<T> newList = list;
            int n = newList.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                T value = newList[k];
                newList[k] = newList[n];
                newList[n] = value;
            }

            return newList;
        }
    }
}