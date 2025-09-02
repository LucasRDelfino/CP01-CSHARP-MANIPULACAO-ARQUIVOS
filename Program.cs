using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

class Program
{
    static async Task Main(string[] args)
    {
        Console.OutputEncoding = Encoding.UTF8;

        Console.WriteLine("===== Processador de Arquivos TXT =====");

        string diretorio;

       
        while (true)
        {
            Console.Write("Informe o diretório com arquivos .txt: ");
            diretorio = Console.ReadLine();

            if (!string.IsNullOrWhiteSpace(diretorio) && Directory.Exists(diretorio))
            {
                break; 
            }

            Console.WriteLine("❌ Diretório inválido! Tente novamente.\n");
        }

        var arquivos = Directory.GetFiles(diretorio, "*.txt");
        if (arquivos.Length == 0)
        {
            Console.WriteLine("Nenhum arquivo .txt encontrado.");
            return;
        }

        Console.WriteLine($"\n{arquivos.Length} arquivos encontrados:");
        foreach (var arq in arquivos)
            Console.WriteLine($"- {Path.GetFileName(arq)}");

        Console.WriteLine("\nIniciando processamento...\n");

        var resultados = await ProcessarArquivosAsync(arquivos);

        await GerarRelatorioAsync(resultados);

        Console.WriteLine("\n✅ Processamento concluído!");
        Console.WriteLine("Relatório salvo em: ./export/relatorio.txt");
    }

    static async Task<Dictionary<string, (int linhas, int palavras)>> ProcessarArquivosAsync(string[] arquivos)
    {
        var resultados = new Dictionary<string, (int linhas, int palavras)>();
        var tarefas = new List<Task>();

        foreach (var arquivo in arquivos)
        {
            tarefas.Add(Task.Run(async () =>
            {
                Console.WriteLine($"Processando arquivo {Path.GetFileName(arquivo)}...");
                var (linhas, palavras) = await ContarLinhasEPalavrasAsync(arquivo);
                lock (resultados)
                {
                    resultados[Path.GetFileName(arquivo)] = (linhas, palavras);
                }
            }));
        }

        await Task.WhenAll(tarefas);
        return resultados;
    }

    static async Task<(int linhas, int palavras)> ContarLinhasEPalavrasAsync(string caminhoArquivo)
    {
        using var reader = new StreamReader(caminhoArquivo);
        int linhas = 0;
        int palavras = 0;

        while (!reader.EndOfStream)
        {
            var linha = await reader.ReadLineAsync();
            if (linha != null)
            {
                linhas++;
                palavras += linha.Split(' ', StringSplitOptions.RemoveEmptyEntries).Length;
            }
        }

        return (linhas, palavras);
    }

    static async Task GerarRelatorioAsync(Dictionary<string, (int linhas, int palavras)> resultados)
    {
        string pastaExport = Path.Combine(Directory.GetCurrentDirectory(), "export");
        Directory.CreateDirectory(pastaExport);

        string caminhoRelatorio = Path.Combine(pastaExport, "relatorio.txt");

        using var writer = new StreamWriter(caminhoRelatorio, false, Encoding.UTF8);

        foreach (var item in resultados.OrderBy(r => r.Key))
        {
            await writer.WriteLineAsync($"{item.Key} - {item.Value.linhas} linhas - {item.Value.palavras} palavras");
        }
    }
}
