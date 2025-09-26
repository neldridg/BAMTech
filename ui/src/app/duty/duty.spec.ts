import { ComponentFixture, TestBed } from '@angular/core/testing';

import { Duty } from './duty';

describe('Duty', () => {
  let component: Duty;
  let fixture: ComponentFixture<Duty>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [Duty]
    })
    .compileComponents();

    fixture = TestBed.createComponent(Duty);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
