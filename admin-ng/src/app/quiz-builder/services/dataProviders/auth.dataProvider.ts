import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ApiResponse } from './apiResponse';
import { environment } from 'environments/environment';
import { User } from 'app/quiz-builder/model/user';

@Injectable({
    providedIn: 'root'
})
export class AuthDataProvider {

    apiUrl = environment.apiUrl;

    constructor(private http: HttpClient) {
    }

    signUp(name: string, email: string, password: string): Observable<ApiResponse<User>> {
        const body = { name, email, password };
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
