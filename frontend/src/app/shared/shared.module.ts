import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PagerComponent } from './components/pager/pager.component';
import { PagingHeaderComponent } from './components/paging-header/paging-header.component';
import { TextInputComponent } from './components/text-input/text-input.component';



@NgModule({
  declarations: [PagerComponent, PagingHeaderComponent, TextInputComponent],
  imports: [
    CommonModule
  ]
})
export class SharedModule { }
