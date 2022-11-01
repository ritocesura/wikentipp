import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor,
} from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { AuthService } from '../services/auth.service';
import { environment } from 'src/environments/environment';
import { Router } from '@angular/router';
import { NzMessageService } from "ng-zorro-antd/message";

@Injectable()
export class UnauthorizedInterceptor implements HttpInterceptor {
  constructor(private authService: AuthService, private router: Router, private message: NzMessageService) { }

  intercept(
    request: HttpRequest<unknown>,
    next: HttpHandler
  ): Observable<HttpEvent<unknown>> {
    return next.handle(request).pipe(
      catchError((err) => {
        if (err.status === 401) {
          console.log("UNAUTHORIZED REACHED");
          this.authService.clearLocalStorage();
          this.router.navigate(['']);
        } else {
          this.message.error(err.error); 
        }

        if (!environment.production) {
          console.error(err);
        }

        const error = (err && err.error && err.error.message) || err.statusText;
        return throwError(error);
      })
    );
  }
}
