import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { UserProfileComponent } from './user-profile.component';
import { UserProfileRoutingModule } from './user-profile-routing.module';
import { DashboardComponent } from './dashboard/dashboard.component';
import { AcceptedQuestsComponent } from './accepted-quests/accepted-quests.component';
import { ProposedQuestsComponent } from './proposed-quests/proposed-quests.component';
import { CompletedQuestsComponent } from './completed-quests/completed-quests.component';
import { QuestsModule } from '../quests/quests.module';

@NgModule({
    declarations: [
        UserProfileComponent,
        DashboardComponent,
        AcceptedQuestsComponent,
        ProposedQuestsComponent,
        CompletedQuestsComponent,
    ],
    imports: [CommonModule, UserProfileRoutingModule, QuestsModule],
})
export class UserProfileModule {}
