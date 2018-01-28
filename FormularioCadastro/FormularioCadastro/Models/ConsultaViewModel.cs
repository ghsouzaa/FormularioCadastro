using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FormularioCadastro.Models
{
    public class ConsultaViewModel
    {
        public int IDPessoa { get; set; }
        public string Nome { get; set; }
        public string CPF { get; set; }
        public System.DateTime DataNascimento { get; set; }
        public string Celular { get; set; }
        public string Email { get; set; }
    }
}