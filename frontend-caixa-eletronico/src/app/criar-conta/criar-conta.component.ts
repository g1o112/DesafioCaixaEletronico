import { Component, EventEmitter, Output } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-criar-conta',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './criar-conta.component.html',
  styleUrls: ['./criar-conta.component.scss']
})
export class CriarContaComponent {
  nome: string = '';
  saldo: number = 0;
  senha: string = '';
  mensagemErro: string = '';
  mensagemSucesso: string = '';

  @Output() contaCriada = new EventEmitter<void>();
  @Output() voltar = new EventEmitter<void>();


  constructor(private http: HttpClient) {}

  criarConta() {
    const numeroConta = Math.random().toString().slice(2, 10); // gera nmr de conta aleatório

    const criarContaRequest = {
      Nome: this.nome,
      Saldo: this.saldo,
      Senha: this.senha,
      NumeroConta: numeroConta
    };

    this.http.post<any>('https://localhost:7263/api/conta/criar', criarContaRequest)
      .subscribe({
        next: (resultado) => {
          this.mensagemSucesso = `Conta criada com sucesso! Número da conta: ${numeroConta}`;
          this.mensagemErro = '';
          this.contaCriada.emit(); //conta criada c sucesso
        },
        error: (err) => {
          console.error('Erro ao criar conta:', err);
          this.mensagemErro = 'Erro ao criar conta: ' + err.message;
          this.mensagemSucesso = '';
        }
      });
  }

  Voltar() {
    this.voltar.emit();
  }
}
