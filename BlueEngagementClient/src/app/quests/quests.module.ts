import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { QuestsComponent } from './quests.component';
import { QuestsRoutingModule } from './quests-routing.module';
import { QuestCardComponent } from './quest-card/quest-card.component';
import { QuestDetailsComponent } from './quest-details/quest-details.component';
import { SharedModule } from '../shared/shared.module';
import { CreateQuestComponent } from './create-quest/create-quest.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

@NgModule({
    declarations: [
        QuestsComponent,
        QuestCardComponent,
        QuestDetailsComponent,
        CreateQuestComponent,
    ],
    imports: [
        CommonModule,
        QuestsRoutingModule,
        SharedModule,
        ReactiveFormsModule,
        FormsModule,
    ],
    exports: [QuestCardComponent],
})
export class QuestsModule {}
