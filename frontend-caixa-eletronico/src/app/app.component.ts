import { Component } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { RouterOutlet } from '@angular/router';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, FormsModule],
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  title = 'frontend-caixa-eletronico';
  loginData = {
    numeroConta: '',
    senha: ''
  };

  constructor(private http: HttpClient) {}

  FazerLogin() {
    console.log('Dados de login:', this.loginData); // Log os dados de login

    this.http.post<any>('https://localhost:7263/api/conta/login', this.loginData)
      .subscribe({
        next: (response) => {
          console.log('Resposta do servidor:', response);
          // Adicione lógica para lidar com a resposta
          alert('Login bem-sucedido: ' + JSON.stringify(response));
        },
        error: (err) => {
          console.error('Erro ao fazer login:', err);
          alert('Erro ao fazer login: ' + err.message);
          // Adicione lógica para lidar com erros
        }
      });
  }

}
