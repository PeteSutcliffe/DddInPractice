using System.Collections.Generic;
using DddInPractice.Logic.Common;

namespace DddInPractice.Logic.SnackMachines
{
    public interface ISnackMachineRepository : IRepository<SnackMachine>
    {
        IReadOnlyList<SnackMachineDto> GetSnackMachineList();
    }
}
