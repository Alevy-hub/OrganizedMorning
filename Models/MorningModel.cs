using OrganizedMorning.Entities;
using System.Text;

namespace OrganizedMorning.Models
{
	public class MorningModel
	{
		public int MorningPlanId { get; set; }
		public string? MorningPlanTitle { get; set; }
		public string? MorningPlanEncodedTitle { get; set; }
		public TimeSpan? MorningPlanBaseTime { get; set; }
		public List<Times>? MorningStages { get; set; }
	}
}
