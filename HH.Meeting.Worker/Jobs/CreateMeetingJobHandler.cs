using System.Threading.Tasks;
using Serilog;

namespace HH.Meeting.Worker.Jobs
{
    public class CreateMeetingJobHandler
    {
        private readonly ILogger _logger;

        public CreateMeetingJobHandler(ILogger logger)
        {
            _logger = logger;
        }

        public async Task ProcessQueueMessage()
        {

        }
    }
}