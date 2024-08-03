import { Component } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-calcular-notas',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './calcular-notas.component.html',
  styleUrls: ['./calcular-notas.component.scss']
})
export class CalcularNotasComponent {
  valor: number = 10;
  resultado: any = null;
  mensagemErro: string = '';

  constructor(private http: HttpClient) {}

  calcularNotas() {
      if (this.valor % 10 !== 0 || this.valor < 10) {
        this.mensagemErro = 'O valor deve ser um múltiplo de R$ 10 e no mínimo R$ 10.';
        this.resultado = null;
        return;
      }
    this.http.post<any>('https://localhost:7263/api/atm/calcular', { Valor: this.valor })
      .subscribe({
        next: (notas) => {
          this.resultado = notas;
          this.mensagemErro = '';
        },
        error: (err) => {
          console.error('Erro ao calcular notas:', err);
          this.mensagemErro = 'Erro ao calcular notas: ' + err.message;
          this.resultado = null;
        }
      });
  }
}
