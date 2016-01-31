using System.Collections.Generic;
using System.Linq;
using DddInPractice.Logic.Atms;
using MongoDB.Driver;

namespace DddInPractice.Mongo
{
    public class AtmRepository : Repository<Atm>, IAtmRepository
    {
        public IReadOnlyList<AtmDto> GetAtmList()
        {
            return Collection.FindSync(FilterDefinition<Atm>.Empty)
                .ToList()
                .Select(x => new AtmDto(x.Id, x.MoneyInside.Amount))
                .ToList();
        }
    }
}