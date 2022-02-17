using System.Collections.Generic;
using tabuleiro; 

namespace xadrez
{
    internal class PartidaDeXadrez
    {
        private readonly HashSet<Peca> capturadas;

        
        private readonly HashSet<Peca> pecas;

        
        public PartidaDeXadrez()
        {
            tab = new Tabuleiro(8, 8);
            turno = 1;
            jogadorAtual = Cor.Branca;
            terminada = false; 
            xeque = false;
            vulneravelEnPassant = null;

            
            pecas = new HashSet<Peca>();
            capturadas = new HashSet<Peca>();

            
            colocarPecas();
        }

       
        public Tabuleiro tab { get; }
        public int turno { get; private set; }
        public Cor jogadorAtual { get; private set; }
        public bool terminada { get; private set; } 
        public bool xeque { get; private set; } 
        public Peca vulneravelEnPassant { get; private set; }

       
        public Peca executaMovimento(Posicao origem, Posicao destino)
        {
            var p = tab.retirarPeca(origem);
            p.incrementarQuantidadeDeMovimentos();
            var pecaCapturada = tab.retirarPeca(destino); 
            tab.colocarPeca(p, destino); 
            if (pecaCapturada != null)
                capturadas.Add(
                    pecaCapturada); 

            
            if (p is Rei && destino.coluna == origem.coluna + 2)
            {
                var origemT = new Posicao(origem.linha, origem.coluna + 3);
                var destinoT = new Posicao(origem.linha, origem.coluna + 1);
                var T = tab.retirarPeca(origemT);
                T.incrementarQuantidadeDeMovimentos();
                tab.colocarPeca(T, destinoT);
            }

            
            if (p is Rei && destino.coluna == origem.coluna - 2)
            {
                var origemT = new Posicao(origem.linha, origem.coluna - 4);
                var destinoT = new Posicao(origem.linha, origem.coluna - 1);
                var T = tab.retirarPeca(origemT);
                T.incrementarQuantidadeDeMovimentos();
                tab.colocarPeca(T, destinoT);
            }

            
            if (p is Peao)
                if (origem.coluna != destino.coluna && pecaCapturada == null)
                {
                    Posicao posP;
                    if (p.cor == Cor.Branca)
                        posP = new Posicao(destino.linha + 1, destino.coluna);

                    else
                        posP = new Posicao(destino.linha - 1, destino.coluna);
                    pecaCapturada = tab.retirarPeca(posP);
                    capturadas.Add(pecaCapturada);
                }

            return pecaCapturada;
        }

        public void desfazMovimento(Posicao origem, Posicao destino, Peca pecaCapturada)
        {
            var p = tab.retirarPeca(destino);
            p.decrementarQuantidadeDeMovimentos();
            if (pecaCapturada != null)
            {
                tab.colocarPeca(pecaCapturada, destino);
                capturadas.Remove(pecaCapturada); 
            }

            tab.colocarPeca(p, origem); 

            
            if (p is Rei && destino.coluna == origem.coluna + 2)
            {
                var origemT = new Posicao(origem.linha, origem.coluna + 3);
                var destinoT = new Posicao(origem.linha, origem.coluna + 1);
                var T = tab.retirarPeca(destinoT);
                T.decrementarQuantidadeDeMovimentos();
                tab.colocarPeca(T, origemT);
            }

            
            if (p is Rei && destino.coluna == origem.coluna - 2)
            {
                var origemT = new Posicao(origem.linha, origem.coluna - 4);
                var destinoT = new Posicao(origem.linha, origem.coluna - 1);
                var T = tab.retirarPeca(origemT);
                T.incrementarQuantidadeDeMovimentos();
                tab.colocarPeca(T, destinoT);
            }

           
            if (p is Peao)
                if (origem.coluna != destino.coluna && pecaCapturada == vulneravelEnPassant)
                {
                    var peao = tab.retirarPeca(destino);
                    Posicao posP;
                    if (p.cor == Cor.Branca)
                        posP = new Posicao(3, destino.coluna);

                    else
                        posP = new Posicao(4, destino.coluna);
                    tab.colocarPeca(peao, posP);
                }
        }

        
        public void realizaJogada(Posicao origem, Posicao destino)
        {
            var pecaCapturada = executaMovimento(origem, destino);

           
            if (estaemXeque(jogadorAtual))
            {
                desfazMovimento(origem, destino, pecaCapturada);
                throw new TabuleiroException("Você não pode se colocar em xeque!");
            }

            var p = tab.peca(destino); 

            
            if (p is Peao)
                if (p.cor == Cor.Branca && destino.linha == 0 || p.cor == Cor.Preta && destino.linha == 7)
                {
                    p = tab.retirarPeca(destino);
                    pecas.Remove(p);
                    Peca dama = new Dama(tab, p.cor); 
                    tab.colocarPeca(dama, destino); 
                    pecas.Add(dama);
                }

            if (estaemXeque(adversaria(jogadorAtual)))
                xeque = true;
            else
                xeque = false;

            if (testeXequemate(adversaria(jogadorAtual)))
            {
                terminada = true;
            }

            else 
            {
                turno++;
                mudaJogador();
            }

            
            if (p is Peao && (destino.linha == origem.linha - 2 || destino.linha == origem.linha + 2))
                vulneravelEnPassant = p;

            else
                vulneravelEnPassant = null;
        }

        
        public void validarPosicaoDeOrigem(Posicao pos)
        {
            if (tab.peca(pos) == null)
                throw new TabuleiroException("Não existe peça na posição de origem escolhida!");

            if (jogadorAtual != tab.peca(pos).cor)
                throw new TabuleiroException("A peça de origem escolhida não é sua!");

            if (!tab.peca(pos).existeMovimentosPossiveis())
                throw new TabuleiroException("Não há movimentos possíveis para a peça de origem escolhida!");
        }

        
        public void validarPosicaoDeDestino(Posicao origem, Posicao destino) 
        {
            if (!tab.peca(origem).movimentoPossivel(destino))
                throw new TabuleiroException("Posição de destino inválida!");
        }


