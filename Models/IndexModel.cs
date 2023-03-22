namespace OrganizedMorning.Models
{
    public class IndexModel
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? BaseTime { get; set; }
        public string[]? Times { get; set; }
        public DateTime? Created { get; set; }
    }
}
