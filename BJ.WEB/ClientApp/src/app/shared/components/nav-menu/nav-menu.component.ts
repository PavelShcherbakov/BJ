import { Component, OnInit } from '@angular/core';
import { AuthService } from '../../services/account/auth.service';
import { Router } from '@angular/router';


@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.scss']
})
export class NavMenuComponent implements OnInit {

  constructor(private authService:AuthService,private router:Router ) { }

  ngOnInit() {
  }

  logout() {
    const res:boolean=confirm("Are you sure?")
    if(res){
      this.authService.logout();
      debugger;
      this.router.navigate(["/account/login"]);
    }
    this.router.navigate(["/game/create"]);
  }


  
}
