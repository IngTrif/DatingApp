import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';



@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {

  title = 'Dating APP';
  users: any;

  constructor (private http: HttpClient) {} //private-> what we are using here will be available just in this class
  //http injected in our constructor-< is part of the class->this.http

  ngOnInit(): void {
   this.http.get('http://localhost:5193/api/users').subscribe({
    next: response => this.users = response,
    error: error => console.log(error),
    complete: () => console.log('Request has completed')
   })
  }

 
}
