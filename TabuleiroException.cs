// Exceção personalizada

using System;

namespace tabuleiro
{
    internal class TabuleiroException : Exception // Herda da classe Exception
    {
        public TabuleiroException(string msg) : base(msg)
        {
        } // O Construtor recebe uma mensagem e 
        // repassa para 
    }
}