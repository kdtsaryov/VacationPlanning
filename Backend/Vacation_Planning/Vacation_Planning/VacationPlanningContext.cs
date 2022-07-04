using Microsoft.EntityFrameworkCore;

namespace Vacation_Planning
{
    /// <summary>
    /// Контекст базы данных
    /// </summary>
    public class VacationPlanningContext : DbContext
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="options">Параметры базы данных</param>
        public VacationPlanningContext(DbContextOptions<VacationPlanningContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        /// <summary>
        /// Создание моделей
        /// </summary>
        /// <param name="builder">"Строитель", связывающий модеди с контекстом БД</param>
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

        /// <summary>
        /// Сотрудники
        /// </summary>
        public DbSet<Employee> Employees { get; set; }
    }
}
