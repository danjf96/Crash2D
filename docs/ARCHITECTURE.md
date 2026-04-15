# Arquitetura do Projeto Crash2D

Este documento descreve a arquitetura principal do projeto com foco no uso de `ScriptableObject` para separar comportamento e dados de caixas (`Box`) e como essa estrutura segue princípios SOLID de forma profissional.

## Visão Geral

O projeto segue uma arquitetura modular típica de Unity, com componentes de gameplay implementados em `MonoBehaviour` para a execução no ciclo de vida do Unity, e comportamentos reutilizáveis embalados em `ScriptableObject` para configuração de dados e lógica.

A parte mais significativa da arquitetura está no sistema de caixas colecionáveis (`Collectables/Box`), onde o padrão de projeto `Strategy` é aplicado via `BoxBehavior`.

## Padrão Principal: ScriptableObject como Strategy

### Classes envolvidas

- `Assets/Scripts/Collectables/Box/Box.cs`
  - `MonoBehaviour` que gerencia colisões, animações e execução de estado de tempo de vida.
  - Concentra apenas a lógica de interação com o jogador e a transição de estado (`hits`, `break`).

- `Assets/Scripts/Collectables/Box/BoxBehavior.cs`
  - Classe abstrata derivada de `ScriptableObject`.
  - Define a interface de comportamento com métodos `OnHit(Box box, PlayerController player)` e `OnBreak(Box box)`.
  - Contém dados de configuração que são facilmente editáveis no inspector.

- `Assets/Scripts/Collectables/Box/NormalBox.cs`
  - Implementação concreta do comportamento de caixa padrão.
  - Lida com spawn de fruta, animação de coleta e atualiza o contador de frutas.

- `Assets/Scripts/Collectables/Box/UgaBugaBox.cs`
  - Implementação concreta para uma caixa especial que gera o objeto `UgaBuga`.
  - Separa a lógica de ativação e efeitos sonoros específicos.

## Benefícios desta arquitetura

### 1. Single Responsibility Principle (SRP)

- `Box.cs` gerencia colisões, animação e fluxo de vida da caixa.
- `BoxBehavior` e suas subclasses gerenciam comportamento específico de cada tipo de caixa.
- Dados de configuração (`hitsToBreak`, `bounceForce`, clips de áudio, controller de animação) ficam no `ScriptableObject`, reduzindo acoplamento com o objeto de cena.

### 2. Open/Closed Principle (OCP)

- É possível adicionar novos tipos de caixa sem modificar `Box.cs`.
- Basta criar uma nova subclasse de `BoxBehavior` e um novo asset via `CreateAssetMenu`.
- O `Box` permanece fechado para modificação e aberto para extensão.

### 3. Liskov Substitution Principle (LSP)

- `NormalBox` e `UgaBugaBox` podem substituir `BoxBehavior` sem alterar o comportamento esperado de `Box`.
- A abstração `OnHit` / `OnBreak` garante que qualquer comportamento executa o contrato de resposta a eventos.

### 4. Dependency Inversion Principle (DIP)

- `Box` depende de `BoxBehavior` (abstração) e não de implementações concretas.
- A dependência é injetada no editor via serialização Unity, permitindo troca de comportamento em tempo de design.

### 5. Interface Segregation Principle (ISP)

- A abstração é enxuta: apenas métodos relevantes para o fluxo da caixa.
- Não há um grande contrato único; cada `ScriptableObject` implementa apenas o necessário para o domínio de `Box`.

## Como o projeto usa ScriptableObject corretamente

- `BoxBehavior` centraliza dados e lógica de comportamento em assets reutilizáveis.
- Valores como `hitsToBreak`, `bounceForce`, `hitClip`, `breakClip` e `animatorController` ficam configuráveis por asset.
- Essa abordagem é adequada para designers, pois permite criar variantes de caixa sem alterar código.

## Arquitetura de runtime

Fluxo simplificado:

1. `Box` recebe colisão com o jogador.
2. `Box` verifica estado do jogador via `PlayerController`.
3. `Box` dispara animação e som de impacto.
4. `BoxBehavior.OnHit(...)` executa o comportamento concreto.
5. Se o contador de hits acabar, `BoxBehavior.OnBreak(...)` destrói o objeto.

## Recomendações para evolução

- Para tornar a arquitetura ainda mais robusta, considere adicionar uma interface `IBoxBehavior` ou `ICollectableBehavior` e manter `BoxBehavior` como implementação padrão.
- Se o domínio crescer, separe a lógica de efeitos sonoros e spawn em handlers específicos para manter SRP.
- Use `CreateAssetMenu` em cada nova classe de comportamento para facilitar a criação de variantes no editor.
- Mantenha a lógica de `BoxBehavior` sem referências diretas a objetos de cena, recebendo sempre o `Box` e o `PlayerController` como contexto.

## Observações do projeto

- Atualmente, o uso de `ScriptableObject` está limitado ao subsistema de caixas colecionáveis.
- Outras áreas do projeto seguem o padrão de componentes Unity tradicionais (`PlayerController`, `Enemy`, `GameManager`).
- Essa arquitetura pode ser aplicada a outros sistemas de gameplay para aumentar reutilização e configurar variações sem código.

## Conclusão

O projeto já utiliza um padrão profissional de arquitetura para o subsistema de caixas, combinando `ScriptableObject` com princípios SOLID. Esse padrão garante extensibilidade, mantém responsabilidades claras e facilita a criação de novas variantes de conteúdo sem tocar na lógica central de colisão.
