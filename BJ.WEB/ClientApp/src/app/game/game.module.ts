import { NgModule }      from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule }   from '@angular/forms';
import { CreateGameComponent } from './create-game/create-game.component';

import { ReactiveFormsModule }   from '@angular/forms';

@NgModule({
  declarations: [CreateGameComponent],
  imports: [
    BrowserModule,
    FormsModule,
    ReactiveFormsModule
  ],
  exports: [
    CreateGameComponent
  ]
})
export class GameModule { }
