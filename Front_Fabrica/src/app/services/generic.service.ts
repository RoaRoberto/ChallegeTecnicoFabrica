import { Observable } from 'rxjs';
import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { FormGroup, FormControl } from '@angular/forms';



export abstract class GenericService {
    public apiURL: string = '';
    public ssoURL: string = '';
    httpClient: HttpClient;

    constructor(httpClient: HttpClient) {
        this.httpClient = httpClient;
    }


    public async createAsync(entity: any): Promise<any> {
        const reqHeaders = new HttpHeaders({ 'Content-Type': 'application/json' });
        const entityJson = JSON.stringify(entity);
        return await this.httpClient.post<any>(this.apiURL, entityJson, { headers: reqHeaders }).toPromise();
    }

    public async updateAsync(entity: any, id: string): Promise<any> {
        const reqHeaders = new HttpHeaders({ 'Content-Type': 'application/json' });
        const entityJson = JSON.stringify(entity);
        return await this.httpClient.put<any>(this.apiURL + '/' + id, entityJson, { headers: reqHeaders }).toPromise();
    }



    public async deleteAsync(id: any): Promise<any> {
        return await this.httpClient.delete<any>(this.apiURL + '/' + id).toPromise();
    }

    public async getByIdAsync(id: any): Promise<any> {
        return this.httpClient.get(this.apiURL + '/' + id).toPromise();
    }

    public async getAllAsync(): Promise<any> {
        return await this.httpClient.get<any>(this.apiURL).toPromise();
    }

    public create(entity: any): Observable<any> {
        const reqHeaders = new HttpHeaders({ 'Content-Type': 'application/json' });
        const entityJson = JSON.stringify(entity);
        return this.httpClient.post<any>(this.apiURL, entityJson, { headers: reqHeaders });
    }

    public update(entity: any, id: string): Observable<any> {
        const reqHeaders = new HttpHeaders({ 'Content-Type': 'application/json' });
        const entityJson = JSON.stringify(entity);
        return this.httpClient.put<any>(this.apiURL + '/' + id, entityJson, { headers: reqHeaders });
    }
    public delete(id: any): Observable<any> {
        return this.httpClient.delete<any>(this.apiURL + '/' + id);
    }

    public getById(id: any): Observable<any> {
        return this.httpClient.get(this.apiURL + '/' + id);
    }

    public getAll(): Observable<any> {
        return this.httpClient.get<any>(this.apiURL);
    }


    // //////////////////////////////////////////////////////////////////////////
    public getDateFormat(date: Date, time?: boolean, space?: boolean): string {

        const year = date.getFullYear();
        let month = (date.getMonth() + 1) + '';
        let dt = date.getDate() + '';
        let hours = date.getHours() + '';
        let minutes = date.getMinutes() + '';
        if (date.getDate() < 10) {
            dt = '0' + dt;
        }
        if ((date.getMonth() + 1) < 10) {
            month = '0' + month;
        }
        if (time) {
            if (date.getMinutes() < 10) {
                minutes = '0' + minutes;
            }
            if (date.getHours() < 10) {
                hours = '0' + hours;
            }
            if (space) {

                return `${year}-${month}-${dt} ${hours}:${minutes}`;
            }
            return `${year}-${month}-${dt}T${hours}:${minutes}`;
        }
        return `${year}-${month}-${dt}`;
    }

    public getDateStrFormat(dateStr: string): string {
        const date = new Date(dateStr);
        const year = date.getFullYear();
        let month = (date.getMonth() + 1) + '';
        let dt = date.getDate() + '';
        let hours = date.getHours() + '';
        let minutes = date.getMinutes() + '';
        if (date.getDate() < 10) {
            dt = '0' + dt;
        }
        if ((date.getMonth() + 1) < 10) {
            month = '0' + month;
        }
        return `${dt}/${month}/${year}`;
    }

    public cloneEntity<T>(c: T): T {
        const clone = <T>{};
        // tslint:disable-next-line:forin
        for (const prop in c) {
            clone[prop] = c[prop];
        }
        return clone;
    }


    getLocale(): any {

        const locale = {
            firstDayOfWeek: 1,
            dayNames: ['domingo', 'lunes', 'martes', 'miércoles', 'jueves', 'viernes', 'sábado'],
            dayNamesShort: ['dom', 'lun', 'mar', 'mié', 'jue', 'vie', 'sáb'],
            dayNamesMin: ['D', 'L', 'M', 'X', 'J', 'V', 'S'],
            monthNames: ['enero', 'febrero', 'marzo', 'abril', 'mayo', 'junio',
                'julio', 'agosto', 'septiembre', 'octubre', 'noviembre', 'diciembre'],
            monthNamesShort: ['ene', 'feb', 'mar', 'abr', 'may', 'jun', 'jul', 'ago', 'sep', 'oct', 'nov', 'dic'],
            today: 'Hoy',
            clear: 'Borrar'
        };
        return locale;

    }

    toFixed(num: any, fixed: any) {
        fixed = fixed || 0;
        fixed = Math.pow(10, fixed);
        return Math.floor(num * fixed) / fixed;
    }


    validateAllFormFields(formGroup: FormGroup) {
        Object.keys(formGroup.controls).forEach(field => {
            const control = formGroup.get(field);
            if (control instanceof FormControl) {
                control.markAsTouched({ onlySelf: true });
            } else if (control instanceof FormGroup) {
                this.validateAllFormFields(control);
            }
        });
    }



    zeroTimeInDate(date: Date) {
        return date.toISOString().split('T')[0].concat("T00:00:00");
    }
}






