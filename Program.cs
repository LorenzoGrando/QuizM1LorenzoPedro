using System;

namespace QuizAvaliacaoM1
{
    // Grupo: Lorenzo Grando e Pedro Henrique D'Avila
    class Program
    {
        static void Main(string[] args)
        {
            string jogarQuiz;
            string[,] listaDePerguntas;

            //Aplica o set de perguntas à cada rejogar, pois modificamos as perguntas durante o jogo para evitar repetição.
            //Executa a função do quiz ao iniciar e a repete enquanto o usuário retornar "S" para rejogar
            do
            {
                listaDePerguntas = ResetarPerguntas();
                jogarQuiz = RealizarQuiz(listaDePerguntas);
            } while (jogarQuiz == "S");
        }

        //Aplica as perguntas padrão ao array de perguntas. Se quiser adicionar uma nova pergunta, só adicione uma nova linha
        //seguindo o padrão.
        static string[,] ResetarPerguntas()
        {
            string[,] perguntasPadrao = new string[,]
            {
                //Coluna 1: Pergunta | Coluna 2: Resposta da Pergunta
                {"Brasília é a capital do Brasil.", "V"},
                {"Tokyo é a capital do Japão.", "V" },
                {"Dublin é a capital da Irlanda.", "V" },
                {"Lima é a capital do Peru.", "V" },
                {"Cairo é a capital do Egito.", "V" },
                {"Lagos é a capital da Nigéria.", "V" },
                {"Instanbul é a capital da Turquia.", "V" },
                {"Washington é a capital dos EUA.", "V" },
                {"Luxemburgo é a capital de Luxemburgo.", "V" },
                {"Viena é a capital da Áustria.", "V" },
                {"Sydney é a capital da Austrália.", "F" },
                {"Shanghai é a capital da China.", "F" },
                {"Santiago é a capital do Uruguai.", "F" },
                {"Vancouver é a capital do Canadá.", "F" },
                {"Oslo é a capital da Finlândia.", "F" },
                {"Londres é a capital da França.", "F" },
                {"Cracóvia é a capital da Polônia.", "F" },
                {"Veneza é a capital da Itália.", "F" },
                {"Seul é a capital da Coréia do Norte.", "F" },
                {"Bogotá é a capital da Bolívia.", "F" },
            };

            return perguntasPadrao;
        }

        //Realiza o quiz e retorna o input do jogador de continuar ou não.
        static string RealizarQuiz(string[,] perguntasDoQuiz)
        {
            //Reset do valor das ints para o padrão em cada rejogada
            int nDaPergunta = 1;
            int nAcertos = 0;
            string resposta;
            string respostaCerta;

            Console.WriteLine("Olá! Esse é o quiz das capitais de países do mundo.");
            //Checa a quantidade de linhas do array para poder dizer qual o tamanho da pool de perguntas automaticamente;
            Console.WriteLine("Você receberá 5 perguntas de Verdadeiro ou Falso selecionadas aleatóriamente de uma lista de " + perguntasDoQuiz.GetLength(0) +" perguntas!");
            Console.WriteLine();

            //Faz a primeira pergunta e recebe a resposta certa dela
            respostaCerta = FazerPergunta(nDaPergunta, perguntasDoQuiz);

            //Repete o loop de perguntas e respostas até serem feitas 4 perguntas (5 loops, o último não faz pergunta)
            for(int quantidadeDePerguntas = 0; quantidadeDePerguntas < 5; quantidadeDePerguntas++)
            {
                //Chama o input do usuário e repete enquanto não receber um valor aceitável
                do
                {
                    Console.WriteLine("Digite <V> para Verdadeiro e <F> para Falso.");
                    resposta = Console.ReadLine()!;
                } while (resposta != "V" && resposta != "F");

                //Checa se a resposta do usuário está certa ou errada
                if (ChecarPergunta(resposta, respostaCerta) == true)
                {
                    nAcertos++;
                    nDaPergunta++;

                    Console.WriteLine("Resposta Certa!\n");

                    //Evita chamar a pergunta no último loop
                    if(quantidadeDePerguntas < 4)
                    {
                        respostaCerta = FazerPergunta(nDaPergunta, perguntasDoQuiz);
                    }
                }

                else
                {
                    nDaPergunta++;

                    Console.WriteLine("Resposta Errada!\n");

                    if (quantidadeDePerguntas < 4)
                    {
                        respostaCerta = FazerPergunta(nDaPergunta, perguntasDoQuiz);
                    }
                }
            }

            //Fim do quiz, executa após o fim do for. Exibe acertos e erros;
            Console.WriteLine(
            "\nFim do quiz! Você acertou " + nAcertos + " questões e errou " + ((nDaPergunta - 1) - nAcertos + " questões.")
            );
            Console.WriteLine("\nDeseja Jogar Novamente?");

            string recomeçarQuiz;

            //Repete até receber uma resposta válida;
            do
            {
                Console.WriteLine("Digite <S> se Sim, <N> se Não");
                recomeçarQuiz = Console.ReadLine()!;
            } while (recomeçarQuiz != "S" && recomeçarQuiz != "N");

            //Se receber S, limpa a tela e retorna o "S" necessário para manter o while na função Main.
            if (recomeçarQuiz == "S")
            {
                Console.Clear();
                return recomeçarQuiz;
            }

            //Qualquer valor além de "S" termina o loop de repetição do quiz na Main e finaliza o programa.
            else
            {
                Console.WriteLine("\nObrigado por jogar!");
                return "";
            }
        }

        //Recebe e checa se a resposta do usuário é igual a resposta esperada da pergunta mais recente 
        static bool ChecarPergunta(string respostaUsuario, string respostaCorreta)
        {
            if (respostaUsuario == respostaCorreta)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //Faz a pergunta, e retorna a resposta certa dela;
        static string FazerPergunta(int numeroAtualDaPergunta, string[,] arrayDePerguntas)
        {
            string respostaEsperada;
            //Variáveis usadas para gerar um unm número inteiro aleatório. Seguimos a msdn sobre a class Random como base:
            //https://docs.microsoft.com/en-us/dotnet/api/system.random?view=net-6.0

            var geradorDeNumero = new Random();
            int numeroAleatorio;

            //Gera um número aleatório entre 0 e o total de linhas(perguntas) do array.
            //Repete até a pergunta respectiva do número não ser vazia; 
            do
            {
                numeroAleatorio = geradorDeNumero.Next(arrayDePerguntas.GetLength(0));
            } while (arrayDePerguntas[numeroAleatorio, 0] == "");


            Console.WriteLine("Pergunta " + numeroAtualDaPergunta+ ": " + arrayDePerguntas[numeroAleatorio, 0]);
            //Guarda a resposta armazenada na segunda coluna do array;
            respostaEsperada = arrayDePerguntas[numeroAleatorio, 1];

            //Faz a pergunta utilizada virar vazia, para evitar que seja repetida no mesmo quiz.
            arrayDePerguntas[numeroAleatorio, 0] = "";

            return respostaEsperada;

        }
    }
}