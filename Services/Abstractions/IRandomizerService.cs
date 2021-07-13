using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Abstractions
{
    public interface IRandomizerService
    {
        ICollection<T> Shuffle<T>(IList<T> list);

        T PickRandom<T>(IList<T> list);
    }
}