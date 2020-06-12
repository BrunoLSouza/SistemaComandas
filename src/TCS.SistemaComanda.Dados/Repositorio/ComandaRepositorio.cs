using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using TCS.SistemaComanda.Dominio;
using TCS.SistemaComanda.Dominio.Interfaces.Repositorio;

namespace TCS.SistemaComanda.Dados.Repositorio
{
    public class ComandaRepositorio : RepositorioBase<Comanda>, IComandaRepositorio
    {
    }
}
