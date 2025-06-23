# Testando Arquiteturas de Jogo com um Roguelite

Recentemente, me inspirei na série de vídeos "[How to make a Roguelite Game](https://www.youtube.com/playlist?list=PLSHqi2dTiNGCncSOksACfJChpfPa6qz9w)" e resolvi usar esse projeto como base para testar diferentes arquiteturas de código: **Spaghetti**, **ECS** e **SOAP**. A ideia é observar, na prática, como cada abordagem lida com escalabilidade, organização e facilidade de manutenção ao longo do desenvolvimento.

Neste primeiro post, começo pela abordagem **Spaghetti** — ou seja, um estilo de programação sem uma arquitetura clara, onde tudo vai sendo implementado diretamente, de forma acoplada e sem muita preocupação com separação de responsabilidades.

---

## Arquitetura Spaghetti - Parte 1

**Tempo de desenvolvimento:** 0 á 53 minutos
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

---

## Arquitetura Spaghetti - Parte 2

**Tempo de desenvolvimento:** 53 minutos até 1h39  
**Vídeo:** [How to make a Roguelite Game with SOAP - Part 2: Scriptable Events, Scriptable Variables and Bindings](https://www.youtube.com/watch?v=Xl5l3HqoQAk&list=PLSHqi2dTiNGCncSOksACfJChpfPa6qz9w&index=2)

![Gameplay 2](README/Gameplay2.png)

Na segunda etapa do desenvolvimento, começamos a adicionar novos elementos visuais, como partículas e vinhetas na tela. O resultado? A classe `Player`, que já estava com 53 linhas, agora saltou para **79 linhas**. E com isso, aumentou ainda mais sua carga de responsabilidades:

- Gerenciar o input do jogador
    
- Controlar a vida do personagem
    
- Armazenar e atualizar os próprios dados
    
- Acionar efeitos visuais (como partículas e vinhetas)

![Vscode Organization 2](README/VscodeOrganization2.png)

Além disso, o número de dependências também cresceu. Agora o `Player` depende de **5 elementos de UI**, e **2 elementos também dependem diretamente do `Player`**. Isso gera um acoplamento perigoso que, embora não seja um obstáculo imediato, pode comprometer a flexibilidade e a manutenibilidade do projeto conforme ele cresce.

![Dependences 2](README/DependenceGraph2.png)

---

### Conclusão

Apesar da abordagem "espaguete" continuar permitindo um **ritmo de desenvolvimento rápido**, fica evidente que o projeto começa a **escalar com complexidade desorganizada**. A classe `Player` está se tornando um **"Deus Objeto"**, concentrando responsabilidades demais e criando um emaranhado de dependências com o sistema de UI e efeitos.

Essa abordagem ainda funciona bem para protótipos rápidos ou pequenos projetos, mas é um **sinal de alerta** se o objetivo for escalar o jogo, manter clareza no código e facilitar a colaboração.
