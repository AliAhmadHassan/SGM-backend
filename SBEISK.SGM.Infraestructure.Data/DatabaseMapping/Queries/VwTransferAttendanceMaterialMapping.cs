using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SBEISK.SGM.Domain.Entities.Views;
using System;
using System.Collections.Generic;
using System.Text;

namespace SBEISK.SGM.Infraestructure.Data.DatabaseMapping.Queries
{
    public class VwTransferAttendanceMaterialMapping : IQueryTypeConfiguration<VwTransferAttendanceMaterial>
    {
        public void Configure(QueryTypeBuilder<VwTransferAttendanceMaterial> builder)
        {
            builder.ToView("VW_TransferAttendanceMaterialMapping");
            builder.Property(x => x.StmDtCriacao).HasColumnName("STM_DT_CRIACAO");
            builder.Property(x => x.StmDtAprovacao).HasColumnName("STM_DT_APROVACAO");
            builder.Property(x => x.StmDtAtualizacao).HasColumnName("STM_DT_ATUALIZACAO");
            builder.Property(x => x.UsuIdCriacao).HasColumnName("USU_ID_CRIACAO");
            builder.Property(x => x.UsuNomeCriacao).HasColumnName("USU_Nome_CRIACAO");
            builder.Property(x => x.UsuIdAprovador).HasColumnName("USU_ID_APROVADOR");
            builder.Property(x => x.UsuNomeAprovador).HasColumnName("USU_Nome_APROVADOR");
            builder.Property(x => x.UsuIdSolicitante).HasColumnName("USU_ID_SOLICITANTE");
            builder.Property(x => x.UsuNomeSolicitante).HasColumnName("USU_Nome_SOLICITANTE");
            builder.Property(x => x.InsIdOrigem).HasColumnName("INS_ID_ORIGEM");
            builder.Property(x => x.InsNomeOrigem).HasColumnName("INS_NOME_ORIGEM");
            builder.Property(x => x.InstIdDestino).HasColumnName("INS_ID_DESTINO");
            builder.Property(x => x.InsNomeDestino).HasColumnName("INS_NOME_DESTINO");
            builder.Property(x => x.StsId).HasColumnName("STS_ID");
            builder.Property(x => x.stsDescricao).HasColumnName("STS_DESCRICAO");
        }
    }
}
