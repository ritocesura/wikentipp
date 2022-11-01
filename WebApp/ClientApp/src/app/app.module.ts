import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { HomeComponent } from './home/home.component';
import { ResultsComponent } from './admin/results/results.component';
import { AddMatchComponent } from './admin/results/add-match/add-match.component';
import { OverviewComponent } from './overview/overview.component';
import { BettingOverviewComponent } from './betting-overview/betting-overview.component';
import { PredictionComponent } from './prediction/prediction.component';
import { MatchdayTabComponent } from './prediction/matchday-tab/matchday-tab.component';
import { AuthPopupsModule } from 'src/app/auth-popups/auth-popups.module';
import { CoreModule } from 'src/app/core/core.module';
import { AuthGuard } from 'src/app/core/guards/auth.guard';
import { NZ_I18N } from 'ng-zorro-antd/i18n';
import { de_DE } from 'ng-zorro-antd/i18n';
import { registerLocaleData } from '@angular/common';
import de from '@angular/common/locales/de';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { Role } from './core/models/role';

import { NzSpaceModule } from 'ng-zorro-antd/space';
import { NzBadgeModule } from 'ng-zorro-antd/badge';
import { NzLayoutModule } from "ng-zorro-antd/layout";
import { NzMenuModule } from "ng-zorro-antd/menu";
import { NzDropDownModule } from "ng-zorro-antd/dropdown";
import { NzModalModule } from "ng-zorro-antd/modal";
import { NzMessageModule } from "ng-zorro-antd/message";
import { NzInputModule } from "ng-zorro-antd/input";
import { NzSelectModule } from "ng-zorro-antd/select";
import { NzTableModule } from "ng-zorro-antd/table";
import { NzTagModule } from 'ng-zorro-antd/tag';
import { NzAvatarModule } from 'ng-zorro-antd/avatar';
import { NzDividerModule } from 'ng-zorro-antd/divider';
import { NzPageHeaderModule } from 'ng-zorro-antd/page-header';
import { NzIconModule } from 'ng-zorro-antd/icon';
import { NzTabsModule } from 'ng-zorro-antd/tabs';
import { NzListModule } from 'ng-zorro-antd/list';
import { NzButtonModule } from 'ng-zorro-antd/button';
import { NzInputNumberModule } from 'ng-zorro-antd/input-number';
import { NzFormModule } from 'ng-zorro-antd/form';
import { NzDrawerModule } from 'ng-zorro-antd/drawer';
import { NzDatePickerModule } from 'ng-zorro-antd/date-picker';
import { NzPopconfirmModule } from 'ng-zorro-antd/popconfirm';
import { NzCardModule } from 'ng-zorro-antd/card';

registerLocaleData(de);

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    OverviewComponent,
    BettingOverviewComponent,
    PredictionComponent,
    MatchdayTabComponent,
    ResultsComponent,
    AddMatchComponent,
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    NzLayoutModule,
    NzMenuModule,
    NzDropDownModule,
    NzModalModule,
    NzMessageModule,
    NzInputModule,
    NzSelectModule,
    NzTableModule,
    NzBadgeModule,
    NzSpaceModule,
    NzTagModule,
    NzAvatarModule,
    NzDividerModule,
    NzPageHeaderModule,
    NzIconModule,
    NzTabsModule,
    NzListModule,
    NzButtonModule,
    NzInputNumberModule,
    NzFormModule,
    NzDrawerModule,
    NzDatePickerModule,
    NzPopconfirmModule,
    NzCardModule,

    CoreModule,
    AuthPopupsModule,
    RouterModule.forRoot([
      { path: '', redirectTo: 'home', pathMatch: 'full' },
      { path: 'home', component: HomeComponent, pathMatch: 'full' },
      { path: 'overview', component: OverviewComponent, canActivate: [AuthGuard] },
      { path: 'bettingOverview', component: BettingOverviewComponent, canActivate: [AuthGuard] },
      { path: 'prediction', component: PredictionComponent, canActivate: [AuthGuard] },
      { path: 'results', component: ResultsComponent, canActivate: [AuthGuard], data: { roles: [Role.Admin] } },
      { path: '**', redirectTo: '' },
    ]),
    BrowserAnimationsModule
  ],
  providers: [
    { provide: NZ_I18N, useValue: de_DE }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
