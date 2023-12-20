using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebAppAleff.Application.Dto;
using WebAppAleff.Application.Services;
using WebAppAleff.Domain.Entities;

namespace WebAppAleff.Application.Models
{
    public class HistoricoModel
    {
        public DateTime DataHora { get; set; }
        public string NomeUsuario { get; set; }
        public string EnderecoIp { get; set; }

        public static List<HistoricoModel> RecurarListaAcessos()
        {
            List<Acesso> lsitaAcesso = AcessoService.Exibir();

            List<HistoricoModel> listaHistoricoModel = new List<HistoricoModel>();

            foreach (Acesso acesso in lsitaAcesso)
            {
                HistoricoModel _HistoricoModel = new HistoricoModel();
                _HistoricoModel.NomeUsuario = acesso.NomeUsuario;
                _HistoricoModel.DataHora = acesso.DataHoraAcesso;
                _HistoricoModel.EnderecoIp = acesso.EnderecoIp;

                listaHistoricoModel.Add(_HistoricoModel);
            }

            return listaHistoricoModel;
        }

        public static List<HistoricoModel> RecurarListaAcessosUsuario(int usuarioId)
        {
            List<Acesso> lsitaAcesso = AcessoService.Exibir(usuarioId);

            List<HistoricoModel> listaHistoricoModel = new List<HistoricoModel>();

            foreach (Acesso acesso in lsitaAcesso)
            {
                HistoricoModel _HistoricoModel = new HistoricoModel();
                _HistoricoModel.NomeUsuario = acesso.NomeUsuario;
                _HistoricoModel.DataHora = acesso.DataHoraAcesso;
                _HistoricoModel.EnderecoIp = acesso.EnderecoIp;

                listaHistoricoModel.Add(_HistoricoModel);
            }

            return listaHistoricoModel;
        }

        public static List<Usuario> RecurarListaUsuairosAcesso()
        {
            return AcessoService.ListaUsuairosAcesso();
        }

        public static List<AcessosHoraDto> AcessosHora(int usuarioId = 0)
        {
            return AcessoService.GetListAcessosHora(usuarioId);
        }



    }
}