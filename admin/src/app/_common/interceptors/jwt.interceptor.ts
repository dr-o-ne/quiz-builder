import { Injectable } from '@angular/core';
import { HttpRequest, HttpHandler, HttpEvent, HttpInterceptor } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { AuthService } from 'src/app/_service/auth/auth.service';

@Injectable()
export class JwtInterceptor implements HttpInterceptor {

    apiUrl = environment.apiUrl;

    constructor(
        private authenticationService: AuthService
    ) { }

    intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {

        const currentUser = this.authenticationService.currentUserValue;
        const isLoggedIn = currentUser && currentUser.token;
        const isApiUrl = request.url.startsWith(this.apiUrl);

        if (isLoggedIn && isApiUrl) {
            request = request.clone({
                setHeaders: {
                    Authorization: `Bearer ${currentUser.token}`
                }
            });
        }

        return next.handle(request);
    }
}