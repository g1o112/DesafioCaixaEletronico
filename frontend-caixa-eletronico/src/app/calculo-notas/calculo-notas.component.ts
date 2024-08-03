import { HttpClient } from '@angular/common/http';
import { Component, inject } from '@angular/core';

@Component({
  selector: 'app-calcular-notas',
  standalone: true,
  templateUrl: './calculo-notas.component.html',
  styleUrls: ['./calculo-notas.component.scss']
})
export class CalcularNotasComponent {
  url = 'https://localhost:7263/api/atm/calcular';
  valorSaque: number | undefined;
  resultadoNotas: { [key: number]: number } = {};
  errorMessage: string | undefined;

  http = inject(HttpClient);

  calcularNotas() {
    if (this.valorSaque !== undefined && this.valorSaque >= 10 && this.valorSaque % 10 === 0) {
      this.http.post<{ [key: number]: number }>(this.url, { Valor: this.valorSaque })
        .subscribe({
          next: (response) => {
            this.resultadoNotas = response;
            this.errorMessage = undefined;
          },
          error: (err) => {
            this.errorMessage = `Erro ao calcular notas: ${err.message}`;
            this.resultadoNotas = {};
          }
        });
    } else {
      this.errorMessage = 'O valor deve ser múltiplo de R$ 10 e no mínimo R$ 10.';
      this.resultadoNotas = {};
    }
  }
}
