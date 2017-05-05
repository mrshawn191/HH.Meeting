using HH.Meeting.Internal.Repositories;
using SimpleInjector;

namespace HH.Meeting
{
    public class SimpleInjectorContainer
    {
        public void Initialize(Container container)
        {
            container.Register<IMeetingRepository, MeetingRepository>(Lifestyle.Scoped);
            container.Register<IVideoRepository, VideoRepository>(Lifestyle.Scoped);
            container.Register<IChatroomRepository, ChatroomRepository>(Lifestyle.Scoped);
            container.Register<ISmsMessageRepository, SmsMessageRepository>(Lifestyle.Scoped);

            container.Verify();
        }
    }
}