        private void mudaJogador() 
        {
            if (jogadorAtual == Cor.Branca)
                jogadorAtual = Cor.Preta;

            else
                jogadorAtual = Cor.Branca;
        }

        
        public HashSet<Peca> pecasCapturadas(Cor cor)
        {
            var aux = new HashSet<Peca>();
            foreach (var x in capturadas)
                if (x.cor == cor)
                    aux.Add(x);
            return aux;
        }

        
        public HashSet<Peca> pecasEmJogo(Cor cor)
        {
            var aux = new HashSet<Peca>();
            foreach (var x in pecas)
                if (x.cor == cor)
                    aux.Add(x);
            
            aux.ExceptWith(pecasCapturadas(cor));
            return aux;
        }

        

        private Cor adversaria(Cor cor)
        {
            if (cor == Cor.Branca)
                return Cor.Preta;
            return Cor.Branca;
        }

        
        private Peca rei(Cor cor)
        {
            foreach (var x in pecasEmJogo(cor))
                if (x is Rei) 
                    
                    return x;
            return null;
        }

        
        public bool estaemXeque(Cor cor)
        {
            var R = rei(cor);
            if (R == null) throw new TabuleiroException("Não tem rei da cor " + cor + " no tabuleiro!");
            foreach (var x in pecasEmJogo(adversaria(cor)))
            {
                var mat = x.movimentosPossiveis();
                if (mat[R.posicao.linha, R.posicao.coluna])
                     
                    
                    return true;
            } 

            return false;
        }

        
        public bool testeXequemate(Cor cor)
        {
            if (!estaemXeque(cor))
                return false;
            foreach (var x in pecasEmJogo(cor))
            {
                
                var mat = x.movimentosPossiveis();
                for (var i = 0; i < tab.linhas; i++)
                for (var j = 0; j < tab.colunas; j++)
                    if (mat[i, j] /*== true*/)
                    {
                        var origem = x.posicao;
                        var destino = new Posicao(i, j);

                       
                        var pecaCapturada = executaMovimento(origem, destino);
                        var testeXeque = estaemXeque(cor);
                        desfazMovimento(origem, destino, pecaCapturada);

                       
                        if (!testeXeque) return false;
                    }
            }

            return true;
        }

        
        public void colocarNovaPeca(char coluna, int linha, Peca peca)
        {
            tab.colocarPeca(peca, new PoiscaoXadrez(coluna, linha).toPosicao());
            pecas.Add(peca);
        }

        private void colocarPecas()
        {
            colocarNovaPeca('a', 1, new Torre(tab, Cor.Branca));
            colocarNovaPeca('b', 1, new Cavalo(tab, Cor.Branca));
            colocarNovaPeca('c', 1, new Bispo(tab, Cor.Branca));
            colocarNovaPeca('d', 1, new Dama(tab, Cor.Branca));
            colocarNovaPeca('e', 1, new Rei(tab, Cor.Branca, this)); 
            colocarNovaPeca('f', 1, new Bispo(tab, Cor.Branca));
            colocarNovaPeca('g', 1, new Cavalo(tab, Cor.Branca));
            colocarNovaPeca('h', 1, new Torre(tab, Cor.Branca));
            colocarNovaPeca('a', 2, new Peao(tab, Cor.Branca, this));
            colocarNovaPeca('b', 2, new Peao(tab, Cor.Branca, this));
            colocarNovaPeca('c', 2, new Peao(tab, Cor.Branca, this));
            colocarNovaPeca('d', 2, new Peao(tab, Cor.Branca, this));
            colocarNovaPeca('e', 2, new Peao(tab, Cor.Branca, this));
            colocarNovaPeca('f', 2, new Peao(tab, Cor.Branca, this));
            colocarNovaPeca('g', 2, new Peao(tab, Cor.Branca, this));
            colocarNovaPeca('h', 2, new Peao(tab, Cor.Branca, this));

            colocarNovaPeca('a', 8, new Torre(tab, Cor.Preta));
            colocarNovaPeca('b', 8, new Cavalo(tab, Cor.Preta));
            colocarNovaPeca('c', 8, new Bispo(tab, Cor.Preta));
            colocarNovaPeca('d', 8, new Dama(tab, Cor.Preta));
            colocarNovaPeca('e', 8, new Rei(tab, Cor.Preta, this)); 
            colocarNovaPeca('f', 8, new Bispo(tab, Cor.Preta));
            colocarNovaPeca('g', 8, new Cavalo(tab, Cor.Preta));
            colocarNovaPeca('h', 8, new Torre(tab, Cor.Preta));
            colocarNovaPeca('a', 7, new Peao(tab, Cor.Preta, this));
            colocarNovaPeca('b', 7, new Peao(tab, Cor.Preta, this));
            colocarNovaPeca('c', 7, new Peao(tab, Cor.Preta, this));
            colocarNovaPeca('d', 7, new Peao(tab, Cor.Preta, this));
            colocarNovaPeca('e', 7, new Peao(tab, Cor.Preta, this));
            colocarNovaPeca('f', 7, new Peao(tab, Cor.Preta, this));
            colocarNovaPeca('g', 7, new Peao(tab, Cor.Preta, this));
            colocarNovaPeca('h', 7, new Peao(tab, Cor.Preta, this));
        }
    }
}