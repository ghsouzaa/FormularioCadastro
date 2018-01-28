using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FormularioCadastro.Models;

namespace FormularioCadastro.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Consulta()
        {
            ConsultaViewModel pessoa = new ConsultaViewModel();
            using (EstudoEntities conn = new EstudoEntities())
            {
                pessoa.pessoa = conn.Pessoa.ToList();                
            }
            return View(pessoa);
        }

        #region JSOn
        public JsonResult CPFJaCadastrado(string cpf)
        {
            using (EstudoEntities conn = new EstudoEntities())
            {
                List<Pessoa> pessoa = conn.Pessoa.ToList();
                bool existe = pessoa.Any(pes => pes.CPF == cpf);

                if (existe)
                {
                    return Json(true, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(false, JsonRequestBehavior.AllowGet);
                }
            }
        }

        public JsonResult PessoaMenorDeIdade(DateTime dataNascimento)
        {
            int idade = GetAge(DateTime.Now, dataNascimento);

            if (idade < 18)
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult CadastrarPessoa(string nome, string cpf, DateTime dtNascimento, string celular, string email)
        {
            try
            {
                using (EstudoEntities conn = new EstudoEntities())
                {
                    Pessoa pessoa = new Pessoa();

                    pessoa.Nome = nome;
                    pessoa.CPF = cpf;
                    pessoa.DataNascimento = dtNascimento;
                    pessoa.Celular = celular;
                    pessoa.Email = email;

                    conn.Pessoa.Add(pessoa);
                    conn.SaveChanges();

                    return Json(true, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception e)
            {
                return Json(new { sucesso = false, e.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion

        #region Métodos
        public static int GetAge(DateTime reference, DateTime birthday)
        {
            int age = reference.Year - birthday.Year;
            if (reference < birthday.AddYears(age)) age--;

            return age;
        }
        #endregion
    }
}