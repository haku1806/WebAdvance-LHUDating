import { Component, NgZone, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { finalize } from 'rxjs/operators';
import { AuthenticationAdminService } from 'src/app/core/service/authentication-admin.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {

  formData!: FormGroup;
  submitted: boolean = false;

  constructor(
    private authenticationAdminService: AuthenticationAdminService,
    private formBuilder: FormBuilder,
    private ngZone: NgZone,
    private router: Router,
    private spinner: NgxSpinnerService,
    private toastr: ToastrService
  ) {
  }

  ngOnInit() {
    this.formData = this.formBuilder.group({
      UserName: ["", Validators.required],
      Password: ["", Validators.required],
    });
  }

  onSubmit() {
    this.submitted = true;
    if (this.formData.invalid) {
      return;
    }
    this.spinner.show();
    this.authenticationAdminService
      .login(this.formData.getRawValue())
      .pipe(
        finalize(() => {
          this.spinner.hide();
        })
      )
      .subscribe(
        (data) => {
          this.toastr.success("Đăng nhập thành công")
          this.navigate("/admin")
        },
        (error) => {
          this.toastr.error(error.error.message);
        }
      );
  }

  navigate(path: string): void {
    this.ngZone.run(() => this.router.navigateByUrl(path)).then();
  }

}
