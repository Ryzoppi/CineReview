## 🎬 CineReview: Plataforma de Reviews de Filmes e Séries

CineReview é um projeto C# Web API desenvolvido com **.NET 8** e **Entity Framework Core**, focado na gestão e avaliação (reviews) de mídias audiovisuais (Filmes e Séries).

Este projeto demonstra a aplicação de conceitos de **Programação Orientada a Objetos (POO)** avançada, como **herança TPT (Table Per Type)** para modelagem de dados e a utilização do padrão **DDD (Domain-Driven Design)** em sua estrutura.

---

## ✨ Tecnologias Principais

* **Linguagem:** C#
* **Framework:** .NET 8 (Web API)
* **Banco de Dados:** SQL Server
* **ORM:** Entity Framework Core
* **Padrão de Herança:** Table Per Type (TPT)

---

## 🏗️ Estrutura e Arquitetura

Diagrama de Classes:

<img width="880" height="598" alt="image" src="https://github.com/user-attachments/assets/970d6d39-7e34-4036-972a-2d060774555a" />


O projeto utiliza uma estrutura limpa, focada em separar as responsabilidades e implementar o padrão DDD simplificado:

1.  **Modelos (Domain):** Contém as entidades principais (`User`, `Review`, `Media`, `Movie`, `Serie`, etc.).
2.  **Data (Infraestrutura):** Contém o `DataContext` e as configurações de migração do Entity Framework Core.
3.  **Controllers (Aplicação):** Expõe a API através de endpoints HTTP.

### Herança TPT (Table Per Type)

O core da modelagem de mídia é a hierarquia de herança:

* **`Media` (Classe Abstrata):** Contém atributos comuns a Filmes e Séries (Ex: `Name`, `Synopsis`).
* **`Movie` (Derivada):** Possui atributos específicos de Filmes (Ex: `Duration`).
* **`Serie` (Derivada):** Possui atributos específicos de Séries.

Esta configuração garante que o banco de dados crie tabelas separadas (`Media`, `Movies`, `Series`) e que a tabela `Reviews` possa se relacionar de forma polimórfica com a tabela base `Media`.

---

## 🚀 Como Rodar o Projeto

### Pré-requisitos

* .NET 8 SDK
* SQL Server (LocalDB ou Instância)
* Uma IDE (Visual Studio ou VS Code)

### Passos de Configuração

1.  **Clone o Repositório:**
    ```bash
    git clone [https://github.com/UInfinitu/CineReview.git](https://github.com/UInfinitu/CineReview.git)
    cd CineReview
    ```

2.  **Configuração do Banco de Dados:**
    * Abra o arquivo `appsettings.json`.
    * Defina sua string de conexão (`"ConnectionStrings"`). Certifique-se de que o SQL Server esteja acessível.
    
    > **Exemplo (SQL Server LocalDB):**
    > `"DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=CineReviewDB;Trusted_Connection=True;MultipleActiveResultSets=true"`

3.  **Aplicar Migrações:**
    * Abra o terminal na pasta raiz do projeto.
    * Aplique as migrações para criar o esquema do banco de dados (incluindo as tabelas TPT):
    
    ```bash
    dotnet ef database update
    ```

4.  **Executar a API:**
    ```bash
    dotnet run
    ```

A API estará acessível, geralmente na porta `5000` ou `7000`. Você pode usar o **Swagger UI** (acessível em `/swagger`) para testar os endpoints.

---

## 🔗 Endpoints Principais (Exemplo)

| Ação | Método | Endpoint | Descrição |
| :--- | :--- | :--- | :--- |
| **Criar Filme** | `POST` | `/api/Movies` | Adiciona um novo filme ao sistema (cria registros em `Media` e `Movies`). |
| **Criar Review**| `POST` | `/api/Reviews` | Adiciona um review a uma mídia existente (relacionamento com `MediaId`). |
| **Obter Mídia** | `GET` | `/api/Media/{id}`| Obtém detalhes da mídia. O EF Core carrega o tipo correto (`Movie` ou `Serie`). |
| **Autenticação** | `POST` | `/api/Users/login` | Endpoint de autenticação de usuário. |
