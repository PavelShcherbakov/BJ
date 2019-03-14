import { HttpRequest, HttpHandler, HttpEvent, HttpInterceptor, HttpErrorResponse } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { Injectable } from '@angular/core';
import { catchError, map } from 'rxjs/internal/operators';


@Injectable(
    {
        providedIn: 'root'
    })
export class ErrorInterceptor implements HttpInterceptor {

    constructor() { }

    intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        return next.handle(req)
            .pipe(
                catchError(
                    (error: HttpErrorResponse) => {
                        const err = new Error(error.error.error);
                        return throwError(err);
                    }
                )
            );
    }
}
