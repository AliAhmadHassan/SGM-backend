using System;
using System.Collections.Generic;
using System.Text;

namespace SBEISK.SGM.Domain.Entities.Views
{
    public class VwTransferAttendanceMaterial
    {
        public int Stm_Id { get; set; }
        public DateTime StmDtCriacao { get; set; }
        public DateTime StmDtAprovacao { get; set; }
        public DateTime StmDtAtualizacao { get; set; }
        public int UsuIdCriacao { get; set; }
        public string UsuNomeCriacao { get; set; }
        public int UsuIdAprovador { get; set; }
        public string UsuNomeAprovador { get; set; }
        public int UsuIdSolicitante { get; set; }
        public string UsuNomeSolicitante { get; set; }
        public int InsIdOrigem { get; set; }
        public string InsNomeOrigem { get; set; }
        public int InstIdDestino { get; set; }
        public string InsNomeDestino { get; set; }
        public int StsId { get; set; }
        public string stsDescricao { get; set; }

    }
}
