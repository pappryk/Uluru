import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { FetchDataComponent } from './fetch-data/fetch-data.component';
import { RegistrationPageComponent } from './registration-page/registration-page.component';
import { UserLogin } from './user-login/user-login.component';
import { UserAvailabilityComponent } from './user-availability/user-availability.component';
import { UserAvailabilityListComponent } from './user-availability-list/user-availability-list.component';
import { OwlDateTimeModule, OwlNativeDateTimeModule } from 'ng-pick-datetime';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { GroupComponent } from './group/group.component';
import { WorkingGroupScheduleComponent } from './working-group-schedule/working-group-schedule.component';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    FetchDataComponent,
    RegistrationPageComponent,
    UserLogin,
    UserAvailabilityComponent,
    UserAvailabilityListComponent,
    GroupComponent,
    WorkingGroupScheduleComponent,
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    OwlDateTimeModule,
    OwlNativeDateTimeModule,
    BrowserAnimationsModule,
    RouterModule.forRoot([
      { path: 'availability', component: UserAvailabilityListComponent },
      { path: 'register', component: RegistrationPageComponent },
      { path: 'group', component: GroupComponent },
      ], { onSameUrlNavigation: 'reload' }
    )
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
