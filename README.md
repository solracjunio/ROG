# Testando Arquiteturas de Jogo com um Roguelite

Recentemente, me inspirei na série de vídeos "[How to make a Roguelite Game](https://www.youtube.com/playlist?list=PLSHqi2dTiNGCncSOksACfJChpfPa6qz9w)" e resolvi usar esse projeto como base para testar diferentes arquiteturas de código: **Spaghetti**, **ECS** e **SOAP**. A ideia é observar, na prática, como cada abordagem lida com escalabilidade, organização e facilidade de manutenção ao longo do desenvolvimento.

Neste primeiro post, começo pela abordagem **Spaghetti** — ou seja, um estilo de programação sem uma arquitetura clara, onde tudo vai sendo implementado diretamente, de forma acoplada e sem muita preocupação com separação de responsabilidades.

---
## Arquitetura Spaghetti - Parte 1

**Tempo:** 0 á 53 minutos
**Vídeo:** [# How to make a Roguelite Game with Soap. Part 1: ScriptableVariables and Bindings](https://www.youtube.com/watch?v=Yfp9aUxkfw4&list=PLSHqi2dTiNGCncSOksACfJChpfPa6qz9w&index=1)

![Screenshot Game Run](README/ScreenshotGameRun.png)

Logo no início da implementação, já nos deparamos com uma classe `Player` com **53 linhas** de código. Essa classe é responsável por várias coisas:

- Gerenciar o input do jogador
    
- Controlar a vida do personagem
    
- Armazenar os próprios dados
    

Além disso, temos dois _binds_ adicionados diretamente:

- Um para atualizar o texto com a vida atual
    
- Outro para atualizar a barra de vida
    

![Vscode Organization](README/VsCodeCodesAndOrganization.png)

Esses elementos — `Text` e `Health Bar` — estão diretamente dependentes da classe `Player`:

![Dependences](README/Dependencias.png)

Por enquanto, essa dependência não é exatamente um problema. Tudo funciona bem e de maneira rápida. Mas à medida que o jogo crescer, esse acoplamento pode se tornar uma armadilha difícil de manter.

---

### Prós:

- **Fácil de implementar**
    
- **Rápido para começar**
    

### Contras:

- A classe `Player` já começa a ficar um pouco grande demais
    
- Componentes estão fortemente acoplados
    
- Pouca separação de responsabilidades, o que pode dificultar a manutenção no futuro
