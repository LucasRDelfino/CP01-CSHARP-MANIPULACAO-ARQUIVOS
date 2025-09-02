# ProcessAsyncTxt

## Descrição do Projeto
Aplicação Console em C# (.NET 8) que processa arquivos `.txt` de forma assíncrona, contando linhas e palavras de cada arquivo, e gera um relatório consolidado em `./export/relatorio.txt`.

O objetivo do projeto é demonstrar o uso de **programação assíncrona** (`async/await`) em .NET, manipulação de arquivos e geração de relatórios.

## Funcionalidades
- Solicita ao usuário um diretório contendo arquivos `.txt`.
- Lista todos os arquivos encontrados.
- Processa cada arquivo **em paralelo**, mostrando mensagens de progresso no console.
- Conta **linhas** e **palavras** de cada arquivo.
- Gera um relatório consolidado em `./export/relatorio.txt`.
- Loop de validação para diretório inválido, garantindo que o programa não quebre.

## Como Executar

1. Abra o terminal na pasta do projeto (`ProcessAsyncTxt`):
   
```bash
cd C:\Users\labsfiap\Desktop\cp01\ProcessAsyncTxt
```

2.Execute o projeto com o comando:

```bash
dotnet run
```

3.Quando solicitado, informe o diretório onde estão seus arquivos .txt.
Exemplo:

```bash
seucaminho/arquivos.txt
```

4.O relatório será gerado automaticamente em:

```bash
C:\Users\labsfiap\Desktop\cp01\ProcessAsyncTxt\export\relatorio.txt
```

# Integrantes
- **Gustavo Vegi** - RM550188
- **Pedro Henrique Silva de Morais** - RM98804
- **Lucas Rodrigues Delfino** - RM550196
- **Luisa Cristina dos Santos Neves** - RM551889
- **Gabriel Aparecido Cassalho Xavier** - RM99794
