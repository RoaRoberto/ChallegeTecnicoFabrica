import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { GenericService } from './generic.service';

@Injectable({
  providedIn: 'root',
})
export class AuthService extends GenericService {
  public currentUser = new BehaviorSubject('');

  constructor(private http: HttpClient) {
    super(http);

    this.apiURL = environment.serverBackUrl + 'api/auth';
  }

  isAuthenticated() {
    const token = sessionStorage.getItem('token');
    return token != null;
  }

  getToken() {
    const token = sessionStorage.getItem('token');
    return token;
  }

  setToken(token: string) {
    sessionStorage.setItem('token', token);
  }

  logout() {
    sessionStorage.removeItem('token');
    this.currentUser.next('');
  }
}
