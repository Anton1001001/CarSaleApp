import { Component, OnInit } from '@angular/core';
import { AuthService } from '../../data-access/auth.service';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { take } from 'rxjs';
import { User } from '../../../shared/models/user';
import { NzMessageService } from 'ng-zorro-antd/message';
import { CommonModule } from '@angular/common';
import { NzButtonModule } from 'ng-zorro-antd/button';
import { NzFormModule } from 'ng-zorro-antd/form';
import { NzInputModule } from 'ng-zorro-antd/input';
import { NzSelectModule } from 'ng-zorro-antd/select';
import { NzRadioModule } from 'ng-zorro-antd/radio';
import { NzListModule } from 'ng-zorro-antd/list';
import { NzIconModule } from 'ng-zorro-antd/icon';
import { NzCardModule } from 'ng-zorro-antd/card';

@Component({
  selector: 'app-send-email',
  imports: [CommonModule, 
    ReactiveFormsModule, 
    NzButtonModule,
    NzFormModule,
    NzInputModule,
    NzSelectModule,
    NzRadioModule,
    NzListModule,
    NzIconModule,
    NzCardModule],
  templateUrl: './send-email.component.html',
  styleUrl: './send-email.component.css'
})
export class SendEmailComponent implements OnInit {

  emailForm: FormGroup = new FormGroup({});
  isSubmitted = false;
  mode: string | undefined;
  error: any;

  constructor(private authService: AuthService,
    private formBuider: FormBuilder,
    private router: Router,
    private activatedRoute: ActivatedRoute,
    private message: NzMessageService) {}

  ngOnInit(): void {
    this.activatedRoute.params.subscribe({
      next: (params) => {
        this.emailForm.reset();
        this.mode = params['mode']
      }
    })
    this.authService.user$.subscribe({
      next: (user: User | null) => {
        if (user) {
          this.router.navigateByUrl('/')
        } else {
          if (this.mode) {
            this.ininializeForm();
          }
        }
      }
    })
  }

  ininializeForm() {
    this.emailForm = this.formBuider.group({
      email: this.formBuider.control('', [Validators.required, Validators.email])
    })
  }

  sendEmail() {
    this.isSubmitted = true;
    if (this.emailForm.valid && this.mode?.includes('resend-email-confirmation-link')) {
      this.authService.resendEmailConfirmationLink(this.emailForm.get('email')?.value).subscribe({
        next: () => {
          this.message.success('Письмо с подтверждением отправлено на ваш email.')
          this.router.navigateByUrl('/login')
        },
        error: (error) => {
          this.message.error('error from send-email');
          console.log(error);
        }
      })
    } else if (this.mode?.includes('forgot-password')) {
      this.authService.forgotPassword(this.emailForm.get('email')?.value).subscribe({
        next: () => {
          this.message.success('Письмо для восстановления пароля отправлено на ваш email.');
          this.router.navigateByUrl('/login');
        }, 
        error: (error) => {
          this.error = error.error;
          this.message.error(this.error.message);
          console.log(error);
        }
      })
    }
  }

  resendEmailConfirmationLink() {
  
    this.router.navigate(['/send-email', 'resend-email-confirmation-link']);
  }
  
  

  cancel() {
    this.router.navigateByUrl('/login');
  }

}
