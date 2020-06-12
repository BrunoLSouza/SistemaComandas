using System;
using System.Collections.Generic;
using System.Linq;
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

        public List<ItemComanda> ListarItens(int idComanda)
        {
            if (idComanda > 0)
            {
                List<ItemComanda> itens = _itemComandaData.Buscar(i => i.IdComanda == idComanda).ToList();
                return itens;                

            }
            
            return null;     

        }

    }
}
