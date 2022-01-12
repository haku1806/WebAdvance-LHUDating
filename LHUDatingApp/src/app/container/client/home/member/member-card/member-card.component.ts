import { Component, Input, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { finalize } from 'rxjs/operators';
import { User } from 'src/app/core/model/user';
import { AuthenticationService } from 'src/app/core/service/authentication.service';
import { UserService } from 'src/app/core/service/user.service';

@Component({
  selector: 'app-member-card',
  templateUrl: './member-card.component.html',
  styleUrls: ['./member-card.component.scss']
})
export class MemberCardComponent implements OnInit {
  @Input() user!: User;
  contactInfo: any = null;

  constructor(
    private authenticationService: AuthenticationService,
    private userService: UserService,
    private toastr: ToastrService
  ) { }

  ngOnInit() {
  }

  sendLike(recipientId: string) {
    this.userService.sendLike(recipientId)
    .pipe(
      finalize(() => {
      })
    ).subscribe((resp: any) => {
      this.contactInfo = JSON.parse(resp["data"])
      this.toastr.success("Bạn đã like user: " + this.user.FullName);
    }, (error) => {
      this.toastr.error(error.error.message);
      console.log(error);
    })
    
  }
}
