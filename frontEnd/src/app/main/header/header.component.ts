import { Component, OnInit } from '@angular/core';
import { AuthService } from 'src/app/services/auth.service';
import { StorageService } from 'src/app/services/storage.service';


@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent implements OnInit {

  constructor(
    private auth:AuthService,
    private storageService : StorageService,
    ){}

    userRole : any;
  ngOnInit(): void {

    this.userRole =this.storageService.getRole();
  }

  logOut(){
    this.auth.logOut();
  }
 
}
