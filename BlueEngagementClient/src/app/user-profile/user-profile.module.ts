import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { UserProfileComponent } from './user-profile.component';
import { UserProfileRoutingModule } from './user-profile-routing.module';
import { DashboardComponent } from './dashboard/dashboard.component';

@NgModule({
    declarations: [UserProfileComponent, DashboardComponent],
    imports: [CommonModule, UserProfileRoutingModule],
})
export class UserProfileModule {}
