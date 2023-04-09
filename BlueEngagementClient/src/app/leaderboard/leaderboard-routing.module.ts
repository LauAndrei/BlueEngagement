import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { LeaderboardComponent } from './leaderboard.component';

const routes: Routes = [
    {
        path: '',
        pathMatch: 'full',
        component: LeaderboardComponent,
    },
];

@NgModule({
    declarations: [],
    imports: [CommonModule, RouterModule.forChild(routes)],
    exports: [RouterModule],
})
export class LeaderboardRoutingModule {}
