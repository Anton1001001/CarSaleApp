import { Component } from '@angular/core';

import { NzButtonModule } from 'ng-zorro-antd/button';
import { NzDrawerModule } from 'ng-zorro-antd/drawer';
import { LoginComponent } from "../login/login.component";
import { RegisterComponent } from "../register/register.component";
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-auth-drawer',
  imports: [CommonModule, NzButtonModule, NzDrawerModule, LoginComponent, RegisterComponent],
  templateUrl: './auth-drawer.component.html',
  styleUrl: './auth-drawer.component.css'
})
export class AuthDrawerComponent {

  visible = false;
  mode: 'login' | 'register' = 'login';

  switchToRegister() {
    this.mode = 'register';
  }

  switchToLogin() {
    this.mode = 'login';
  }

  open(): void {
    this.visible = true;
  }

  close(): void {
    this.visible = false;
  }
}
