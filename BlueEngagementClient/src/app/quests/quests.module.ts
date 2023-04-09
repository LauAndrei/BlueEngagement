import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { QuestsComponent } from './quests.component';
import { QuestsRoutingModule } from './quests-routing.module';
import { QuestCardComponent } from './quest-card/quest-card.component';

@NgModule({
    declarations: [QuestsComponent, QuestCardComponent],
    imports: [CommonModule, QuestsRoutingModule],
})
export class QuestsModule {}
