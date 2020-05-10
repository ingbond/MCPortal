import { UsersService, UserViewModel } from './../../swagger-services/api.client.generated';
import { Router } from '@angular/router';
import { AuthService } from './../../main/services/auth.service';
import { Component, OnInit } from '@angular/core';
import { ImageHandlerService } from 'src/app/main/services/image-handler.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss'],
})
export class HeaderComponent implements OnInit {
  user: UserViewModel;

  constructor(
    private router: Router,
    private authService: AuthService,
    private imageHandlerService: ImageHandlerService
  ) {
  }

  ngOnInit() {
    this.router.events.subscribe(x => console.log('router change', x)
    );
    this.authService.getCurrentUser().subscribe(
      (data) => (this.user = data),
      (error) => console.error(error)
    );
  }

  getUserImage() {
    if (this.user && this.user.country && this.user.country.fileDoc) {
      return this.imageHandlerService.getImage(this.user.country.fileDoc);
    }

    return null;
  }

  onLoggedout() {
    this.authService.logout();
    this.router.navigate(['/login']);
  }
}
