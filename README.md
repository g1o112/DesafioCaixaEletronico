# Desafio Caixa Eletrônico

## Descrição

Este repositório contém a solução para o desafio técnico referente à vaga de estágio. O desafio envolve a implementação de um sistema de caixa eletrônico que realiza saques e calcula a quantidade mínima de notas a serem entregues. O projeto utiliza MongoDB para o banco de dados, C# .NET para o backend e Angular para o frontend.

## Tecnologias Utilizadas

- **Backend**: C# .NET
- **Frontend**: Angular
- **Banco de Dados**: MongoDB

## Funcionalidades

- **Login**: Permite o acesso ao sistema com número de conta e senha.
- **Criar Conta**: Permite a criação de uma nova conta com saldo inicial.
- **Realizar Saque**: Permite a realização de saques e calcula a quantidade mínima de notas a serem entregues.
- **Visualização de Saldo**: Exibe o saldo atual do usuário após o login.
- **Controle de Acesso**: Verifica o horário e o saldo disponível antes de permitir o saque.

## Regras do Caixa Eletrônico

- **Notas Disponíveis**: O caixa utiliza notas de R$ 50, R$ 20 e R$ 10.
- **Valor Mínimo de Saque**: O valor de saque mínimo é de R$ 10.
- **Múltiplo de 10**: O valor do saque deve ser múltiplo de 10.
- **Limite de Saque**:
  - Das 22h às 06h: Limite de R$ 1.000,00.
  - Das 06h01 às 21h59: Limite de R$ 10.000,00.
