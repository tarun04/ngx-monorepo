import { Component, Input, OnInit } from '@angular/core';

import { ActiveUsers } from '@ngx-monorepo/shared/admin/types';

@Component({
  selector: 'active-users',
  templateUrl: './active-users.component.html',
  styleUrls: ['./active-users.component.scss'],
})
export class ActiveUsersComponent implements OnInit {
  @Input()
  activeUsers: ActiveUsers;

  constructor() {}

  ngOnInit(): void {}
}
