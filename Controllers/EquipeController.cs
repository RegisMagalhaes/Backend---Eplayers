using EPlayers_AspNetCore.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EPlayers_AspNetCore.Controllers
{
    public class EquipeController : Controller
    {

        //Criamos uma instancia equipeModel com a estrutura Equipe
        Equipe equipemodel = new Equipe();
        private object equipeModel;

        public IActionResult Index()
        {

            //Listando todas as equipes e enviando para a View, através da ViewBag
            ViewBag.Equipes = equipeModel.ReadAll();
            return View();
        }

        public IActionResult Cadastrar(IFormCollection form)
        {
            Equipe novaEquipe = new Equipe();
            novaEquipe.IdEquipe = Int32.Parse(form["IdEquipe"]);
            novaEquipe.Nome = form["Nome"];
            novaEquipe.Imagem = form["Imagem"];

            equipeModel.Create(novaEquipe);
            ViewBag.Equipes = equipeModel.ReadAll();

            return LocalRedirect("~/Equipe");
        }

    }
}