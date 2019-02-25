import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { BotHandComponent } from './bot-hand.component';

describe('BotHandComponent', () => {
  let component: BotHandComponent;
  let fixture: ComponentFixture<BotHandComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ BotHandComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(BotHandComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
