import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { NotAuthGuard } from './core/guards/not-auth.guard';
import { AuthGuard } from './core/guards/auth.guard';

const routes: Routes = [
    {
        path: '',
        canActivate: [NotAuthGuard],
        loadChildren: () =>
            import('./account/account.module').then((mod) => mod.AccountModule),
    },
    {
        path: 'home',
        canActivate: [AuthGuard],
        loadChildren: () =>
            import('./home/home.module').then((mod) => mod.HomeModule),
    },
    {
        path: 'quests',
        canActivate: [AuthGuard],
        loadChildren: () =>
            import('./quests/quests.module').then((mod) => mod.QuestsModule),
    },
    {
        path: 'leaderboard',
        canActivate: [AuthGuard],
        loadChildren: () =>
            import('./leaderboard/leaderboard.module').then(
                (mod) => mod.LeaderboardModule,
            ),
    },
    {
        path: 'profile',
        canActivate: [AuthGuard],
        loadChildren: () =>
            import('./user-profile/user-profile.module').then(
                (mod) => mod.UserProfileModule,
            ),
    },
    {
        path: '**',
        redirectTo: '/quests',
    },
];

@NgModule({
    imports: [RouterModule.forRoot(routes)],
    exports: [RouterModule],
})
export class AppRoutingModule {}
