import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { StoreModule } from '@ngrx/store';
import { EffectsModule } from '@ngrx/effects';
import * as fromActiveUsers from './+state/active-users.reducer';
import { ActiveUsersEffects } from './+state/active-users.effects';

@NgModule({
  imports: [
    CommonModule,
    StoreModule.forFeature(
      fromActiveUsers.ACTIVE_USERS_FEATURE_KEY,
      fromActiveUsers.reducer
    ),
    EffectsModule.forFeature([ActiveUsersEffects]),
  ],
})
export class ActiveUsersStateModule {}
