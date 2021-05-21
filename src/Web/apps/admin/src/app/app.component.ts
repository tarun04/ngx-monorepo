import { Component } from '@angular/core';

import { NavRoute } from '@ngx-monorepo/shared/data-models';

@Component({
  selector: 'admin-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
})
export class AppComponent {
  title = 'admin';
  routes: NavRoute[] = [
    {
      subroutes: [
        {
          title: 'Analytics',
          route: '/analytics',
          icon: 'analytics',
          isActive: true,
        },
        {
          title: 'CRM',
          route: '/crm',
          icon: 'cloud_circle',
          isActive: true,
        },
        {
          title: 'Ecommerce',
          route: '/ecommerce',
          icon: 'shopping_cart',
          isActive: true,
        },
        {
          title: 'Projects',
          route: '/projects',
          icon: 'work_outline',
          isActive: true,
        },
      ],
    },
  ];
}
