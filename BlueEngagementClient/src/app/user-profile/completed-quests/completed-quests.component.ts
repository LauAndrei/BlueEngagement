import { Component, OnInit } from '@angular/core';
import { IQuestCard } from '../../shared/models/quest';
import { QuestService } from '../../quests/quest.service';

@Component({
    selector: 'app-completed-quests',
    templateUrl: './completed-quests.component.html',
    styleUrls: ['./completed-quests.component.scss'],
})
export class CompletedQuestsComponent implements OnInit {

    completedQuests: IQuestCard[] = [];

    constructor(private questService: QuestService) {
    }

    ngOnInit(): void {
        this.questService.getAllCompletedQuestsForUser().subscribe((quests) => {
            this.completedQuests = quests;
        }, (err) => {
            console.log(err);
        })
    }
}
