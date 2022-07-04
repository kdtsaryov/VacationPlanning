using System;
using System.ComponentModel.DataAnnotations;

namespace Vacation_Planning
{
    /// <summary>
    /// Сотрудник
    /// </summary>
    public class Employee
    {
        /// <summary>
        /// ID сотрудника
        /// </summary>
        [Key]
        public long Id { get; set; }

        /// <summary>
        /// Фамилия
        /// </summary>
        [StringLength(41, MinimumLength = 2, ErrorMessage = "Фамилия должна быть от 2 до 40 символов")]
        [RegularExpression(@"[А-Яа-яЁёA-Za-z]+$", ErrorMessage = "В фамилии могут присутствовать только буквы")]
        public string Surname { get; set; }

        /// <summary>
        /// Имя
        /// </summary>
        [StringLength(41, MinimumLength = 2, ErrorMessage = "Имя должно быть от 2 до 40 символов")]
        [RegularExpression(@"[А-Яа-яЁёA-Za-z]+$", ErrorMessage = "В имени могут присутствовать только буквы")]
        public string Name { get; set; }

        /// <summary>
        /// Отчество
        /// </summary>
        [StringLength(41, MinimumLength = 2, ErrorMessage = "Отчество должно быть от 2 до 40 символов")]
        [RegularExpression(@"[А-Яа-яЁёA-Za-z]+$", ErrorMessage = "В отчестве могут присутствовать только буквы")]
        public string SecondName { get; set; }

        /// <summary>
        /// Дата рождения
        /// </summary>
        [DateRange(16, 99, ErrorMessage = "Некорректная дата рождения")]
        public DateTime BirthDate { get; set; }

        /// <summary>
        /// Пол
        /// </summary>
        [Range(0, 2, ErrorMessage = "Некорректный пол")]
        public int Gender { get; set; }

        /// <summary>
        /// Должность
        /// </summary>
        [Range(0, 13, ErrorMessage = "Некорректная должность")]
        public int Position { get; set; }

        /// <summary>
        /// Даты отпуска
        /// </summary>
        public string VacationDate { get; set; }
    }
}
