using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using TSC.SistemaComanda.Web.DTO;
using TSC.SistemaComanda.Web.Models;
using TSC.SistemaComanda.Web.Services.Comanda;

namespace TSC.SistemaComanda.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IComandaService _comandaService;

        public HomeController()
        {
            _comandaService = new ComandaService();
        }

        public async Task<ActionResult> Index()
        {
            string token = await _comandaService.Token();
            List<ComandaDTO> comandasDTO = await _comandaService.ObterComandasAsync(token);
            
            var jsonDTO = JsonConvert.SerializeObject(comandasDTO);
            List<ComandaViewModel> comandasVM = JsonConvert.DeserializeObject<List<ComandaViewModel>>(jsonDTO);

            return View(comandasVM);

        }

        public async Task<ActionResult> Produtos(int idComanda)
        {
            string token = await _comandaService.Token();
            List<ProdutoDTO> produtosDTO = await _comandaService.ObterProdutosAsync(token);

            var jsonDTO = JsonConvert.SerializeObject(produtosDTO);
            List<ProdutoViewModel> produtosVM = JsonConvert.DeserializeObject<List<ProdutoViewModel>>(jsonDTO);

            ViewBag.IdComanda = idComanda;

            return View(produtosVM);
        }

        public async Task<ActionResult> AdicionarProdutos(int idComanda, int idProduto)
        {            
            if (idComanda > 0 && idProduto > 0) 
            {
                List<int> idProdutos = new List<int>();
                idProdutos.Add(idProduto);

                InserirProdutoDTO inserirProdutoDTO = new InserirProdutoDTO()
                {
                    IdComanda = idComanda,
                    IdProdutos = idProdutos
                };

                string token = await _comandaService.Token();
                MensagemDTO mensagemDTO =  await _comandaService.AdicionarProdutoAsync(inserirProdutoDTO, token);
                return Json(mensagemDTO, JsonRequestBehavior.AllowGet);

            }
            else
            {
                MensagemDTO mensagemDTO = new MensagemDTO() { 
                    Mensagem = "Produto e/ou Comanda invalido.",
                    Tipo = "Alerta"
                };
                return Json(mensagemDTO, JsonRequestBehavior.AllowGet);
            }
            

        }

        public async Task<ActionResult> FecharComanda(int idComanda)
        {
            string token = await _comandaService.Token();
            NotaFiscalDTO notaFiscalDTO = await _comandaService.FecharComandaAsync(idComanda, token);

            var jsonDTO = JsonConvert.SerializeObject(notaFiscalDTO);
            NotaFiscalViewModel notaFiscalVM = JsonConvert.DeserializeObject<NotaFiscalViewModel>(jsonDTO);

            ViewBag.IdComanda = idComanda;

            double totalNF = 0;
            if (notaFiscalVM != null && notaFiscalVM.Itens.Count > 0)
            {
                foreach (var item in notaFiscalVM.Itens)
                {
                    totalNF += item.Total;
                }

                notaFiscalVM.Total = totalNF;

            }

            return View("NotaFiscal",notaFiscalVM);
        }
    }
}