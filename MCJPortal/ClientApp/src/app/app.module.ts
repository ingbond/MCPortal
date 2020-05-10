import { LoginModule } from './login/login.module';
import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule} from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { AppComponent } from './app.component';
import { AppRoutingModule } from './app-routing.module';
import { NotificationModule } from '@progress/kendo-angular-notification';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { CustomNotificationService } from './main/services/custom-notification.service';
import { ApiHttpInterceptor } from './interceptors/api-interceptor';
import { AuthService } from './main/services/auth.service';
import { AuthGuard } from './guards/auth.guard';
import { LayoutModule } from './layout/layout.module';
import { API_BASE_URL, UsersService } from './swagger-services/api.client.generated';
import { environment } from 'src/environments/environment';
import { LayoutComponent } from './layout/layout.component';

@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    FormsModule,
    AppRoutingModule,
    NotificationModule,
    BrowserAnimationsModule,
    LayoutModule
  ],
  providers: [
    {
      provide: API_BASE_URL,
      useValue: environment.apiRoot
  },
    CustomNotificationService,
    AuthService,
    AuthGuard,
    UsersService,
    {
        provide: HTTP_INTERCEPTORS,
        useClass: ApiHttpInterceptor,
        multi: true
    }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
