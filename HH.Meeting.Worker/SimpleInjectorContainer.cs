using HH.Meeting.Internal.Repositories;
using SimpleInjector;

namespace HH.Meeting.Worker
{
    public class SimpleInjectorContainer
    {
        public void Initialize(Container container)
        {
            container.Register<IMeetingRepository, MeetingRepository>(Lifestyle.Scoped);

            container.Verify();
        }
    }
}