import { Component, OnInit } from '@angular/core';
import { AccountService } from './account/account.service';

@Component({
    selector: 'app-root',
    templateUrl: './app.component.html',
    styleUrls: ['./app.component.scss'],
})
export class AppComponent implements OnInit {
    title = 'BlueEngagementClient';

    constructor(private accountService: AccountService) {}

    ngOnInit(): void {
        const token = localStorage.getItem('token');
        this.accountService.loadLoggedInUser(token).subscribe();
    }
}
