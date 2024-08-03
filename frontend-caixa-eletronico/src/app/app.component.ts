import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { RouterOutlet } from '@angular/router';
import { SaqueComponent } from './saque/saque.component';
import { CriarContaComponent } from './criar-conta/criar-conta.component';
import { CalcularNotasComponent } from './calcular-notas/calcular-notas.component';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, FormsModule, SaqueComponent, CriarContaComponent, CalcularNotasComponent, CommonModule],
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  title = 'frontend-caixa-eletronico';
  loginData = {
    numeroConta: '',
    senha: ''
  };
  usuarioLogado: any = null;
  mostrarFormularioCriarConta: boolean = false;
  mostrarCalcularNotas: boolean = true;


  constructor(private http: HttpClient) {}

  FazerLogin() {
    this.http.post<any>('https://localhost:7263/api/conta/login', this.loginData)
      .subscribe({
        next: (response) => {
          console.log('Login bem-sucedido:', response);
          this.usuarioLogado = response;
        },
        error: (err) => {
          console.error('Erro ao fazer login:', err);
        }
      });
  }

  sair() {
    this.usuarioLogado = null; // sair do login
  }

  mostrarCriarConta() {
    this.mostrarFormularioCriarConta = true; // Exibe o formulário de criar conta
  }

  esconderFormularioCriarConta() {
    this.mostrarFormularioCriarConta = false; // Oculta o formulário de criar conta
  }

}
