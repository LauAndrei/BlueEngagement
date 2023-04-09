import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { RESPONSE } from 'src/app/shared/constants/response';
import { AccountService } from '../account.service';

@Component({
    selector: 'app-login',
    templateUrl: './login.component.html',
    styleUrls: ['./login.component.scss'],
})
export class LoginComponent implements OnInit {
    form!: FormGroup;
    formSubmitted: boolean = false;

    constructor(
        private accountService: AccountService,
        private router: Router,
        private toastrService: ToastrService,
    ) {}

    ngOnInit(): void {
        this.form = new FormGroup({
            usernameOrEmail: new FormControl(null, Validators.required),
            password: new FormControl(null, Validators.required),
        });
    }

    onSubmit() {
        this.formSubmitted = true;
        if (this.form.valid) {
            this.accountService.login(this.form.value).subscribe(
                () => {
                    this.router.navigateByUrl('/home');
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
