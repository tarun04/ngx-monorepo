import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { Store } from '@ngrx/store';

import {
  ActiveUsersActions,
  ActiveUsersQuery,
} from '@ngx-monorepo/shared/admin/state/active-users-state';
import { ActiveUsers } from '@ngx-monorepo/shared/admin/types';
import { filter, takeWhile, tap } from 'rxjs/operators';

@Component({
  selector: 'ngx-monorepo-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss'],
})
export class DashboardComponent implements OnInit {
  activeUsers$: Observable<ActiveUsers>;
  activeUsersLoaded$: Observable<boolean>;

  constructor(private activeUsersStore: Store) {}

  ngOnInit(): void {
    this.initActiveUsers();
  }

  private initActiveUsers(): void {
    this.activeUsers$ = this.activeUsersStore.select(
      ActiveUsersQuery.getActiveUsers
    );

    this.activeUsersStore
      .select(ActiveUsersQuery.getActiveUsersLoaded)
      .pipe(
        filter((isLoaded) => !isLoaded),
        tap((_) => {
          this.activeUsersStore.dispatch(ActiveUsersActions.loadActiveUsers());
        })
      )
      .subscribe();

    this.activeUsersLoaded$ = this.activeUsersStore.select(
      ActiveUsersQuery.getActiveUsersLoaded
    );
  }
}
