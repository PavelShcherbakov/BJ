import { Component, OnInit } from '@angular/core';
import { FormGroup, Validators, FormBuilder } from '@angular/forms';
import { StartGameView } from 'src/app/shared/entities/game.views/start.game.view';
import { GameDataService } from 'src/app/shared/services/game/game-data.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-create-game',
  templateUrl: './create-game.component.html',
  styleUrls: ['./create-game.component.scss']
})
export class CreateGameComponent implements OnInit {

  constructor(private fb: FormBuilder, private dataService: GameDataService, private router: Router) { }

  private startGameForm: FormGroup;

  public ngOnInit() {
    this.startGameForm = this.fb.group({
      numberOfBots: [1, [Validators.required, Validators.min(1), Validators.max(100), Validators.pattern('^[0-9]+$')]]
    });
  }

  private isControlInvalid(controlName: string): boolean {
    const control = this.startGameForm.controls[controlName];
    const result = control.invalid && control.touched;
    return result;
  }

  private onSubmit(): void {
    let startGameView = new StartGameView();
    startGameView = { ...this.startGameForm.value };
    this.dataService.startGame(startGameView).subscribe(
      x => {
        if (x.state === 0) {
          this.router.navigate(['/game/table']);
        } else {
          this.router.navigate(['/history/game', x.gameId]);
        }
      }
    );
  }

  private setControl(range: number): void {
    this.startGameForm.controls.numberOfBots.setValue(range);
  }

}
