using System;
using AutoMapper;
using SBEISK.SGM.Domain.Entities;
using SBEISK.SGM.Domain.Entities.Views;
using SBEISK.SGM.Presentation.API.ViewModels.STM;
using SBEISK.SGM.Presentation.API.ViewModels.TransferAttendanceMaterial;

namespace SBEISK.SGM.Presentation.API.AutoMapper
{
    public class VwTransferAttendanceMaterialMapProfile : Profile
    {
        public VwTransferAttendanceMaterialMapProfile()
        {
            CreateMap<VwTransferAttendanceMaterialResponse, VwTransferAttendanceMaterial>()
                .ForMember(x => x.InsIdOrigem, map => map.MapFrom(x => x.InsIdOrigem))
                .ForMember(x => x.InsNomeDestino, map => map.MapFrom(x => x.InsNomeDestino))
                .ForMember(x => x.InsNomeOrigem, map => map.MapFrom(x => x.InsNomeOrigem))
                .ForMember(x => x.InstIdDestino, map => map.MapFrom(x => x.InstIdDestino))
                .ForMember(x => x.StmDtAprovacao, map => map.MapFrom(x => x.StmDtAprovacao))
                .ForMember(x => x.StmDtAtualizacao, map => map.MapFrom(x => x.StmDtAtualizacao))
                .ForMember(x => x.StmDtCriacao, map => map.MapFrom(x => x.StmDtCriacao))
                .ForMember(x => x.Stm_Id, map => map.MapFrom(x => x.Stm_Id))
                .ForMember(x => x.stsDescricao, map => map.MapFrom(x => x.stsDescricao))
                .ForMember(x => x.StsId, map => map.MapFrom(x => x.StsId))
                .ForMember(x => x.UsuIdAprovador, map => map.MapFrom(x => x.UsuIdAprovador))
                .ForMember(x => x.UsuIdCriacao, map => map.MapFrom(x => x.UsuIdCriacao))
                .ForMember(x => x.UsuIdSolicitante, map => map.MapFrom(x => x.UsuIdSolicitante))
                .ForMember(x => x.UsuNomeAprovador, map => map.MapFrom(x => x.UsuNomeAprovador))
                .ForMember(x => x.UsuNomeCriacao, map => map.MapFrom(x => x.UsuNomeCriacao))
                .ForMember(x => x.UsuNomeSolicitante, map => map.MapFrom(x => x.UsuNomeSolicitante));

            CreateMap<VwTransferAttendanceMaterial, VwTransferAttendanceMaterialResponse>()
                .ForMember(x => x.InsIdOrigem, map => map.MapFrom(x => x.InsIdOrigem))
                .ForMember(x => x.InsNomeDestino, map => map.MapFrom(x => x.InsNomeDestino))
                .ForMember(x => x.InsNomeOrigem, map => map.MapFrom(x => x.InsNomeOrigem))
                .ForMember(x => x.InstIdDestino, map => map.MapFrom(x => x.InstIdDestino))
                .ForMember(x => x.StmDtAprovacao, map => map.MapFrom(x => x.StmDtAprovacao))
                .ForMember(x => x.StmDtAtualizacao, map => map.MapFrom(x => x.StmDtAtualizacao))
                .ForMember(x => x.StmDtCriacao, map => map.MapFrom(x => x.StmDtCriacao))
                .ForMember(x => x.Stm_Id, map => map.MapFrom(x => x.Stm_Id))
                .ForMember(x => x.stsDescricao, map => map.MapFrom(x => x.stsDescricao))
                .ForMember(x => x.StsId, map => map.MapFrom(x => x.StsId))
                .ForMember(x => x.UsuIdAprovador, map => map.MapFrom(x => x.UsuIdAprovador))
                .ForMember(x => x.UsuIdCriacao, map => map.MapFrom(x => x.UsuIdCriacao))
                .ForMember(x => x.UsuIdSolicitante, map => map.MapFrom(x => x.UsuIdSolicitante))
                .ForMember(x => x.UsuNomeAprovador, map => map.MapFrom(x => x.UsuNomeAprovador))
                .ForMember(x => x.UsuNomeCriacao, map => map.MapFrom(x => x.UsuNomeCriacao))
                .ForMember(x => x.UsuNomeSolicitante, map => map.MapFrom(x => x.UsuNomeSolicitante));


            CreateMap<STMMaterial, MaterialSTMResponse>()
                .ForMember(x => x.Description, map => map.MapFrom(x => x.Material.Description))
                .ForMember(x => x.Discipline, map => map.Ignore())
                .ForMember(x => x.MaterialCode, map => map.MapFrom(x => x.Material.Code))
                .ForMember(x => x.Requested, map => map.MapFrom(x => x.Amount))
                .ForMember(x => x.Item, map => map.MapFrom(x => x.Item))
                .ForMember(x => x.Amount, map => map.MapFrom(x => x.Amount))
                .ForMember(x => x.Unity, map => map.MapFrom(x => x.Material.Unity));
        }
    }
}