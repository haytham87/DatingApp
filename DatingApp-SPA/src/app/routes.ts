import {Routes} from '@angular/router';
import { HomeComponent } from './home/home.component';
import { MemberListComponent } from './member/member-list/member-list.component';
import { MessagesComponent } from './messages/messages.component';
import { ListsComponent } from './lists/lists.component';
import { AuthGuard } from './_guards/auth.guard';
import { MemberDetailComponent } from './member/member-detail/member-detail.component';
import { MemberDetailResolvser } from './_resolvers/member-detail.resolver';
import { MemberListResolvser } from './_resolvers/member-list.resolver';
import { MemberEditComponent } from './member/member-Edit/member-Edit.component';
import { MemberEditResolvser } from './_resolvers/member-edit.resolver';
import { PreventUnsavedChanges } from './_guards/prevent-unsaved-changes.guard';

export const appRoutes: Routes = [
    {path: '', component: HomeComponent},
    {
        path: '',
        runGuardsAndResolvers: 'always',
        canActivate: [AuthGuard],
        children: [
            {path: 'members' , component: MemberListComponent , resolve: {users: MemberListResolvser} },
            {path: 'members/:userID' , component: MemberDetailComponent , resolve: {user: MemberDetailResolvser}},
            {path: 'member/edit' , component: MemberEditComponent,
             resolve: {user: MemberEditResolvser} , canDeactivate: [PreventUnsavedChanges]},
            {path: 'messages' , component: MessagesComponent},
            {path: 'lists' , component: ListsComponent},
        ]
    },
    {path: '**' , redirectTo: '' , pathMatch: 'full'},
];
