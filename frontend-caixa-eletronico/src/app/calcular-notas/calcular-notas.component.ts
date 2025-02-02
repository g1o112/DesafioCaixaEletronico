import { Component } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

interface NotaQuantidades {
  [nota: string]: number;
}

@Component({
  selector: 'app-calcular-notas',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './calcular-notas.component.html',
  styleUrls: ['./calcular-notas.component.scss']
})
export class CalcularNotasComponent {
  valor: number = 10;
  resultado: NotaQuantidades | null = null;
  mensagemErro: string = '';
  totalNotas: number = 0;

  constructor(private http: HttpClient) {}

  calcularNotas() {
    const currentHour = new Date().getHours();
    const saqueLimite = (currentHour >= 22 || currentHour < 6) ? 1000 : 10000;

    if (this.valor % 10 !== 0 || this.valor < 10) {
      this.mensagemErro = 'O valor deve ser um múltiplo de R$ 10 e no mínimo R$ 10.';
      this.resultado = null;
      this.totalNotas = 0;
      return;
    }

    if (this.valor > saqueLimite) {
      this.mensagemErro = `O valor máximo de saque é de R$ ${saqueLimite},00 neste horário.`;
      this.resultado = null;
      this.totalNotas = 0;
      return;
    }

    this.http.post<NotaQuantidades>('https://localhost:7263/api/atm/calcular', { Valor: this.valor })
      .subscribe({
        next: (notas: NotaQuantidades) => {
          this.resultado = notas;
          this.totalNotas = Object.values(notas).reduce((total, qtd) => total + (typeof qtd === 'number' ? qtd : 0), 0); // Calcula o total de notas
          this.mensagemErro = '';
        },
        error: (err) => {
          console.error('Erro ao calcular notas:', err);
          this.mensagemErro = 'Erro ao calcular notas: ' + err.message;
          this.resultado = null;
          this.totalNotas = 0; // Reseta o total de notas em caso de erro
        }
      });
  }
}
