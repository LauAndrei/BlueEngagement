import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { RESPONSE } from '../../shared/constants/response';
import { AccountService } from '../account.service';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

@Component({
    selector: 'app-register',
    templateUrl: './register.component.html',
    styleUrls: ['./register.component.scss'],
})
export class RegisterComponent implements OnInit {
    form!: FormGroup;
    formSubmitted: boolean = false;

    constructor(
        private accountService: AccountService,
        private router: Router,
        private toastrService: ToastrService,
    ) {}

    ngOnInit(): void {
        this.form = new FormGroup({
            firstName: new FormControl(null, Validators.required),
            lastName: new FormControl(null, Validators.required),
            username: new FormControl(null, Validators.required),
            email: new FormControl(null, Validators.required),
            password: new FormControl(null, Validators.required),
        });
    }

    onSubmit() {
        this.formSubmitted = true;
        if (this.form.valid) {
            this.accountService.register(this.form.value).subscribe(
                () => {
                    this.router.navigateByUrl('/tasks');
                    this.toastrService.success(RESPONSE.SUCCESS);
                },
                (err) => {
                    this.toastrService.error(RESPONSE.ERROR);
                    console.log(err);
                    console.log(this.form.value);
                },
            );
        }
    }
}
