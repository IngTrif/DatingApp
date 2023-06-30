
import { Component, OnInit } from '@angular/core';
import { AccountService } from './_services/account.service';
import { User } from './_models/user';



@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {

  title = 'Dating APP';
  users: any;

  constructor ( private accountService: AccountService) {} //private-> what we are using here will be available just in this class
  //http injected in our constructor-< is part of the class->this.http

  ngOnInit(): void {
    
    this.setCurrentUser();
   
  }

//method to set the current user:
setCurrentUser(){
  const userString = localStorage.getItem('user'); //!=tyoescrit safety
  if (!userString) return; //check if youhave the user
  const user: User =JSON.parse(userString);
  this.accountService.setCurrentUser(user);
}
 
}
