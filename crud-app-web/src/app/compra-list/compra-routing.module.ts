import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { RegistroCompraComponent } from '../registro-compra/registro-compra.component';
import { CompraListComponent } from './compra-list.component';

const routes: Routes = [{path:'',redirectTo:'compra',pathMatch:'full'},{path:'compra',component:CompraListComponent},{path:'registroCompra',component:RegistroCompraComponent}];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class CompraRoutingModule { }
