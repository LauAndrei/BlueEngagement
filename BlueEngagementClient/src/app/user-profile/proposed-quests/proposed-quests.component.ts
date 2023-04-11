import { Component, OnInit } from '@angular/core';
import { QuestService } from '../../quests/quest.service';
import { IQuestCard } from '../../shared/models/quest';

@Component({
    selector: 'app-proposed-quests',
    templateUrl: './proposed-quests.component.html',
    styleUrls: ['./proposed-quests.component.scss'],
})
export class ProposedQuestsComponent implements OnInit {
    questCards: IQuestCard[] = [];

    constructor(private questService: QuestService) {}

    ngOnInit(): void {
        this.questService.getLoggedInUsersQuests().subscribe(
            (quests) => {
                this.questCards = quests;
            },
            (err) => {
                console.log(err);
            },
        );
    }
}
