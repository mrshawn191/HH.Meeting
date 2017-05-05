using System.Threading.Tasks;
using System.Web.Http;
using HH.Meeting.Internal.Repositories;
using HH.Meeting.Public;

namespace HH.Meeting.Controllers
{
    public class MeetingController : ApiController
    {
        private readonly IMeetingRepository _meetingRepository;
        private readonly IServiceBus _serviceBus;

        public MeetingController(IMeetingRepository meetingRepository, IServiceBus serviceBus)
        {
            _meetingRepository = meetingRepository;
            _serviceBus = serviceBus;
        }

        [HttpGet, Route("api/meetings/{id}")]
        public  Task<IHttpActionResult> MeetingById()
        {
            return Ok();
        }
    }
}