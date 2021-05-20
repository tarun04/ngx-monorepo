export interface NavRoute {
  subheading?: string;
  subroutes: SubRoute[];
}

export interface SubRoute {
  route: string;
  title: string;
  icon: string;
  isActive: boolean;
}
