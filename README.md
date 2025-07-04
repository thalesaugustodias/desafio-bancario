# Desafio Técnico - Sistema Bancário

## Resumo Executivo

Este projeto implementa um sistema bancário completo seguindo as melhores práticas de desenvolvimento .NET, incluindo Clean Architecture, DDD (Domain-Driven Design), SOLID e Clean Code.

## Estrutura do Projeto

### Arquitetura em Camadas

```
DesafioTecnico/
├── DesafioTecnico.Domain/           # Camada de Domínio
│   ├── Entities/                    # Entidades de negócio
│   ├── Repositories/                # Interfaces de repositório
│   └── Exceptions/                  # Exceções de negócio
├── DesafioTecnico.Application/      # Camada de Aplicação
│   ├── Services/                    # Serviços de aplicação
│   └── DTOs/                        # Objetos de transferência de dados
├── DesafioTecnico.Infrastructure/   # Camada de Infraestrutura
│   └── Repositories/                # Implementações dos repositórios
├── DesafioTecnico.API/              # Camada de Apresentação
│   └── Controllers/                 # Controllers da API
└── DesafioTecnico.Tests/            # Testes unitários
```

## Questões Implementadas

### Questão 1: Classe ContaBancaria
- **Funcionalidade**: Sistema de conta bancária com depósito e saque
- **Características**:
  - Número da conta imutável
  - Nome do titular mutável
  - Saldo protegido com operações controladas
  - Taxa de saque de R$ 3,50
  - Permite saldo negativo

### Questão 2: API de Futebol
- **Funcionalidade**: Consulta de gols marcados por times
- **Características**:
  - Integração com API externa
  - Paginação automática
  - Cálculo de gols como team1 e team2
  - Resultados para PSG (2013) e Chelsea (2014)

### Questão 3: Git
- **Resposta**: style.css, apenas
- **Explicação**: Após a sequência de comandos git, apenas o style.css permanece no diretório além do README.md

### Questão 4: Query SQL
```sql
SELECT assunto, ano, COUNT(*) as quantidade
FROM atendimentos
GROUP BY assunto, ano
HAVING COUNT(*) > 3
ORDER BY ano DESC, quantidade DESC;
```

### Questão 5: API REST Bancária
- **Funcionalidades**:
  - Movimentação de conta corrente
  - Consulta de saldo
- **Características**:
  - Clean Architecture com 4 camadas
  - Domain-Driven Design
  - Princípios SOLID
  - Validações de negócio robustas
  - Documentação Swagger completa
  - Testes unitários
  - Dapper para acesso a dados
  - SQLite como banco de dados

## Tecnologias Utilizadas

### Questão 1
- **.NET 8**: Framework principal
- **C#**: Linguagem de programação
- **Console Application**: Tipo de aplicação

### Questão 2
- **.NET 8**: Framework principal
- **HttpClient**: Para requisições HTTP
- **Newtonsoft.Json**: Para deserialização JSON

### Questão 5 (Principal)
- **.NET 8**: Framework principal
- **ASP.NET Core**: Para API REST
- **Dapper**: Micro-ORM para acesso a dados
- **SQLite**: Banco de dados
- **Swagger/OpenAPI**: Documentação da API
- **MSTest**: Framework de testes
- **Moq**: Biblioteca de mocking

## Padrões Implementados

### Clean Architecture
- **Domain Layer**: Entidades, interfaces e regras de negócio
- **Application Layer**: Casos de uso e serviços de aplicação
- **Infrastructure Layer**: Implementações de repositórios e acesso a dados
- **Presentation Layer**: Controllers e DTOs

### SOLID Principles
- **S**ingle Responsibility: Cada classe tem uma única responsabilidade
- **O**pen/Closed: Aberto para extensão, fechado para modificação
- **L**iskov Substitution: Interfaces bem definidas
- **I**nterface Segregation: Interfaces específicas e coesas
- **D**ependency Inversion: Inversão de dependências com injeção

### Domain-Driven Design
- **Entities**: ContaCorrente, Movimento
- **Value Objects**: TipoMovimento (enum)
- **Domain Services**: Lógica de negócio encapsulada
- **Repositories**: Abstração de acesso a dados
- **Domain Exceptions**: Exceções específicas de negócio

## Validações de Negócio

### Movimentação
- ✅ Apenas contas cadastradas podem receber movimentação
- ✅ Apenas contas ativas podem receber movimentação
- ✅ Apenas valores positivos são aceitos
- ✅ Apenas tipos 'C' (Crédito) ou 'D' (Débito) são aceitos

