import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map, Observable, ReplaySubject, tap } from 'rxjs';
import { RegisterRequest } from './models/register-request';
import { LoginRequest } from './models/login-request';
import { environment } from '../../../environments/environment.development';
import { User } from '../../shared/models/user';
import { LoginResponse } from './models/login-response';
import { ConfirmEmail } from './models/confirm-email';
import { ResetPassword } from './models/reset-password';

@Injectable({
  providedIn: 'root',
})
export class AuthService {

  private currentUser: User | null = null;
  private userSource = new ReplaySubject<User | null>(1);
  user$ = this.userSource.asObservable();

  constructor(private http: HttpClient) {}

  get user(): User | null {
    return this.currentUser;
  }

  login(request: LoginRequest) {
    return this.http.post<LoginResponse>(`${environment.appUrl}/api/auth/login/sign-in`, request).pipe(
      tap((response: LoginResponse) => {
        localStorage.setItem(environment.appKey, response.accessToken);
        const user: User = {
          userId: response.userId,
          name: response.name,
          email: response.email
        }
        this.currentUser = user;
        this.userSource.next(user);
      }),
      map(() => undefined)
    );
  }

  registerByEmail(request: RegisterRequest): Observable<any> {
    return this.http.post(`${environment.appUrl}/api/auth/email/sign-up`, request); 
  }

  confirmEmail(request: ConfirmEmail) {
    return this.http.put(`${environment.appUrl}/api/auth/confirm-email`, request);
  }

  loadUserFromStorage(): void {
    const accessToken = localStorage.getItem(environment.appKey);
    console.log(accessToken);
    if(accessToken) {
      const headers = new HttpHeaders({
        Authorization: `Bearer ${accessToken}`
      });

      this.http.get<User>(`${ environment.appUrl }/api/auth/me`, { headers }).subscribe({
        next: (user) => {
          this.currentUser = user; 
          this.userSource.next(user) 
        },
        error: () => { console.log("Error me"); this.logout() }
      });
    } else {
      this.currentUser = null;
      this.userSource.next(null);
    }

  }

  logout() {
    localStorage.removeItem(environment.appKey);
    this.currentUser = null;
    this.userSource.next(null);
  }

  resetPassword(request: ResetPassword) {
    return this.http.put(`${environment.appUrl}/api/auth/reset-password`, request)
  }

  resendEmailConfirmationLink(email: string) {
    return this.http.post(`${environment.appUrl}/api/auth/resend-email-confirmation-link/${email}`, {});
  }

  forgotPassword(email: string) {
    return this.http.post(`${environment.appUrl}/api/auth/forgot-password/${email}`, {});
  }
  
}
