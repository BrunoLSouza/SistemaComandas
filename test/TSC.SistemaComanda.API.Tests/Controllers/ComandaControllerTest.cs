﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.UI.WebControls;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using TCS.SistemaComanda.Core;
using TCS.SistemaComanda.Core.DTOs;
using TSC.SistemaComanda.API.Controllers;

namespace TSC.SistemaComanda.API.Tests.Controllers
{
    [TestClass]
    public class ComandaControllerTest
    {       

        [TestMethod]
        public void ObterTodas()
        {
            ComandaController controller = new ComandaController();

            List<ComandaDTO> result = controller.ObterTodas();

            Assert.IsNotNull(result);
            Assert.AreEqual(10, result.Count());
        }

        [TestMethod]
        public void ObterTodosProdutos()
        {
            ComandaController controller = new ComandaController();

            List<ProdutoDTO> result = controller.ObterTodosProdutos();

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Count() > 0);
        }

        [TestMethod]
        public void Fechar()
        {
            ComandaController controller = new ComandaController();

            List<int> listaIdProdutos = new List<int>();
            listaIdProdutos.Add(1);
            listaIdProdutos.Add(2);
            listaIdProdutos.Add(3);
            listaIdProdutos.Add(4);
                
            InserirProdutoDTO inserirProduto = new InserirProdutoDTO()
            {
                IdComanda = 10,
                IdProdutos = listaIdProdutos
            };

            controller.AdicionarItem(inserirProduto);

            NotaFiscalDTO result = controller.Fechar(10);

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Itens.Count() > 0);
        }

        [TestMethod]
        public void AdicionarItem()
        {
            ComandaController controller = new ComandaController();

            List<int> listaIdProdutos = new List<int>();
            listaIdProdutos.Add(1);
            listaIdProdutos.Add(2);
            listaIdProdutos.Add(3);
            listaIdProdutos.Add(4);

            InserirProdutoDTO inserirProduto = new InserirProdutoDTO()
            {
                IdComanda = 10,
                IdProdutos = listaIdProdutos
            };

            //bool result = false;
            MensagemDTO result = controller.AdicionarItem(inserirProduto);

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Sucesso);

            controller.Fechar(10);

        }

        [TestMethod]
        public void ValidaLimiteSuco()
        {
            ComandaController controller = new ComandaController();

            List<int> listaIdProdutos = new List<int>();
            listaIdProdutos.Add(3);
            listaIdProdutos.Add(3);
            listaIdProdutos.Add(3);

            InserirProdutoDTO inserirProduto = new InserirProdutoDTO()
            {
                IdComanda = 10,
                IdProdutos = listaIdProdutos
            };

            controller.AdicionarItem(inserirProduto);

            List<int> novaLista = new List<int>();
            novaLista.Add(3);

            //bool result = false;
            MensagemDTO result = controller.AdicionarItem(inserirProduto);

            Assert.IsNotNull(result);
            Assert.IsTrue(!result.Sucesso);
            Assert.AreEqual("alerta", result.Tipo.ToLower());
            Assert.AreEqual("essa comanda atingiu o limite suco", result.Mensagem.ToLower());

            controller.Fechar(10);
        }

    }
}