### Consulta de Saldo
- ✅ Apenas contas cadastradas podem consultar saldo
- ✅ Apenas contas ativas podem consultar saldo
- ✅ Fórmula: SALDO = SOMA_CRÉDITOS - SOMA_DÉBITOS
- ✅ Retorna 0.00 se não houver movimentações

## Endpoints da API

### POST /api/contacorrente/movimentacao
Realiza movimentação em conta corrente

**Request Body:**
```json
{
  "idRequisicao": "string",
  "idContaCorrente": "string",
  "valor": 100.00,
  "tipoMovimento": "C"
}
```

**Response 200:**
```json
{
  "idMovimento": "guid-do-movimento"
}
```

### GET /api/contacorrente/saldo/{idContaCorrente}
Consulta saldo da conta corrente

**Response 200:**
```json
{
  "numeroContaCorrente": 123,
  "nomeTitular": "Katherine Sanchez",
  "dataHoraConsulta": "2025-07-03T10:30:00",
  "valorSaldo": 250.75
}
```

## Tratamento de Erros

### Response 400 (Bad Request):
```json
{
  "message": "Descrição do erro",
  "type": "INVALID_ACCOUNT | INACTIVE_ACCOUNT | INVALID_VALUE | INVALID_TYPE"
}
```

## Como Executar

### Pré-requisitos
- .NET 8 SDK
- Visual Studio 2022 ou VS Code

### Executar API (Questão 5)
```bash
cd DesafioTecnico.Questao5.API
dotnet run
```

### Executar Testes
```bash
cd DesafioTecnico.Questao5.Teste
dotnet test
```

### Acessar Swagger
- URL: `http://localhost:5001`
- Documentação interativa completa

## Estrutura de Dados

### Tabela: contacorrente
| Campo | Tipo | Descrição |
|-------|------|-----------|
| idcontacorrente | TEXT(37) | ID único da conta |
| numero | INTEGER(10) | Número da conta |
| nome | TEXT(100) | Nome do titular |
| ativo | INTEGER(1) | Status da conta (0=inativa, 1=ativa) |

### Tabela: movimento
| Campo | Tipo | Descrição |
|-------|------|-----------|
| idmovimento | TEXT(37) | ID único do movimento |
| idcontacorrente | TEXT(37) | ID da conta corrente |
| datamovimento | TEXT(25) | Data do movimento (DD/MM/YYYY) |
| tipomovimento | TEXT(1) | Tipo: C=Crédito, D=Débito |
| valor | REAL | Valor do movimento |

## Dados de Teste

### Contas Ativas
- **ID**: B6BAFC09-6967-ED11-A567-055DFA4A16C9, **Número**: 123, **Nome**: Katherine Sanchez
- **ID**: FA99D033-7067-ED11-96C6-7C5DFA4A16C9, **Número**: 456, **Nome**: Eva Woodward
- **ID**: 382D323D-7067-ED11-8866-7D5DFA4A16C9, **Número**: 789, **Nome**: Tevin Mcconnell

### Contas Inativas
- **ID**: F475F943-7067-ED11-A06B-7E5DFA4A16C9, **Número**: 741, **Nome**: Ameena Lynn
- **ID**: BCDACA4A-7067-ED11-AF81-825DFA4A16C9, **Número**: 852, **Nome**: Jarrad Mckee
- **ID**: D2E02051-7067-ED11-94C0-835DFA4A16C9, **Número**: 963, **Nome**: Elisha Simons

## Qualidade do Código

### Cobertura de Testes
- ✅ Testes unitários para todos os casos de uso
- ✅ Testes de validação de negócio
- ✅ Testes de exceções
- ✅ Mocking de dependências

### Clean Code
- ✅ Nomes descritivos e claros
- ✅ Métodos pequenos e focados
- ✅ Comentários XML para documentação
- ✅ Separação clara de responsabilidades
- ✅ Tratamento adequado de erros

## Considerações Finais

Este projeto demonstra um nível profissional de desenvolvimento, seguindo as melhores práticas da indústria. A arquitetura limpa permite fácil manutenção e extensão, enquanto os testes garantem a qualidade e confiabilidade do sistema.

O código está estruturado para ser facilmente compreendido por outros desenvolvedores e pode ser usado como base para sistemas bancários mais complexos.
