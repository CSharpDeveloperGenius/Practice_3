namespace Practice_3.Entities
{
    public class EProgram
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int Plan { get; set; }

        public int DepartmentId { get; set; }
        public Department? Department { get; set; }

        public List<EProgramEnrollee> EProgramEnrollees { get; set; } = [];
        public List<EProgramSubject> EProgramSubjects { get; set; } = [];
    }
}
