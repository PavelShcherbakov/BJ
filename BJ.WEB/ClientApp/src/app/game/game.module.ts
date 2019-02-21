import { NgModule }      from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule,ReactiveFormsModule }   from '@angular/forms';
import { CreateGameComponent } from './create-game/create-game.component';
// import { BrowserModule } from '@angular/platform-browser';

@NgModule({
  declarations: [CreateGameComponent],
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule
  ],
  exports: [
    CreateGameComponent
  ]
})
export class GameModule { }
