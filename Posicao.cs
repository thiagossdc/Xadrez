namespace tabuleiro
{
    internal class Posicao
    {
        // Construtor 
        public Posicao(int linha, int coluna)
        {
            this.linha = linha;
            this.coluna = coluna;
        }

        public int linha { get; set; } // Encapsulamento
        public int coluna { get; set; }

        // Método para definir os valores da posição
        public void definirValores(int linha, int coluna)
        {
            this.linha = linha;
            this.coluna = coluna;
        }

        public override string ToString()
        {
            return linha
                   + ", "
                   + coluna;
        }
    }
}