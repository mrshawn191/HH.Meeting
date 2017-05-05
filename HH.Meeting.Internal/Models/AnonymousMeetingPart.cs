using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HH.Meeting.Internal.Models
{
    public class AnonymousMeetingPart
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [ForeignKey("Meeting")]
        public int MeetingId { get; set; }

        [Required]
        public virtual Meeting Meeting { get; set; }

        [Required]
        public virtual AnonymousMeeting AnonymousMeeting { get; set; }

        [Required]
        public bool Visible { get; set; }
    }
}