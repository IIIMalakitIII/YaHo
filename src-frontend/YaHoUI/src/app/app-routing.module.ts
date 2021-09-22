import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';


const routes: Routes = [
  {
    path: '',
    redirectTo: 'project',
    pathMatch: 'full'
  },
  {
    path: 'account',
    loadChildren: () =>
      import('./features/account/account.module').then(m => m.AccountModule)
  },
  {
    path: 'order',
    loadChildren: () =>
      import('./features/order-info/order-info.module').then(m => m.OrderInfoModule)
  },
  {
    path: 'auth',
    loadChildren: () =>
      import('./features/authentication/authentication.module').then(m => m.AuthenticationModule)
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
