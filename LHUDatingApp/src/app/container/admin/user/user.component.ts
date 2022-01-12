import { Component, OnInit, ViewChild } from '@angular/core';
import { NzMessageService } from 'ng-zorro-antd/message';
import { NgxSpinnerService } from 'ngx-spinner';
import { finalize } from 'rxjs/operators';
import { User } from 'src/app/core/model/user';
import { UserService } from 'src/app/core/service/user.service';
import { UserDetailComponent } from './user-detail/user-detail.component';

@Component({
  selector: 'app-user',
  templateUrl: './user.component.html',
  styleUrls: ['./user.component.scss']
})
export class UserComponent implements OnInit {

  @ViewChild("frmDetail", { static: true }) frmDetail!: UserDetailComponent

  datas: User[] = [];

  filter = {
    keySearch: ""
  }

  constructor(
    private userService: UserService,
    private spinner: NgxSpinnerService,
    private messageService: NzMessageService
  ) { }

  ngOnInit() {
    this.getData();
  }

  getData() {
    this.spinner.show();
    this.userService.getUsers()
      .pipe(
        finalize(() => {
          this.spinner.hide();
        })
      )
      .subscribe((resp: any) => {
        this.datas = JSON.parse(resp["data"]);
      }, error => {
        this.messageService.error(error.error.message);
      })
  }

  showDetail(user: User) {
    // this.spinner.show();
    // this.userService.getById(user.Code)
    //   .pipe(
    //     finalize(() => {
    //       this.spinner.hide();
    //     })
    //   )
    //   .subscribe((resp: any) => {
    //     this.frmDetail.visible = true;
    //     this.frmDetail.setForm(JSON.parse(resp['data']));
    //   }, error => {
    //     this.messageService.error(error.error.message);
    //   })
  }

}
