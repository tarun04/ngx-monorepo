import { Injectable } from '@angular/core';
import { createEffect, Actions, ofType } from '@ngrx/effects';
import { fetch } from '@nrwl/angular';
import { map, take } from 'rxjs/operators';

import * as ActiveUsersFeature from './active-users.reducer';
import * as ActiveUsersActions from './active-users.actions';
import { ActiveUsersService } from '@ngx-monorepo/shared/admin/services';

@Injectable()
export class ActiveUsersEffects {
  loadAuthUser$ = createEffect(() =>
    this.actions$.pipe(
      ofType(ActiveUsersActions.loadActiveUsers),
      fetch({
        run: (action) => {
          return this.activeUsersService.getActiveUsers().pipe(
            map((response) => {
              return ActiveUsersActions.loadActiveUsersSuccess({
                activeUsers: response,
              });
            })
          );
        },

        onError: (action, error) => {
          console.error('Error', error);
          return ActiveUsersActions.loadActiveUsersFailure({ error });
        },
      })
    )
  );

  constructor(
    private actions$: Actions,
    private activeUsersService: ActiveUsersService
  ) {}
}
