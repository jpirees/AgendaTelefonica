using System;

namespace AgendaTelefonica
{
    internal class ListaTelefones
    {
        public Telefone Cabeca { get; set; }
        public Telefone Cauda { get; set; }
        public int Elementos { get; set; }

        public ListaTelefones()
        {
            Cabeca = null;
            Cauda = null;
            Elementos = 0;
        }


        public void PrintOut()
        {
            if (IsHeadAndTailNull())
            {
                Console.WriteLine("[   Não há telefones registrados!   ]");
                return;
            }

            Telefone telefone = Cabeca;

            do
            {
                Console.WriteLine(telefone.ToString());
                telefone = telefone.Proximo;
            } while (telefone != null);
        }

        public void Push(Telefone telefone)
        {
            if (IsHeadAndTailNull())
            {
                Cabeca = telefone;
                Cauda = telefone;
            }
            else
            {
                Cauda.Proximo = telefone;
                telefone.Anterior = Cauda;
                Cauda = telefone;

                Sort();
            }

            Elementos++;
        }

        public void SearchAndPop(string tipo)
        {
            Telefone aux = Cabeca;
            Telefone telefone = Cabeca;

            int opcao;
            int repetidos = 0;
            bool existe = false;

            Console.Clear();

            do
            {
                if (String.Compare(tipo, aux.Tipo.ToLower()) == 0)
                {
                    Console.WriteLine(aux.ToString());
                    Console.WriteLine("-------------------------------------");
                    telefone = aux;
                    repetidos++;
                    existe = true;
                }

                aux = aux.Proximo;
            } while (aux != null);

            if (!existe)
            {
                Console.WriteLine("[   Tipo Telefone não encontrado   ]");
                return;
            }


            if (repetidos >= 2)
            {
                do
                {
                    Console.WriteLine($"Qual telefone deseja remover? (1 a {repetidos})");
                    opcao = Int32.Parse(Console.ReadLine());
                } while (opcao < 1 || opcao > repetidos);

                Pop(telefone.Tipo, opcao);
            }
            else
                Pop(telefone.Tipo);
        }

        public void SearchAndEdit(string tipo)
        {
            Telefone aux = Cabeca;
            Telefone telefone = Cabeca;

            int opcao;
            int repetidos = 0;
            bool existe = false;

            do
            {
                if (String.Compare(tipo, aux.Tipo.ToLower()) == 0)
                {
                    Console.WriteLine(aux.ToString());
                    Console.WriteLine("-------------------------------------");
                    telefone = aux;
                    repetidos++;
                    existe = true;
                }

                aux = aux.Proximo;
            } while (aux != null);

            if (!existe)
            {
                Console.WriteLine("[   Telefone não encontrado   ]");
                return;
            }

            if (repetidos >= 2)
            {
                aux = Cabeca;

                int contador = 1;

                do
                {
                    Console.WriteLine($"Qual telefone deseja editar? (1 a {repetidos})");
                    opcao = Int32.Parse(Console.ReadLine());
                } while (opcao < 1 || opcao > repetidos);


                for (int i = 0; i < Elementos; i++)
                {
                    if (String.Compare(tipo, aux.Tipo) == 0)
                    {
                        if (opcao == contador)
                        {
                            telefone = aux;
                            break;
                        }
                        else
                        {
                            contador++;
                        }
                    }

                    aux = aux.Proximo;
                }

                aux = EditPhone(telefone);

                Edit(telefone, aux, opcao);

            }
            else
            {
                aux = EditPhone(telefone);

                Edit(telefone, aux);
            }

            Sort();
        }

        public void Pop(string tipo)
        {

            if (String.Compare(Cabeca.Tipo.ToLower(), tipo.ToLower()) == 0)
            {
                Cabeca = Cabeca.Proximo;
            }
            else if (String.Compare(Cauda.Tipo.ToLower(), tipo.ToLower()) == 0)
            {
                Cauda = Cauda.Anterior;
                Cauda.Proximo = null;
            }
            else
            {
                Telefone aux = Cabeca;

                for (int i = 0; i < Elementos - 1; i++)
                {
                    if (String.Compare(aux.Tipo.ToLower(), tipo.ToLower()) == 0)
                    {
                        aux.Proximo.Anterior = aux.Anterior;
                        aux.Anterior.Proximo = aux.Proximo;

                        break;
                    }

                    aux = aux.Proximo;
                }
            }

            Elementos--;
        }

