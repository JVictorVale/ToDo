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
- Solicitar `Name`, `Email` e `Password`.
- Realizar confirmação do `Password`.
- Verificar se o `Email` já está em uso.
- O `password` deve ser armazenado usando algum algoritmo de hash.

### Login
- Solicitar `Email` e `Password`.
- Verificar se o `Password` informado corresponde ao `Password` armazenado.
- Caso o usuário não seja encontrado ou a senha seja incorreta, retorne um erro genérico, como "Usuário ou senha incorretos".
- Após as verificações, gere um token `JWT`.

### Cadastrar uma Lista (AssignmentList)
- Solicitar um `nome` para a lista.
- O `UserId` deve ser obtido do `JWT`.
- Valide se o `nome` foi preenchido.
- Verifique se o `UserId` não é inválido.

### Swagger

![image](https://github.com/JVictorVale/todo-list-api/assets/114615104/30b4d40e-b75c-4776-8fb1-481fb92f06c6)
