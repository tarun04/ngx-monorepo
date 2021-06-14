import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { FlexLayoutModule } from '@angular/flex-layout';
import { MatCardModule } from '@angular/material/card';
import { MatIconModule } from '@angular/material/icon';
import { ActiveUsersStateModule } from '@ngx-monorepo/shared/admin/state/active-users-state';

import { DashboardComponent } from './containers/dashboard/dashboard.component';
import { ActiveUsersComponent } from './components/active-users/active-users.component';

export const routes: Routes = [
  {
    path: '',
    pathMatch: 'full',
    component: DashboardComponent,
  },
];

@NgModule({
  imports: [
    CommonModule,
    RouterModule.forChild(routes),
    FlexLayoutModule,
    MatCardModule,
    MatIconModule,
    ActiveUsersStateModule,
  ],
  declarations: [DashboardComponent, ActiveUsersComponent],
})
export class AdminAnalyticsPageModule {}
