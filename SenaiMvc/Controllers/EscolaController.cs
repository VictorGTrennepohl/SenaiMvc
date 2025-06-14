using Microsoft.AspNetCore.Mvc;
using SenaiMvc.Models.Escola;
using SenaiMvc.Service.Interfaces;

namespace SenaiMvc.Controllers
{
    public class EscolaController : Controller
    {

        private readonly IApiService _apiService;

        public EscolaController(IApiService apiService)
        {
            _apiService = apiService;
        }   


        public async Task<IActionResult> Index()
        {
            var escolas = await _apiService.GetAsync<List<EscolaModel>>("Escola/api/v1/buscar-todos");
            return View(escolas);
        }

        [HttpGet]
        public IActionResult Form() 
        {
            var model = new EscolaModel();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Form(EscolaModel model)//salvar
        {
            if (ModelState.IsValid) 
            {
                var retorno = await _apiService.PostAsync<EscolaModel>("Escola/api/v1", model);
                return Redirect("Index");
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Editar(long id)
        {
            var model = await _apiService.GetAsync<EscolaModel>($"Escola/api/v1/PegarPorId?id={id}");
            return View("Form", model);
        }

        [HttpGet]
        public async Task<IActionResult> Excluir(long id)
        {
            var model = await _apiService.DeleteAsync($"/Escola/api/v1?id={id}");
            return Redirect("/Escola/Index");
        }
    }
}
