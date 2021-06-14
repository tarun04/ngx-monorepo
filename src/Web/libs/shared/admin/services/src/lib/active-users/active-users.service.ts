import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

import { ActiveUsers } from '@ngx-monorepo/shared/admin/types';
import { activeUsersEndpoints } from './active-users.endpoints';

@Injectable({
  providedIn: 'root',
})
export class ActiveUsersService {
  constructor(private http: HttpClient) {}

  getActiveUsers(): Observable<ActiveUsers> {
    return this.http.get<ActiveUsers>(activeUsersEndpoints.activeUsers);
  }
}
