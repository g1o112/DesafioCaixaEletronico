import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HttpClient } from '@angular/common/http';
import { RouterOutlet } from '@angular/router';
import { SaqueComponent } from './saque/saque.component';
import { CriarContaComponent } from './criar-conta/criar-conta.component';
import { LoginComponent } from './login/login.component';
import { CalcularNotasComponent } from './calcular-notas/calcular-notas.component';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, FormsModule, SaqueComponent, CriarContaComponent, CommonModule, LoginComponent, CalcularNotasComponent],
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  title = 'frontend-caixa-eletronico';
  usuarioLogado: any = null;
  mostrarFormularioCriarConta: boolean = false;
  mostrarCalcularNotas: boolean = true;

  constructor(private http: HttpClient) {}

  onLoginSuccess(usuario: any) {
    this.usuarioLogado = usuario;
    this.mostrarCalcularNotas = false;
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
