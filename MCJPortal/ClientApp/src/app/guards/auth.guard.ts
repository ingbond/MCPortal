import { Injectable } from '@angular/core';
import { Router, CanActivate, RouterStateSnapshot, ActivatedRouteSnapshot, CanLoad, Route } from '@angular/router';
import { AuthService } from '../main/services/auth.service';

@Injectable()
export class AuthGuard implements CanActivate, CanLoad {

    constructor(private authService: AuthService, private router: Router) { }

    canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
        if (!this.authService.isLoggedIn) {
            this.router.navigate(['/login'], { queryParams: { returnUrl: state.url } });
            return false;
        }
        return true;
    }

    canLoad(route: Route): boolean {

        if (!this.authService.isLoggedIn) {
            this.router.navigate(['/login'], {});
            return false;
        }
        return true;
      }
}