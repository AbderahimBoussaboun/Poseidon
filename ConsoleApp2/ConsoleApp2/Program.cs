using System;
using ConsoleApp2;  // Ajusta según tus archivos generados por ANTLR
using Antlr4.Runtime;
using Antlr4.Runtime.Tree;

namespace ConsoleApp2
{
    class Program
    {
        static void Main(string[] args)
        {

            var inputStream = new AntlrFileStream("configuracion_qsqbalalc03_qsqbalalc04_29062023.txt");
            var lexer = new BigIPConfigLexer(inputStream);

            var commonTokenStream = new CommonTokenStream(lexer);
            var parser = new BigIPConfigParser(commonTokenStream);

            var walker = new ParseTreeWalker();
            var listener = new MyCustomListener();

            Console.WriteLine("Parseando Documento...");

            walker.Walk(listener, parser.config());

            Console.WriteLine("Documento parseado correctamente");
        }
    }
}
