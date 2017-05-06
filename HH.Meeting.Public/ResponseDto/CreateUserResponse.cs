using System;

namespace HH.Meeting.Public.ResponseDto
{
    public class CreateUserResponse
    {
        public string Url { get; set; }
        public string Id { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }
        public int Level { get; set; }
        public DateTimeOffset? JoinDate { get; set; }
    }
}