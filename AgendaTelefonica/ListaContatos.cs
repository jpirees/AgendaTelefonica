using System;

namespace AgendaTelefonica
{
    internal class ListaContatos
    {
        public Contato Cabeca { get; set; }
        public Contato Cauda { get; set; }
        public int Elementos { get; set; }

        public ListaContatos()
        {
            Cabeca = Cauda = null;
            Elementos = 0;
        }

        public void PrintOut()
        {
            if (IsHeadAndTailNull())
            {
                Console.WriteLine("[   Lista de contatos vazia   ]");
                return;
            }

            Contato contato = Cabeca;

            do
            {
                Console.WriteLine(contato.ToString());
                contato.Telefones.PrintOut();
                Console.WriteLine("-------------------------------------");
                Console.ReadKey();
                contato = contato.Proximo;
            } while (contato != null);
        }

        public void Search(string nome)
        {
            Contato contato = Cabeca;
            bool existe = false;

            do
            {
                if (contato.Nome.ToLower().Contains(nome))
                {
                    Console.WriteLine(contato.ToString());
                    contato.Telefones.PrintOut();
                    Console.WriteLine("-------------------------------------");

                    existe = true;
                }

                contato = contato.Proximo;
            } while (contato != null);

            if (!existe)
                Console.WriteLine("[   Contato não encontrado   ]");

        }

        public void Search(string nome, string acao)
        {
            Contato aux = Cabeca;
            Contato contato = Cabeca;

            int opcao;
            int repetidos = 0;
            bool existe = false;

            do
            {
                if (String.Compare(nome, aux.Nome, StringComparison.CurrentCultureIgnoreCase) == 0)
                {
                    Console.WriteLine(aux.ToString());
                    Console.WriteLine("-------------------------------------");
                    contato = aux;
                    repetidos++;
                    existe = true;
                }

                aux = aux.Proximo;
            } while (aux != null);

            if (!existe)
            {
                Console.WriteLine("[   Contato não encontrado   ]");
                return;
            }

            if (repetidos >= 2)
            {
                int contador = 1;

                do
                {
                    Console.WriteLine($"Qual contato deseja {acao}? (1 a {repetidos})");
                    opcao = int.Parse(Console.ReadLine());
                } while (opcao < 1 || opcao > repetidos);


                for (Contato aux1 = Cabeca; aux1 != null; aux1 = aux1.Proximo)
                {
                    if (String.Compare(nome, aux1.Nome, StringComparison.CurrentCultureIgnoreCase) == 0)
                    {
                        if (opcao == contador)
                        {
                            contato = aux1;
                            break;
                        }
                        else
                        {
                            contador++;
                        }
                    }
                }
            }

            if (acao == "remover")
                Pop(contato.Nome);
            else
            {
                Edit(contato);
                Sort();
            }
        }

        public void Push(Contato contato)
        {
            if (IsHeadAndTailNull())
            {
                Cabeca = contato;
                Cauda = contato;
            }
            else
            {
                if (String.Compare(contato.Nome, Cabeca.Nome, StringComparison.CurrentCultureIgnoreCase) == -1)
                {
                    contato.Proximo = Cabeca;
                    Cabeca.Anterior = contato;
                    Cabeca = contato;
                }
                else if (String.Compare(contato.Nome, Cauda.Nome, StringComparison.CurrentCultureIgnoreCase) >= 0)
                {
                    contato.Anterior = Cauda;
                    Cauda.Proximo = contato;
                    Cauda = contato;
                }
                else
                {
                    for (Contato aux = Cabeca.Proximo; aux != null; aux = aux.Proximo)
                    {
                        if (String.Compare(contato.Nome, aux.Nome, StringComparison.CurrentCultureIgnoreCase) == -1)
                        {
                            contato.Proximo = aux;
                            contato.Anterior = aux.Anterior;

                            aux.Anterior.Proximo = contato;
                            aux.Anterior = contato;
                        }

                    }
                }


            }

            Elementos++;
        }

        public void Pop(string nome)
        {

            if (String.Compare(Cabeca.Nome, nome, StringComparison.CurrentCultureIgnoreCase) == 0)
            {
                Cabeca = Cabeca.Proximo;
            }
            else if (String.Compare(Cauda.Nome, nome, StringComparison.CurrentCultureIgnoreCase) == 0)
            {
                Cauda = Cauda.Anterior;
                Cauda.Proximo = null;
            }
            else
            {
                Contato aux = Cabeca;

                for (int i = 0; i < Elementos - 1; i++)
                {
                    if (String.Compare(aux.Nome, nome, StringComparison.CurrentCultureIgnoreCase) == 0)
                    {
                        aux.Proximo.Anterior = aux.Anterior;
                        aux.Anterior.Proximo = aux.Proximo;

                        break;
                    }

                    aux = aux.Proximo;
                }
            }

            if (Cabeca == null) Cauda = null;

            Elementos--;
        }

