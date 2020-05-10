import { UserViewModel } from './../../swagger-services/api.client.generated';
import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Observable, Subject, of } from 'rxjs';
import { map, tap } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { UsersService } from 'src/app/swagger-services/api.client.generated';

@Injectable()
export class AuthService {
    isLoginError: boolean;

    loginStatusChange: Subject<boolean> = new Subject<boolean>();

    constructor(private http: HttpClient, private usersService: UsersService) { }

    get isLoggedIn(): boolean {
        if (typeof window !== 'undefined') { // hack for server side rendering, server side dose not have --
            const tokenJson = localStorage.getItem('auth-token');

            if (!tokenJson) {
                return false;
            }

            const token = <AuthTokenModel>JSON.parse(tokenJson);

            return (new Date().getTime() < +token.expiration_date);
        }

        return false;
    }

    logout(): void {
        localStorage.removeItem('auth-token');
        localStorage.removeItem('current-user');
        this.loginStatusChange.next(false);
    }

    login(username: string, password: string): Observable<boolean> {

        const httpOptions = {
            headers: new HttpHeaders({
                'Content-Type': 'application/x-www-form-urlencoded'
            })
        };

        const params = new HttpParams()
            .append('grant_type', 'password')
            .append('username', username)
            .append('password', password)
            .append('scope', 'openid email phone profile offline_access');

            const requestBody = params.toString();

        // this call will give 401 (access denied HTTP status code) if login unsuccessful)
        return this.http.post<boolean>(environment.apiRoot + '/api/auth/connect', requestBody, httpOptions).pipe<boolean>(
            map((value: any) => {
                const token = value as AuthTokenModel;
                localStorage.setItem('refresh-token', token.refresh_token);
                this.parseToken(token);
                return true;
            })
        );
    }

    getCurrentUser(): Observable<UserViewModel> {
        const user = localStorage.getItem('current-user');

        if (!user) {
            return this.usersService.getCurrentUser().pipe(
                tap(u => localStorage.setItem('current-user', JSON.stringify(u)))
            );
        }

        return of(JSON.parse(user));
    }

    parseToken(token: AuthTokenModel) {
        const now = new Date();
        token.expiration_date = new Date(now.getTime() + token.expires_in * 1000).getTime().toString();

        this.storeToken(token);
        this.loginStatusChange.next(true);
    }

    storeToken(token: AuthTokenModel) {
        localStorage.setItem('auth-token', JSON.stringify(token));
    }
}

export interface AuthTokenModel {
    access_token: string;
    refresh_token: string;
    id_token: string;
    expires_in: number;
    token_type: string;
    expiration_date: string;
}
