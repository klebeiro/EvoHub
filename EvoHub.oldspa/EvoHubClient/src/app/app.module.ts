import { BrowserModule } from '@angular/platform-browser';
import { NgModule, LOCALE_ID } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { registerLocaleData } from '@angular/common';
import localePt from '@angular/common/locales/pt';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './components/nav-menu/nav-menu.component';
import { RepositoryListComponent } from './components/repository-list/repository-list.component';
import { MyRepositoriesComponent } from './pages/my-repositories/my-repositories.component';
import { OtherRepositoriesComponent } from './pages/other-repos/other-repositories.component';

registerLocaleData(localePt);

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    MyRepositoriesComponent,
    OtherRepositoriesComponent,
    RepositoryListComponent,
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      { path: '', redirectTo: 'meus-repositorios', pathMatch: 'full' },
      { path: 'meus-repositorios', component: MyRepositoriesComponent },
      { path: 'outros-repositorios', component: OtherRepositoriesComponent, pathMatch: 'full' },
    ])
  ],
  providers: [
    { provide: LOCALE_ID, useValue: 'pt-BR' }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
