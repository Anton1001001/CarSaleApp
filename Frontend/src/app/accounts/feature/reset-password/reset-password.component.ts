import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { AuthService } from '../../data-access/auth.service';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { NzMessageService } from 'ng-zorro-antd/message';
import { User } from '../../../shared/models/user';
import { take } from 'rxjs';
import { ResetPassword } from '../../data-access/models/reset-password';
import { NzCardModule } from 'ng-zorro-antd/card';
import { NzFormModule } from 'ng-zorro-antd/form';
import { NzButtonModule } from 'ng-zorro-antd/button';
import { NzInputModule } from 'ng-zorro-antd/input';
import { NzSelectModule } from 'ng-zorro-antd/select';
import { NzRadioModule } from 'ng-zorro-antd/radio';
import { NzListModule } from 'ng-zorro-antd/list';
import { NzIconModule } from 'ng-zorro-antd/icon';

@Component({
  selector: 'app-reset-password',
  imports: [CommonModule, 
    ReactiveFormsModule, 
    NzCardModule, 
    NzFormModule, 
    NzButtonModule,
    NzInputModule,
    NzSelectModule,
    NzRadioModule,
    NzListModule,
    NzIconModule,
    NzCardModule,
    RouterLink],
  templateUrl: './reset-password.component.html',
  styleUrl: './reset-password.component.css'
})
export class ResetPasswordComponent {
  resetPasswordForm: FormGroup = new FormGroup({});
  token: string | undefined;
  email: string | undefined;
  submitted = false;
  error: any;

  constructor(private authService: AuthService,
    private formBuilder: FormBuilder,
    private router: Router,
    private activatedRoute: ActivatedRoute,
    private message: NzMessageService) {}

  ngOnInit(): void {
    this.authService.user$.pipe(take(1)).subscribe({
      next: (user: User | null) => {
        if (user) {
          this.router.navigateByUrl('/');
        } else {
          this.activatedRoute.queryParamMap.subscribe({
            next: (params: any) => {
              this.token = params.get('token');
              this.email = params.get('email');

              if (this.token && this.email) {
                this.initializeForm(this.email);
              } else {
                this.router.navigateByUrl('/login');
              }
            }
          })
        }
      }
    })
  }

  initializeForm(email: string) {
    this.resetPasswordForm = this.formBuilder.group({
      email: [{value: email, disabled: true}],
      newPassword: ['']
    })
  }

  resetPassword() {
    this.submitted = true;

    if (this.resetPasswordForm.valid && this.email && this.token) {
      const request: ResetPassword = {
        token: this.token,
        email: this.email,
        newPassword: this.resetPasswordForm.get('newPassword')?.value
      };

      this.authService.resetPassword(request).subscribe({
        next: () => {
          this.message.success('Пароль успешно изменен!');
          this.router.navigateByUrl('/login');
        }, 
        error: (error) => {
          this.error = error.error;
          this.message.error(this.error.message);
        }
      })
    }
  }


}
