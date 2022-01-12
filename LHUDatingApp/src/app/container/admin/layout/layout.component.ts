import { Component, NgZone, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-layout',
  templateUrl: './layout.component.html',
  styleUrls: ['./layout.component.scss']
})
export class LayoutComponent implements OnInit {

  isCollapsed: boolean = false;
  constructor(
    private ngZone: NgZone,
    private router: Router
  ) { }

  ngOnInit() {
  }

  logout() {
    this.navigate("/admin/logout")
  }

  navigate(path: string): void {
    this.ngZone.run(() => this.router.navigateByUrl(path)).then();
  }

}
