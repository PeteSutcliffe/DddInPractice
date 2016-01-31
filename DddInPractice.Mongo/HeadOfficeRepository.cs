using DddInPractice.Logic.Management;
using MongoDB.Bson.Serialization;

namespace DddInPractice.Mongo
{
    public class HeadOfficeRepository : Repository<HeadOffice>, IHeadOfficeRepository
    {
        static HeadOfficeRepository()
        {
            BsonClassMap.RegisterClassMap<HeadOffice>(sm =>
            {
                sm.AutoMap();
            });
        }
    }
}