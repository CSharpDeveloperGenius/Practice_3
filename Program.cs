using Microsoft.EntityFrameworkCore;

namespace Practice_3
{
    sealed class Program
    {
        private const string CONNECTION_STRING = "Host=localhost;Port=5432;Database=practice3;Username=postgres;Password=Gleb4ik17072005;";


        static void Main(string[] args)
        {
            using var dbContext = new ApplicationDbContext(CONNECTION_STRING);
            var flag = true;

            while (flag)
            {

                Console.WriteLine("Выберите запрос:" +
                    "\n\t1. Вывести абитуриентов, которые хотят поступать на определенную образовательную программу." +
                    "\n\t2. Вывести образовательные программы, на которые для поступления необходим определенный предмет ЕГЭ." +
                    "\n\t3. Вывести статистическую информацию по каждому предмету ЕГЭ (минимальный и максимальный балл, количество абитуриентов, которые этот предмет сдавали)." +
                    "\n\t4. Вывести образовательные программы, минимальные баллы по каждому предмету которых, превышают заданное значение." +
                    "\n\t5. Вывести образовательные программы. которые имеют самый большой план набора." +
                    "\n\t6. Посчитать, сколько дополнительных баллов получит каждый абитуриент." +
                    "\n\t7. Посчитать конкурс на каждую образовательную программу." +
                    "\n\t8. Вывести образовательные программы, на которые для поступления необходимы два определенных предмета ЕГЭ." +
                    "\n\t9. Посчитать количество баллов каждого абитуриента на каждую образовательную программу по результатам ЕГЭ." +
                    "\n\t10. Вывести абитуриентов, которые не могут быть зачислены на образовательную программу.");

                Console.Write("\nЗапрос: ");

                if (!int.TryParse(Console.ReadLine(), out int query))
                {
                    Console.Clear();
                    Console.WriteLine("Выбрано неверное действие. Попробуйте еще раз.\n\n");
                    continue;
                }

                switch (query)
                {
                    case 1:
                        Task_1(dbContext);
                        break;
                    case 2:
                        Task_2(dbContext);
                        break;
                    case 3:
                        Task_3(dbContext);
                        break;
                    case 4:
                        Task_4(dbContext);
                        break;
                    case 5:
                        Task_5(dbContext);
                        break;
                    case 6:
                        Task_6(dbContext);
                        break;
                    case 7:
                        Task_7(dbContext);
                        break;
                    case 8:
                        Task_8(dbContext);
                        break;
                    case 9:
                        Task_9(dbContext);
                        break;
                    case 10:
                        Task_10(dbContext);
                        break;
                    default:
                        Console.Clear();
                        Console.WriteLine("Выбрано неверное действие. Попробуйте еще раз.\n\n");
                        continue;
                }

                flag = false;
            }
        }

        static void Task_1(ApplicationDbContext dbContext)
        {
            Console.WriteLine("\nВыберите образовательную программу: ");

            var programs = dbContext.EPrograms.AsNoTracking().ToList();

            foreach (var prog in programs)
                Console.WriteLine($"{prog.Id}. {prog.Name}");

            Console.Write("\nПрограмма (№): ");

            if (!int.TryParse(Console.ReadLine(), out int chosen)
                || !programs.Select(p => p.Id).Contains(chosen))
            {
                Console.WriteLine("Выбрана несуществующая программа");
                return;
            }

            var program = programs.FirstOrDefault(p => p.Id == chosen)!;

            Console.WriteLine($"Абитуриенты, которые хотят поступить на программу '{program.Name}':");

            var programEnrollees = dbContext.EProgramEnrollees
                .AsNoTracking()
                .Where(pe => pe.EProgramId == program.Id)
                .Include(pe => pe.Enrollee)
                .ToList();

            foreach(var enrollee in programEnrollees)
                Console.WriteLine($"{enrollee.EnrolleeId}. {enrollee.Enrollee!.Name}");
        }
    
