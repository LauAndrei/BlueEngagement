import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { QuestsComponent } from './quests.component';

const routes: Routes = [
    {
        path: '',
        pathMatch: 'full',
        component: QuestsComponent,
    },
];

@NgModule({
    declarations: [],
    imports: [CommonModule, RouterModule.forChild(routes)],
    exports: [RouterModule],
})
export class QuestsRoutingModule {}
