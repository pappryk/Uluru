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
import { CreateUserAccountComponent } from './create-user-account/create-user-account.component';
import { ManageGroupComponent } from './manage-group/manage-group.component';
import { MatButtonToggleModule} from '@angular/material/button-toggle';
import { MatIconModule } from '@angular/material/icon';
import { MatGridListModule } from '@angular/material/grid-list';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { MatButtonModule } from '@angular/material/button';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner'; 


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
    CreateUserAccountComponent,
    ManageGroupComponent,
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    OwlDateTimeModule,
    OwlNativeDateTimeModule,
    BrowserAnimationsModule,
    MatButtonToggleModule,
    MatIconModule,
    MatGridListModule,
    MatFormFieldModule,
    MatInputModule,
    MatSelectModule,
    MatButtonModule,
    MatProgressSpinnerModule,
    RouterModule.forRoot([
      { path: 'availability', component: UserAvailabilityListComponent },
      { path: 'register', component: RegistrationPageComponent },
      { path: 'group', component: GroupComponent },
      { path: 'create-user-account', component: CreateUserAccountComponent },
      { path: 'manage-group', component: ManageGroupComponent },
      ], { onSameUrlNavigation: 'reload' }
    )
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
