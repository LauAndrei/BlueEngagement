import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { QuestsComponent } from './quests.component';
import { QuestDetailsComponent } from './quest-details/quest-details.component';
import { CreateQuestComponent } from './create-quest/create-quest.component';

const routes: Routes = [
    {
        path: '',
        pathMatch: 'full',
        component: QuestsComponent,
    },
    {
        path: ':id/:slug',
        component: QuestDetailsComponent,
    },
    {
        path: 'create-quest',
        component: CreateQuestComponent,
    },
];

@NgModule({
    declarations: [],
    imports: [CommonModule, RouterModule.forChild(routes)],
    exports: [RouterModule],
})
export class QuestsRoutingModule {}
