import { Component, OnInit } from '@angular/core';
import { AccountService } from '../../account/account.service';
import { ToastrService } from 'ngx-toastr';
import { RESPONSE } from '../../shared/constants/response';
import { Observable } from 'rxjs';
import { ILoggedInUser } from '../../shared/models/user';

@Component({
    selector: 'app-dashboard',
    templateUrl: './dashboard.component.html',
    styleUrls: ['./dashboard.component.scss'],
})
export class DashboardComponent implements OnInit {

    currentUser$: Observable<ILoggedInUser>

    constructor(
        private accountService: AccountService,
        private toastrService: ToastrService,
    ) {
    }

    ngOnInit(): void {
        this.currentUser$ = this.accountService.currentUser$
    }

    logOut() {
        this.accountService.logOut();
        this.toastrService.success(RESPONSE.ACCOUNT.LOG_OUT.SUCCESS);
    }

}
