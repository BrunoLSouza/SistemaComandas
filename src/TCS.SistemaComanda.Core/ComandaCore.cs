using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using TCS.SistemaComanda.Core.DTOs;
using TCS.SistemaComanda.Dados.Repositorio;
using TCS.SistemaComanda.Dominio;
using TCS.SistemaComanda.Dominio.Interfaces.Repositorio;

namespace TCS.SistemaComanda.Core
{
    public class ComandaCore
    {
        private IComandaRepositorio _comandaData;
        private IItemComandaRepositorio _itemComandaData;

        public ComandaCore()
        {
            _comandaData = new ComandaRepositorio();
            _itemComandaData = new ItemComandaRepositorio();
        }

        public bool VerificaComandaAberta(int idComanda)
        {
            bool comandaAberta = false;

            Comanda comanda = _comandaData.ObterPorId(idComanda);

            if (comanda != null)
            {
                comandaAberta = true;
            }

            return comandaAberta;
        }

        public bool InserirItens(int idComanda, List<ProdutoDTO> produtosDTO)
        {
            //if (VerificaComandaAberta(idComanda) == true)
            //{
                Comanda comanda = _comandaData.ObterPorId(idComanda);

                ProdutoCore produtoCore = new ProdutoCore();
                List<Produto> produtos = produtoCore.ObterListaProduto(produtosDTO);

                if (produtos != null && produtos.Count > 0)
                {

                    foreach (var produto in produtos)
                    {
                        ItemComanda itemComanda = new ItemComanda()
                        {
                            IdItemComanda = Guid.NewGuid(),
                            IdComanda = comanda.IdComanda,
                            IdProduto = produto.IdProduto
                        };

                       _itemComandaData.Salvar(itemComanda);

                    }

                    return true;
                }
                
            //}

            return false;

        }

        public List<ItemComandaDTO> ListarItens(int idComanda)
        {
            if (idComanda > 0)
            {
                Comanda comanda = _comandaData.ObterPorId(idComanda);

                if (comanda != null && comanda.Itens.Count > 0)
                {
                    List<ItemComandaDTO> itensDTO = new List<ItemComandaDTO>();

                    foreach (var item in comanda.Itens)
                    {
                        var json = JsonConvert.SerializeObject(item);
                        ItemComandaDTO jsonDTO = JsonConvert.DeserializeObject<ItemComandaDTO>(json);

                        itensDTO.Add(jsonDTO);

                    }

                    

                    return itensDTO;
                }                           

            }
            
            return null;     

        }

    }
}
