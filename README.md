# 🎬 CineReview

CineReview é uma Web API em **C#** desenvolvida com **.NET 8** e
**Entity Framework Core**, para registrar mídias (filmes e séries),
permitir que usuários façam reviews, atribuam notas e criem listas de
favoritos.

## ✨ Tecnologias utilizadas

-   C#
-   .NET 8 (Web API)
-   Entity Framework Core
-   SQL Server
-   Domain‑Driven Design (DDD)
-   Table Per Type (TPT)

## 🧱 Arquitetura do Projeto

O projeto segue uma arquitetura limpa com separação de camadas.

### Herança TPT

-   `Media` (classe base)
-   `Movie` e `Serie` (herdam de Media)
-   Tabelas separadas no banco.

## 🚀 Como Rodar o Projeto

### Passos

``` bash
git clone https://github.com/UInfinitu/CineReview.git
cd CineReview
```

Configurar string de conexão em `appsettings.json`.

Criar banco:

``` bash
dotnet ef database update
```

Rodar API:

``` bash
dotnet run
```

## Pesquisa de Mercado

[Link de Acesso](https://docs.google.com/document/d/1BMCbuGBjwu5cxFLjEAYbzHM5Ujw0LStfoViAsKW_-ZA/edit?usp=sharing)

## Diagrama de Classes

<img width="863" height="388" alt="image" src="https://github.com/user-attachments/assets/30c012d2-804d-4e3e-82c1-9cbe89328f32" />