        static void Task_2(ApplicationDbContext dbContext)
        {
            Console.WriteLine("\nВыберите предмет ЕГЭ: ");

            var subjects = dbContext.Subjects.AsNoTracking().ToList();

            foreach (var subj in subjects)
                Console.WriteLine($"{subj.Id}. {subj.Name}");

            Console.Write("\nПредмет (№): ");

            if (!int.TryParse(Console.ReadLine(), out int chosen)
                || !subjects.Select(s => s.Id).Contains(chosen))
            {
                Console.WriteLine("Выбран несуществующий предмет");
                return;
            }

            var subject = subjects.FirstOrDefault(s =>  s.Id == chosen)!;

            Console.WriteLine($"Образовательные программы, на которые для поступления необходим предмет '{subject.Name}'");

            var programSubjects = dbContext.EProgramSubjects
                .AsNoTracking()
                .Where(ps => ps.SubjectId == subject.Id)
                .Include(ps => ps.EProgram)
                .ToList();

            foreach(var program in programSubjects)
                Console.WriteLine($"{program.EProgramId}. {program.EProgram!.Name}");
        }

        static void Task_3(ApplicationDbContext dbContext)
        {
            var results = dbContext.EnrolleeSubjects
                .AsNoTracking()
                .GroupBy(es => es.Subject!.Name)
                .Select(g => new
                {
                    Name = g.Key,
                    MaxResult = g.Max(x => x.Result),
                    MinResult = g.Min(x => x.Result),
                    Count = g.Count()
                })
                .ToList();

            Console.WriteLine("\nСтатистика по предметам:");

            foreach(var result in results)
                Console.WriteLine($"{result.Name}: макс. {result.MaxResult}, мин. {result.MinResult}, всего {result.Count}");
        }

        static void Task_4(ApplicationDbContext dbContext)
        {
            Console.WriteLine("Введите минимальное значение (0..100): ");

            if (!int.TryParse(Console.ReadLine()!, out int value)
                || value < 0 || value > 100)
            {
                Console.WriteLine("Введено неверное значение");
                return;
            }

            var programs = dbContext.EProgramSubjects
                .AsNoTracking()
                .GroupBy(ep => ep.EProgram!.Name)
                .Where(g => g.Min(p => p.Min_Result) > value)
                .Select(g => g.Key)
                .ToList();

            Console.WriteLine($"\nОбразовательные программы, минимальные баллы по каждому предмету которых превышают {value}:");

            foreach(var program in programs)
                Console.WriteLine($"{program}");
        }
    
        static void Task_5(ApplicationDbContext dbContext)
        {
            var maxPlan = dbContext.EPrograms.Max(p => p.Plan);

            var programs = dbContext.EPrograms
                .AsNoTracking()
                .Where(p => p.Plan == maxPlan)
                .ToList();

            Console.WriteLine("Программы с самым большим планом набора:");

            foreach (var program in programs)
                Console.WriteLine($"{program.Id}. {program.Name} (план: {program.Plan})");
        }

        static void Task_6(ApplicationDbContext dbContext)
        {
            var bonuses = dbContext.EnrolleeAchievements
                .AsNoTracking()
                .GroupBy(e => e.Enrollee!.Name)
                .Select(g => new
                {
                    Name = g.Key,
                    Bonus = g.Sum(e => e.Achievement!.Bonus)
                })
                .ToList();

            Console.WriteLine("\nДополнительные баллы абитуриентов:");

            foreach (var bonus in bonuses)
                Console.WriteLine($"{bonus.Name}: {bonus.Bonus} доп. баллов");
        }
    
        static void Task_7(ApplicationDbContext dbContext)
        {
            var competitions = dbContext.EPrograms
                .AsNoTracking()
                .Select(p => new
                {
                    p.Name,
                    Enrollees = dbContext.EProgramEnrollees.Count(pe => pe.EProgramId == p.Id),
                    p.Plan
                })
                .ToList();

            Console.WriteLine("\nКонкурс на образовательные программы: ");

            foreach (var competition in competitions)
                Console.WriteLine($"{competition.Name}: {competition.Enrollees} абитуриентов на {competition.Plan} мест");
        }
    
