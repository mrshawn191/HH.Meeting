using System.Net;
using System.Web.Http;
using HH.Meeting.Internal.Repositories;
using HH.Meeting.Public;
using HH.Meeting.Public.Requests;

namespace HH.Meeting.Controllers
{
    public class VideosController : ApiController
    {
        private readonly IVideoRepository _videoRepository;
        private readonly IServiceBus _serviceBus;

        public VideosController(IVideoRepository videoRepository, IServiceBus serviceBus)
        {
            _videoRepository = videoRepository;
            _serviceBus = serviceBus;
        }

        [HttpGet, Route("api/videos/{id}")]
        public IHttpActionResult GetVideo([FromUri] int id)
        {
            var video = _videoRepository.GetVideoById(id);

            return Ok(video);
        }

        [HttpGet, Route("api/videos/meeting/{meetingId}")]
        public IHttpActionResult GetAllVideosInChatroom([FromUri] int meetingId)
        {
            return Ok();
        }

        [HttpPut, Route("api/videos/{id}")]
        public IHttpActionResult UpdateVideo([FromUri] int id)
        {
            var video = _videoRepository.GetVideoById(id);

            return Ok(video);
        }

        [HttpPost, Route("api/videos/{id}")]
        public IHttpActionResult CreateOrUpdateVideo([FromUri] int id, [FromBody] CreateOrUpdateVideoRequest request)
        {
            return Ok();
        }
    }
}