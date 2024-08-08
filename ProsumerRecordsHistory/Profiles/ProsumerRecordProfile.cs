namespace ProsumerRecordsHistory.Profiles
{
    public class ProsumerRecordProfile : AutoMapper.Profile
    {
        public ProsumerRecordProfile()
        {
            CreateMap<Db.ProsumerRecordDbModel, Models.ProsumerRecord>();
        }
    }
}
