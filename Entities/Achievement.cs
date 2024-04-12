namespace Practice_3.Entities
{
    public class Achievement
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int Bonus { get; set; }

        public List<EnrolleeAchievement> EnrolleeAchievements { get; set; } = [];
    }
}
