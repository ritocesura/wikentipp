import { Component } from "@angular/core";
import { AbstractControl, FormBuilder, ValidationErrors, Validators } from "@angular/forms";
import { AuthService } from "../../core/services/auth.service";
import { NzModalRef } from "ng-zorro-antd/modal";
import { NzMessageService } from "ng-zorro-antd/message";

@Component({
  selector: "login-popup",
  templateUrl: "./register-popup.component.html",
  styleUrls: ["./register-popup.component.less"]
})
export class RegisterPopupComponent {
  isLoading = false;

  public form = this.fb.group({
    username: ["", [Validators.required]],
    email: ["", [Validators.required, Validators.email]],
    password: ["", [Validators.required, Validators.minLength(6)]],
    confirmPassword: ["", [Validators.required, Validators.minLength(6)]]
  }, {
    validators: [this.matchPasswords]
  });

  constructor(private fb: FormBuilder, private auth: AuthService,
    private modalRef: NzModalRef, private message: NzMessageService) {
  }

  private matchPasswords(AC: AbstractControl): ValidationErrors | null {
    const password = AC.get("password")?.value;
    const confirmPassword = AC.get("confirmPassword")?.value;
    if (password !== confirmPassword) {
      return { matchPassword: true };
    } else {
      return null;
    }
  }

  submit(): void {
    this.isLoading = true;
    const creds = this.form.getRawValue();
    this.auth.register(creds.username, creds.password, creds.confirmPassword, creds.email)
      .subscribe(() => {
        this.modalRef.close();
        this.message.success('Erfolgreich registriert', {
          nzDuration: 5000
        });
      },
        () => { },
        () => { this.isLoading = false; }
      );
  }
}
