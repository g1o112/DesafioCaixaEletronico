import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { RouterOutlet } from '@angular/router';
import { SaqueComponent } from './saque/saque.component';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, FormsModule, SaqueComponent, CommonModule],
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

  constructor(private http: HttpClient) {}

  FazerLogin() {
    this.http.post<any>('https://localhost:7263/api/conta/login', this.loginData)
      .subscribe({
        next: (response) => {
          console.log('Login bem-sucedido:', response);
          this.usuarioLogado = response; // Atualiza a propriedade para mostrar o conteúdo condicional
        },
        error: (err) => {
          console.error('Erro ao fazer login:', err);
        }
      });
  }

  sair() {
    this.usuarioLogado = null; // Redefine o estado de login
  }

}
