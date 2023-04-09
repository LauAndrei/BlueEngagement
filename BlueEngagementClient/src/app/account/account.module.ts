import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { AccountRoutingModule } from './account-routing.module';
import { AccountComponent } from './account.component';
import { RouterOutlet } from '@angular/router';

@NgModule({
    declarations: [AccountComponent, LoginComponent, RegisterComponent],
    imports: [CommonModule, AccountRoutingModule, RouterOutlet],
})
export class AccountModule {}
