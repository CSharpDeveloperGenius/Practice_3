namespace Practice_3.Entities
{
    public class Subject
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public List<EnrolleeSubject> EnrolleeSubjects { get; set; } = [];
        public List<EProgramSubject> EProgramSubjects { get; set; } = [];
    }
}
