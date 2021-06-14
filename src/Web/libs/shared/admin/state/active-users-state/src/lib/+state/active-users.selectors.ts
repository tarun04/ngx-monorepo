import { createFeatureSelector, createSelector } from '@ngrx/store';
import {
  ACTIVE_USERS_FEATURE_KEY,
  ActiveUsersState,
  ActiveUsersPartialState,
  activeUsersAdapter,
} from './active-users.reducer';

// Lookup the 'ActiveUsers' feature state managed by NgRx
export const getActiveUsersState = createFeatureSelector<
  ActiveUsersPartialState,
  ActiveUsersState
>(ACTIVE_USERS_FEATURE_KEY);

const { selectAll, selectEntities } = activeUsersAdapter.getSelectors();

export const getActiveUsersLoaded = createSelector(
  getActiveUsersState,
  (state: ActiveUsersState) => state.loaded
);

export const getActiveUsersError = createSelector(
  getActiveUsersState,
  (state: ActiveUsersState) => state.error
);

export const getAllActiveUsers = createSelector(
  getActiveUsersState,
  (state: ActiveUsersState) => selectAll(state)
);

export const getActiveUsersEntities = createSelector(
  getActiveUsersState,
  (state: ActiveUsersState) => selectEntities(state)
);

export const getSelectedId = createSelector(
  getActiveUsersState,
  (state: ActiveUsersState) => state.selectedId
);

export const getSelected = createSelector(
  getActiveUsersEntities,
  getSelectedId,
  (entities, selectedId) => selectedId && entities[selectedId]
);

export const getActiveUsers = createSelector(
  getActiveUsersState,
  (state: ActiveUsersState) => state.activeUsers
);

export const ActiveUsersQuery = {
  getActiveUsersLoaded,
  getActiveUsers,
};
