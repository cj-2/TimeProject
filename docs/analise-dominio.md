# Análise da camada de Domínio

> Levantamento feito em 2026-10-23 sobre o estado do commit `35c5d67`.
> O objetivo é servir de material de estudo: aponta o que está certo, o que não está, e por quê. As correções ficam por sua conta.

## Resumo

A camada `TimeProject.Domain` está **no lugar certo e com a dependência certa**, mas funciona como um *modelo de persistência*, não como um *modelo de domínio*. As regras de negócio — que são o que caracteriza um domínio — estão espalhadas pela Application (`Utils`, `UseCases`) e pela Infrastructure (`Errors`, `IUnitOfWork`).

---

## O que está correto

### 1. Direção de dependência

`TimeProject.Domain.csproj` não referencia nenhum projeto nem pacote. Infrastructure e Application dependem dele, e não o contrário. O domínio é o centro e não conhece banco, HTTP ou frameworks.

### 2. Interfaces de repositório no Domain

`TimeProject.Domain/Repositories/I*Repository.cs` declara *o que* o domínio precisa; `TimeProject.Infrastructure/Repositories/*` implementa *como*. É a inversão de dependência (o "D" do SOLID).

### 3. Enums de domínio

`Entities/Enums` (`UserRoleType`, `ConfirmCodeType`, `SessionType`…) são conceitos do negócio e estão no lugar certo.

---

## O que não está correto

### 1. Entidades anêmicas

Todas as entidades são só `{ get; set; }` públicos, sem comportamento nem invariantes. Nada impede criar um `Period` com `End < Start` ou trocar `User.IsActive` diretamente de qualquer lugar.

As regras existem, mas moram fora das entidades:

| Regra de negócio | Onde está hoje | Onde poderia estar |
|---|---|---|
| `Start <= End`; duração mínima > 2s | `Application/Utils/PeriodValidateUtil.cs` (repetida em `UseCases/Periods/CreatePeriodUseCase.cs`) | `Period` (construtor/factory) |
| Código de confirmação expirado ou já usado | `Application/UseCases/Codes/ValidateConfirmCodeUseCase.cs` | `ConfirmCode.IsValid(now)` / `ConfirmCode.Use()` |
| Recortar um período dentro de um intervalo | `Application/Utils/PeriodCutUtil.cs` | `Period.ClampTo(start, end)` |
| Ativar/desativar usuário | `Application/UseCases/Users/Stories/DisableUserUseCase.cs` | `User.Disable()` / `User.Activate()` |

**Sintoma a observar:** os `*Util` com interface (`IPeriodValidateUtil`, `IPeriodCutUtil`) são lógica de domínio que virou "serviço injetável" porque não tinha onde morar. Quando uma regra depende só dos dados de uma entidade, ela normalmente pertence à entidade.

**Por que importa:** com entidades anêmicas, cada caso de uso precisa lembrar de validar. Basta um esquecer (ou duplicar com diferença) para o banco aceitar estado inválido. Com invariantes na entidade, o estado inválido deixa de ser representável.

### 2. Erros de domínio na Infrastructure

`Infrastructure/Errors/*MessageErrors.cs` (ex.: `PeriodMessageErrors.EndDateIsBiggerThenStartDate`) são conceitos de negócio. Existem ~33 `using TimeProject.Infrastructure.Errors` na Application.

**Pergunta para refletir:** se você trocasse PostgreSQL por outro banco, essas mensagens mudariam? Se não, elas não são infraestrutura.

### 3. Application depende de Infrastructure

`TimeProject.Application.csproj` tem `ProjectReference` para `TimeProject.Infrastructure`. Isso inverte a Clean Architecture que a divisão em projetos sugere.

O ponto mais visível é `Infrastructure/Interfaces/IUnitOfWork.cs`:

```csharp
CustomDbContext Context { get; }   // tipo do EF exposto aos casos de uso
```

O caso de uso passa a poder acessar o EF diretamente, e a abstração vaza. A interface deveria estar no Domain (ou Application), sem o `DbContext`. O mesmo raciocínio vale para `ICustomSmtp`, `ICustomBot`, `IUserChallenge`, `IHookHandler`: **a interface pertence a quem consome, a implementação a quem fornece.**

### 4. Tipos de leitura/paginação dentro do Domain

- `Repositories/Shared/Pagination.cs`, `IndexResult.cs`, `PaginationQuery.cs` — modelos de consulta/API.
- `Repositories/ObjectValues/SearchRecordItem.cs`, `DateSearch.cs` — são DTOs, não *Value Objects*.

Um **Value Object** de verdade é imutável, tem igualdade por valor e geralmente carrega validação. Exemplo do próprio projeto: um intervalo de tempo.

```csharp
public sealed record TimeRange
{
    public DateTimeOffset Start { get; }
    public DateTimeOffset End { get; }

    public TimeRange(DateTimeOffset start, DateTimeOffset end)
    {
        if (start > end) throw new DomainException("...");
        Start = start;
        End = end;
    }

    public TimeSpan Duration => End - Start;
}
```

`IStatisticRepository` também não é um repositório de agregado — é um *serviço de consulta* (o lado de leitura em CQRS). Não há problema em existir; o ponto é nomear e posicionar pelo que é.

### 5. Sem agregados nem fronteiras de consistência

Pelo modelo, `Record` parece ser a raiz de `Period`, `Minute` e `RecordResume`. Mas cada um tem seu repositório e é alterado de forma independente — por exemplo, `CreatePeriodUseCase` cria o período e depois chama `SyncRecordResumeUseCase` separadamente.

Se `Record` fosse um **agregado**, algo como `record.AddPeriod(...)` garantiria que o resumo fica consistente na mesma operação.

**Pergunta para refletir:** um `Period` faz sentido sem um `Record`? Quem é responsável por manter `RecordResume` correto?

### 6. Código morto

`Customer`, `Price` e `Project` existem em `Entities/`, mas não têm repositório nem configuração no EF.

---

## Bug encontrado

`Application/Utils/PeriodCutUtil.cs:13`

```csharp
End = entity.End > endDate ? entity.End = endDate.AddMilliseconds(-1) : entity.End
```

O `entity.End = ...` **altera a entidade original** quando a intenção era só calcular o valor do novo `Period`. Se a entidade estiver rastreada pelo EF, a alteração pode ser persistida.

**Exercício:** escreva um teste em `UnitTests/Utils/PeriodCutUtilTests.cs` que falhe por causa disso antes de corrigir.

---

## Sugestão de trilha de estudo

Em vez de refatorar tudo, pegue **uma** entidade e leve até o fim. `Period` é a melhor candidata:

1. Torne os setters privados e valide no construtor/factory. Absorva `PeriodValidateUtil` e `PeriodCutUtil`.
2. Extraia `TimeRange` como Value Object.
3. Mova `PeriodMessageErrors` para o Domain.
4. Veja o que quebra no EF (construtores, setters privados, *owned types*) — isso é parte do aprendizado.

Depois replique o padrão em `ConfirmCode` e `User`. Por último, ataque a dependência Application → Infrastructure.

## Leituras

- Eric Evans — *Domain-Driven Design* (cap. 5 e 6: Entities, Value Objects, Aggregates).
- Vaughn Vernon — *Implementing Domain-Driven Design* (cap. 10: Aggregates).
- Martin Fowler — [Anemic Domain Model](https://martinfowler.com/bliki/AnemicDomainModel.html).
