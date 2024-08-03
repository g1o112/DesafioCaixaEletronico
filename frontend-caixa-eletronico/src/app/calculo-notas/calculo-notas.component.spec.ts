import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CalculoNotasComponent } from './calculo-notas.component';

describe('CalculoNotasComponent', () => {
  let component: CalculoNotasComponent;
  let fixture: ComponentFixture<CalculoNotasComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CalculoNotasComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CalculoNotasComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
