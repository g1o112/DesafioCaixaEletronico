import { Component, Input } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

interface NotaQuantidades {
  [nota: string]: number;
}

@Component({
  selector: 'app-saque',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './saque.component.html',
  styleUrls: ['./saque.component.scss']
})
export class SaqueComponent {
  @Input() usuarioLogado: any;
  @Input() sair!: () => void;

  valorSaque: number = 0;
  resultadoSaque: string = '';
  mensagemErro: string = '';

  constructor(private http: HttpClient) {}

  realizarSaque() {
    if (!this.usuarioLogado) {
      this.mensagemErro = 'Você precisa estar logado para realizar um saque.';
      return;
    }

    const currentHour = new Date().getHours();
    const saqueLimite = (currentHour >= 22 || currentHour < 6) ? 1000 : 10000;

    if (this.valorSaque % 10 !== 0 || this.valorSaque < 10) {
      this.mensagemErro = 'O valor do saque deve ser múltiplo de 10 e no mínimo R$10.';
      return;
    }

    if (this.valorSaque > saqueLimite) {
      this.mensagemErro = `O valor máximo de saque é de R$ ${saqueLimite},00 neste horário.`;
      return;
    }

    if (this.valorSaque > this.usuarioLogado.saldo) {
      this.mensagemErro = 'Saldo insuficiente para realizar este saque.';
      return;
    }

    const saqueRequest = { Valor: this.valorSaque };
    console.log('Enviando solicitação com payload:', saqueRequest);

    this.http.post<NotaQuantidades>('https://localhost:7263/api/atm/calcular', saqueRequest)
      .subscribe({
        next: (resultado) => {
          console.log('Resposta recebida:', resultado);
          this.exibirResultadoSaque(resultado);
          this.usuarioLogado.saldo -= this.valorSaque; // att o saldo do usuário localmente
          this.mensagemErro = '';
        },
        error: (err) => {
          console.error('Erro ao realizar saque:', err);
          this.mensagemErro = 'Erro ao realizar saque: ' + err.message;
        }
      });
  }

  exibirResultadoSaque(notas: NotaQuantidades) {
    let resultado = '';
    let totalNotas = 0;

    for (const [nota, quantidade] of Object.entries(notas)) {
      resultado += `${quantidade} nota(s) de R$ ${nota}<br>`;
      totalNotas += quantidade;
    }
    resultado += `<strong>Total de notas: ${totalNotas}</strong><br>`;
    this.resultadoSaque = resultado;
  }
}
