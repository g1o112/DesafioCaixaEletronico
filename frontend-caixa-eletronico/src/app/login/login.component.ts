import { Component, EventEmitter, Output } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent {
  loginData = {
    numeroConta: '',
    senha: ''
  };

  @Output() loginSuccess = new EventEmitter<any>();
  @Output() criarConta = new EventEmitter<void>();

  constructor(private http: HttpClient) {}

  FazerLogin() {
    this.http.post<any>('https://localhost:7263/api/conta/login', this.loginData)
      .subscribe({
        next: (response) => {
          console.log('Login bem-sucedido:', response);
          this.loginSuccess.emit(response);
        },
        error: (err) => {
          console.error('Erro ao fazer login:', err);
        }
      });
  }

  mostrarCriarConta() {
    this.criarConta.emit();
  }
}
