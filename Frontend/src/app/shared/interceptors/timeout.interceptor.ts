import { HttpInterceptorFn } from '@angular/common/http';
import { timeout, catchError } from 'rxjs';
import { throwError } from 'rxjs';

export const timeoutInterceptor: HttpInterceptorFn = (req, next) => {
  const TIMEOUT = 100000;

  return next(req).pipe(
    timeout(TIMEOUT),
    catchError((err) => {
      if (err.name === 'TimeoutError') {
        return throwError(() => new Error('Request timed out!'));
      }
      return throwError(() => err);
    })
  );
};
