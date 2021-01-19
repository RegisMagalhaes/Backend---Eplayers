using EPlayers_AspNetCore.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;

namespace EPlayers_AspNetCore.Controllers
{
    //http:localhost:5001
    [Route("Equipe")]
    public class EquipeController : Controller
    {

        //Criamos uma instancia equipeModel com a estrutura Equipe
        Equipe equipeModel = new Equipe();
        
        //http://localhost:5001/Equipe/Listar
        [Route("Listar")]
        public IActionResult Index()
        {

            //Listando todas as equipes e enviando para a View, através da ViewBag
            ViewBag.Equipes = equipeModel.ReadAll();
            return View();
        }

        //http://localhost:5001/Equipe/Cadastrar
        [Route("Cadastrar")]
        public IActionResult Cadastrar(IFormCollection form)
        {
            //Criamos uma nova instância de Equipe
            //e através os dados enviados pelo usuário
            //atráves do formulario
            // e salvamos no objeto novaEquipe
            Equipe novaEquipe = new Equipe();
            novaEquipe.IdEquipe = Int32.Parse(form["IdEquipe"]);
            novaEquipe.Nome = form["Nome"];
            
            // Upload Inicio
            // Verificamos se o usuário anexou um arquivo
            if (form.Files.Count > 0)
            {
                // Se sim,
                // Armazenamos o arquivo na variável file
                var file = form.Files[0];
                var folder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/Equipes");

                //Verificamos se a pasta Equipes não existe
                if (!Directory.Exists(folder))
                {
                    //Se não existe a pasta, a criamos
                    Directory.CreateDirectory(folder);
                }

                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/", folder, file.FileName);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    //Salvamos o arquivo no caminho especificado
                    file.CopyTo(stream);
                }

                novaEquipe.Imagem = file.FileName;
                
            }
            else{
                novaEquipe.Imagem  = "padrao.png";
            }

            // Upload Término


            //Chamamos o método Create para salvar
            //a novaEquipe no CSV
            equipeModel.Create(novaEquipe);
            ViewBag.Equipes = equipeModel.ReadAll();

            return LocalRedirect("~/Equipe/Listar");
        }

        //http://localhost>5001/Equipe/1
        [Route("{id}")]
        public IActionResult Excluir(int id)
        {
            equipeModel.Delete(id);

            ViewBag.Equipes = equipeModel.ReadAll();

            return LocalRedirect("~/Equipe/Listar");
        }

    }
}