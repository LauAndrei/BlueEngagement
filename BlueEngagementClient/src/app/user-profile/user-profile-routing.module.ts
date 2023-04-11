import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { UserProfileComponent } from './user-profile.component';
import { AcceptedQuestsComponent } from './accepted-quests/accepted-quests.component';
import { CompletedQuestsComponent } from './completed-quests/completed-quests.component';
import { ProposedQuestsComponent } from './proposed-quests/proposed-quests.component';

const routes: Routes = [
    {
        path: '',
        component: UserProfileComponent,
        children: [
            {
                path: '',
                pathMatch: 'full',
                redirectTo: 'proposed-quests',
            },
            {
                path: 'proposed-quests',
                component: ProposedQuestsComponent,
            },
            {
                path: 'accepted-quests',
                component: AcceptedQuestsComponent,
            },
            {
                path: 'completed-quests',
                component: CompletedQuestsComponent,
            },
        ],
    },
];

@NgModule({
    declarations: [],
    imports: [CommonModule, RouterModule.forChild(routes)],
    exports: [RouterModule],
})
export class UserProfileRoutingModule {}
