import { Injectable } from '@angular/core';
import { AuthDataProvider } from '../dataProviders/auth.dataProvider';
import { map } from 'rxjs/operators';

@Injectable({
    providedIn: 'root'
})
export class AuthService {

    constructor(private dataProvider: AuthDataProvider) {
    }

    signUp(email: string, password: string) {
        this.dataProvider.signUp(email, password).subscribe();
    }

    login(email: string, password: string) {
        this.dataProvider.login(email, password).pipe(
            map( response => {       
                
                const user = response.payload;
                localStorage.setItem('currentUser', JSON.stringify(user));                

                console.log(localStorage.getItem('currentUser'));
            } ) 
        ).subscribe();
    }

    logout() {
        localStorage.removeItem('currentUser');
    }

}
