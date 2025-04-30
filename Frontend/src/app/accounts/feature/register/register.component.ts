import { ChangeDetectorRef, Component, EventEmitter, inject, Input, Output } from '@angular/core';
import { NonNullableFormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router, RouterLink } from '@angular/router';

import { NzButtonModule } from 'ng-zorro-antd/button';
import { NzCheckboxModule } from 'ng-zorro-antd/checkbox';
import { NzFormModule } from 'ng-zorro-antd/form';
import { NzInputModule } from 'ng-zorro-antd/input';
import { AuthService } from '../../data-access/auth.service';
import { NzMessageService } from 'ng-zorro-antd/message';
import { RegisterRequest } from '../../data-access/models/register-request';
import { CommonModule } from '@angular/common';
import { NzIconModule } from 'ng-zorro-antd/icon';

@Component({
  selector: 'app-register',
  imports: [CommonModule, ReactiveFormsModule, NzButtonModule, NzCheckboxModule, NzFormModule, NzIconModule, NzInputModule],
  templateUrl: './register.component.html',
  styleUrl: './register.component.css'
})
export class RegisterComponent {
  @Input() isInDrawer = false;
  @Output() switchToLogin = new EventEmitter<void>();
  
  private fb = inject(NonNullableFormBuilder);
  private authService = inject(AuthService);
  private router = inject(Router);
  private message = inject(NzMessageService);
  private cdr = inject(ChangeDetectorRef);
  passwordVisible: boolean = false;

  validateForm = this.fb.group({
    name: this.fb.control('', []/*[Validators.required]*/),
    email: this.fb.control('', []/*[Validators.required, Validators.email]*/),
    password: this.fb.control('', []/*[Validators.required]*/),
  });

  submitForm(): void {
    if (this.validateForm.valid) {
      const { name, email, password } = this.validateForm.value;
      const request: RegisterRequest = { name, email, password };

      this.authService.registerByEmail(request).subscribe({
        next: () => {
          this.message.success('Письмо с подтверждением отправлено на ваш email.');
          this.validateForm.reset();
        },
        error: (err) => {
          if (err.error?.details) {
            this.setServerValidationErrors(err.error.details);
          }
          this.message.error(err.error?.message || 'Произошла ошибка при регистрации');
        }
      });
      this.validateForm.markAllAsTouched();
    }
    this.validateForm.markAllAsTouched();
  }

  private setServerValidationErrors(details: { field: string; message: string }[]) {
    details.forEach(({ field, message }) => {
      const control = this.findControlByField(field.toLowerCase());
      if (control) {
        control.setErrors({ server: message });
      }
    });
  }

  private findControlByField(field: string) {
    const lower = field.toLowerCase();
    switch (true) {
      case lower.includes('email'):
        case lower.includes('username'):
          return this.validateForm.get('email');
      case lower.includes('name'):
        return this.validateForm.get('name');
      case lower.includes('password'):
        return this.validateForm.get('password');
      default:
        return null;
    }
  }
  
  

  onLoginClick(): void {
    if (this.isInDrawer) {
      this.switchToLogin.emit();
    } else {
      this.router.navigate(['/login']);
    }
  }
}
