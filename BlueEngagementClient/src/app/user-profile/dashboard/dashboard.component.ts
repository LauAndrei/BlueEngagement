import { Component } from '@angular/core';
import { AccountService } from '../../account/account.service';
import { ToastrService } from 'ngx-toastr';
import { RESPONSE } from '../../shared/constants/response';

@Component({
    selector: 'app-dashboard',
    templateUrl: './dashboard.component.html',
    styleUrls: ['./dashboard.component.scss'],
})
export class DashboardComponent {
    constructor(
        private accountService: AccountService,
        private toastrService: ToastrService,
    ) {}

    logOut() {
        this.accountService.logOut();
        this.toastrService.success(RESPONSE.ACCOUNT.LOG_OUT.SUCCESS);
    }
}
