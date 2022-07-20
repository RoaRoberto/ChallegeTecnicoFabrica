import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthDto } from '../dto/AuthDto';
import { AuthResponseDto } from '../dto/AuthResponseDto';
import { MessageToast } from '../dto/MessageToast';
import { AuthService } from '../services/Auth.service';

@Component({
  selector: 'app-auth',
  templateUrl: './auth.component.html',
  styleUrls: ['./auth.component.css'],
})
export class AuthComponent implements OnInit {
  message: MessageToast = {} as MessageToast;
  authDto: AuthDto = {} as AuthDto;
  show: boolean = false;

  constructor(private authService: AuthService, private router: Router) {}

  ngOnInit(): void {}

  async login() {
    try {
      const response = (await this.authService.createAsync(
        this.authDto
      )) as AuthResponseDto;
      this.authService.setToken(response.token);
      this.authService.currentUser.next(response.userName);
      this.router.navigate(['Users']);
    } catch (error: any) {
      debugger;
      console.log(error);
      this.showAlert('Error', error.error);
    }
  }



  showAlert(title: string, body: string) {
    if (title == 'Error') {
      this.message.class = 'bg-success text-light';
    } else if (title == 'Exito') {
      this.message.class = 'bg-danger text-light';
    } else {
      this.message.class = 'text-light';
    }
    this.message.body = body;
    this.message.title = title;
    this.show = true;
    setTimeout(() => (this.show = false), 5000);
  }
}
