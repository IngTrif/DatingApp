import { Component, OnInit } from '@angular/core';
import { AccountService } from '../_services/account.service';
import { User } from '../_models/user';
import { Observable, of } from 'rxjs';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {
  model: any = {}
 // loggedIn = false;
 //we want to use an async pipe which will do for us automatically the login/out
 //currentUser$: Observable<User | null> =  of(null);
  

  constructor(public accountService: AccountService, private router: Router, 
    private toastr: ToastrService) { }

  ngOnInit(): void {
   // this.getCurrentUser();
   //this.currentUser$ = this.accountService.currentUsers$;
  }


 // getCurrentUser(){
   // this.accountService.currentUsers$.subscribe({
     // next: user =>this.loggedIn = !!user, //return object user into a boolean: if we have user return true if not false 
     // error: error => console.log(error)
   // })//we tell what we want to do next
//}

  login(){
    this.accountService.login(this.model).subscribe({
      next: _ => this.router.navigateByUrl('/members'),
      error: error => {
         this.toastr.error(error.error),
         console.log(error)
      }
    })
  }

  logout(){
    this.accountService.logout(); //remove the item from local storage
   // this.loggedIn = false;
   this.router.navigateByUrl('/');
  }

}
