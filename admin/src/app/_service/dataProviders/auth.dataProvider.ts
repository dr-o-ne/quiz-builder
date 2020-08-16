import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';

@Injectable({
    providedIn: 'root'
})
export class AuthDataProvider {

    apiUrl = environment.apiUrl;

    constructor(private http: HttpClient) {
    }

    signUp(email: string, password: string): Observable<object> {
        const body = { email, password };
        return this.http.post(this.apiUrl + 'authentication/', body);
    }

}