        public void Pop(string tipo, int telefoneARemover)
        {
            int contador = 1;

            if (String.Compare(tipo, Cabeca.Tipo) == 0 && telefoneARemover == contador)
            {
                Cabeca = Cabeca.Proximo;
            }
            else
            {
                contador = (Cabeca.Tipo == tipo)
                    ? contador + 1
                    : contador;

                Telefone aux2 = Cabeca;
                Telefone aux1 = Cabeca.Proximo;

                for (int i = 1; i < Elementos; i++)
                {
                    if (String.Compare(tipo, aux1.Tipo) == 0)
                    {
                        if (contador == telefoneARemover)
                        {
                            aux2.Proximo = aux1.Proximo;
                            if (aux1.Proximo == null) Cauda = aux2;
                            break;
                        }
                        else
                        {
                            contador++;
                        }

                    }

                    aux2 = aux1;
                    aux1 = aux2.Proximo;
                }
            }

            if (Cabeca == null) Cauda = null;

            Elementos--;
        }

        public void Edit(Telefone telefone, Telefone telefoneEditado)
        {
            Telefone aux = Cabeca;

            for (int i = 0; i < Elementos; i++)
            {
                if (String.Compare(telefone.Tipo, aux.Tipo) == 0)
                {
                    aux = telefoneEditado;
                    break;
                }

                aux = aux.Proximo;
            }

        }

        public void Edit(Telefone telefone, Telefone telefoneEditado, int opcao)
        {
            Telefone aux = Cabeca;
            int contador = 1;

            for (int i = 0; i < Elementos; i++)
            {
                if (String.Compare(telefone.Tipo, aux.Tipo) == 0)
                {
                    if (contador == opcao)
                    {
                        aux = telefoneEditado;
                        break;
                    }
                    else
                    {
                        contador++;
                    }
                }

                aux = aux.Proximo;
            }

        }

        public void Sort()
        {
            if (Elementos <= 1) return;


            for (Telefone telefone1 = Cabeca; telefone1 != null; telefone1 = telefone1.Proximo)
            {
                for (Telefone telefone2 = telefone1.Proximo; telefone2 != null; telefone2 = telefone2.Proximo)
                {
                    if (String.Compare(telefone1.Tipo, telefone2.Tipo, StringComparison.CurrentCultureIgnoreCase) > 0)
                    {
                        Telefone aux = new Telefone();

                        aux.Tipo = telefone1.Tipo;
                        aux.DDD = telefone1.DDD;
                        aux.Numero = telefone1.Numero;

                        telefone1.Tipo = telefone2.Tipo;
                        telefone1.DDD = telefone2.DDD;
                        telefone1.Numero = telefone2.Numero;

                        telefone2.Tipo = aux.Tipo;
                        telefone2.DDD = aux.DDD;
                        telefone2.Numero = aux.Numero;
                    }
                }

            }
        }

        public Telefone EditPhone(Telefone telefone)
        {
            string opcao;
            Telefone aux = telefone;

            do
            {
                Console.Clear();
                Console.WriteLine(telefone.ToString());
                Console.WriteLine("O que deseja editar?");
                Console.WriteLine("---------- Telefone ----------");
                Console.WriteLine("[1] Tipo");
                Console.WriteLine("[2] DDD");
                Console.WriteLine("[3] Número");
                Console.WriteLine("-----------------------------");
                Console.WriteLine("[0] Voltar");
                opcao = Console.ReadLine();


                switch (opcao)
                {
                    case "1":
                        Console.Clear();
                        Console.Write("Tipo:\t");
                        aux.Tipo = Console.ReadLine();
                        break;

                    case "2":
                        Console.Clear();
                        Console.Write("DDD:\t");
                        aux.DDD = Console.ReadLine();
                        break;

                    case "3":
                        Console.Clear();
                        Console.Write("Número:\t");
                        aux.Numero = Console.ReadLine();
                        break;

                    case "0":
                        break;

                    default:
                        break;
                }

            } while (opcao != "0");

            return aux;
        }

        public bool IsHeadAndTailNull()
        {
            return (Cabeca == null && Cauda == null) ? true : false;
        }

    }
}