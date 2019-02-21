import { Component, OnInit } from '@angular/core';
import { FormGroup, Validators, FormBuilder } from '@angular/forms';
import { StartGameView } from 'src/app/shared/entities/game.views/start.game.view';
import { GameDataService } from 'src/app/shared/services/game/game-data.service';

@Component({
  selector: 'app-create-game',
  templateUrl: './create-game.component.html',
  styleUrls: ['./create-game.component.scss']
})
export class CreateGameComponent implements OnInit {

  constructor( private fb: FormBuilder,private dataService: GameDataService) { }

  startGameForm: FormGroup;

  ngOnInit() {
    this.startGameForm = this.fb.group({
      numberOfBots: [1, [Validators.required, Validators.min(1), Validators.max(100), Validators.pattern("^[0-9]+$")]]
    });
    this.startGameForm.valueChanges.subscribe((value) => console.log(value));
    this.startGameForm.statusChanges.subscribe((status) => {
      console.log(this.startGameForm.errors);
      console.log(status);
    })
    debugger
  }

  isControlInvalid(controlName: string): boolean {
    const control = this.startGameForm.controls[controlName];
    const result = control.invalid && control.touched;
    return result;
  }


  onSubmit() {
    const controls = this.startGameForm.controls;
    
     /** Проверяем форму на валидность */ 
     if (this.startGameForm.invalid) {
      /** Если форма не валидна, то помечаем все контролы как touched*/
      Object.keys(controls)
       .forEach(controlName => controls[controlName].markAsTouched());
       
       /** Прерываем выполнение метода*/
       return;
      }
    
     /** TODO: Обработка данных формы */
     console.log(this.startGameForm.value);
     let startGameView = new StartGameView();
     startGameView.numberOfBots=this.startGameForm.controls.numberOfBots.value;
     this.dataService.startGame(startGameView).subscribe(x=>x);
    }
}
