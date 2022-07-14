import { Component, OnInit } from '@angular/core';
import { DataService } from './data.service';
import { Employee } from './employee';

@Component({
    selector: 'app',
    templateUrl: './app.component.html',
    providers: [DataService]
})
export class AppComponent implements OnInit {

    // Изменяемый сотрудник
    employee: Employee = new Employee();
    // Массив сотрудников
    employees: Employee[];
    // Находимся ли в режиме просмотра
    tableMode: boolean = true;

    constructor(private dataService: DataService) { }

    ngOnInit() {
        // Загрузка данных при старте компонента
        this.loadEmployees();
    }
    // Получаем данные через сервис
    loadEmployees() {
        this.dataService.getEmployees().subscribe((data: Employee[]) => this.employees = data);
    }
    // Сохранение данных
    save() {
        if (this.employee.id == null) {
            this.dataService.postEmployee(this.employee).subscribe((data: Employee) => this.employees.push(data));
        } else {
            this.dataService.putEmployee(this.employee.id, this.employee).subscribe(data => this.loadEmployees());
        }
        this.cancel();
    }
    // Изменение сотрудника
    editEmployee(e: Employee) {
        this.employee = e;
    }
    // Отмена
    cancel() {
        this.employee = new Employee();
        this.tableMode = true;
    }
    // Удаление сотрудника
    delete(e: Employee) {
        this.dataService.deleteEmployee(e.id).subscribe(data => this.loadEmployees());
    }
    // Переход к добавлению сотрудника
    add() {
        this.cancel();
        this.tableMode = false;
    }
    // Генерация отпусков
    generate() {
        this.dataService.generate().subscribe(data => this.loadEmployees());
    }
}