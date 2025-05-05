import { Component, OnInit } from '@angular/core';
import { AuthService } from '../../data-access/auth.service';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { take } from 'rxjs';
import { User } from '../../../shared/models/user';
import { ConfirmEmail } from '../../data-access/models/confirm-email';
import { CommonModule } from '@angular/common';
import { NzMessageService } from 'ng-zorro-antd/message';

@Component({
  selector: 'app-confirm-email',
  imports: [CommonModule, RouterLink],
  templateUrl: './confirm-email.component.html',
  styleUrl: './confirm-email.component.css'
})
export class ConfirmEmailComponent implements OnInit {

  isSuccess = true;

  constructor(
    private authService: AuthService, 
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
              const confirmEmail: ConfirmEmail = {
                token: params.get('token'),
                email: params.get('email')
              }
              
              this.authService.confirmEmail(confirmEmail).subscribe({
                next: () => {
                  this.message.success("Ваша почта успешно подтверждена. Вы можете войти в систему.");
                },
                error: (error) => {
                  console.log(error);
                  this.isSuccess = false;
                }
              })
            }
          })
        }

      }
    })

  }

  resendEmailConfirmationLink() {
    this.router.navigateByUrl('send-email/resend-email-confirmation-link');
  }

}
