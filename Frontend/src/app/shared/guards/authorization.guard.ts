import { CanActivateFn, Router } from '@angular/router';
import { AuthService } from '../../accounts/data-access/auth.service';
import { inject } from '@angular/core';
import { User } from '../models/user';
import { map, take } from 'rxjs';

export const authorizationGuard: CanActivateFn = (route, state) => {
  const authService = inject(AuthService);
  const router = inject(Router);

  return authService.user$.pipe(
    take(1),
    map((user: User | null) => {
      if (user) {
        return true;
      }
      return router.createUrlTree(['/login'], {
        queryParams: { returnUrl: state.url }
      });
    })
  )

};

