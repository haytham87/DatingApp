import { BrowserModule, HammerGestureConfig, HAMMER_GESTURE_CONFIG } from '@angular/platform-browser';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { TabsModule } from 'ngx-bootstrap/tabs';
import { RouterModule } from '@angular/router';
import { appRoutes } from './routes';
import { JwtModule } from '@auth0/angular-jwt';
import { HttpClientModule } from '@angular/common/http';
import { NgModule,Component, enableProdMode  } from '@angular/core';
import { AlertifyService } from './_services/alertify.service';
import { MemberEditResolvser } from './_resolvers/member-edit.resolver';
import { PreventUnsavedChanges } from './_guards/prevent-unsaved-changes.guard';
import { AppComponent } from './app.component';
import { MemberDetailResolvser } from './_resolvers/member-detail.resolver';
import { MemberListResolvser } from './_resolvers/member-list.resolver';
import { NgxGalleryModule } from '@kolkov/ngx-gallery';
import {FormsModule} from '@angular/forms';
import { AuthService } from './_services/auth.service';
import { ErrorInterceptorProvider } from './_services/error.interceptor';
import { FileUploadModule } from 'ng2-file-upload';
import { DxDataGridModule } from 'devextreme-angular';
import { platformBrowserDynamic } from '@angular/platform-browser-dynamic';

import { NavComponent } from './nav/nav.component';
import { MemberDetailComponent } from './member/member-detail/member-detail.component';
import { HomeComponent } from './home/home.component';
import { RegisterComponent } from './register/register.component';
import { MemberEditComponent } from './member/member-Edit/member-Edit.component';
import { MemberListComponent } from './member/member-list/member-list.component';
import {PhotoEditorComponent} from './member/photo-editor/photo-editor.component';
import { ListsComponent } from './lists/lists.component';
import { MessagesComponent } from './messages/messages.component';
import { MemberCardComponent } from './member/member-card/member-card.component';


export function tokenGetter() {
   return localStorage.getItem('token');
}

// export class CustomHammerConfig extends HammerGestureConfig {
//    overrides = {
//       pinch: { enable: false},
//       rotate: {enable: false}
//    };
// }

@NgModule({
   declarations: [
      AppComponent,
      NavComponent,
      HomeComponent,
      RegisterComponent,
      MemberListComponent,
      ListsComponent,
      MessagesComponent,
      MemberCardComponent,
      MemberDetailComponent,
      MemberEditComponent,
      PhotoEditorComponent
   ],
   imports: [
      BrowserModule,
      HttpClientModule,
      FormsModule,
      BrowserAnimationsModule,
      NgxGalleryModule,
      TabsModule.forRoot(),
      BsDropdownModule.forRoot(),
      RouterModule.forRoot(appRoutes),
      JwtModule.forRoot({
         config: {
            tokenGetter,
            whitelistedDomains: ['localhost:5000'],
            blacklistedRoutes: ['localhost:5000/api/auth']
         }
      }),
      FileUploadModule,
      DxDataGridModule
   ],
   providers: [
      AuthService,
      ErrorInterceptorProvider,
      AlertifyService,
      MemberDetailResolvser,
      MemberListResolvser,
      MemberEditResolvser,
      PreventUnsavedChanges
      // {provide: HAMMER_GESTURE_CONFIG, useClass: CustomHammerConfig}
   ],
   bootstrap: [
      AppComponent
   ]
})
export class AppModule { }
platformBrowserDynamic().bootstrapModule(AppModule);