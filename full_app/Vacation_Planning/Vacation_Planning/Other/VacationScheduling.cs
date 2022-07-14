using Google.OrTools.Sat;
using System.Collections.Generic;
using System.Linq;

namespace Vacation_Planning
{
    public class VacationScheduling
    {
        /// <summary>
        /// Генерация расписания отпусков
        /// </summary>
        /// <param name="numEmployees">Количество сотрудников</param>
        /// <param name="Employees">Сотрудники</param>
        public static List<Employee> SolveVacationScheduling(int numEmployees, List<Employee> Employees)
        {
            if (numEmployees == 1)
            {
                Employees.ElementAt(0).VacationDate = "У сотрудника нет замены!";
                return Employees;
            }

            int numDays = 365;

            var model = new CpModel();

            IntVar[,] vacations = new IntVar[numEmployees, numDays];

            foreach (int e in Range(numEmployees))
            {
                foreach (int d in Range(numDays))
                {
                    vacations[e, d] = model.NewBoolVar($"schedule{e}_{d}");
                }
            }

            // В каждый день только не больше половины сотрудников могут быть в отпуске
            foreach (int d in Range(numDays))
            {
                var temp = new IntVar[numEmployees];
                foreach (int e in Range(numEmployees))
                {
                    temp[e] = vacations[e, d];
                }
                model.Add(LinearExpr.Sum(temp) <= numEmployees / 2);
            }

            // У каждого сотрудника максимум два раза по две недели отпуска
            foreach (int e in Range(numEmployees))
            {
                var temp = new IntVar[numDays];
                foreach (int d in Range(numDays))
                {
                    temp[d] = vacations[e, d];
                }
                model.Add(LinearExpr.Sum(temp) == 2);
            }

            // Поиск решения
            var solver = new CpSolver();
            var status = solver.Solve(model);

            if (status == CpSolverStatus.Optimal || status == CpSolverStatus.Feasible)
            {
                for (int e = 0; e < numEmployees; e++)
                {
                    for (int d = 0; d < numDays; d++)
                    {
                        if (solver.BooleanValue(vacations[e, d]))
                        {
                            for (int i = 1; i <= 13; i++)
                            {
                                if (d + i >= numDays)
                                {
                                    break;
                                }
                                vacations[e, d + i] = vacations[e, d];
                            }
                            d += 13;
                        }
                    }
                }
            }

            // Преобразование в формат даты
            if (status == CpSolverStatus.Optimal || status == CpSolverStatus.Feasible)
            {
                for (int e = 0; e < numEmployees; e++)
                {
                    // Находим границы отпусков
                    int start1 = -1;
                    int finish1 = -1;
                    int start2 = -1;
                    int finish2 = -1;
                    for (int d = 1; d < numDays - 1; d++)
                    {
                        if (solver.BooleanValue(vacations[e, 0]))
                        {
                            start1 = 0;
                        }
                        if (solver.BooleanValue(vacations[e, d]) && !solver.BooleanValue(vacations[e, d - 1]) && start1 == -1)
                        {
                            start1 = d;
                        }
                        if (solver.BooleanValue(vacations[e, d]) && !solver.BooleanValue(vacations[e, d + 1]) && finish1 == -1)
                        {
                            finish1 = d;
                        }
                        if (solver.BooleanValue(vacations[e, d]) && !solver.BooleanValue(vacations[e, d - 1]) && start1 != -1 && d != start1)
                        {
                            start2 = d;
                        }
                        if (solver.BooleanValue(vacations[e, d]) && !solver.BooleanValue(vacations[e, d + 1]) && finish1 != -1 && d != finish1)
                        {
                            finish2 = d;
                        }
                        if (solver.BooleanValue(vacations[e, numDays - 1]) && numDays - 1 != finish1)
                        {
                            finish2 = numDays - 1;
                        }
                    }
                    if (start2 == -1 && finish2 == -1)
                    {
                        string str = IntToData(start1) + " - " + IntToData(finish1);
                        Employees.ElementAt(e).VacationDate = str;
                    }
                    else
                    {
                        string str = IntToData(start1) + " - " + IntToData(finish1) + "; " + IntToData(start2) + " - " + IntToData(finish2);
                        Employees.ElementAt(e).VacationDate = str;
                    }
                }
            }
            return Employees;
        }

        /// <summary>
        /// Преобразование числа в дату
        /// </summary>
        /// <param name="num">Число</param>
        /// <returns>Дата</returns>
        private static string IntToData (int num)
        {
            string str = "";
            if (num >= 0 && num <= 30)
            {
                str = num + 1 + ".01";
            }
            if (num >= 31 && num <= 58)
            {
                str = num - 30 + ".02";
            }
            if (num >= 59 && num <= 89)
            {
                str = num - 58 + ".03";
            }
            if (num >= 90 && num <= 119)
            {
                str = num - 89 + ".04";
            }
            if (num >= 120 && num <= 150)
            {
                str = num - 119 + ".05";
            }
            if (num >= 151 && num <= 180)
            {
                str = num - 150 + ".06";
            }
            if (num >= 181 && num <= 211)
            {
                str = num - 180 + ".07";
            }
            if (num >= 212 && num <= 242)
            {
                str = num - 211 + ".08";
            }
            if (num >= 243 && num <= 272)
            {
                str = num - 242 + ".09";
            }
            if (num >= 273 && num <= 303)
            {
                str = num - 272 + ".10";
            }
            if (num >= 304 && num <= 333)
            {
                str = num - 303 + ".11";
            }
            if (num >= 334 && num <= 364)
            {
                str = num - 333 + ".12";
            }
            return str;
        }

        /// <summary>
        /// Аналог питоновского Range
        /// </summary>
        private static IEnumerable<int> Range(int start, int stop)
        {
            foreach (var i in Enumerable.Range(start, stop - start))
                yield return i;
        }

        /// <summary>
        /// Аналог питоновского Range
        /// </summary>
        private static IEnumerable<int> Range(int stop)
        {
            return Range(0, stop);
        }
    }
}
