using Microsoft.Ajax.Utilities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.UI.WebControls;
using TCS.SistemaComanda.Core;
using TCS.SistemaComanda.Core.DTOs;

namespace TSC.SistemaComanda.API.Controllers
{
    [Authorize]
    [RoutePrefix("api/Comanda")]
    public class ComandaController : ApiController
    {   
        [HttpGet]
        [Route("ObterTodas")]
        public List<ComandaDTO> ObterTodas()
        {
            try
            {
                ComandaCore comandaCore = new ComandaCore();
                List<ComandaDTO> comandasDto = comandaCore.ObterTodasComandas();

                return comandasDto;
            }
            catch (Exception e)
            {
                //return Request.CreateResponse(HttpStatusCode.InternalServerError, "Ocorreu um erro no servidor");
                return null;
            }
        }

        [HttpGet]
        [Route("ObterTodosProdutos")]
        public List<ProdutoDTO> ObterTodosProdutos()
        {
            try
            {
                ProdutoCore produtoCore = new ProdutoCore();
                List<ProdutoDTO> produtosDTO = produtoCore.ObterTodos();

                return produtosDTO;
            }
            catch (Exception e)
            {
                //return Request.CreateResponse(HttpStatusCode.InternalServerError, "Ocorreu um erro no servidor");
                return null;
            }
        }

        [HttpGet]
        [Route("Fechar")]
        public NotaFiscalDTO Fechar(int idComanda)
        {
            try
            {
                ComandaCore comandaCore = new ComandaCore();
                NotaFiscalDTO notaFiscalDTO = comandaCore.FecharComanda(idComanda);

                return notaFiscalDTO;
                


            }
            catch (Exception e)
            {
                return null;
            }

        }

        [HttpPost]
        [Route("Adicionar")]
        public MensagemDTO AdicionarItem([FromBody] InserirProdutoDTO inserirProdutoDTO)
        {
            try
            {
                ComandaCore comandaCore = new ComandaCore();
                MensagemDTO retorno = comandaCore.InserirItens(inserirProdutoDTO.IdComanda, inserirProdutoDTO.IdProdutos);

                
                return retorno;

            }
            catch (Exception e)
            {
                MensagemDTO mensagemDTO = new MensagemDTO();
                mensagemDTO.Mensagem = e.Message;
                mensagemDTO.Sucesso = false;
                mensagemDTO.Tipo = "Erro";

                return mensagemDTO;
            }

        }

    }
}
