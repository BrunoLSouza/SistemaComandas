using Microsoft.Ajax.Utilities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TCS.SistemaComanda.Core;

namespace TSC.SistemaComanda.API.Controllers
{
    [RoutePrefix("api/Comanda")]
    public class ComandaController : ApiController
    {
        [HttpPost]
        [Route("Adicionar")]
        public HttpResponseMessage AdicionarItem(int idComanda, [FromBody] List<ProdutoDTO> produtos)
        {
            try
            {
                ComandaCore comandaCore = new ComandaCore();
                var retorno = comandaCore.InserirItens(idComanda, produtos);

                if (retorno == true)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, "Produto Adicionado");
                }

            }
            catch (Exception e)
            {                                
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Ocorreu um erro no servidor");
            }

            return Request.CreateResponse(HttpStatusCode.BadRequest, "Ocorreu um erro ao adicionar o produto na comanda");
        }

        [HttpPost]
        [Route("Fechar")]
        public string Fechar(int idComanda)
        {
            return "Comanda Fechada";
        }

        [HttpGet]
        [Route("ListarItens")]
        public HttpResponseMessage ListarItens(int idComanda)
        {
            try
            {
                ComandaCore comandaCore = new ComandaCore();
                var retorno = comandaCore.ListarItens(idComanda);
                return Request.CreateResponse(HttpStatusCode.OK,JsonConvert.SerializeObject(retorno));
            }
            catch (Exception)
            {

                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Ocorreu um erro no servidor");
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest, "Ocorreu um erro ao adicionar o produto na comanda");
        }

        [HttpPost]
        [Route("Resetar")]
        public string Resetar(int idProduto)
        {
            return "Comanda Zerada";
        }

    }
}
