import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ILeaderboardUser } from '../shared/models/user';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../environments/environment';
import { ENDPOINTS_MAP } from '../shared/constants/endpoints';

@Injectable({
    providedIn: 'root',
})
export class LeaderboardService {
    constructor(private http: HttpClient) {}

    getLeaderboard(): Observable<ILeaderboardUser[]> {
        return this.http.get<ILeaderboardUser[]>(
            environment.apiUrl + ENDPOINTS_MAP.USER.GET_LEADERBOARD,
        );
    }
}
