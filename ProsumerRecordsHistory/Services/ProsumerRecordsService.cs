using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using ProsumerRecordsHistory.Interface;
using RecordsHistory.Grpc;
using Serilog;

namespace ProsumerRecordsHistory.Services
{
    public class ProsumerRecordsService : ProsumerRecords.ProsumerRecordsBase
    {
        private readonly IProsumerRecordsRepository _repository;

        public ProsumerRecordsService(IProsumerRecordsRepository repository)
        {
            Log.Information("gRPC object is created");
            _repository = repository;
        }

        public override async Task<GetProsumerRecordByIdResponse> GetProsumerRecord(GetProsumerRecrodByIdRequest request, ServerCallContext context)
        {
            Log.Information("Get prosumer record gRPC is starting");
            var response = new GetProsumerRecordByIdResponse();
            var record = await _repository.GetProsumerRecordAsynch(request.RecordId);
            Log.Information($"Record is received from data base {record.Record}");
            response.Record = new ProsumerRecord
            {
                ID = record.Record.ID,
                PowerGenerated = (float)record.Record.PowerGenerated,
                PowerConsumped = (float)record.Record.PowerConsumped,
                PowerPulled = (float)record.Record.PowerPulled,
                Day = Timestamp.FromDateTimeOffset(record.Record.Day)
            };
            Log.Information($"Record is mapped to the gRPC record {response.Record}");
            return response;
        }
    }
}
