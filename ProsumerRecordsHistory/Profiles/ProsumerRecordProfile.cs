namespace ProsumerRecordsHistory.Profiles
{
    public class ProsumerRecordProfile : AutoMapper.Profile
    {
        public ProsumerRecordProfile()
        {
            CreateMap<Db.ProsumerRecordDbModel, Models.ProsumerRecord>();
            CreateMap<Models.ProsumerRecord, Db.ProsumerRecordDbModel>().ReverseMap();
        }
    }
}
