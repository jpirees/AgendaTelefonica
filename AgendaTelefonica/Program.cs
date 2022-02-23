using System;

namespace AgendaTelefonica
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ListaContatos lista_contatos = new ListaContatos();

            string opcao;

            do
            {
                switch (opcao = Menu())
                {
                    case "0":
                        Environment.Exit(0);
                        break;

                    case "1":
                        
                        Imprimir();
                        break;

                    case "2":
                        Buscar();
                        break;

                    case "3":
                        Inserir();
                        break;

                    case "4":
                        Localizar("editar");
                        break;

                    case "5":
                        Localizar("remover");
                        break;

                    default:
                        Console.WriteLine("Opção inválida");
                        break;
                }

                Console.ReadKey();
            
            } while (opcao != "0");





            string Menu()
            {
                Console.Clear();
                Console.WriteLine("------------ MENU ------------");
                Console.WriteLine("[1] Imprimir contatos");
                Console.WriteLine("[2] Localizar contato");
                Console.WriteLine("[3] Inserir contato");
                Console.WriteLine("[4] Editar contato");
                Console.WriteLine("[5] Remover contato");
                Console.WriteLine("[0] Sair");
                Console.WriteLine("------------------------------");

                return Console.ReadLine();
            }

            void Imprimir()
            {
                Console.Clear();
                lista_contatos.PrintOut();
            }

            void Buscar()
            {
                Console.Clear();

                if (lista_contatos.Elementos == 0)
                {
                    Console.WriteLine("[   Lista de contatos vazia   ]");
                    return;
                }

                Console.WriteLine("-------------------------------------");
                Console.Write("Buscar: ");
                string buscar = Console.ReadLine().ToLower();
                Console.WriteLine("-------------------------------------");

                lista_contatos.Search(buscar);
            }

            void Inserir()
            {
                Console.Clear();
                Console.Write("Nome:\t");
                string nome = Console.ReadLine().Trim(' ');
                Console.Write("E-mail:\t");
                string email = Console.ReadLine().Trim(' ');

                ListaTelefones lista_telefones = InserirTelefone();

                lista_contatos.Push(new Contato(nome, email, lista_telefones));
            }

            void Localizar(string tipo)
            {
                Console.Clear();

                if (lista_contatos.Elementos == 0)
                {
                    Console.WriteLine("[   Lista de contatos vazia   ]");
                    return;
                }

                Console.WriteLine("-------------------------------------");
                Console.Write("Localizar: ");
                string busca = Console.ReadLine().ToLower();
                Console.WriteLine("-------------------------------------");

                lista_contatos.Search(busca, tipo);
            }

            ListaTelefones InserirTelefone()
            {
                ListaTelefones lista_telefones = new ListaTelefones();
                string opcao;

                do
                {
                    Console.Clear();
                    Console.WriteLine("Deseja inserir um novo telefone?");
                    Console.WriteLine("[S]im | [N]ão");
                    opcao = Console.ReadLine().ToLower();

                    if (opcao == "s")
                    {
                        Telefone telefone = NovoTelefone();
                        lista_telefones.Push(telefone);
                    }

                } while (opcao != "n");

                return lista_telefones;
            }

            Telefone NovoTelefone()
            {
                Console.Clear();
                Console.Write("Tipo:\t");
                string tipo = Console.ReadLine().Trim(' ');
                Console.Write("DDD:\t");
                string ddd = Console.ReadLine().Trim(' ');
                Console.Write("Número:\t");
                string numero = Console.ReadLine().Trim(' ');

                return new Telefone(tipo, ddd, numero);
            }

        }
    }
}