        public void Edit(Contato contato)
        {
            string opcao;

            do
            {
                Console.Clear();
                Console.WriteLine(contato.ToString());
                Console.WriteLine("O que deseja editar?");
                Console.WriteLine("---------- Contato ----------");
                Console.WriteLine("[1] Nome");
                Console.WriteLine("[2] E-mail");
                Console.WriteLine("---------- Telefone ---------");
                Console.WriteLine("[3] Adicionar telefone");
                Console.WriteLine("[4] Listar telefones");
                Console.WriteLine("[5] Editar telefone");
                Console.WriteLine("[6] Remover telefone");
                Console.WriteLine("-----------------------------");
                Console.WriteLine("[0] Voltar");
                opcao = Console.ReadLine();


                switch (opcao)
                {
                    case "1":
                        Console.Clear();
                        Console.Write("Nome:\t");
                        contato.Nome = Console.ReadLine();
                        break;

                    case "2":
                        Console.Clear();
                        Console.Write("E-mail:\t");
                        contato.Email = Console.ReadLine();
                        break;

                    case "3":
                        contato.Telefones.Push(NewPhone());
                        break;

                    case "4":
                        Console.Clear();
                        contato.Telefones.PrintOut();
                        Console.ReadKey();
                        break;

                    case "5":
                        EditPhones(contato);
                        break;

                    case "6":
                        PopPhones(contato);
                        break;

                    case "0":
                        break;

                    default:
                        break;
                }

            } while (opcao != "0");

        }

        public void Sort()
        {
            if (Elementos <= 1) return;


            for (Contato contato1 = Cabeca; contato1 != null; contato1 = contato1.Proximo)
            {
                for (Contato contato2 = contato1.Proximo; contato2 != null; contato2 = contato2.Proximo)
                {
                    if (String.Compare(contato1.Nome, contato2.Nome, StringComparison.CurrentCultureIgnoreCase) > 0)
                    {
                        Contato aux = new Contato();

                        aux.Nome = contato1.Nome;
                        aux.Email = contato1.Email;
                        aux.Telefones = contato1.Telefones;

                        contato1.Nome = contato2.Nome;
                        contato1.Email = contato2.Email;
                        contato1.Telefones = contato2.Telefones;

                        contato2.Nome = aux.Nome;
                        contato2.Email = aux.Email;
                        contato2.Telefones = aux.Telefones;
                    }
                }

            }
        }

        public void EditPhones(Contato contato)
        {

            if (contato.Telefones.Elementos == 0)
            {
                Console.Clear();
                Console.WriteLine("[   Lista de telefones vazia   ]");
                return;
            }


            Console.Clear();
            contato.Telefones.PrintOut();

            Console.WriteLine("-------------------------------------");
            Console.Write("Qual tipo deseja editar?: ");
            string tipo = Console.ReadLine().ToLower();
            Console.WriteLine("-------------------------------------");

            contato.Telefones.SearchAndEdit(tipo);

        }

        public void PopPhones(Contato contato)
        {

            if (contato.Telefones.Elementos == 0)
            {
                Console.Clear();
                Console.WriteLine("[   Lista de telefones vazia   ]");
                return;
            }


            Console.Clear();
            contato.Telefones.PrintOut();

            Console.WriteLine("-------------------------------------");
            Console.Write("Qual tipo deseja remover?: ");
            string tipo = Console.ReadLine().ToLower();
            Console.WriteLine("-------------------------------------");

            contato.Telefones.SearchAndPop(tipo);
        }

        public Telefone NewPhone()
        {
            Console.Clear();
            Console.Write("Tipo: ");
            string tipo = Console.ReadLine().Trim(' ');
            Console.Write("DDD: ");
            string ddd = Console.ReadLine().Trim(' ');
            Console.Write("Número: ");
            string numero = Console.ReadLine().Trim(' ');

            return new Telefone(tipo, ddd, numero);
        }

        public bool IsHeadAndTailNull()
        {
            return (Cabeca == null && Cauda == null) ? true : false;
        }

    }
}