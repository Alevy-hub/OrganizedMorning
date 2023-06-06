using System.ComponentModel.DataAnnotations;

namespace OrganizedMorning.Models
{
    public class MorningCreateModel
    {
        [DataType(DataType.Text, ErrorMessage = "Nazwa musi być tekstem!")]
        [Required(ErrorMessage = "Musisz wpisać nazwę planu!")]
        public string MorningPlanTitle { get; set; }

        [Required(ErrorMessage = "Musisz podać godzinę, od której chcesz odliczać!")]
        [DataType(DataType.Time, ErrorMessage = "Musisz podać godzine, od której chcesz odliczać!")]
        public TimeSpan MorningPlanBaseTime { get; set; }
        public List<Stages> MorningStages { get; set; }
    }
}
