import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-logout',
  templateUrl: './logout.component.html',
  styleUrls: ['./logout.component.scss']
})
export class LogoutComponent implements OnInit {

  constructor() { }

  ngOnInit() {
    localStorage.clear();
    location.href = '/admin/login';
  }

}
