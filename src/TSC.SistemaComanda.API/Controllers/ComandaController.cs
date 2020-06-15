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
    [RoutePrefix("api/Comanda")]
    public class ComandaController : ApiController
    {
        //[HttpPost]
        //[Route("Adicionar")]
        //public HttpResponseMessage AdicionarItem([FromBody] InserirProdutoDTO inserirProdutoDTO)
        //{
        //    try
        //    {
        //        ComandaCore comandaCore = new ComandaCore();
        //        var retorno = comandaCore.InserirItens(inserirProdutoDTO.IdComanda, inserirProdutoDTO.IdProdutos);

        //        if (retorno == true)
        //        {
        //            return Request.CreateResponse(HttpStatusCode.OK, "Produto Adicionado");
        //        }

        //    }
        //    catch (Exception e)
        //    {                                
        //        return Request.CreateResponse(HttpStatusCode.InternalServerError, "Ocorreu um erro no servidor");
        //    }

        //    return Request.CreateResponse(HttpStatusCode.BadRequest, "Ocorreu um erro ao adicionar o produto na comanda");
        //}

        //[HttpGet]
        //[Route("Fechar")]
        //[ResponseType(typeof(List<NotaFiscalDTO>))]
        //public IHttpActionResult Fechar(int idComanda)
        //{
        //    try
        //    {
        //        ComandaCore comandaCore = new ComandaCore();
        //        NotaFiscalDTO notaFiscalDTO = comandaCore.FecharComanda(idComanda);

        //        if(notaFiscalDTO != null)
        //        {
        //            return Ok(notaFiscalDTO);
        //        }
        //        else
        //        {
        //            return Content(HttpStatusCode.NoContent,notaFiscalDTO);
        //        }

                
        //    }
        //    catch (Exception e)
        //    {
        //        return InternalServerError();
        //    }

        //}

        ////[HttpGet]
        ////[Route("ListarItens")]
        ////public HttpResponseMessage ListarItens(int idComanda)
        ////{
        ////    try
        ////    {
        ////        ComandaCore comandaCore = new ComandaCore();
        ////        var retorno = comandaCore.ListarItens(idComanda);
        ////        return Request.CreateResponse(HttpStatusCode.OK,JsonConvert.SerializeObject(retorno));
        ////    }
        ////    catch (Exception e)
        ////    {
        ////        return Request.CreateResponse(HttpStatusCode.InternalServerError, "Ocorreu um erro no servidor");
        ////    }
        ////    return Request.CreateResponse(HttpStatusCode.BadRequest, "Ocorreu um erro ao adicionar o produto na comanda");
        ////}

        ////[HttpPost]
        ////[Route("Resetar")]
        ////public HttpResponseMessage Resetar(int idComanda)
        ////{
        ////    try
        ////    {
        ////        ComandaCore comandaCore = new ComandaCore();
        ////        comandaCore.ResetarComanda(idComanda);

        ////        return Request.CreateResponse(HttpStatusCode.OK, "Comanda Resetada");
        ////    }
        ////    catch (Exception e)
        ////    {
        ////        return Request.CreateResponse(HttpStatusCode.InternalServerError, "Ocorreu um erro no servidor");
        ////    }

        ////}

        //[HttpGet]
        //[Route("ObterTodas")]
        //[ResponseType(typeof(List<ComandaDTO>))]
        //public IHttpActionResult ObterTodas()
        //{
        //    try
        //    {
        //        ComandaCore comandaCore = new ComandaCore();
        //        List<ComandaDTO> comandasDto = comandaCore.ObterTodasComandas();

        //        return Ok(comandasDto);
        //    }
        //    catch (Exception e)
        //    {
        //        //return Request.CreateResponse(HttpStatusCode.InternalServerError, "Ocorreu um erro no servidor");
        //        return InternalServerError();
        //    }
        //}

        //[HttpGet]
        //[Route("ObterTodosProdutos")]
        //[ResponseType(typeof(List<ProdutoDTO>))]
        //public IHttpActionResult ObterTodosProdutos()
        //{
        //    try
        //    {
        //        ProdutoCore produtoCore = new ProdutoCore();
        //        List<ProdutoDTO> produtosDTO = produtoCore.ObterTodos();

        //        return Ok(produtosDTO);
        //    }
        //    catch (Exception e)
        //    {
        //        //return Request.CreateResponse(HttpStatusCode.InternalServerError, "Ocorreu um erro no servidor");
        //        return InternalServerError();
        //    }
        //}

        #region Novos para teste

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
        public bool AdicionarItem([FromBody] InserirProdutoDTO inserirProdutoDTO)
        {
            try
            {
                ComandaCore comandaCore = new ComandaCore();
                var retorno = comandaCore.InserirItens(inserirProdutoDTO.IdComanda, inserirProdutoDTO.IdProdutos);

                
                return retorno;

            }
            catch (Exception e)
            {
                return false;
            }

        }

        #endregion
    }
}
