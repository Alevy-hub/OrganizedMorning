using System.Text;

namespace OrganizedMorning.Entities
{
    public class Times
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public TimeSpan? Time { get; set; }
        public int? Order { get; set; }
        public MorningPlan? MorningPlan { get; set; }
        public string? EncodedTitle { get; set; }

		public void EncodeTitle() => EncodedTitle = Title.Replace(" ", "-").ToLower().Normalize(NormalizationForm.FormD).Replace("[^\\p{ASCII}]", "");

	}
}
