import { Component, Input } from '@angular/core';
import { IMainTaskCard } from '../../shared/models/task';

@Component({
    selector: 'app-quest-card',
    templateUrl: './quest-card.component.html',
    styleUrls: ['./quest-card.component.scss'],
})
export class QuestCardComponent {

    @Input() task: IMainTaskCard;
}
