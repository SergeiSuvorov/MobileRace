using System.Collections.Generic;

namespace Tools
{
    public interface IRepository<Tkey, Tvalue>
    {
        IReadOnlyDictionary<Tkey, Tvalue> Content { get; }
    }
}

