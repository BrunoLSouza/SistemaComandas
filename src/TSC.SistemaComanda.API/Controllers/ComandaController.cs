using Microsoft.Ajax.Utilities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TCS.SistemaComanda.Core;
using TCS.SistemaComanda.Core.DTOs;

namespace TSC.SistemaComanda.API.Controllers
{
    [RoutePrefix("api/Comanda")]
    public class ComandaController : ApiController
    {
        [HttpPost]
        [Route("Adicionar")]
        public HttpResponseMessage AdicionarItem(int idComanda, [FromBody] List<int> idProdutos)
        {
            try
            {
                ComandaCore comandaCore = new ComandaCore();
                var retorno = comandaCore.InserirItens(idComanda, idProdutos);

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
        public HttpResponseMessage Fechar(int idComanda)
        {
            try
            {
                ComandaCore comandaCore = new ComandaCore();
                comandaCore.FecharComanda(idComanda);

                return Request.CreateResponse(HttpStatusCode.OK, "Comanda Fechada");
            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Ocorreu um erro no servidor");
            }

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
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Ocorreu um erro no servidor");
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest, "Ocorreu um erro ao adicionar o produto na comanda");
        }

        [HttpPost]
        [Route("Resetar")]
        public HttpResponseMessage Resetar(int idComanda)
        {
            try
            {
                ComandaCore comandaCore = new ComandaCore();
                comandaCore.ResetarComanda(idComanda);

                return Request.CreateResponse(HttpStatusCode.OK, "Comanda Resetada");
            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Ocorreu um erro no servidor");
            }

        }

        [HttpPost]
        [Route("ObterTodas")]
        public HttpResponseMessage ObterTodas()
        {
            try
            {
                ComandaCore comandaCore = new ComandaCore();
                List<ComandaDTO> comandaDto = comandaCore.ObterTodasComandas();

                return Request.CreateResponse(HttpStatusCode.OK, JsonConvert.SerializeObject(comandaDto));
            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Ocorreu um erro no servidor");
            }
        }

    }
}
