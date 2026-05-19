---

# 📚 Automação de Estudos Teóricos (Anti-Distração)

Este é um utilitário em linha de comando desenvolvido em **.NET 10** para otimizar sessões de estudo focadas. O programa serve como um "cronômetro de tarefas": ele abre as referências de estudo necessárias e garante que o usuário vá descansar assim que fechar o navegador, desligando o computador de forma automática.

## ⚙️ Como o Programa Funciona

O fluxo de execução do programa segue uma lógica linear dividida em três etapas automáticas:

```
[ Inicialização ] ──> [ Abertura de Janelas ] ──> [ Monitoramento Ativo ] ──> [ Desligamento ]

```

### 1. Inicialização e Disparo de Links

Ao ser executado, o programa lê uma lista oculta interna contendo os sites configurados para o estudo (por padrão, foram definidos 5 sites essenciais).

### 2. Isolamento por Janelas Separadas

Em vez de misturar o conteúdo em abas de uma única sessão, o programa se comunica diretamente com o motor do navegador padrão (ex: `chrome.exe`) e injeta o argumento `--new-window`.

* Isso força o Windows a abrir **uma janela isolada do navegador para cada site**.
* Existe uma pausa programada de **800 milissegundos** entre a abertura de cada site, evitando picos de processamento na CPU e garantindo que o sistema organize as janelas na tela sem travar.

### 3. Monitoramento Invisível em Segundo Plano

Após abrir os sites, o programa entra em um ciclo de checagem contínua (`loop while`):

* A cada **3 segundos**, o programa consulta a tabela de processos ativos do sistema operacional Windows.
* Ele verifica se o processo do navegador alvo (ex: Google Chrome) ainda consta na lista de tarefas sendo executadas.

### 4. Encerramento Induzido

Quando o usuário conclui a leitura de todo o conteúdo teórico e **fecha a última janela aberta do navegador**, o programa detecta que a lista de processos retornou o valor de `0` janelas ativas.
O ciclo de monitoramento é quebrado e o utilitário dispara o comando:

```bash
shutdown /s /t 0 /f

```

O computador é desligado imediatamente em 0 segundos, forçando o fechamento de qualquer outra atividade pendente, consolidando o fim do período de estudos.

---

## 🛠️ Personalização dos Sites

Para alterar ou adicionar novos portais de estudo, a modificação deve ser feita diretamente na matriz (array) de strings localizada no início do método principal do código-fonte:

```csharp
string[] sites = {
    "https://www.google.com",
    "https://github.com",
    "https://wikipedia.org",
    "https://stackoverflow.com",
    "https://www.youtube.com",
    "NOVO_SITE_AQUI" // Para expandir, basta adicionar uma vírgula e o link entre aspas
};

```

---

## 🚀 Arquitetura de Compilação

O programa foi arquitetado para ser compilado no modo **Self-Contained (Autônomo)** com **Single-File (Arquivo Único)**.

Isso significa que o motor de execução do runtime do .NET 10 é embutido e compactado diretamente dentro do arquivo executável final. O `.exe` gerado torna-se 100% independente, podendo ser transportado para qualquer outro computador Windows sem a necessidade de instalar ferramentas externas ou pacotes de desenvolvimento (SDK).

---