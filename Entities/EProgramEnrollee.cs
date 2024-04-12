namespace Practice_3.Entities
{
    public class EProgramEnrollee
    {
        public int Id { get; set; }

        public int EProgramId { get; set; }
        public EProgram? EProgram { get; set; }

        public int EnrolleeId { get; set; }
        public Enrollee? Enrollee { get; set; }
    }
}
