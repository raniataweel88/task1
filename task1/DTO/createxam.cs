using System.ComponentModel.DataAnnotations;

namespace task1.DTO
{
    public class createxam
    {
        public string Title { get; set; }
        public DateTime Date { get; set; }
        public int Mark { get; set; }
        public int QuestionCount { get; set; }
        public int Duration { get; set; }

        public bool IsPassed { get; set; }
        public string Level { get; set; }
        [Key]
        public int EXAMID { get; set; }
        [Key]
        public int USERID { get; set; }
        [Key]
        public string NationalityId { get; set; }
        public float ACTUALMARK { get; set; }
        public DateTime SUBMITTIME { get; set; }
        public string NOTES { get; set; }

        public bool IsActiveNOTES { get; set; }

    }
}
