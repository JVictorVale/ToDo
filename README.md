# To-do List API

### Visão Geral
O objetivo deste projeto é criar uma API Rest usando .NET 6 e EF Core para gerenciar uma lista de tarefas. O sistema incluirá uma página de registro e login para os usuários, e cada usuário terá a capacidade de criar, editar e visualizar suas próprias tarefas sem acesso às tarefas de outros usuários.

> #### Observação:
> A aplicação é organizada em quatro camadas distintas: API, Application, Domain e Infra. Isso garante uma separação clara de responsabilidades e facilita a manutenção do código.

### Tecnologias Utilizadas
- Entity Framework Core
- MySQL
- AutoMapper
- FluentValidation
- ScottBrady91.AspNetCore.Identity.Argon2PasswordHasher

### Registro do Usuário
- Solicita `Name`, `Email` e `Password`.
- Realiza a confirmação do `Password`.
- Verifica se o `Email` já está em uso.
- O `password` será armazenado usando algoritmo de hash.

### Login
- Solicita `Email` e `Password`.
- Verifica se o `Password` informado corresponde ao `Password` armazenado.
- Caso o usuário não seja encontrado ou a senha seja incorreta, retorna um erro: "Usuário ou senha incorretos".
- Após as verificações, gera um token `JWT`.

### Cadastrar uma Lista (AssignmentList)
- Solicita um `nome` para a lista.
- O `UserId` deve será obtido do `JWT`.
- Valida se o `nome` foi preenchido.
- Verifica se o `UserId` não é inválido.

