import { HttpInterceptorFn } from '@angular/common/http';
import { AuthService } from '../../accounts/data-access/auth.service';
import { inject } from '@angular/core';
import { environment } from '../../../environments/environment.development';

export const authInterceptor: HttpInterceptorFn = (req, next) => {
  const authService = inject(AuthService);

  const accessToken = localStorage.getItem(environment.appKey);
  const user = authService.user;

  if (accessToken && user) {
    req = req.clone({
      setHeaders: {
        Authorization: `Bearer ${accessToken}`
      }
    })
  }

  return next(req);
};
