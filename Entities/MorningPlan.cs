namespace OrganizedMorning.Entities
{
    public class MorningPlan
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? BaseTime { get; set; }
        public DateTime? Created { get; set; }
    }
}
