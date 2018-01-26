using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using FormularioCadastro.Models;

namespace FormularioCadastro.Controllers
{
    public class HomeController : Controller
    {
        #region Views

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Consultar()
        {
            ConsultarViewModel viewModel = new ConsultarViewModel();

            using (EstudoEntities con = new EstudoEntities())
            {
                viewModel.pessoas = con.Pessoa.ToList();
            }
            return View(viewModel);
        }

        #endregion

        #region Json

        [HandleError]
        public JsonResult ExcluirPessoa(int idPessoa)
        {
            try
            {
                string nomePessoa;
                using (EstudoEntities con = new EstudoEntities())
                {
                    Pessoa pessoa = con.Pessoa.FirstOrDefault(x => x.IDPessoa == idPessoa);

                    if (pessoa == null)
                        throw new Exception("Pessoa não encontrada na base de dados.");

                    nomePessoa = pessoa.Nome;
                    con.Pessoa.Remove(pessoa);
                    con.SaveChanges();
                }

                Thread.Sleep(2000);
                return Json(new { Success = true, Message = nomePessoa }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                Thread.Sleep(2000);
                return Json(new { Success = false,  e.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        #endregion
    }
}