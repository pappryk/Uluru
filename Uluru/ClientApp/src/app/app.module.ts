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
import { GroupsUserListComponent } from './groups-user-list/groups-user-list.component';
import { MatListModule } from '@angular/material/list';
import { MatTabsModule } from '@angular/material/tabs';
import { MatExpansionModule } from '@angular/material/expansion';
import { ManagePositionsComponent } from './manage-positions/manage-positions.component';
import { ManageWorkingSchedulesComponent } from './manage-working-schedules/manage-working-schedules.component';
import { MatDialogModule } from '@angular/material/dialog';
import { CreateWorkEntryComponent } from './create-work-entry/create-work-entry.component';
import { ProfileComponent } from './profile/profile.component';
import { PrivacySettingsComponent } from './privacy-settings/privacy-settings.component';
import { ChangePasswordComponent } from './change-password/change-password.component';
import { EditWorkEntryComponent } from './edit-work-entry/edit-work-entry.component';
import { UserBalanceComponent } from './user-balance/user-balance.component';
import { GroupBalanceComponent } from './group-balance/group-balance.component';

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
    GroupsUserListComponent,
    ManagePositionsComponent,
    ManageWorkingSchedulesComponent,
    CreateWorkEntryComponent,
    ProfileComponent,
    PrivacySettingsComponent,
    ChangePasswordComponent,
    EditWorkEntryComponent,
    UserBalanceComponent,
    GroupBalanceComponent,
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
    MatListModule,
    MatTabsModule,
    MatExpansionModule,
    MatDialogModule,
    RouterModule.forRoot([
      { path: 'availability', component: UserAvailabilityListComponent },
      { path: 'register', component: RegistrationPageComponent },
      { path: 'group', component: GroupComponent },
      { path: 'create-user-account', component: CreateUserAccountComponent },
      { path: 'profile', component: ProfileComponent },
      ], { onSameUrlNavigation: 'reload' }
    )
  ],
  entryComponents: [
    CreateWorkEntryComponent,
    EditWorkEntryComponent
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
