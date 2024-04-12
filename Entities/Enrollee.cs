namespace Practice_3.Entities
{
    public class Enrollee
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public List<EnrolleeAchievement> EnrolleeAchievements { get; set; } = [];
        public List<EProgramEnrollee> EProgramEnrollees { get; set; } = [];
        public List<EnrolleeSubject> EnrolleeSubjects { get; set; } = [];
    }
}
