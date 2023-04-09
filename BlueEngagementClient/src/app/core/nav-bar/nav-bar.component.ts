import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { ILoggedInUser } from '../../shared/models/user';
import { AccountService } from '../../account/account.service';

@Component({
    selector: 'app-nav-bar',
    templateUrl: './nav-bar.component.html',
    styleUrls: ['./nav-bar.component.scss'],
})
export class NavBarComponent implements OnInit {
    currentUser$: Observable<ILoggedInUser>;

    constructor(private accountService: AccountService) {}

    ngOnInit(): void {
        this.currentUser$ = this.accountService.currentUser$;
    }
}
