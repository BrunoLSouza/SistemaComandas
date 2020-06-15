using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using TCS.SistemaComanda.Core.DTOs;
using TCS.SistemaComanda.Dados.Repositorio;
using TCS.SistemaComanda.Dominio;

namespace TCS.SistemaComanda.Core
{
    public class ProdutoCore
    {
        private ProdutoRepositorio _data;

        public ProdutoCore()
        {
            _data = new ProdutoRepositorio();
        }

        public Produto ObterProduto(int idProduto)
        {
            if (idProduto > 0)
            {
                return _data.ObterPorId(idProduto);
            }

            return null;

        }

        public List<Produto> ObterListaProduto(List<int> idProdutos)
        {
            if (idProdutos != null && idProdutos.Count > 0)
            {
                List<Produto> listaProduto = new List<Produto>();

                foreach (var id in idProdutos)
                {
                    Produto produto = ObterProduto(id);
                    listaProduto.Add(produto);
                }

                return listaProduto;

            }

            return null;

        }

        public List<ProdutoDTO> ObterTodos()
        {
            List<Produto> produtos = _data.ObterTodos().ToList();

            var json = JsonConvert.SerializeObject(produtos);
            List<ProdutoDTO> produtosDTO = JsonConvert.DeserializeObject<List<ProdutoDTO>>(json);

            return produtosDTO;

        }
    }
}

