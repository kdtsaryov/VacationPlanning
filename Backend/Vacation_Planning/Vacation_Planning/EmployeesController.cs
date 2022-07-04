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
            VacationScheduling.SolveVacationScheduling(temp.Count, temp);
            temp = await _context.Employees.Where(e => e.Position == 3 || e.Position == 4).ToListAsync();
            VacationScheduling.SolveVacationScheduling(temp.Count, temp);
            temp = await _context.Employees.Where(e => e.Position == 5 || e.Position == 6).ToListAsync();
            VacationScheduling.SolveVacationScheduling(temp.Count, temp);
            temp = await _context.Employees.Where(e => e.Position == 7).ToListAsync();
            VacationScheduling.SolveVacationScheduling(temp.Count, temp);
            temp = await _context.Employees.Where(e => e.Position == 8).ToListAsync();
            VacationScheduling.SolveVacationScheduling(temp.Count, temp);
            temp = await _context.Employees.Where(e => e.Position == 9).ToListAsync();
            VacationScheduling.SolveVacationScheduling(temp.Count, temp);
            temp = await _context.Employees.Where(e => e.Position == 10).ToListAsync();
            VacationScheduling.SolveVacationScheduling(temp.Count, temp);
            temp = await _context.Employees.Where(e => e.Position == 11).ToListAsync();
            VacationScheduling.SolveVacationScheduling(temp.Count, temp);
            temp = await _context.Employees.Where(e => e.Position == 12).ToListAsync();
            VacationScheduling.SolveVacationScheduling(temp.Count, temp);

            await _context.SaveChangesAsync();
            return Ok();
        }

        /// <summary>
        /// Возвращает всех сотрудников
        /// </summary>
        // GET: api/Employees
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Employee>>> GetEmployees()
        {
            return await _context.Employees.ToListAsync();
        }

        /// <summary>
        /// Возвращает конкретного сотрудника по ID
        /// </summary>
        /// <param name="id">ID сотрудника</param>
        // GET: api/Employees/1
        [HttpGet("{id}")]
        public async Task<ActionResult<Employee>> GetEmployee(long id)
        {
            // Находим пользователя и возвращаем его, если такой нашелся
            var user = await _context.Employees.FirstOrDefaultAsync(u => u.Id == id);
            if (user == null)
                return NotFound();
            return user;
        }

        /// <summary>
        /// Добавление нового сотрудника
        /// </summary>
        /// <param name="emp">Сотрудник</param>
        // POST: api/Employees
        [HttpPost]
        public async Task<ActionResult<Employee>> PostEmployee(Employee emp)
        {
            var e = await _context.Employees.FindAsync(emp.Id);
            if (e != null)
                return BadRequest();
            _context.Employees.Add(emp);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetEmployee", new { id = emp.Id }, emp);
        }

        /// <summary>
        /// Обновление данных конкретного сотрудника
        /// </summary>
        /// <param name="id">ID сотрудника</param>
        /// <param name="emp">Сотрудник</param>
        // PUT: api/Employees/1
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmployee(long id, Employee emp)
        {
            if (id != emp.Id)
                return BadRequest();
            _context.Entry(emp).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeExists(id))
                    return NotFound();
                else
                    throw;
            }
            return NoContent();
        }

        /// <summary>
        /// Удаление сотрудника
        /// </summary>
        /// <param name="id">ID сотрудника</param>
        // DELETE: api/Employees/1
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(long id)
        {
            var emp = await _context.Employees.FindAsync(id);
            if (emp == null)
                return NotFound();
            _context.Employees.Remove(emp);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        /// <summary>
        /// Удаление всех сотрудников
        /// </summary>
        // DELETE: api/Employees
        [HttpDelete]
        public async Task<IActionResult> DeleteEmployees()
        {
            _context.Database.EnsureDeleted();
            await _context.SaveChangesAsync();
            return NoContent();
        }

        /// <summary>
        /// Проверка наличия сотрудника
        /// </summary>
        /// <param name="id">ID сотрудника</param>
        /// <returns>Существует ли такой сотрудник</returns>
        private bool EmployeeExists(long id)
        {
            return _context.Employees.Any(e => e.Id == id);
        }
    }
}
