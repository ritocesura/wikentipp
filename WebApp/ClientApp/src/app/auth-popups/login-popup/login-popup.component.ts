import { Component } from "@angular/core";
import { FormBuilder, Validators } from "@angular/forms";
import { AuthService } from "../../core/services/auth.service";
import { NzModalRef } from "ng-zorro-antd/modal";
import { NzMessageService } from "ng-zorro-antd/message";

@Component({
  selector: "login-popup",
  templateUrl: "./login-popup.component.html",
  styleUrls: ["./login-popup.component.less"]
})
export class LoginPopupComponent {
  isLoading = false;

  public form = this.fb.group({
    username: ["", [Validators.required]],
    password: ["", [Validators.required, Validators.minLength(6)]]
  });

  constructor(private fb: FormBuilder, private auth: AuthService,
    private modalRef: NzModalRef, private message: NzMessageService) {
  }

  //public sendResetPassword(): void {
  //  const email = this.form.getRawValue().email;
  //  this.auth.sendResetPassword(email);
  //  this.message.success("Reset password email sent");
  //  this.modalRef.close();
  //}

  login(): void {
    this.isLoading = true;
    const creds = this.form.getRawValue();
    this.auth.login(creds.username, creds.password)
      .subscribe(
        () => {
          this.modalRef.close();
          this.message.success('Erfolgreich eingeloggt', {
            nzDuration: 5000
          });
        },
        () => {},
        () => { this.isLoading = false; }
      );
  }
}
