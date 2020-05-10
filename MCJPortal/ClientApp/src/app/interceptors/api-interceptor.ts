import { CustomNotificationService } from '../main/services/custom-notification.service';
import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor,
  HttpErrorResponse
} from '@angular/common/http';
import { Router } from '@angular/router';
import { Observable, throwError } from 'rxjs';
import { tap, catchError } from 'rxjs/operators';
import { AuthTokenModel } from '../main/services/auth.service';
import { toJSON } from '@progress/kendo-angular-grid/dist/es2015/filtering/operators/filter-operator.base';

@Injectable()
export class ApiHttpInterceptor implements HttpInterceptor {
  constructor(
    private router: Router,
    private notificationService: CustomNotificationService
  ) {}

  intercept(
    req: HttpRequest<any>,
    next: HttpHandler
  ): Observable<HttpEvent<any>> {
    const token = this.getAccessToken();
    const tokenRequest = req.url.indexOf('api/auth/connect') >= 0;

    if (token != null || tokenRequest) {
      const updatedHeaders =
        req.headers.get('Authorization') || tokenRequest
          ? req.headers
          : req.headers.set('Authorization', 'Bearer ' + token);
      const clonedreq = req.clone({
        headers: updatedHeaders,
      });

      return next.handle(clonedreq).pipe(
        catchError(this.parseError.bind(this))
      );
    } else {
      this.router.navigateByUrl('/login');
    }
  }

  getAccessToken(): string {
    const tokenJson = localStorage.getItem('auth-token');

    if (tokenJson) {
      const token = <AuthTokenModel>JSON.parse(tokenJson);
      return token.access_token;
    }
    return null;
  }

  parseError(err: HttpErrorResponse) {
    console.log( 'parseError', err);

    if (err.status === 401) {
      this.router.navigate(['login']);
      return;
    }

    if (err.error instanceof Blob) {
      this.blobToText(err.error).subscribe(
        text => {
          const error = JSON.parse(text);
          this.notificationService.show(`Something goes wrong${': ' + error.error}`, 'error');
        }
      );

    } else {
      this.notificationService.show(`Something goes wrong${': ' + err.error.error}`, 'error');
    }

    throw err;
  }

  private blobToText(blob: any): Observable<string> {
    return new Observable<string>((observer: any) => {
        if (!blob) {
            observer.next('');
            observer.complete();
        } else {
            const reader = new FileReader();
            reader.onload = event => {
                observer.next((<any>event.target).result);
                observer.complete();
            };
            reader.readAsText(blob);
        }
    });
}
}
