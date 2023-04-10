import { Component, OnDestroy, OnInit } from '@angular/core';
import { QuestService } from '../quest.service';
import { ActivatedRoute } from '@angular/router';
import { Subscription } from 'rxjs';
import { IQuestCard, IQuestDetails } from '../../shared/models/quest';

@Component({
    selector: 'app-quest-details',
    templateUrl: './quest-details.component.html',
    styleUrls: ['./quest-details.component.scss'],
})
export class QuestDetailsComponent implements OnInit, OnDestroy {
    NOT_ACCEPTED = 'NotAccepted';
    ACCEPTED = 'Accepted';
    COMPLETED = 'Completed';
    questId: number = 0;
    questDetails?: IQuestDetails;
    questCardsFromUser: IQuestCard[] = [];

    activatedRouteSubscription: Subscription;
    questServiceSubscription: Subscription;
    isLoading: boolean = true;

    constructor(
        private questService: QuestService,
        private activatedRoute: ActivatedRoute,
    ) {}

    ngOnInit() {
        this.activatedRouteSubscription =
            this.activatedRoute.paramMap.subscribe((params) => {
                this.questId = +params.get('id');
            });

        this.questServiceSubscription = this.questService
            .getQuestDetails(this.questId)
            .subscribe(
                (questDetails) => {
                    this.questDetails = questDetails;
                    this.questService
                        .getQuestsFromUser(this.questDetails.ownerUsername)
                        .subscribe((quests) => {
                            this.questCardsFromUser = quests;
                        });
                },
                (error) => {
                    console.log(error);
                },
                () => {
                    this.isLoading = false;
                },
            );
    }

    ngOnDestroy() {
        this.activatedRouteSubscription.unsubscribe();
        this.questServiceSubscription.unsubscribe();
    }
}
