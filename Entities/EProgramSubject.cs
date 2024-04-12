namespace Practice_3.Entities
{
    public class EProgramSubject
    {
        public int Id { get; set; }

        public int EProgramId { get; set; }
        public EProgram? EProgram { get; set; }

        public int SubjectId { get; set; }
        public Subject? Subject { get; set; }

        public int Min_Result { get; set; }
    }
}
