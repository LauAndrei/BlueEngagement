import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TextInputComponent } from './text-input/text-input.component';
import { ReactiveFormsModule } from '@angular/forms';
import { DateAgoPipe } from './pipes/date-ago.pipe';

@NgModule({
    declarations: [TextInputComponent, DateAgoPipe],
    imports: [CommonModule, ReactiveFormsModule],
    exports: [TextInputComponent, DateAgoPipe],
})
export class SharedModule {}
