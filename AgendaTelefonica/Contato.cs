
namespace AgendaTelefonica
{
    internal class Contato
    {
        public string Nome { get; set; }

        public string Email { get; set; }

        public Contato Anterior { get; set; }
        public Contato Proximo { get; set; }

        public ListaTelefones Telefones { get; set; }

        public Contato() { }

        public Contato(string nome, string email, ListaTelefones telefones)
        {
            Nome = nome;
            Email = email;
            Telefones = telefones;
            Anterior = null;
            Proximo = null;
        }

        public override string ToString()
        {
            return $"\nNome:\t{Nome}\nE-mail:\t{Email}\nTelefones ({Telefones.Elementos}):\n";
        }
    }
}