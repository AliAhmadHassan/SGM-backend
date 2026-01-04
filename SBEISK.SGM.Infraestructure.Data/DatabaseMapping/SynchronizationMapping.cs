using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SBEISK.SGM.Domain.Entities;
using System;

namespace SBEISK.SGM.Infraestructure.Data.DatabaseMapping
{
    public class SynchronizationMapping : IEntityTypeConfiguration<Synchronization>
    {
        public void Configure(EntityTypeBuilder<Synchronization> builder)
        {
            builder.ToTable("SINCRONIZACAO");
            builder.Property(x => x.Id).HasColumnName("SINC_ID");
            builder.Property(x => x.Target).HasColumnName("SINC_ALVO");
            builder.Property(x => x.LastSuccess).HasColumnName("SINC_ULTIMO_SUCESSO");
            builder.Property(x => x.IsRunning).HasColumnName("SINC_EXECUTANDO");
            builder.Property(x => x.LastTrigger).HasColumnName("SINC_ULTIMO_DISPARO");
            builder.Property(x => x.Command).HasColumnName("SINC_COMANDO");
        }
    }
}
