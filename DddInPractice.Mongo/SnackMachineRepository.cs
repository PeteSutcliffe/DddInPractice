using System.Collections.Generic;
using System.Linq;
using DddInPractice.Logic.SharedKernel;
using DddInPractice.Logic.SnackMachines;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;

namespace DddInPractice.Mongo
{
    public class SnackMachineRepository : Repository<SnackMachine>, ISnackMachineRepository
    {
        static SnackMachineRepository()
        {
            BsonClassMap.RegisterClassMap<SnackMachine>(sm =>
            {
                sm.MapProperty("MoneyInside");
                sm.MapProperty("MoneyInTransaction");
                sm.MapProperty("Slots");
            });

            BsonClassMap.RegisterClassMap<Slot>(s =>
            {
                s.AutoMap();
                s.MapProperty("Position");
            });

            BsonClassMap.RegisterClassMap<SnackPile>(s =>
            {
                s.MapProperty("Snack");
                s.MapProperty("Quantity");
                s.MapProperty("Price");
            });

            BsonClassMap.RegisterClassMap<Snack>(s =>
            {
                s.MapProperty("Name");
            });
        }

        public IReadOnlyList<SnackMachineDto> GetSnackMachineList()
        {
            var snackMachines = Collection
                .FindSync(FilterDefinition<SnackMachine>.Empty)
                .ToList();

            return snackMachines
                .Select(x => new SnackMachineDto(x.Id, x.MoneyInside.Amount))
                .ToList();
        }
    }
}