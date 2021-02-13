import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NotFoundComponent } from './not-found/not-found.component';
import { SectionHeaderComponent } from './section-header/section-header.component';



@NgModule({
  declarations: [NotFoundComponent, SectionHeaderComponent],
  imports: [
    CommonModule
  ]
})
export class CoreModule { }
