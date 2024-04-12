using Microsoft.EntityFrameworkCore;
using Practice_3.Entities;

namespace Practice_3
{
    public class ApplicationDbContext : DbContext
    {
        public string ConnectionString { get; set; }

        public DbSet<Achievement> Achievements { get; set; } = null!;
        public DbSet<Department> Departments { get; set; } = null!;
        public DbSet<Enrollee> Enrollees { get; set;} = null!;
        public DbSet<EnrolleeAchievement> EnrolleeAchievements { get; set; } = null!;
        public DbSet<EnrolleeSubject> EnrolleeSubjects { get; set; } = null!;
        public DbSet<EProgram> EPrograms { get; set; } = null!;
        public DbSet<EProgramEnrollee> EProgramEnrollees { get; set; } = null!;
        public DbSet<EProgramSubject> EProgramSubjects { get; set; } = null!;
        public DbSet<Subject> Subjects { get; set; } = null!;

        public ApplicationDbContext(string connectionString)
        {
            ConnectionString = connectionString;
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(ConnectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Department>().HasData(
                new Department { Id = 1, Name = "ИПТИП" },
                new Department { Id = 2, Name = "ИКБ" },
                new Department { Id = 3, Name = "ИИТ" },
                new Department { Id = 4, Name = "ИИИ" }
            );

            modelBuilder.Entity<Subject>().HasData(
                new Subject { Id = 1, Name = "Математика" },
                new Subject { Id = 2, Name = "Информатика" },
                new Subject { Id = 3, Name = "Русский язык" },
                new Subject { Id = 4, Name = "Физика" },
                new Subject { Id = 5, Name = "Обществознание" }
            );

            modelBuilder.Entity<Enrollee>().HasData(
                new Enrollee { Id = 1, Name = "Тер-Сааков Глеб Олегович" },    
                new Enrollee { Id = 2, Name = "Тер-Сааков Герман Олегович" },    
                new Enrollee { Id = 3, Name = "Булискерия Будимир Бакуриевич" },    
                new Enrollee { Id = 4, Name = "Жидков Георгий Сергеевич" },    
                new Enrollee { Id = 5, Name = "Малов Илья Максимович" },    
                new Enrollee { Id = 6, Name = "Андреев Владислав Андреевич" },    
                new Enrollee { Id = 7, Name = "Степанов Илья Владимирович" },    
                new Enrollee { Id = 8, Name = "Панфилова Мария Серегевна" }    
            );

            modelBuilder.Entity<Achievement>().HasData(
                new Achievement { Id = 1, Name = "Золотая медаль", Bonus = 10 },
                new Achievement { Id = 2, Name = "ГТО", Bonus = 5 },
                new Achievement { Id = 3, Name = "Олимпиады", Bonus = 7 },
                new Achievement { Id = 4, Name = "Предпрофессиональный экзамен", Bonus = 5 },
                new Achievement { Id = 5, Name = "МС по настольному теннису", Bonus = 10 }
            );

            modelBuilder.Entity<EProgram>().HasData(
                new EProgram { Id = 1, Name = "Информационные системы и технологии", Plan = 2, DepartmentId = 1 },
                new EProgram { Id = 2, Name = "Информационно-аналитические системы безопасности", Plan = 2, DepartmentId = 2 },
                new EProgram { Id = 3, Name = "Программная инженерия", Plan = 2, DepartmentId = 3 },
                new EProgram { Id = 4, Name = "Компьютерная безопасность", Plan = 3, DepartmentId = 4 },
                new EProgram { Id = 5, Name = "Дизайн", Plan = 3, DepartmentId = 1 },
                new EProgram { Id = 6, Name = "Информационная безопасность", Plan = 2, DepartmentId = 2 },
                new EProgram { Id = 7, Name = "Прикладная информатика", Plan = 3, DepartmentId = 3 },
                new EProgram { Id = 8, Name = "Информатика и вычислительня техника", Plan = 3, DepartmentId = 4 }
            );

            modelBuilder.Entity<EnrolleeAchievement>().HasData(
                new EnrolleeAchievement { Id = 1, EnrolleeId = 1, AchievementId = 1 },
                new EnrolleeAchievement { Id = 2, EnrolleeId = 1, AchievementId = 2 },
                new EnrolleeAchievement { Id = 3, EnrolleeId = 1, AchievementId = 3 },
                new EnrolleeAchievement { Id = 4, EnrolleeId = 3, AchievementId = 2 },
                new EnrolleeAchievement { Id = 5, EnrolleeId = 4, AchievementId = 1 },
                new EnrolleeAchievement { Id = 6, EnrolleeId = 4, AchievementId = 3 },
                new EnrolleeAchievement { Id = 7, EnrolleeId = 4, AchievementId = 4 },
                new EnrolleeAchievement { Id = 8, EnrolleeId = 4, AchievementId = 5 },
                new EnrolleeAchievement { Id = 9, EnrolleeId = 5, AchievementId = 2 },
                new EnrolleeAchievement { Id = 10, EnrolleeId = 5, AchievementId = 3 },
                new EnrolleeAchievement { Id = 11, EnrolleeId = 6, AchievementId = 2 },
                new EnrolleeAchievement { Id = 12, EnrolleeId = 7, AchievementId = 4 },
                new EnrolleeAchievement { Id = 13, EnrolleeId = 8, AchievementId = 5 }
            );

            modelBuilder.Entity<EnrolleeSubject>().HasData(
                new EnrolleeSubject { Id = 1, EnrolleeId = 1, SubjectId = 1, Result = 100 },
                new EnrolleeSubject { Id = 2, EnrolleeId = 1, SubjectId = 3, Result = 100 },
                new EnrolleeSubject { Id = 3, EnrolleeId = 1, SubjectId = 2, Result = 99 },
                new EnrolleeSubject { Id = 4, EnrolleeId = 2, SubjectId = 1, Result = 76 },
                new EnrolleeSubject { Id = 5, EnrolleeId = 2, SubjectId = 3, Result = 54 },
                new EnrolleeSubject { Id = 6, EnrolleeId = 2, SubjectId = 4, Result = 96 },
                new EnrolleeSubject { Id = 7, EnrolleeId = 3, SubjectId = 1, Result = 77 },
                new EnrolleeSubject { Id = 8, EnrolleeId = 3, SubjectId = 3, Result = 89 },
                new EnrolleeSubject { Id = 9, EnrolleeId = 3, SubjectId = 2, Result = 75 },
                new EnrolleeSubject { Id = 10, EnrolleeId = 4, SubjectId = 1, Result = 100 },
                new EnrolleeSubject { Id = 11, EnrolleeId = 4, SubjectId = 3, Result = 100 },
                new EnrolleeSubject { Id = 12, EnrolleeId = 4, SubjectId = 2, Result = 100 },
                new EnrolleeSubject { Id = 13, EnrolleeId = 5, SubjectId = 1, Result = 97 },
                new EnrolleeSubject { Id = 14, EnrolleeId = 5, SubjectId = 3, Result = 98 },
                new EnrolleeSubject { Id = 15, EnrolleeId = 5, SubjectId = 4, Result = 96 },
                new EnrolleeSubject { Id = 16, EnrolleeId = 6, SubjectId = 1, Result = 23 },
                new EnrolleeSubject { Id = 17, EnrolleeId = 6, SubjectId = 3, Result = 21 },
                new EnrolleeSubject { Id = 18, EnrolleeId = 6, SubjectId = 5, Result = 5 },
                new EnrolleeSubject { Id = 19, EnrolleeId = 7, SubjectId = 1, Result = 65 },
                new EnrolleeSubject { Id = 20, EnrolleeId = 7, SubjectId = 3, Result = 54 },
                new EnrolleeSubject { Id = 21, EnrolleeId = 7, SubjectId = 4, Result = 76 },
                new EnrolleeSubject { Id = 22, EnrolleeId = 8, SubjectId = 1, Result = 100 },
                new EnrolleeSubject { Id = 23, EnrolleeId = 8, SubjectId = 3, Result = 100 },
                new EnrolleeSubject { Id = 24, EnrolleeId = 8, SubjectId = 2, Result = 100 }
            );

            modelBuilder.Entity<EProgramEnrollee>().HasData(
                new EProgramEnrollee { Id = 1, EnrolleeId = 1, EProgramId = 1 },
                new EProgramEnrollee { Id = 2, EnrolleeId = 1, EProgramId = 2 },
                new EProgramEnrollee { Id = 3, EnrolleeId = 1, EProgramId = 3 },
                new EProgramEnrollee { Id = 4, EnrolleeId = 2, EProgramId = 4 },
                new EProgramEnrollee { Id = 5, EnrolleeId = 2, EProgramId = 5 },
                new EProgramEnrollee { Id = 6, EnrolleeId = 2, EProgramId = 6 },
                new EProgramEnrollee { Id = 7, EnrolleeId = 3, EProgramId = 7 },
                new EProgramEnrollee { Id = 8, EnrolleeId = 3, EProgramId = 8 },
                new EProgramEnrollee { Id = 9, EnrolleeId = 3, EProgramId = 1 },
                new EProgramEnrollee { Id = 10, EnrolleeId = 4, EProgramId = 2 },
                new EProgramEnrollee { Id = 11, EnrolleeId = 4, EProgramId = 3 },
                new EProgramEnrollee { Id = 12, EnrolleeId = 4, EProgramId = 4 },
                new EProgramEnrollee { Id = 13, EnrolleeId = 5, EProgramId = 5 },
                new EProgramEnrollee { Id = 14, EnrolleeId = 5, EProgramId = 6 },
                new EProgramEnrollee { Id = 15, EnrolleeId = 5, EProgramId = 7 },
                new EProgramEnrollee { Id = 16, EnrolleeId = 6, EProgramId = 8 },
                new EProgramEnrollee { Id = 17, EnrolleeId = 6, EProgramId = 1 },
                new EProgramEnrollee { Id = 18, EnrolleeId = 6, EProgramId = 2 },
                new EProgramEnrollee { Id = 19, EnrolleeId = 7, EProgramId = 3 },
                new EProgramEnrollee { Id = 20, EnrolleeId = 7, EProgramId = 4 },
                new EProgramEnrollee { Id = 21, EnrolleeId = 7, EProgramId = 5 },
                new EProgramEnrollee { Id = 22, EnrolleeId = 8, EProgramId = 2 },
                new EProgramEnrollee { Id = 23, EnrolleeId = 8, EProgramId = 7 },
                new EProgramEnrollee { Id = 24, EnrolleeId = 8, EProgramId = 8 }
            );

            modelBuilder.Entity<EProgramSubject>().HasData(
                new EProgramSubject { Id = 1, EProgramId = 1, SubjectId = 1, Min_Result = 65 },
                new EProgramSubject { Id = 2, EProgramId = 1, SubjectId = 2, Min_Result = 67 },
                new EProgramSubject { Id = 3, EProgramId = 1, SubjectId = 3, Min_Result = 63 },
                new EProgramSubject { Id = 4, EProgramId = 2, SubjectId = 4, Min_Result = 56 },
                new EProgramSubject { Id = 5, EProgramId = 2, SubjectId = 5, Min_Result = 65 },
                new EProgramSubject { Id = 6, EProgramId = 2, SubjectId = 1, Min_Result = 76 },
                new EProgramSubject { Id = 7, EProgramId = 3, SubjectId = 2, Min_Result = 43 },
                new EProgramSubject { Id = 8, EProgramId = 3, SubjectId = 3, Min_Result = 54 },
                new EProgramSubject { Id = 9, EProgramId = 3, SubjectId = 4, Min_Result = 32 },
                new EProgramSubject { Id = 10, EProgramId = 4, SubjectId = 5, Min_Result = 55 },
                new EProgramSubject { Id = 11, EProgramId = 4, SubjectId = 1, Min_Result = 56 },
                new EProgramSubject { Id = 12, EProgramId = 4, SubjectId = 2, Min_Result = 70 },
                new EProgramSubject { Id = 13, EProgramId = 5, SubjectId = 3, Min_Result = 57 },
                new EProgramSubject { Id = 14, EProgramId = 5, SubjectId = 4, Min_Result = 80 },
                new EProgramSubject { Id = 15, EProgramId = 5, SubjectId = 5, Min_Result = 56 },
                new EProgramSubject { Id = 16, EProgramId = 6, SubjectId = 1, Min_Result = 55 },
                new EProgramSubject { Id = 17, EProgramId = 6, SubjectId = 2, Min_Result = 67 },
                new EProgramSubject { Id = 18, EProgramId = 6, SubjectId = 3, Min_Result = 87 },
                new EProgramSubject { Id = 19, EProgramId = 7, SubjectId = 4, Min_Result = 43 },
                new EProgramSubject { Id = 20, EProgramId = 7, SubjectId = 5, Min_Result = 42 },
                new EProgramSubject { Id = 21, EProgramId = 7, SubjectId = 1, Min_Result = 41 },
                new EProgramSubject { Id = 22, EProgramId = 8, SubjectId = 2, Min_Result = 65 },
                new EProgramSubject { Id = 23, EProgramId = 8, SubjectId = 3, Min_Result = 76 },
                new EProgramSubject { Id = 24, EProgramId = 8, SubjectId = 4, Min_Result = 39 }
            );            
        }
    }
}
