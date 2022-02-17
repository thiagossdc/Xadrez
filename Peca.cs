namespace tabuleiro
{
    internal abstract class Peca // Quando a classe tem ao menos um método abstrato, a mesma se torna abstrata também
    {
        // Construtor
        public Peca(Tabuleiro tab, Cor cor)
        {
            posicao = null;
            this.tab = tab;
            this.cor = cor;
            qteMovimento = 0; // Começa com zero movimentos, por isso ela não foi um argumento
        }

        public Posicao posicao { get; set; }
        public Cor cor { get; protected set; } // A cor so pode ser acessada por ela mesma e suas pelas subclasses

        public int
            qteMovimento
        {
            get;
            protected set;
        } // O movimento so pode ser acessado por ele mesma e suas pelas subclasses

        public Tabuleiro
            tab { get; protected set; } // O tabuleiro so pode ser acessado por ele mesma e suas pelas subclasses

        // Método para incrementar a quantidade de movimentos das peça
        public void incrementarQuantidadeDeMovimentos()
        {
            qteMovimento++;
        }


        // Método para decrementar a quantidade de movimentos das peça
        public void decrementarQuantidadeDeMovimentos()
        {
            qteMovimento--;
        }

        // Operação que diz se existe movimentos possíveis
        public bool existeMovimentosPossiveis()
        {
            var mat = movimentosPossiveis();
            for (var i = 0; i < tab.linhas; i++)
            for (var j = 0; j < tab.colunas; j++)
                if (mat[i, j] /*== true*/)
                    return true;
            return false;
        }

        // Só para melhorar a legibilidade
        public bool movimentoPossivel(Posicao pos)
        {
            return movimentosPossiveis()[pos.linha, pos.coluna];
        }

        public abstract bool[,] movimentosPossiveis(); // Matriz de bool pois iremos marcar nela os moviemntos possíveis
    }
}