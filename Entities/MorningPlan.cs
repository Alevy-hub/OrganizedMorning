using Microsoft.AspNetCore.Http.HttpResults;
using System.Text;

namespace OrganizedMorning.Entities
{
    public class MorningPlan
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public TimeSpan? BaseTime { get; set; }
        public DateTime? Created { get; set; }
        public string? EncodedTitle { get; set; }
        public string? UserId { get; set; }

        public void EncodeTitle() => EncodedTitle = Title.Replace(" ", "-").ToLower().Normalize(NormalizationForm.FormD).Replace("[^\\p{ASCII}]", "");
        public MorningPlan()
        {
            Created = DateTime.Now;
		}

    }
}
