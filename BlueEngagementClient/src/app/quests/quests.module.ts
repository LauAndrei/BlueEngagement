import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { QuestsComponent } from './quests.component';
import { QuestsRoutingModule } from './quests-routing.module';
import { QuestCardComponent } from './quest-card/quest-card.component';
import { QuestDetailsComponent } from './quest-details/quest-details.component';
import { SharedModule } from '../shared/shared.module';

@NgModule({
    declarations: [QuestsComponent, QuestCardComponent, QuestDetailsComponent],
    imports: [CommonModule, QuestsRoutingModule, SharedModule],
})
export class QuestsModule {}
