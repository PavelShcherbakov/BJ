import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, Router } from '@angular/router';
import { Observable } from 'rxjs';
import { AuthService } from '../services/account/auth.service';

@Injectable()
export class OnlyLogoutGuard implements CanActivate {

  constructor(private authService: AuthService, private router: Router) { }

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot):
    boolean |
    import('@angular/router').UrlTree |
    Observable<boolean | import('@angular/router').UrlTree> |
    Promise<boolean | import('@angular/router').UrlTree> {

    if (!this.authService.isAuth()) {
      return true;
    }
    this.router.navigate(['/game/create']);
  }
}
