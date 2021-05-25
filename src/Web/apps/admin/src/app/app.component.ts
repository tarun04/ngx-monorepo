import { Component } from '@angular/core';
import {
  NavigationEnd,
  NavigationStart,
  Router,
  RouterEvent,
} from '@angular/router';

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
  loading: boolean;

  constructor(router: Router) {
    this.loading = false;
    router.events.subscribe((event: RouterEvent): void => {
      if (event instanceof NavigationStart) {
        this.loading = true;
      } else if (event instanceof NavigationEnd) {
        this.loading = false;
      }
    });
  }
}
