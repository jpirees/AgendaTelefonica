
namespace AgendaTelefonica
{
    internal class Telefone
    {
        public string Tipo { get; set; }

        public string DDD { get; set; }

        public string Numero { get; set; }

        public Telefone Anterior { get; set; }

        public Telefone Proximo { get; set; }

        public Telefone() { }

        public Telefone(string tipo, string dDD, string numero)
        {
            Tipo = tipo;
            DDD = dDD;
            Numero = numero;
            Proximo = null;
            Anterior = null;
        }

        public override string ToString()
        {
            return $"Tipo:\t{Tipo}\nTel:\t({DDD}) {Numero}\n";
        }
    }
}