import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';

@Injectable({
    providedIn: 'root'
})
export class UserDataProvider {

    apiUrl = environment.apiUrl;

    constructor(private http: HttpClient) {
    }

    createUser(email: string, password: string): Observable<object> {
        const body = { email, password };
        return this.http.post(this.apiUrl + 'users/', body);
    }

}
