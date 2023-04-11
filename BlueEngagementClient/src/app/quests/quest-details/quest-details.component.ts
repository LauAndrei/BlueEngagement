import { Component, OnDestroy, OnInit } from '@angular/core';
import { QuestService } from '../quest.service';
import { ActivatedRoute } from '@angular/router';
import { Subscription } from 'rxjs';
import { IQuestCard, IQuestDetails } from '../../shared/models/quest';
import { ToastrService } from 'ngx-toastr';
import { RESPONSE } from '../../shared/constants/response';
import { FormControl, FormGroup, Validators } from '@angular/forms';

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

    completeQuestForm: FormGroup;
    formSubmitted: boolean = false;
    showForm: boolean = false;

    activatedRouteSubscription: Subscription;
    questServiceSubscription: Subscription;
    isLoading: boolean = true;

    constructor(
        private questService: QuestService,
        private activatedRoute: ActivatedRoute,
        private toastrService: ToastrService,
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

        this.completeQuestForm = new FormGroup({
            description: new FormControl(null),
            pictureUrl: new FormControl(null, Validators.required),
        });
    }

    ngOnDestroy() {
        this.activatedRouteSubscription.unsubscribe();
        this.questServiceSubscription.unsubscribe();
    }

    acceptQuest() {
        this.questService.acceptQuest(this.questId).subscribe(
            (response) => {
                if (response) {
                    this.toastrService.success(
                        RESPONSE.QUEST.ACCEPT_QUEST.SUCCESS,
                    );
                    this.questDetails.questStatus = this.ACCEPTED;
                }
            },
            (err) => {
                this.toastrService.error(RESPONSE.ERROR);
                console.log(err);
            },
        );
    }

    completeQuest() {
        this.formSubmitted = true;

        if (this.completeQuestForm.valid) {
            this.questService
                .completeTakenQuest(this.questId, this.completeQuestForm.value)
                .subscribe(
                    (result) => {
                        if (result) {
                            this.toastrService.success(
                                RESPONSE.QUEST.COMPLETE_QUEST.SUCCESS,
                            );
                            this.showForm = false;
                            this.updateQuestDetailsAfterCompletion();
                        } else {
                            this.toastrService.error(
                                'Something in db',
                                RESPONSE.ERROR,
                            );
                        }
                    },
                    (err) => {
                        console.log(err);
                        this.toastrService.error(RESPONSE.ERROR);
                    },
                );
        }
    }

    showOrHideForm() {
        this.showForm = !this.showForm;
    }

    private updateQuestDetailsAfterCompletion() {
        this.questDetails.questStatus = this.COMPLETED;
        this.questDetails.numberOfCompletions++;
        this.questDetails.rewardsLeft--;
    }
}
