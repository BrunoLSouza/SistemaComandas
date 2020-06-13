using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Reflection;
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
        private INotaFiscalRepositorio _notaFiscalComandaData;
        
        public ComandaCore()
        {
            _comandaData = new ComandaRepositorio();
            _itemComandaData = new ItemComandaRepositorio();
            _notaFiscalComandaData = new NotaFiscalRepositorio();
        }

        public bool VerificaComandaAberta(int idComanda)
        {
            bool comandaAberta = false;

            Comanda comanda = _comandaData.ObterPorId(idComanda);

            if (comanda != null && comanda.Aberta == true)
            {
                comandaAberta = true;
            }

            return comandaAberta;
        }

        public bool InserirItens(int idComanda, List<int> idProdutos)
        {
            //if (VerificaComandaAberta(idComanda) == true)
            //{
                Comanda comanda = _comandaData.ObterPorId(idComanda);

                ProdutoCore produtoCore = new ProdutoCore();
                List<Produto> produtos = produtoCore.ObterListaProduto(idProdutos);

                if (produtos != null && produtos.Count() > 0)
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

                comanda.Aberta = true;
                _comandaData.Alterar(comanda);

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

                if (comanda != null && comanda.Itens.Count() > 0)
                {
                    List<ItemComandaDTO> itensDTO = new List<ItemComandaDTO>();

                    foreach (ItemComanda item in comanda.Itens)
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

        public void AbrirComanda(int idComanda) 
        {
            if (idComanda > 0)
            {
                Comanda comanda = _comandaData.ObterPorId(idComanda);
                
                if (comanda != null)
                {
                    comanda.Aberta = true;
                    _comandaData.Alterar(comanda);
                }                
            }
        }

        public void ResetarComanda(int idComanda)
        {
            if (idComanda > 0)
            {
                Comanda comanda = _comandaData.ObterPorId(idComanda);

                if (comanda != null && comanda.Itens.Count() > 0)
                {
                    List<ItemComanda> itens = new List<ItemComanda>();
                    itens.AddRange(comanda.Itens);

                    foreach (ItemComanda item in itens)
                    {
                        _itemComandaData.Remover(item.IdItemComanda);
                    }

                    comanda.Aberta = false;
                    _comandaData.Alterar(comanda);

                }

            }
        }

        public void FecharComanda(int idComanda)
        {
            if (idComanda > 0 && VerificaComandaAberta(idComanda))
            {
                Comanda comanda = _comandaData.ObterPorId(idComanda);

                if (comanda.Itens.Count() > 1)
                {
                    //Obtem uma lista com todos os produtos
                    List<Produto> produtos = new List<Produto>();

                    foreach (var p in comanda.Itens)
                    {
                        produtos.Add(p.Produto);
                    }

                    //Nota Fiscal
                    NotaFiscal notafiscal = GeraNotaFiscal(produtos);

                    if (notafiscal.IdNotaFiscal != Guid.Empty)
                    {
                        ResetarComanda(idComanda);
                    }

                }

            }
        }

        public NotaFiscal GeraNotaFiscal(List<Produto> produtos) 
        {
            List<ItemNotaFiscal> itensNotaFiscal = new List<ItemNotaFiscal>();
            NotaFiscal notaFiscal = new NotaFiscal();

            if (produtos != null && produtos.Count() > 0)
            {
                List<Produto> produtosDistintos = produtos.Distinct().ToList();

                foreach (var prod in produtosDistintos)
                {
                    ItemNotaFiscal itemNotaFiscal = new ItemNotaFiscal()
                    {
                        IdItemNotaFiscal = Guid.NewGuid(),
                        Produto = prod.Nome,
                        Quantidade = produtos.Where(p => p.Nome.ToLower() == prod.Nome.ToLower()).Count(),
                        Valor = prod.Valor
                    };

                    itensNotaFiscal.Add(itemNotaFiscal);

                };

                List<AnotacaoNotaFiscal> anotacoesNotaFiscal = new List<AnotacaoNotaFiscal>();

                //promoções
                DescontoCerveja(itensNotaFiscal);
                AguaGratis(itensNotaFiscal, anotacoesNotaFiscal);

                notaFiscal.IdNotaFiscal = Guid.NewGuid();
                notaFiscal.Itens = itensNotaFiscal;
                notaFiscal.Anotacoes = anotacoesNotaFiscal;

            }

            notaFiscal = _notaFiscalComandaData.Salvar(notaFiscal);



            return notaFiscal;

        }
        
        public void DescontoCerveja(List<ItemNotaFiscal> itensNF)
        {
            if (itensNF != null && itensNF.Count() > 0)
            {
                int qtdCerveja = itensNF.Where(i => i.Produto.ToLower() == "cerveja").FirstOrDefault().Quantidade;
                int qtdSuco = itensNF.Where(i => i.Produto.ToLower() == "suco").FirstOrDefault().Quantidade;

                int qtdCervejaComDesconto = (qtdCerveja - qtdSuco <= 0) ? qtdCerveja : qtdSuco;

                if (qtdCervejaComDesconto > 0)
                {
                    foreach (var itemNF in itensNF)
                    {
                        if (itemNF.Produto.ToLower() == "cerveja")
                        {
                            itemNF.Quantidade -= qtdCervejaComDesconto;
                        }
                    }

                    ItemNotaFiscal novoItem = new ItemNotaFiscal()
                    {
                        IdItemNotaFiscal = Guid.NewGuid(),
                        Produto = "Cerveja (Promoção)",
                        Valor = 3.00,
                        Quantidade = qtdCervejaComDesconto
                    };

                    itensNF.Add(novoItem);

                }
            }

        }

        public void AguaGratis(List<ItemNotaFiscal> itensNF, List<AnotacaoNotaFiscal> anotacoesNF) 
        {
            var qtdAguaDeGraca = 0;

            int qtdCerveja = itensNF.Where(i => i.Produto.ToLower() == "cerveja").FirstOrDefault().Quantidade;
            int qtdConhaque = itensNF.Where(i => i.Produto.ToLower() == "conhaque").FirstOrDefault().Quantidade;

            var qtdConhaquePromo = qtdConhaque / 3;
            var qtdCervejaPromo = qtdCerveja / 2;

            if (qtdConhaquePromo > 0 && qtdCervejaPromo > 0)
            {
                if (qtdConhaquePromo < qtdCervejaPromo)
                {
                    qtdAguaDeGraca = qtdConhaquePromo;
                }
                else
                {
                    qtdAguaDeGraca = qtdCervejaPromo;
                }
            }

            if (qtdAguaDeGraca > 0)
            {
                bool temAgua = false;

                foreach (var itemNF in itensNF)
                {
                    if (itemNF.Produto.ToLower() == "água") 
                    {
                        temAgua = true;
                        itemNF.Quantidade -= qtdAguaDeGraca;

                        if (itemNF.Quantidade < 0)
                        {
                            AnotacaoNotaFiscal anotacao = new AnotacaoNotaFiscal()
                            {
                                IdAnotacaoNotaFiscal = Guid.NewGuid(),
                                Descricao = "O cliente tem direito a " + Math.Abs(itemNF.Quantidade) + " água(s) grátis.",
                            };
                            anotacoesNF.Add(anotacao);

                            itemNF.Quantidade = 0;

                        }

                        break;
                    }
                }

                if (temAgua == false)
                {
                    AnotacaoNotaFiscal anotacao = new AnotacaoNotaFiscal()
                    {
                        IdAnotacaoNotaFiscal = Guid.NewGuid(),
                        Descricao = "O cliente tem direito a " + qtdAguaDeGraca + " água(s) grátis.",
                    };
                    anotacoesNF.Add(anotacao);
                }

            }

        }

        public List<ComandaDTO> ObterTodasComandas()
        {
            List<Comanda> comandas =  _comandaData.ObterTodos().ToList();

            var json = JsonConvert.SerializeObject(comandas);
            List<ComandaDTO> comandasDTO = JsonConvert.DeserializeObject<List<ComandaDTO>>(json);

            return comandasDTO;

        }

    }
}
