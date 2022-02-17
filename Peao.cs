using tabuleiro;

namespace xadrez
{
    internal class Peao : Peca
    {
        private readonly PartidaDeXadrez partida;

        public Peao(Tabuleiro tab, Cor cor, PartidaDeXadrez partida) : base(tab, cor)
        {
            this.partida = partida;
        }

        public override string ToString()
        {
            return "P";
        }

        private bool existeInimigo(Posicao pos)
        {
            var p = tab.peca(pos);
            return p != null && p.cor != cor; 
        }

        private bool livre(Posicao pos)
        {
            return tab.peca(pos) == null; 
        }

        public override bool[,] movimentosPossiveis()
        {
            var mat = new bool[tab.linhas, tab.colunas];

            var pos = new Posicao(0, 0);

            if (cor == Cor.Branca) 
            {
                pos.definirValores(posicao.linha - 1, posicao.coluna); 
                if (tab.posicaoValida(pos) && livre(pos)) mat[pos.linha, pos.coluna] = true;
                pos.definirValores(posicao.linha - 2, posicao.coluna);

                
                if (tab.posicaoValida(pos) && livre(pos) && qteMovimento == 0) mat[pos.linha, pos.coluna] = true;
                pos.definirValores(posicao.linha - 1, posicao.coluna - 1); 
                if (tab.posicaoValida(pos) && existeInimigo(pos)) mat[pos.linha, pos.coluna] = true;
                pos.definirValores(posicao.linha - 1, posicao.coluna + 1);
                if (tab.posicaoValida(pos) && existeInimigo(pos)) mat[pos.linha, pos.coluna] = true;

                
                if (posicao.linha == 3)
                {
                    var esquerda = new Posicao(posicao.linha, posicao.coluna - 1);
                    if (tab.posicaoValida(esquerda) && existeInimigo(esquerda) &&
                        tab.peca(esquerda) == partida.vulneravelEnPassant)
                        mat[esquerda.linha - 1, esquerda.coluna] = true;

                    var direita = new Posicao(posicao.linha, posicao.coluna + 1);
                    if (tab.posicaoValida(direita) && existeInimigo(direita) &&
                        tab.peca(direita) == partida.vulneravelEnPassant) mat[direita.linha - 1, direita.coluna] = true;
                }
            }

            else 
            {
                pos.definirValores(posicao.linha + 1, posicao.coluna); 
                if (tab.posicaoValida(pos) && livre(pos)) mat[pos.linha, pos.coluna] = true;
                pos.definirValores(posicao.linha + 2, posicao.coluna);

                
                if (tab.posicaoValida(pos) && livre(pos) && qteMovimento == 0) mat[pos.linha, pos.coluna] = true;
                pos.definirValores(posicao.linha + 1, posicao.coluna - 1); 

                if (tab.posicaoValida(pos) && existeInimigo(pos)) mat[pos.linha, pos.coluna] = true;
                pos.definirValores(posicao.linha + 1, posicao.coluna + 1);

                if (tab.posicaoValida(pos) && existeInimigo(pos)) mat[pos.linha, pos.coluna] = true;

                
                if (posicao.linha == 4)
                {
                    var esquerda = new Posicao(posicao.linha, posicao.coluna - 1);
                    if (tab.posicaoValida(esquerda) && existeInimigo(esquerda) &&
                        tab.peca(esquerda) == partida.vulneravelEnPassant)
                        mat[esquerda.linha + 1, esquerda.coluna] = true;

                    var direita = new Posicao(posicao.linha, posicao.coluna + 1);
                    if (tab.posicaoValida(direita) && existeInimigo(direita) &&
                        tab.peca(direita) == partida.vulneravelEnPassant) mat[direita.linha + 1, direita.coluna] = true;
                }
            }

            return mat;
        }
    }
}