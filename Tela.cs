using System;
using System.Collections.Generic;
using tabuleiro;
using xadrez;

namespace Jogo_de_Xadrez
{
    internal class Tela
    {
        public static void imprimirPartida(PartidaDeXadrez partida)
        {
            imprimirTabuleiro(partida.tab);
            Console.WriteLine();
            imprimirPecasCapturadas(partida);
            Console.WriteLine();
            Console.WriteLine("Turno: " + partida.turno); 

            if (!partida.terminada)
            {
                Console.WriteLine("Aguardando jogada: " + partida.jogadorAtual); 
                if (partida.xeque) Console.WriteLine("XEQUE!");
            }
            else
            {
                Console.WriteLine("XEQUEMATE!");
                Console.WriteLine("Vencedor: " + partida.jogadorAtual);
            }
        }

       
        public static void imprimirPecasCapturadas(PartidaDeXadrez partida)
        {
            Console.WriteLine("Peças capturadas: ");
            Console.Write("Brancas: ");
            imprimirConjunto(partida.pecasCapturadas(Cor.Branca));
            Console.WriteLine();
            Console.Write("Pretas: ");
            var aux = Console.ForegroundColor; 
            Console.ForegroundColor = ConsoleColor.Yellow;
            imprimirConjunto(partida.pecasCapturadas(Cor.Preta));
            Console.ForegroundColor = aux;
            Console.WriteLine();
        }

        
        public static void imprimirConjunto(HashSet<Peca> conjunto)
        {
            Console.Write("[");
            foreach (var x in conjunto) Console.Write(x + " ");
            Console.Write("]");
        }


        public static void imprimirTabuleiro(Tabuleiro tab)
        {
            
            for (var i = 0; i < tab.linhas; i++)
            {
                Console.Write(8 - i + " "); 

                for (var j = 0; j < tab.colunas; j++) imprimirPeca(tab.peca(i, j));  

                Console.WriteLine();
            }

            Console.WriteLine("  a b c d e f g h"); 
        }

        public static void imprimirTabuleiro(Tabuleiro tab, bool[,] posicoesPossiveis)
        {
            var fundoOriginal = Console.BackgroundColor;

            
            var fundoAlterado = ConsoleColor.DarkGray;

            
            for (var i = 0; i < tab.linhas; i++)
            {
                Console.Write(8 - i + " "); 

                for (var j = 0; j < tab.colunas; j++)
                {
                    if (posicoesPossiveis[i, j] /* == true*/)
                        Console.BackgroundColor = fundoAlterado;

                    else 
                        Console.BackgroundColor = fundoOriginal;

                    imprimirPeca(tab.peca(i, j)); 
                    Console.BackgroundColor = fundoOriginal;
                }

                Console.WriteLine();
            }

            Console.WriteLine("  a b c d e f g h"); 
            Console.BackgroundColor = fundoOriginal; 
        }

        public static PoiscaoXadrez lerPosicaoXadrez() 
        {
            var s = Console.ReadLine();
            var coluna = s[0];
            var linha = int.Parse(s[1] + ""); 
            return new PoiscaoXadrez(coluna, linha);
        }

        public static void imprimirPeca(Peca peca) 
        {
            if (peca == null)
            {
                Console.Write("- ");
            }

            else
            {
                if (peca.cor == Cor.Branca) 
                {
                    Console.Write(peca);
                }

                else
                {
                    var aux = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write(peca);
                    Console.ForegroundColor = aux;
                }

                Console.Write(" ");
            }
        }
    }
}