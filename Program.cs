 using System;
using System.Diagnostics;
using System.Threading;

namespace AppEstudos
{
    class Program
    {
        static void Main(string[] args)
        {
            // --- SUA LISTA DE SITES PARA ESTUDO ---
            string[] sites = {
                "https://www.google.com",
                "https://github.com",
                "https://wikipedia.org",
                "https://stackoverflow.com",
                "https://www.youtube.com"
            };

            // Aqui nós definimos qual navegador você usa. 
            // Opções comuns: "chrome", "msedge", "firefox"
            string navegadorPadrhao = "chrome"; 

            Console.WriteLine("Iniciando sessao de estudos com JANELAS SEPARADAS...");

            // Abre cada um dos sites em uma janela própria e isolada
            foreach (string site in sites)
            {
                AbrirEmNovaJanela(navegadorPadrhao, site);
                Thread.Sleep(800); // Pausa de quase 1 segundo para o Windows organizar as janelas
            }

            // Espera 10 segundos para dar tempo de o navegador carregar antes de monitorar
            Thread.Sleep(10000);

            Console.WriteLine("Monitorando... Quando fechar TODAS as janelas do navegador, o PC desligara.");

            // Loop que fica checando se você ainda está estudando
            while (true)
            {
                // Se o seu navegador fechou por completo e não tem nenhuma janela dele aberta:
                if (!NavegadorEstaAberto(navegadorPadrhao))
                {
                    break; // Sai do loop para desligar
                }

                // Checa a cada 3 segundos para não pesar no PC
                Thread.Sleep(3000);
            }

            // --- HORA DE DESLIGAR ---
            Console.WriteLine("Todas as janelas fechadas. Encerrando o sistema em 3 segundos...");
            Thread.Sleep(3000);

            // Desliga o computador imediatamente na força bruta
            Process.Start("shutdown", "/s /t 0 /f");
        }

        // Função mágica que força o navegador a abrir uma Nova Janela isolada
        static void AbrirEmNovaJanela(string navegador, string url)
        {
            try
            {
                ProcessStartInfo psi = new ProcessStartInfo
                {
                    FileName = navegador,
                    // O segredo está aqui: "--new-window" força o navegador a abrir uma janela nova e não uma aba
                    Arguments = $"--new-window {url}", 
                    UseShellExecute = true
                };
                Process.Start(psi);
            }
            catch (Exception)
            {
                // Caso dê algum erro com o nome do navegador (ex: se você não tiver o Chrome instalado),
                // ele tenta abrir pelo inicializador padrão do Windows para não travar o programa.
                Process.Start(new ProcessStartInfo { FileName = url, UseShellExecute = true });
            }
        }

        // Função que conta se ainda existem janelas/processos ativos do navegador
        static bool NavegadorEstaAberto(string nomeDoProcesso)
        {
            Process[] processos = Process.GetProcessesByName(nomeDoProcesso);
            return processos.Length > 0;
        }
    }
}