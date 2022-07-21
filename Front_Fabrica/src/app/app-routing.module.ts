import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AuthComponent } from './auth/auth.component';
import { AuthorizeGuard } from './Authorize/authorize.guard';
import { HomeComponent } from './home/home.component';
import { OrderComponent } from './order/order.component';
import { ProductComponent } from './product/product.component';
import { UserComponent } from './user/user.component';

const routes: Routes = [
  {
    path: 'Home',
    component: HomeComponent
  },
  {
    path: 'Users',
    component: UserComponent,
    canActivate: [AuthorizeGuard]
  },

  {
    path: 'Products',
    component: ProductComponent,
    canActivate: [AuthorizeGuard]
  },

  {
    path: 'Orders',
    component: OrderComponent,
    canActivate: [AuthorizeGuard]
  },

  {
    path: '',
    component: AuthComponent
  }
];


@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
