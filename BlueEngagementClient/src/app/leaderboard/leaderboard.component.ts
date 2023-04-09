import { Component, OnInit } from '@angular/core';
import { LeaderboardService } from './leaderboard.service';
import { ILeaderboardUser } from '../shared/models/user';

@Component({
    selector: 'app-leaderboard',
    templateUrl: './leaderboard.component.html',
    styleUrls: ['./leaderboard.component.scss'],
})
export class LeaderboardComponent implements OnInit {
    medals = ['🥇', '🥈', '🥉'];

    leaderboardUsers: ILeaderboardUser[] = [];

    constructor(private leaderboardService: LeaderboardService) {}

    ngOnInit(): void {
        this.leaderboardService.getLeaderboard().subscribe((users) => {
            this.leaderboardUsers = users;
        });
    }

    getMedal(index: number) {
        if (index < 3) {
            return this.medals[index];
        }

        return index + 1;
    }
}
