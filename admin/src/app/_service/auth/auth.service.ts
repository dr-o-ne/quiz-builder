import { Injectable } from '@angular/core';
import { AuthDataProvider } from '../dataProviders/auth.dataProvider';
import { map } from 'rxjs/operators';
import { BehaviorSubject, Observable } from 'rxjs';
import { User } from 'src/app/_models/user';

@Injectable({
    providedIn: 'root'
})
export class AuthService {

    private currentUserSubject: BehaviorSubject<User>;
    public currentUser: Observable<User>;

    constructor(private dataProvider: AuthDataProvider) {
        this.currentUserSubject = new BehaviorSubject<User>(JSON.parse(localStorage.getItem('currentUser')));
        this.currentUser = this.currentUserSubject.asObservable();
    }

    public get currentUserValue(): User {
        return this.currentUserSubject.value;
    }

    signUp(email: string, password: string): void {
        this.dataProvider.signUp(email, password).subscribe();
    }

    login(email: string, password: string): void {
        this.dataProvider.login(email, password).pipe(
            map( response => {       
                const user = response.payload;
                localStorage.setItem('currentUser', JSON.stringify(user));    
                this.currentUserSubject.next(user);            
            } ) 
        ).subscribe();
    }

    logout(): void {
        localStorage.removeItem('currentUser');
        this.currentUserSubject.next(null);
    }

}
