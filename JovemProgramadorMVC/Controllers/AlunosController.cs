using JovemProgramadorMVC.Data.Repositório.Interface;
using JovemProgramadorMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace JovemProgramadorMVC.Controllers
{
    public class AlunosController : Controller
    {
        private readonly IAlunoRepositorio _alunoRepositorio;
        private readonly IConfiguration _configuration;

        public AlunosController(IAlunoRepositorio alunoRepositorio, IConfiguration configuration)
        {
            _alunoRepositorio = alunoRepositorio;
            _configuration = configuration;
        }
        public IActionResult Index()
        {
            try
            {
                var alunos = _alunoRepositorio.BuscarAlunos();
                return View(alunos);
            }
            catch (System.Exception)
            {
                TempData["MensagemErro"] = "Ops, algo deu errado";
                return View();  
            }
        }

        public IActionResult Adicionar()
        {
            try
            {
                return View();
            }
            catch (System.Exception)
            {
                TempData["MensagemErro"] = "Ops, algo deu errado";
                return View();
            }
        }
        public IActionResult Editar(int id)
        {
            try
            {
                var aluno = _alunoRepositorio.BuscarId(id);
                return View("Editar", aluno);
            }
            catch (System.Exception)
            {
                TempData["MensagemErro"] = "Ops, algo deu errado";
                return View();
            }

        }
        public IActionResult EditarAluno(AlunoModel alunos)
        {
            try
            {
                _alunoRepositorio.EditarAluno(alunos);
                TempData["EditadoSucesso"] = "Aluno Editado com sucesso!";
                return RedirectToAction("Index");
            }
            catch (System.Exception)
            {
                TempData["MensagemErro"] = "Ops, algo deu errado";
                return View();
            }
        }

        public IActionResult InserirAluno(AlunoModel alunos)
        {
            try
            {
                _alunoRepositorio.InserirAluno(alunos);

                TempData["MensagemSucesso"] = "Aluno Adicionado com sucesso!";

                return RedirectToAction("Index");
            }
            catch (System.Exception)
            {
                TempData["MensagemErro"] = "Ops, algo deu errado";
                return View();
            }
        }
        public IActionResult ExcluirAluno(int id)
        {
            try
            {
                var aluno = _alunoRepositorio.BuscarId(id);
                _alunoRepositorio.ExcluirAluno(aluno);
                TempData["ExcluidoSucesso"] = "Aluno Excluido com sucesso!";
                return RedirectToAction("Index", aluno);
            }
            catch (System.Exception)
            {
                TempData["MensagemErro"] = "Ops, algo deu errado";
                return View();
            }
        }
        public async Task<IActionResult> BuscarEndereco(string cep)
        {
            try
            {
                cep = cep.Replace("-", "");

                EnderecoModel enderecoModel = new();

                using var client = new HttpClient();

                var result = await client.GetAsync(_configuration.GetSection("ApiCep")["BaseUrl"] + cep + "/json");

                if (result.IsSuccessStatusCode)
                {
                    enderecoModel = JsonSerializer.Deserialize<EnderecoModel>(
                        await result.Content.ReadAsStringAsync(), new JsonSerializerOptions() { });
                }

                return View("Endereco", enderecoModel);
            }
            catch (System.Exception)
            {
                TempData["MensagemErro"] = "Ops, algo deu errado";
                return View();
            }   
        }
    }
}