import { Component, NgZone, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { finalize } from 'rxjs/operators';
import { User } from 'src/app/core/model/user';
import { AuthenticationService } from 'src/app/core/service/authentication.service';
import { UserService } from 'src/app/core/service/user.service';
import { Constants } from 'src/app/core/util/constants';
declare const $: any;

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.scss']
})
export class ProfileComponent implements OnInit {

  currentUser: any = {};
  userProfile: User | any = {};
  
  constructor(
    private authenticationService: AuthenticationService,
    private userService: UserService,
    private ngZone: NgZone,
    private router: Router,
    private spinner: NgxSpinnerService,
    private toastr: ToastrService
  ) { }

  ngOnInit() {
    this.currentUser = this.authenticationService.currentUserValue;
    this.getProfile();
  }

  getProfile() {
    this.spinner.show();
    this.userService.getProfile()
      .pipe(
        finalize(() => {
          this.spinner.hide();
        })
      )
      .subscribe((resp: any) => {
        this.userProfile = JSON.parse(resp["data"]);
        $("#modalProfile").modal();
      }, (error) => {
        console.log(error)
      })
  }

  onloadAvatar(img: any) {
    this.userProfile.Avatar = img;
  }

  updateProfile() {
    this.spinner.show();
    this.userService.updateProfile(this.userProfile)
      .pipe(
        finalize(() => {
          this.spinner.hide();
        })
      )
      .subscribe((resp: any) => {
        this.userProfile = JSON.parse(resp["data"]);
        this.toastr.success("Cập nhật thành công");
        this.currentUser.Avatar = this.userProfile.Avatar;
        this.currentUser.FullName = this.userProfile.FullName;
        localStorage.setItem(Constants.LOCAL_STORAGE_KEY.SESSION, JSON.stringify(this.currentUser));
        $("#modalProfile").modal("hide");
        // window.location.reload();
        this.navigate('/')
      }, (error) => {
        this.toastr.error(error.error.message);
      })
  }

  navigate(path: string): void {
    this.ngZone.run(() => this.router.navigateByUrl(path)).then();
  }
}
