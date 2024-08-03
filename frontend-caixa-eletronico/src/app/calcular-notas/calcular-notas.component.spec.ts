import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CalcularNotasComponent } from './calcular-notas.component';

describe('CalcularNotasComponent', () => {
  let component: CalcularNotasComponent;
  let fixture: ComponentFixture<CalcularNotasComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CalcularNotasComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CalcularNotasComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
