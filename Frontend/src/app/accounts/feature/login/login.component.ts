import { Component, EventEmitter, inject, Input, OnInit, Output } from '@angular/core';
import { NonNullableFormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';

import { NzButtonModule } from 'ng-zorro-antd/button';
import { NzCheckboxModule } from 'ng-zorro-antd/checkbox';
import { NzFormModule } from 'ng-zorro-antd/form';
import { NzInputModule } from 'ng-zorro-antd/input';
import { AuthService } from '../../data-access/auth.service';
import { NzMessageService } from 'ng-zorro-antd/message';
import { LoginRequest } from '../../data-access/models/login-request';
import { CommonModule } from '@angular/common';
import { NzIconModule } from 'ng-zorro-antd/icon';

@Component({
  selector: 'app-login',
  imports: [CommonModule, ReactiveFormsModule, NzButtonModule, NzCheckboxModule, NzFormModule, NzInputModule, NzIconModule, RouterLink],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent implements OnInit {
  @Input() isInDrawer = false;
  @Output() switchToRegister = new EventEmitter<void>();
  @Output() loginSuccess = new EventEmitter<void>();

  private fb = inject(NonNullableFormBuilder);
  private authService = inject(AuthService);
  private message = inject(NzMessageService);
  private router = inject(Router);
  private route = inject(ActivatedRoute); 
  returnUrl: string = '/'; 
  error: any;
  passwordVisible: boolean = false;

  
  ngOnInit(): void {
    this.route.queryParams.subscribe(params => {
      if(params['returnUrl']) {
        this.returnUrl = params['returnUrl']
      }
    })
  }

  validateForm = this.fb.group({
    email: this.fb.control('', [Validators.required]),
    password: this.fb.control('', [Validators.required]),
    remember: this.fb.control(true),
  });

  submitForm(): void {
    if (this.validateForm.valid) {
      const { email, password } = this.validateForm.value;

      const request: LoginRequest = { email, password }

      this.authService.login(request).subscribe({
        next: () => {
          this.message.success('Вы успешно вошли!');
          this.loginSuccess.emit();
          this.router.navigate([this.returnUrl]);
        },
        error: (error) => {
          this.error = error.error;
          console.log(this.error);
          this.message.error(this.error.message);
        }
        
      });
    } 
  }

  onRegisterClick(): void {
    if (this.isInDrawer) {
      this.switchToRegister.emit();
    } else {
      this.router.navigate(['/register']);
    }
  }

  resendEmailConfirmationLink() {
    this.router.navigateByUrl('send-email/resend-email-confirmation-link')
  }
}
