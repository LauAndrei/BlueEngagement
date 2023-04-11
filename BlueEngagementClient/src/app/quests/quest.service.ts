import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import {
    ICreateQuest,
    IQuestCard,
    IQuestDetails,
} from '../shared/models/quest';
import { environment } from '../../environments/environment';
import { ENDPOINTS_MAP } from '../shared/constants/endpoints';
import { IProof } from '../shared/models/proof';

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

    getLoggedInUsersQuests(): Observable<IQuestCard[]> {
        return this.http.get<IQuestCard[]>(
            environment.apiUrl + ENDPOINTS_MAP.QUEST.GET_LOGGED_IN_USERS_QUESTS,
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

    acceptQuest(questId: number): Observable<boolean> {
        return this.http.post<boolean>(
            environment.apiUrl +
                ENDPOINTS_MAP.TAKEN_QUEST.ACCEPT_QUEST +
                questId,
            null,
        );
    }

    createQuest(newPost: ICreateQuest): Observable<IQuestCard> {
        return this.http.post<IQuestCard>(
            environment.apiUrl + ENDPOINTS_MAP.QUEST.CREATE_QUEST,
            newPost,
        );
    }

    getAllAcceptedQuestsForUser(): Observable<IQuestCard[]> {
        return this.http.get<IQuestCard[]>(
            environment.apiUrl +
                ENDPOINTS_MAP.TAKEN_QUEST.GET_ALL_ACCEPTED_QUESTS_FOR_USER,
        );
    }

    completeTakenQuest(questId: number, proof: IProof): Observable<boolean> {
        return this.http.post<boolean>(
            environment.apiUrl +
                ENDPOINTS_MAP.TAKEN_QUEST.COMPLETE_QUEST +
                questId,
            proof,
        );
    }

    getAllCompletedQuestsForUser(): Observable<IQuestCard[]> {
        return this.http.get<IQuestCard[]>(
            environment.apiUrl +
                ENDPOINTS_MAP.TAKEN_QUEST.GET_ALL_COMPLETED_QUESTS_FOR_USER,
        );
    }
}
