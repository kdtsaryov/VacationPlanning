using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Vacation_Planning
{
    /// <summary>
    /// Контроллер сотрудников
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        /// <summary>
        /// Контекст базы данных
        /// </summary>
        private readonly VacationPlanningContext _context;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="context">Контекст базы данных</param>
        public EmployeesController(VacationPlanningContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Генерация отпусков
        /// </summary>
        // GET: api/Employees/generate
        [HttpGet("generate")]
        public async Task<ActionResult<Employee>> GenerateVacations()
        {
            List<Employee> temp = await _context.Employees.Where(e => e.Position == 1 || e.Position == 2).ToListAsync();
            List<Employee> res = VacationScheduling.SolveVacationScheduling(temp.Count, temp);
            foreach (Employee e in res)
            {
                var employee = await _context.Employees.FirstOrDefaultAsync(emp => emp.Id == e.Id);
                employee.VacationDate = e.VacationDate;
            }
            await _context.SaveChangesAsync();
            temp = await _context.Employees.Where(e => e.Position == 3 || e.Position == 4).ToListAsync();
            res = VacationScheduling.SolveVacationScheduling(temp.Count, temp);
            foreach (Employee e in res)
            {
                var employee = await _context.Employees.FirstOrDefaultAsync(emp => emp.Id == e.Id);
                employee.VacationDate = e.VacationDate;
            }
            await _context.SaveChangesAsync();
            temp = await _context.Employees.Where(e => e.Position == 5 || e.Position == 6).ToListAsync();
            res = VacationScheduling.SolveVacationScheduling(temp.Count, temp);
            foreach (Employee e in res)
            {
                var employee = await _context.Employees.FirstOrDefaultAsync(emp => emp.Id == e.Id);
                employee.VacationDate = e.VacationDate;
            }
            await _context.SaveChangesAsync();
            temp = await _context.Employees.Where(e => e.Position == 7).ToListAsync();
            res = VacationScheduling.SolveVacationScheduling(temp.Count, temp);
            foreach (Employee e in res)
            {
                var employee = await _context.Employees.FirstOrDefaultAsync(emp => emp.Id == e.Id);
                employee.VacationDate = e.VacationDate;
            }
            await _context.SaveChangesAsync();
            temp = await _context.Employees.Where(e => e.Position == 8).ToListAsync();
            res = VacationScheduling.SolveVacationScheduling(temp.Count, temp);
            foreach (Employee e in res)
            {
                var employee = await _context.Employees.FirstOrDefaultAsync(emp => emp.Id == e.Id);
                employee.VacationDate = e.VacationDate;
            }
            await _context.SaveChangesAsync();
            temp = await _context.Employees.Where(e => e.Position == 9).ToListAsync();
            res = VacationScheduling.SolveVacationScheduling(temp.Count, temp);
            foreach (Employee e in res)
            {
                var employee = await _context.Employees.FirstOrDefaultAsync(emp => emp.Id == e.Id);
                employee.VacationDate = e.VacationDate;
            }
            await _context.SaveChangesAsync();
            temp = await _context.Employees.Where(e => e.Position == 10).ToListAsync();
            res = VacationScheduling.SolveVacationScheduling(temp.Count, temp);
            foreach (Employee e in res)
            {
                var employee = await _context.Employees.FirstOrDefaultAsync(emp => emp.Id == e.Id);
                employee.VacationDate = e.VacationDate;
            }
            await _context.SaveChangesAsync();
            temp = await _context.Employees.Where(e => e.Position == 11).ToListAsync();
            res = VacationScheduling.SolveVacationScheduling(temp.Count, temp);
            foreach (Employee e in res)
            {
                var employee = await _context.Employees.FirstOrDefaultAsync(emp => emp.Id == e.Id);
                employee.VacationDate = e.VacationDate;
            }
            await _context.SaveChangesAsync();
            temp = await _context.Employees.Where(e => e.Position == 12).ToListAsync();
            res = VacationScheduling.SolveVacationScheduling(temp.Count, temp);
            foreach (Employee e in res)
            {
                var employee = await _context.Employees.FirstOrDefaultAsync(emp => emp.Id == e.Id);
                employee.VacationDate = e.VacationDate;
            }
            await _context.SaveChangesAsync();
            return Ok();
        }

        /// <summary>
        /// Возвращает всех сотрудников
        /// </summary>
        // GET: api/Employees
        [HttpGet]
        public IEnumerable<Employee> GetEmployees()
        {
            return _context.Employees.ToList();
        }

        /// <summary>
        /// Возвращает конкретного сотрудника по ID
        /// </summary>
        /// <param name="id">ID сотрудника</param>
        // GET: api/Employees/1
        [HttpGet("{id}")]
        public Employee GetEmployee(long id)
        {
            // Находим сотрудника и возвращаем его, если такой нашелся
            var emp = _context.Employees.FirstOrDefault(u => u.Id == id);
            return emp;
        }

        /// <summary>
        /// Добавление нового сотрудника
        /// </summary>
        /// <param name="emp">Сотрудник</param>
        // POST: api/Employees
        [HttpPost]
        public IActionResult PostEmployee(Employee emp)
        {
            if (ModelState.IsValid)
            {
                _context.Employees.Add(emp);
                _context.SaveChanges();
                return Ok(emp);
            }
            return BadRequest(ModelState);
        }

        /// <summary>
        /// Обновление данных конкретного сотрудника
        /// </summary>
        /// <param name="id">ID сотрудника</param>
        /// <param name="emp">Сотрудник</param>
        // PUT: api/Employees/1
        [HttpPut("{id}")]
        public IActionResult PutEmployee(long id, Employee emp)
        {
            if (ModelState.IsValid)
            {
                _context.Update(emp);
                _context.SaveChanges();
                return Ok(emp);
            }
            return BadRequest(ModelState);
        }

        /// <summary>
        /// Удаление сотрудника
        /// </summary>
        /// <param name="id">ID сотрудника</param>
        // DELETE: api/Employees/1
        [HttpDelete("{id}")]
        public IActionResult DeleteEmployee(long id)
        {
            Employee emp = _context.Employees.FirstOrDefault(x => x.Id == id);
            if (emp != null)
            {
                _context.Employees.Remove(emp);
                _context.SaveChanges();
            }
            return Ok(emp);
        }
    }
}
