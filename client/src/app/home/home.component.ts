
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  registerMode= false;
  users: any; //paased down our users from the home component to our register component

 //constructor(private http: HttpClient) { }

  ngOnInit(): void {
    //this.getUsers();// home comp is responsible for going out the IPA and get the list of users and setting them to th eproperty
  }

  registerToggle(){
    this.registerMode = !this.registerMode;
  }

  //getUsers(){
    //this.http.get('http://localhost:5193/api/users').subscribe({
    //next: response => this.users = response,
    //error: error => console.log(error),
    //complete: () => console.log('Request has completed')
   //})

  //}
  cancelRegisterMode(event: boolean) {
    this.registerMode = event;
  }

}
