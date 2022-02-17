using System;
using tabuleiro;
using xadrez;

namespace Jogo_de_Xadrez
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            try
            {
                var partida = new PartidaDeXadrez();

                while (!partida.terminada)
                    try
                    {
                        Console.Clear();
                        Tela.imprimirPartida(partida);

                        Console.WriteLine();
                        Console.Write("Origem: ");

                      
                        var origem = Tela.lerPosicaoXadrez().toPosicao();
                        partida.validarPosicaoDeOrigem(origem); 

                        

                        var posicoesPossiveis = partida.tab.peca(origem).movimentosPossiveis();

                        Console.Clear(); 
                        Tela.imprimirTabuleiro(partida.tab, posicoesPossiveis); 

                        Console.WriteLine();
                        Console.Write("Destino: ");
                        var destino = Tela.lerPosicaoXadrez().toPosicao();
                        partida.validarPosicaoDeDestino(origem, destino);

                        partida.realizaJogada(origem, destino); 
                    }

                    catch (TabuleiroException e)
                    {
                        Console.WriteLine(e.Message);
                        Console.ReadLine();
                    }
            }

            catch (TabuleiroException e)
            {
                Console.WriteLine(e.Message);
            }

            Console.ReadLine();
        }
    }
}