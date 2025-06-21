using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Newtonsoft.Json;
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
        public async Task<IActionResult> Form() 
        {
            var model = new EscolaModel();
            await AlimentarEstados(model);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Form(EscolaModel model)//salvar
        {
            if (ModelState.IsValid) 
            {
                if (model.Endereco.Id == null)
                { 
                    model.Endereco.Id = 0;
                }
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

        private async Task AlimentarEstados(EscolaModel model)
        {
            var estados = await _apiService.PegarEstados<EstadoIBGE>();
            model.Estados = estados.OrderBy(e => e.Nome)
                .Select(e => new SelectListItem
                {
                    Value = e.Sigla,
                    Text = e.Nome
                })
                .ToList();
        }

        private async Task AlimentarCidades(EscolaModel model, string uf)
        {
            using var httpClient = new HttpClient();
            var response = await httpClient.GetAsync($"https://servicodados.ibge.gov.br/api/v1/localidades/estados/{uf}/distritos");

            if (!response.IsSuccessStatusCode)
            {
                model.Cidades = new List<SelectListItem>();
                return;
            }

            var json = await response.Content.ReadAsStringAsync(); 
            var cidades = JsonConvert.DeserializeObject<List<CidadeIBGE>>(json);

            model.Cidades = cidades.OrderBy(c => c.Nome)
                .Select(c => new SelectListItem

                {
                    Value = c.Id.ToString(),
                    Text = c.Nome,
                    Selected = c.Id.ToString() == model.Endereco.Cidade.ToString()
                }).ToList();
                
            
        }
    }
}
