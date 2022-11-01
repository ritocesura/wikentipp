import { Component } from '@angular/core';
import { AuthService } from "./core/services/auth.service";
import { LoginPopupComponent } from "./auth-popups/login-popup/login-popup.component";
import { RegisterPopupComponent } from "./auth-popups/register-popup/register-popup.component";
import { NzModalService } from "ng-zorro-antd/modal";

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ["./app.component.less"]
})
export class AppComponent {
  user$ = this.auth.user$;
  isAuthenticated$ = this.auth.isAuthenticated$;

  constructor(
    private auth: AuthService,
    private modalService: NzModalService,
  ){
  }

  signIn(): void {
    this.modalService.create({
      nzContent: LoginPopupComponent,
      nzMaskClosable: false,
      nzFooter: null
    });
  }

  register(): void {
    this.modalService.create({
      nzContent: RegisterPopupComponent,
      nzMaskClosable: false,
      nzFooter: null
    });
  }

  logout(): void {
    this.auth.logout();
  }
}