        static void Task_8(ApplicationDbContext dbContext)
        {
            var subjects = dbContext.Subjects.AsNoTracking().ToList();

            Console.WriteLine("\nВыберите два предмета (№ через пробел):");
            foreach (var subject in subjects)
                Console.WriteLine($"{subject.Id}. {subject.Name}");

            var input = Console.ReadLine()!.Split();

            if (!(input.Length == 2 && int.TryParse(input[0], out int subj1) && int.TryParse(input[1], out int subj2)))
            {
                Console.WriteLine("Введены несуществующие предметы");
                return;
            }

            if (subj1 == subj2)
            {
                Console.WriteLine("Необходимо ввести два разных предмета");
                return;
            }

            var programs = dbContext.EProgramSubjects
                .AsNoTracking()
                .Where(ps => ps.SubjectId == subj1 || ps.SubjectId == subj2)
                .GroupBy(ps => new { ps.EProgramId, ps.EProgram!.Name })
                .Where(g => g.Count() == 2)
                .Select(g => new
                {
                    g.Key.EProgramId,
                    g.Key.Name
                })
                .ToList();

            Console.WriteLine($"\nПрограммы, для поступления на которые необходимы " +
                    $"{subjects.FirstOrDefault(s => s.Id == subj1)!.Name} и {subjects.FirstOrDefault(s => s.Id == subj2)!.Name}: ");
            foreach (var program in programs)
                Console.WriteLine($"{program.EProgramId}. {program.Name}");
        }
    
        static void Task_9(ApplicationDbContext dbContext)
        {
            var scores = dbContext.EnrolleeSubjects
                .AsNoTracking()
                .Join(dbContext.EProgramSubjects,
                      es => es.SubjectId,
                      eps => eps.SubjectId,
                      (es, eps) => new { es.EnrolleeId, eps.EProgramId, es.Result })
                .Join(dbContext.EProgramEnrollees,
                      r => new { r.EnrolleeId, r.EProgramId },
                      ep => new { ep.EnrolleeId, ep.EProgramId },
                      (r, ep) => new { r.EnrolleeId, r.EProgramId, r.Result })
                .Join(dbContext.Enrollees,
                      r => r.EnrolleeId,
                      e => e.Id,
                      (r, e) => new { r.EnrolleeId, r.EProgramId, EnrolleeName = e.Name, r.Result })
                .Join(dbContext.EPrograms,
                      r => r.EProgramId,
                      p => p.Id,
                      (r, p) => new { r.EnrolleeId, r.EnrolleeName, r.EProgramId, EProgramName = p.Name, r.Result })
                .GroupBy(r => new { r.EnrolleeId, r.EnrolleeName, r.EProgramId, r.EProgramName })
                .Select(g => new
                {
                    g.Key.EnrolleeId,
                    g.Key.EProgramName,
                    TotalScore = g.Sum(r => r.Result)
                })
                .ToList();

            var enrollees = dbContext.Enrollees.AsNoTracking().ToList();

            Console.WriteLine("\nРезультаты:");
            foreach (var enrollee in enrollees)
            {
                Console.WriteLine($"\nАбитуриент {enrollee.Name}: ");
                foreach (var score in scores.Where(s => s.EnrolleeId == enrollee.Id))
                    Console.WriteLine($"\tНаправление {score.EProgramName} - {score.TotalScore} баллов");
            }
        }
    
        static void Task_10(ApplicationDbContext dbContext)
        {
            var enrollees = dbContext.EnrolleeSubjects
                .Join(dbContext.Enrollees,
                      es => es.EnrolleeId,
                      e => e.Id,
                      (es, e) => new { es.EnrolleeId, EnrolleeName = e.Name, SubjectResult = es.Result })
                .Join(dbContext.EProgramSubjects,
                      es => es.EnrolleeId,
                      eps => eps.EProgramId,
                      (es, eps) => new { es.EnrolleeId, es.EnrolleeName, es.SubjectResult, MinResult = eps.Min_Result, eps.EProgramId })
                .Join(dbContext.EPrograms,
                      eps => eps.EProgramId,
                      p => p.Id,
                      (eps, p) => new { eps.EnrolleeId, eps.EnrolleeName, eps.SubjectResult, eps.MinResult, eps.EProgramId, EProgramName = p.Name })
                .GroupBy(r => new { r.EnrolleeId, r.EnrolleeName })
                .Where(g => g.All(r => r.SubjectResult >= r.MinResult))
                .Select(g => new
                {
                    g.Key.EnrolleeId,
                    g.Key.EnrolleeName,
                    g.First().EProgramId,
                    g.First().EProgramName
                })
                .ToList();

            foreach(var enrollee in enrollees)
                Console.WriteLine($"Абитуриент {enrollee.EnrolleeName} не может быть зачислен на {enrollee.EProgramName}");
        }
    }
}