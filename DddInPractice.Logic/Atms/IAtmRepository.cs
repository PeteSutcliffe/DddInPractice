using System.Collections.Generic;
using DddInPractice.Logic.Common;

namespace DddInPractice.Logic.Atms
{
    public interface IAtmRepository : IRepository<Atm>
    {
        IReadOnlyList<AtmDto> GetAtmList();
    }
}
