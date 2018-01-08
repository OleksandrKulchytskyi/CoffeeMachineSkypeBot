import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Routes, RouterModule } from '@angular/router';
import { CoreModule } from '../app/core/core.module';

// containers
import * as fromContainers from './containers';
// services
import * as fromServices from './services';

export const ROUTES: Routes = [
    { path: '', redirectTo: 'login', pathMatch: 'full'},
    { path: 'login', component: fromContainers.LoginComponent }
];

@NgModule( {
    imports: [
        CommonModule,
        FormsModule,
        CoreModule,
        RouterModule.forChild(ROUTES),
    ],
    declarations: [...fromContainers.containers],
    exports: [...fromContainers.containers, RouterModule],
    providers: [...fromServices.services]
}
)

export class LoginModule {
}
