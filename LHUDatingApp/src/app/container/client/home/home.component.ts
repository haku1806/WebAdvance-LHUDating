import { Component, NgZone, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { User } from 'src/app/core/model/user';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {
  datas: User[] = [];
  
  showHideRegister: boolean = false;
  constructor(
    private router: Router,
    private ngZone: NgZone,
  ) { }

  ngOnInit() {
  }

  toggleRegister() {
    this.showHideRegister = !this.showHideRegister;
  }
  
  logOut() {
    window.localStorage.clear();
    window.location.reload();
  }

  navigate(path: string): void {
    this.ngZone.run(() => this.router.navigateByUrl(path)).then();
  }
}
