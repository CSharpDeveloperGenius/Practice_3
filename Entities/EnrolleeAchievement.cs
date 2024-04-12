namespace Practice_3.Entities
{
    public class EnrolleeAchievement
    {
        public int Id { get; set; }

        public int EnrolleeId { get; set; }
        public Enrollee? Enrollee { get; set; }

        public int AchievementId { get; set; }
        public Achievement? Achievement { get; set; }
    }
}
