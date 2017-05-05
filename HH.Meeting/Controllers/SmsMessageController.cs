using System.Web.Http;
using HH.Meeting.Internal.Repositories;
using HH.Meeting.Public;
using HH.Meeting.Public.Requests;

namespace HH.Meeting.Controllers
{
    public class SmsMessageController : ApiController
    {
        private readonly ISmsMessageRepository _smsMessageRepository;
        private readonly IServiceBus _serviceBus;

        public SmsMessageController(ISmsMessageRepository smsMessageRepository, IServiceBus serviceBus)
        {
            _smsMessageRepository = smsMessageRepository;
            _serviceBus = serviceBus;
        }

        [HttpGet, Route("api/smsmessages/{id}")]
        public IHttpActionResult GetSmsMessage([FromUri] int id)
        {
            var smsMessage = _smsMessageRepository.GetSmsMessageById(id);

            return Ok(smsMessage);
        }

        [HttpPost, Route("api/smsmessages/{id}")]
        public IHttpActionResult CreateOrUpdateSmsMessage([FromUri] int id, [FromBody] CreateSmsMessageRequest request)
        {
            return Ok();
        }


    }
}