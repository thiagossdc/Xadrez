using tabuleiro;

namespace xadrez
{
    internal class Torre : Peca 
    {
        public Torre(Tabuleiro tab, Cor cor) : base(tab, cor)
        {
        }

        public override string ToString()
        {
            return "T";
        }

        private bool podeMover(Posicao pos)
        {
            var p = tab.peca(pos); 
            return p == null || p.cor != cor;  
        }

        public override bool[,]
            movimentosPossiveis() 
        {
            var mat = new bool[tab.linhas, tab.colunas]; 

            var pos = new Posicao(0, 0);

            
            pos.definirValores(posicao.linha - 1, posicao.coluna);
            while (tab.posicaoValida(pos) && podeMover(pos))
            {
                mat[pos.linha, pos.coluna] = true;
                
                if (tab.peca(pos) != null && tab.peca(pos).cor != cor) break; 
                pos.linha = pos.linha - 1; 
            }

            
            pos.definirValores(posicao.linha + 1, posicao.coluna);
            while (tab.posicaoValida(pos) && podeMover(pos))
            {
                mat[pos.linha, pos.coluna] = true;
                
                if (tab.peca(pos) != null && tab.peca(pos).cor != cor) break;  
                pos.linha = pos.linha + 1; 
            }

           
            pos.definirValores(posicao.linha, posicao.coluna + 1);
            while (tab.posicaoValida(pos) && podeMover(pos))
            {
                mat[pos.linha, pos.coluna] = true;
               
                if (tab.peca(pos) != null && tab.peca(pos).cor != cor) break; 
                pos.coluna = pos.coluna + 1; 
            }

           
            pos.definirValores(posicao.linha, posicao.coluna - 1);
            while (tab.posicaoValida(pos) && podeMover(pos))
            {
                mat[pos.linha, pos.coluna] = true;
                
                if (tab.peca(pos) != null && tab.peca(pos).cor != cor) break; 
                pos.coluna = pos.coluna - 1; 
            }

            return mat;
        }
    }
}