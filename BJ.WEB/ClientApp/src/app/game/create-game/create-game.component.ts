import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { HttpClient, HttpHeaders } from '@angular/common/http';

@Component({
  selector: 'app-create-game',
  templateUrl: './create-game.component.html',
  styleUrls: ['./create-game.component.scss']
})
export class CreateGameComponent implements OnInit {

  constructor(private http: HttpClient) { }

  private url = "/Game/Start";
  myForm: FormGroup = new FormGroup({

    "NumberOfBots": new FormControl(1, Validators.required),

  });
  ngOnInit() {
  }
  submit() {
    let credentials = JSON.stringify(this.myForm.value);

    debugger;
    let model = {
      numberOfBots:this.myForm.controls['NumberOfBots'].value
    };
    debugger
    this.http.post(this.url, model).subscribe(x=>{
      console.log(x);
    })
  }
}
