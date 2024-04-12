namespace Practice_3.Entities
{
    public class EnrolleeSubject
    {
        public int Id { get; set; }

        public int EnrolleeId { get; set; }
        public Enrollee? Enrollee { get; set; }

        public int SubjectId { get; set; }
        public Subject? Subject { get; set; }

        public int Result { get; set; }
    }
}
