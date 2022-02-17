namespace tabuleiro
{
    internal class Tabuleiro
    {
        private readonly Peca[,] pecas; // Xadrez tem uma matriz de peças

        // Somente o tabuleiro tem acesso as pecas
        public Tabuleiro(int linhas, int colunas)
        {
            this.linhas = linhas;
            this.colunas = colunas;
            pecas = new Peca[linhas, colunas]; // Intancia a matriz de pecas
        }

        public int linhas { get; set; }
        public int colunas { get; set; }

        // Método individual para ter acesso a posição das peças na tela
        public Peca peca(int linha, int coluna)
        {
            return pecas[linha, coluna];
        }

        public Peca peca(Posicao pos)
        {
            return pecas[pos.linha, pos.coluna]; // Retorna matriz de posição
        }

        // Método que testa se existe uma peça em uma dada posição
        public bool existePeca(Posicao pos)
        {
            validarPosicao(
                pos); // Colocamos este método caso a posição seja inválida, caso não seja vai para o de baixo
            return peca(pos) != null;
        }

        public void colocarPeca(Peca peca, Posicao pos)
        {
            if (existePeca(pos)) // Se existir uma peça na posiçao pos, dá um erro
                throw new TabuleiroException("Já existe um peça nessa posição!");
            pecas[pos.linha, pos.coluna] = peca; // Joga a peça p na matriz
            peca.posicao = pos; // Indica a posição da peça p
        }

        public Peca retirarPeca(Posicao pos)
        {
            if (peca(pos) == null) // Se for igual a nulo, não tem peça nessa posição
                return null;

            var aux = peca(pos);
            aux.posicao = null; // Indica que a peça foi retirada
            pecas[pos.linha, pos.coluna] = null; // Marca a posição do tabuleiro onde ela estava como nula
            return aux; // Retorna essa peça
        }

        // Método para testar se a posição é valida (pois nosso tabuliero é do tipo 8x8)
        public bool posicaoValida(Posicao pos)
        {
            if (pos.linha < 0 || pos.linha >= linhas || pos.coluna < 0 || pos.coluna >= colunas)
                return false;
            return true; // else é opcional pois o return corta o método
        }

        public void validarPosicao(Posicao pos)
        {
            if (!posicaoValida(pos)) // Caso a posicaoValida pos não for válida, uma exceção ocorrerá
                throw new TabuleiroException("Posição inválida!");
        }
    }
}