import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { QuestService } from '../quest.service';
import { ToastrService } from 'ngx-toastr';
import { RESPONSE } from '../../shared/constants/response';
import { AccountService } from '../../account/account.service';
import { ILoggedInUser } from '../../shared/models/user';

@Component({
    selector: 'app-create-quest',
    templateUrl: './create-quest.component.html',
    styleUrls: ['./create-quest.component.scss'],
})
export class CreateQuestComponent implements OnInit {
    form: FormGroup;
    formSubmitted: boolean = false;

    currentUser: ILoggedInUser;

    constructor(
        private questService: QuestService,
        private toastrService: ToastrService,
        private accountService: AccountService,
    ) {}

    ngOnInit(): void {
        this.form = new FormGroup({
            title: new FormControl(null, Validators.required),
            description: new FormControl(null, Validators.required),
            reward: new FormControl(null, Validators.required),
            capacity: new FormControl(null, Validators.required),
        });

        this.accountService.currentUser$.subscribe((user) => {
            this.currentUser = user;
        });
    }

    createQuest() {
        this.formSubmitted = true;
        if (this.form.valid) {
            this.form.value.capacity = +this.form.value.capacity;
            this.form.value.reward = +this.form.value.reward;

            if (
                this.currentUser.score <
                this.form.value.capacity * this.form.value.reward
            ) {
                this.toastrService.error(
                    `You have ${this.currentUser.score} points`,
                    RESPONSE.QUEST.CREATE_QUEST.NOT_ENOUGH_POINTS,
                );
            } else {
                this.questService.createQuest(this.form.value).subscribe(
                    () => {
                        this.toastrService.success(
                            RESPONSE.QUEST.CREATE_QUEST.SUCCESS,
                        );
                        this.form.reset();
                        this.formSubmitted = false;
                    },
                    (err) => {
                        console.log(err);
                        this.toastrService.error(RESPONSE.ERROR);
                    },
                );
            }
        }
    }
}
