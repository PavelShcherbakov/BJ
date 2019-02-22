import { HttpRequest, HttpHandler, HttpEvent, HttpInterceptor, HttpErrorResponse } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { Injectable } from '@angular/core';
import { catchError,map } from 'rxjs/internal/operators';
import { Router } from '@angular/router';
import { GenericResponseView } from '../entities/generic-response.view';


@Injectable(
    {
        providedIn: 'root'
    })
    export class ErrorInterceptor implements HttpInterceptor {

    constructor(private router: Router) { }

    intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        return next.handle(req)
            .pipe(
                catchError((error:HttpErrorResponse) => {

                    const err = new Error(error.error.error);
                    return throwError(err);
                })
            );
    }
}
