import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CompraListComponent } from './compra-list/compra-list.component';
import { AuthGuard } from './helpers/auth.guard';

// const routes: Routes = [{path:'compras',component:CompraListComponent},
// {path:'',redirectTo:'compras',pathMatch:'full'}];
const routes: Routes = [{path:'',redirectTo:'mlogin',pathMatch:'full'},
{path:'mlogin',loadChildren:()=>import('./login/login.module').then(m=>m.LoginModule)},
{path:'mfacade',loadChildren:()=>import('./facade/facade.module').then(m=>m.FacadeModule),canActivate:[AuthGuard]},
{ path: '**', redirectTo: 'mlogin' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
