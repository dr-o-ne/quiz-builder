import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';
import { User } from 'src/app/_models/user';
import { ApiResponse } from './apiResponse';

@Injectable({
    providedIn: 'root'
})
export class AuthDataProvider {

    apiUrl = environment.apiUrl;

    constructor(private http: HttpClient) {
    }

    signUp(name: string, email: string, password: string): Observable<ApiResponse<User>> {
        const body = { name, email, password };

        console.log(body);
        return this.http.post<ApiResponse<User>>(this.apiUrl + 'authentication/register', body);
    }

    login(email: string, password: string): Observable<ApiResponse<User>> {
        const body = { email, password };
        return this.http.post<ApiResponse<User>>(this.apiUrl + 'authentication/login', body);
    }

    forgotPassord(email: string): Observable<any> {
        const body = { email };
        return this.http.post(this.apiUrl + 'authentication/forgot-password', body);
    }

    newPassword(code: string, email: string, password: string): Observable<ApiResponse<User>> {
        const body = { code, email, password };
        return this.http.post<ApiResponse<User>>(this.apiUrl + 'authentication/new-password', body);
    }

}
