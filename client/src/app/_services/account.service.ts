import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from 'rxjs';
import { User } from '../_models/user';
import {BehaviorSubject} from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable({ //injected into our componnet
  providedIn: 'root'
})
export class AccountService { //using aservice gines us the oportunity to centralize our Http request
  baseUrl = environment.apiUrl;
  private currentUserSource = new BehaviorSubject <User | null>(null);
  currentUser$ = this.currentUserSource.asObservable(); //check if it's a user or no
 

  constructor(private http: HttpClient) { }

  login(model: any) {
    return this.http.post<User>(this.baseUrl + 'account/login', model).pipe(
      map((response: User) => {
        const user = response;
        if (user) {
          localStorage.setItem('user', JSON.stringify(user))
          this.currentUserSource.next(user);
        }
      })
    );//return an object pssed from json
  }

  register(model:any){
    return this.http.post<User>(this.baseUrl + 'account/register', model).pipe(
      map(user => {
        if (user) {
          localStorage.setItem('user', JSON.stringify(user));
          this.currentUserSource.next(user);
        }
      })
    )
  }
  setCurrentUser(user:User) {
    this.currentUserSource.next(user);
  }

  logout(){
    localStorage.removeItem('user');//remove item from local storage
    this.currentUserSource.next(null);
  }
}
