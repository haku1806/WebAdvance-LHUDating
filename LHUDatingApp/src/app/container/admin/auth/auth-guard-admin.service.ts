import { Injectable, NgZone } from '@angular/core';
import { Router } from '@angular/router';
import { AuthenticationAdminService } from 'src/app/core/service/authentication-admin.service';

@Injectable({
  providedIn: 'root'
})
export class AuthGuardAdminService {

  constructor(
    private ngZone: NgZone,
    private router: Router,
    private authenticationAdminService: AuthenticationAdminService
) { }

canActivate() {
    const token = this.authenticationAdminService.getToken;
    if (token == null) {
        this.navigate("/admin/login");
        return false;
    }
    return true;
}

public navigate(path: string): void {
    this.ngZone.run(() => this.router.navigateByUrl(path)).then();
}
}
