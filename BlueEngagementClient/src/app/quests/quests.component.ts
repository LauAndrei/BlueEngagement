import { Component, OnInit } from '@angular/core';
import { QuestService } from './quest.service';
import { IQuestCard } from '../shared/models/quest';

@Component({
    selector: 'app-quests',
    templateUrl: './quests.component.html',
    styleUrls: ['./quests.component.scss'],
})
export class QuestsComponent implements OnInit {
    quests: IQuestCard[] = [];

    constructor(private questService: QuestService) {}

    ngOnInit() {
        this.questService.getAllQuests().subscribe(
            (questCards) => {
                this.quests = questCards;
            },
            (error) => {
                console.log(error);
            },
        );
    }
}
