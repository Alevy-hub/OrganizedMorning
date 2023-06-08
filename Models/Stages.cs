using System.ComponentModel.DataAnnotations;

namespace OrganizedMorning.Models
{
    public class Stages
    {
        [Required(ErrorMessage = "Musisz podać tytuł etapu!")]
        public string? StageTitle { get; set; }
        public TimeSpan? StageTime { get; set; }
    }
}
