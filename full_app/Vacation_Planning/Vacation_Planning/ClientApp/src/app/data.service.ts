import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Employee } from './employee';

@Injectable()
export class DataService {

    private url = "/api/employees";

    constructor(private http: HttpClient) {
    }

    getEmployees() {
        return this.http.get(this.url);
    }

    getEmployee(id: number) {
        return this.http.get(this.url + '/' + id);
    }

    postEmployee(employee: Employee) {
        return this.http.post(this.url, employee);
    }

    putEmployee(id: number, employee: Employee) {

        return this.http.put(this.url + '/' + id, employee);
    }

    deleteEmployee(id: number) {
        return this.http.delete(this.url + '/' + id);
    }

    generate() {
        return this.http.get(this.url + "/generate");
    }
}