using tabuleiro;

namespace xadrez
{
    internal class Rei : Peca 
    {
        private readonly PartidaDeXadrez partida; 

        public Rei(Tabuleiro tab, Cor cor, PartidaDeXadrez partida) : base(tab, cor)
        {
            this.partida = partida;
        }

        public override string ToString()
        {
            return "R";
        }

         
        private bool podeMover(Posicao pos)
        {
            var p = tab.peca(pos); 
            return p == null || p.cor != cor;  
        }

        
        private bool testeTorreParaRoque(Posicao pos)
        {
            var p = tab.peca(pos);
            return p != null && p is Torre && p.cor == cor && p.qteMovimento == 0;
        }

        public override bool[,]
            movimentosPossiveis() 
        {
            var mat = new bool[tab.linhas, tab.colunas]; 

            var pos = new Posicao(0, 0);

            // Acima
            pos.definirValores(posicao.linha - 1, posicao.coluna);
            if (tab.posicaoValida(pos) && podeMover(pos)) mat[pos.linha, pos.coluna] = true;

            // ne
            pos.definirValores(posicao.linha - 1, posicao.coluna + 1);
            if (tab.posicaoValida(pos) && podeMover(pos)) mat[pos.linha, pos.coluna] = true;

            // direita
            pos.definirValores(posicao.linha, posicao.coluna + 1);
            if (tab.posicaoValida(pos) && podeMover(pos)) mat[pos.linha, pos.coluna] = true;

            // se
            pos.definirValores(posicao.linha + 1, posicao.coluna + 1);
            if (tab.posicaoValida(pos) && podeMover(pos)) mat[pos.linha, pos.coluna] = true;

            // abaixo
            pos.definirValores(posicao.linha + 1, posicao.coluna);
            if (tab.posicaoValida(pos) && podeMover(pos)) mat[pos.linha, pos.coluna] = true;

            // so
            pos.definirValores(posicao.linha + 1, posicao.coluna - 1);
            if (tab.posicaoValida(pos) && podeMover(pos)) mat[pos.linha, pos.coluna] = true;

            // esquerda
            pos.definirValores(posicao.linha, posicao.coluna - 1);
            if (tab.posicaoValida(pos) && podeMover(pos)) mat[pos.linha, pos.coluna] = true;

            // no
            pos.definirValores(posicao.linha - 1, posicao.coluna - 1);
            if (tab.posicaoValida(pos) && podeMover(pos)) mat[pos.linha, pos.coluna] = true;

            
            if (qteMovimento == 0 && !partida.xeque)
            {
                
                var posT1 = new Posicao(posicao.linha, posicao.coluna + 3);
                if (testeTorreParaRoque(posT1))
                {
                    var p1 = new Posicao(posicao.linha, posicao.coluna + 1);
                    var p2 = new Posicao(posicao.linha, posicao.coluna + 2);
                    if (tab.peca(p1) == null && tab.peca(p2) == null) mat[posicao.linha, posicao.coluna + 2] = true;
                }

                
                var posT2 = new Posicao(posicao.linha, posicao.coluna - 4);
                if (testeTorreParaRoque(posT2))
                {
                    var p1 = new Posicao(posicao.linha, posicao.coluna - 1);
                    var p2 = new Posicao(posicao.linha, posicao.coluna - 2);
                    var p3 = new Posicao(posicao.linha, posicao.coluna - 3);
                    if (tab.peca(p1) == null && tab.peca(p2) == null && tab.peca(p3) == null)
                        mat[posicao.linha, posicao.coluna - 2] = true;
                }
            }

            return mat;
        }
    }
}