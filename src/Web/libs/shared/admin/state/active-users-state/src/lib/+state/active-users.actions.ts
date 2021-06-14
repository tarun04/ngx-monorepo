import { createAction, props } from '@ngrx/store';
import { ActiveUsers } from '@ngx-monorepo/shared/admin/types';

export const loadActiveUsers = createAction(
  '[ActiveUsers/API] Load ActiveUsers'
);
export const loadActiveUsersSuccess = createAction(
  '[ActiveUsers/API] Load ActiveUsers Success',
  props<{ activeUsers: ActiveUsers }>()
);
export const loadActiveUsersFailure = createAction(
  '[ActiveUsers/API] Load ActiveUsers Failure',
  props<{ error: any }>()
);

export const ActiveUsersActions = {
  loadActiveUsers,
  loadActiveUsersSuccess,
  loadActiveUsersFailure,
};
