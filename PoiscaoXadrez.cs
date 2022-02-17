// Posição das casas na forma do xadrez

using tabuleiro;

namespace xadrez
{
    internal class PoiscaoXadrez
    {
        public PoiscaoXadrez(char coluna, int linha)
        {
            this.coluna = coluna;
            this.linha = linha;
        }

        public char coluna { get; set; }
        public int linha { get; set; }

        
        public Posicao toPosicao()  
        {
            return
                new(8 - linha, coluna -
                               'a'); 
        }

        public override string ToString()
        {
            return "" + coluna + linha; 
        } 
    }
}