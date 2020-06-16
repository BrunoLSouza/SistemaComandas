using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Runtime.InteropServices.WindowsRuntime;
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

        public MensagemDTO InserirItens(int idComanda, List<int> idProdutos)
        {
            MensagemDTO mensagemDTO = new MensagemDTO();

            try
            {
                Comanda comanda = _comandaData.ObterPorId(idComanda);

                ProdutoCore produtoCore = new ProdutoCore();
                List<Produto> produtos = produtoCore.ObterListaProduto(idProdutos);

                if (comanda != null && produtos != null && produtos.Count() > 0 && 
                    ValidaQuantidadeSuco(comanda.Itens) < 3)
                {

                    //comanda.Itens

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

                    mensagemDTO.Mensagem = "Produto Adicionado com Sucesso";
                    mensagemDTO.Sucesso = true;
                    mensagemDTO.Tipo = "Sucesso";


                }
                else
                {
                    mensagemDTO.Mensagem = "Essa comanda atingiu o limite suco";
                    mensagemDTO.Sucesso = false;
                    mensagemDTO.Tipo = "Alerta";
                }

                return mensagemDTO;

            }
            catch (Exception ex)
            {
                mensagemDTO.Mensagem = ex.Message;
                mensagemDTO.Sucesso = false;
                mensagemDTO.Tipo = "Erro";

                return mensagemDTO;
            }

            mensagemDTO.Mensagem = "Erro ao inserir produto";
            mensagemDTO.Sucesso = false;
            mensagemDTO.Tipo = "Erro";

            return mensagemDTO;

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

        public NotaFiscalDTO FecharComanda(int idComanda)
        {
            if (idComanda > 0 && VerificaComandaAberta(idComanda))
            {
                Comanda comanda = _comandaData.ObterPorId(idComanda);

                if (comanda.Itens.Count() > 0)
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

                    var jsonDTO = JsonConvert.SerializeObject(notafiscal);
                    NotaFiscalDTO notaFiscalDTO = JsonConvert.DeserializeObject<NotaFiscalDTO>(jsonDTO);

                    return notaFiscalDTO;

                }

                return null;
            }

            return null;

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
                AguaGratis(itensNotaFiscal, anotacoesNotaFiscal);
                DescontoCerveja(itensNotaFiscal);

                RemoveItemSemQuantidade(itensNotaFiscal);

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
                ItemNotaFiscal cerveja = itensNF.Where(i => i.Produto.ToLower() == "cerveja").FirstOrDefault();
                int qtdCerveja = 0;
                if (cerveja != null)
                {
                    qtdCerveja = cerveja.Quantidade;
                };
                

                ItemNotaFiscal suco = itensNF.Where(i => i.Produto.ToLower() == "suco").FirstOrDefault();
                int qtdSuco = 0;
                if (suco != null)
                {
                    qtdSuco = suco.Quantidade;
                };

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

            ItemNotaFiscal cerveja = itensNF.Where(i => i.Produto.ToLower() == "cerveja").FirstOrDefault();
            int qtdCerveja = 0;
            if (cerveja != null)
            {
                qtdCerveja = cerveja.Quantidade;
            };


            ItemNotaFiscal conhaque = itensNF.Where(i => i.Produto.ToLower() == "conhaque").FirstOrDefault();
            int qtdConhaque = 0;
            if (conhaque != null)
            {
                qtdConhaque = conhaque.Quantidade;
            };

            //int qtdCerveja = itensNF.Where(i => i.Produto.ToLower() == "cerveja").FirstOrDefault().Quantidade;
            //int qtdConhaque = itensNF.Where(i => i.Produto.ToLower() == "conhaque").FirstOrDefault().Quantidade;

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

                        int qtdAddlAgua = 0;
                        if (itemNF.Quantidade >= qtdAguaDeGraca)
                        {
                            qtdAddlAgua = qtdAguaDeGraca;
                        }
                        else
                        {
                            qtdAddlAgua = itemNF.Quantidade;
                        }

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

                        if (qtdAddlAgua > 0)
                        {
                            ItemNotaFiscal novoItem = new ItemNotaFiscal()
                            {
                                IdItemNotaFiscal = Guid.NewGuid(),
                                Produto = "Água (Promoção)",
                                Valor = 0.00,
                                Quantidade = qtdAddlAgua
                            };

                            itensNF.Add(novoItem);
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

        public void RemoveItemSemQuantidade(List<ItemNotaFiscal> itensNF)
        {
            if (itensNF != null && itensNF.Count() > 0)
            {
                List<ItemNotaFiscal> removeItens = itensNF.Where(i => i.Quantidade == 0).ToList();

                if (removeItens.Count > 0)
                {
                    foreach (var item in removeItens)
                    {
                        itensNF.Remove(item);
                    }
                }

            }
        }

        public int ValidaQuantidadeSuco(List<ItemComanda> itens) 
        {
            List<ItemComanda> sucos = new List<ItemComanda>();
            
            sucos = itens.Where(i => i.Produto.Nome.ToLower() == "suco").ToList();

            if (sucos.Count() > 0) 
            {
                return sucos.Count();
            }

            return 0;
        }

    }
}
