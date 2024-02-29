import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { FacadeRoutingModule } from './facade-routing.module';
import { FacadeComponent } from './facade.component';
import { MenuComponent } from './menu/menu.component';


@NgModule({
  declarations: [FacadeComponent, MenuComponent],
  imports: [
    CommonModule,
    FacadeRoutingModule
  ]
})
export class FacadeModule { }
