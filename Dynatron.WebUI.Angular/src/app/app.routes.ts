import { Routes } from "@angular/router";
import { environment } from '../environments/environment';
import { CustomersComponent } from "./customers/customers.component";

export const APP_ROUTES: Routes = [
  {
    path: '', component: CustomersComponent
  },
  {
    path: 'customers',
    loadChildren: () => import('./customers/routes').then(m => m.CUSTOMER_ROUTES)
  }
];
