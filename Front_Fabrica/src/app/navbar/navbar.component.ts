import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../services/Auth.service';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css'],
})
export class NavbarComponent implements OnInit {
  isCollapsed: boolean = true;
  userName: string = '';
  isAuthenticated: boolean = false;

  constructor(private authService: AuthService, private router: Router) {}

  ngOnInit() {

    this.authService.currentUser.subscribe((u) => {
      this.userName = u;
      this.isAuthenticated = this.authService.isAuthenticated();
    });
  }

  logout() {
    this.authService.logout();
    this.router.navigate(['']);
  }
}
