namespace OrganizedMorning.Entities
{
    public class Times
    {
        public int Id { get; set; }
        public int Time { get; set; }
        public int Order { get; set; }
        public MorningPlan MorningPlan { get; set; }
    }
}
