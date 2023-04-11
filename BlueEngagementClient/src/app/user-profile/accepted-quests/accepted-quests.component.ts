import { Component, OnInit } from '@angular/core';
import { QuestService } from '../../quests/quest.service';
import { IQuestCard } from '../../shared/models/quest';

@Component({
    selector: 'app-accepted-quests',
    templateUrl: './accepted-quests.component.html',
    styleUrls: ['./accepted-quests.component.scss'],
})
export class AcceptedQuestsComponent implements OnInit {
    acceptedQuests: IQuestCard[] = [];

    constructor(private questService: QuestService) {}

    ngOnInit(): void {
        this.questService.getAllAcceptedQuestsForUser().subscribe(
            (quests) => {
                this.acceptedQuests = quests;
            },
            (err) => {
                console.log(err);
            },
        );
    }
}
