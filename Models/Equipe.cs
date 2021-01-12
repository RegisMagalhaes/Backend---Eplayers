using System.Collections.Generic;
using System.IO;
using EPlayers_AspNetCore.Interfaces;

namespace EPlayers_AspNetCore.Models
{
    public class Equipe : EplayersBase , IEquipe
    {
        //ID - Identificador

        public int IdEquipe { get; set; }

        public string Nome { get; set; }

        public string Imagem { get; set; }

        private const string PATH = "Database/Equipe.csv";

        public Equipe()
        {
            CreateFolderAndFile(PATH);
        }

        public string Prepare(Equipe e)
        {
            return $"{e.IdEquipe};{e.Nome};{e.Imagem}";
        }

        public void Create(Equipe e)
        {
            string[] linhas = { Prepare(e)};
            File.AppendAllLines(PATH, linhas);
        }

        public void Delete(int id)
        {
            
            List<string> linhas = ReadAllLinesCSV(PATH);

            //Removemos a linha que tenha o código a ser alterado

            linhas.RemoveAll(x => x.Split(";")[0] == id.ToString());


            //Reescreve o csv com as alterações
            RewriteCSV(PATH, linhas);
        }

        public List<Equipe> ReadAll()
        {
            List<Equipe> equipes = new List<Equipe>();
            //Ler todas as linhas do CSV
            string[] linhas = File.ReadAllLines(PATH);

            // PErcorrer as linhas e adicionar na lista de equipes cada objeto equipe

            foreach (var item in linhas)
            {
                //1; VivoKeyd; vivo.jpg
                string[] linha = item.Split(";");

                //[0] = 1
                //[1] = VivoKeyd
                //[2] = vivo.jpg

                //Criamos o objeto equipe
                Equipe equipe = new Equipe();

                // Alimentamos o objeto equipe
                equipe.IdEquipe = int.Parse(linha[0]);
                equipe.Nome = linha[1];
                equipe.Imagem = linha[2];

                // Adicionamos a equipe na lista de equipes
                equipes.Add(equipe);
            }

            return equipes;
        }

        public void Update(Equipe e)
        {
            //1; Vivo; vivo.jpg
            //1; Vivo; vivo.jpg
            //1; Vivo; vivo.jpg
            //1; Vivo; vivo.jpg
            //1; Vivo; vivo.jpg
            //2; Vivo; vivo.jpg


            List<string> linhas = ReadAllLinesCSV(PATH);

            //Removemos a linha que tenha o código a ser alterado

            linhas.RemoveAll(x => x.Split(";")[0] == e.IdEquipe.ToString());

            // Adiciona a linha alterada no final do arquivo com o mesmo código

            linhas.Add(Prepare(e));

            //Reescreve o csv com as alterações
            RewriteCSV(PATH, linhas);
        }
    }
}