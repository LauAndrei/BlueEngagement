import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { IQuestCard, IQuestDetails } from '../shared/models/quest';
import { environment } from '../../environments/environment';
import { ENDPOINTS_MAP } from '../shared/constants/endpoints';

@Injectable({
    providedIn: 'root',
})
export class QuestService {
    constructor(private http: HttpClient) {}

    getAllQuests(): Observable<IQuestCard[]> {
        return this.http.get<IQuestCard[]>(
            environment.apiUrl + ENDPOINTS_MAP.QUEST.GET_ALL_QUESTS,
        );
    }

    getQuestDetails(questId: number): Observable<IQuestDetails> {
        return this.http.get<IQuestDetails>(
            environment.apiUrl +
                ENDPOINTS_MAP.QUEST.GET_QUEST_DETAILS +
                questId,
        );
    }

    getQuestsFromUser(ownerUsername: string): Observable<IQuestCard[]> {
        return this.http.post<IQuestCard[]>(
            environment.apiUrl + ENDPOINTS_MAP.QUEST.GET_ALL_QUESTS_FROM_USER,
            {
                ownerUsername: ownerUsername,
            },
        );
    }
}
