import { Component, Input } from '@angular/core';
import { IQuestCard } from '../../shared/models/quest';
import { Router } from '@angular/router';
import { Location } from '@angular/common';

@Component({
    selector: 'app-quest-card',
    templateUrl: './quest-card.component.html',
    styleUrls: ['./quest-card.component.scss'],
})
export class QuestCardComponent {
    @Input() quest: IQuestCard;

    @Input() main: boolean = true;

    constructor(private router: Router, private location: Location) {}

    seeDetails() {
        if (!this.main) {
            this.router
                .navigateByUrl(
                    '/quests/' + this.quest.id + '/' + this.quest.slug,
                )
                .then(
                    this.location.onUrlChange(() => {
                        location.reload();
                    }),
                );
        }
    }
}
