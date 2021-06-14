import { createReducer, on, Action } from '@ngrx/store';
import { EntityState, EntityAdapter, createEntityAdapter } from '@ngrx/entity';

import * as ActiveUsersActions from './active-users.actions';
import { ActiveUsers } from '@ngx-monorepo/shared/admin/types';

export const ACTIVE_USERS_FEATURE_KEY = 'activeUsers';

export interface ActiveUsersState extends EntityState<ActiveUsers> {
  selectedId?: string | number; // which ActiveUsers record has been selected
  loaded: boolean; // has the ActiveUsers list been loaded
  error?: string | null; // last known error (if any)
  activeUsers: ActiveUsers;
}

export interface ActiveUsersPartialState {
  readonly [ACTIVE_USERS_FEATURE_KEY]: ActiveUsersState;
}

export const activeUsersAdapter: EntityAdapter<ActiveUsers> = createEntityAdapter<ActiveUsers>();

export const initialState: ActiveUsersState = activeUsersAdapter.getInitialState(
  {
    // set initial required properties
    loaded: false,
    activeUsers: undefined,
  }
);

const activeUsersReducer = createReducer(
  initialState,
  on(ActiveUsersActions.loadActiveUsers, (state) => ({
    ...state,
    loaded: false,
    error: null,
  })),
  on(ActiveUsersActions.loadActiveUsersSuccess, (state, { activeUsers }) =>
    activeUsersAdapter.setOne(activeUsers, {
      ...state,
      loaded: true,
      authenticated: true,
      error: null,
    })
  ),
  on(ActiveUsersActions.loadActiveUsersFailure, (state, { error }) => ({
    ...state,
    error,
  }))
);

export function reducer(state: ActiveUsersState | undefined, action: Action) {
  return activeUsersReducer(state, action);
}
