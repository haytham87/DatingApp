import { Component, OnInit } from '@angular/core';
import { AuthService } from '../_services/auth.service';
import { AlertifyService } from '../_services/alertify.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {
  model: any = {};
  photoUrl: string;

  constructor(public authService: AuthService, private alertify: AlertifyService, private router: Router) { }

  ngOnInit() {
    this.authService.currentPhotoUrl.subscribe(photourl => this.photoUrl = photourl);
  }

  login() {
    this.authService.login(this.model).subscribe(
      next => {
        this.alertify.success('Logged in Success');
      }, error => {
        this.alertify.error('Failed to login');
      }, () => {
        this.router.navigate(['/members']);
      }
    );
  }

  loggedIN(){
    return this.authService.loggedIn();
  }

  logOut(){
    localStorage.removeItem('token');
    localStorage.removeItem('user');
    this.authService.decodedToken = null;
    this.authService.currentUser = null;
    this.alertify.warning('logged out');
    this.router.navigate(['/home']);
  }

}
