using System;
using AutoMapper;
using SBEISK.SGM.Domain.Entities;
using SBEISK.SGM.Presentation.API.ViewModels.STM;

namespace SBEISK.SGM.Presentation.API.AutoMapper
{
    public class STMMapProfile : Profile
    {
        public STMMapProfile()
        {
            CreateMap<STMRequestViewModel, STM>()
                .ForMember(x => x.UserIdWithdraw, map => map.MapFrom(x => x.UserWithdrawId))
                .ForMember(x => x.UserIdRequester, map => map.MapFrom(x => x.UserRequestId))
                .ForMember(x => x.SolicitationStatusId, map => map.MapFrom(x => x.STMStatusId))
                .ForMember(x => x.IsDraft, map => map.MapFrom(x => false))
                .ForMember(x => x.EmissionDate, map => map.MapFrom(x => DateTime.Now))
                .ForMember(x => x.Emails, map => map.Ignore())
                .ForMember(x => x.Attachments, map => map.Ignore())
                .ForMember(x => x.STMMaterials, map => map.Ignore())
                .ForMember(x => x.Transfers, map => map.Ignore());

            CreateMap<STMRequestDraft, STM>()
                .ForMember(x => x.UserIdWithdraw, map => map.MapFrom(x => x.UserWithdrawId))
                .ForMember(x => x.UserIdRequester, map => map.MapFrom(x => x.UserRequestId))
                .ForMember(x => x.SolicitationStatusId, map => map.MapFrom(x => x.STMStatusId))
                .ForMember(x => x.IsDraft, map => map.MapFrom(x => true))
                .ForMember(x => x.EmissionDate, Mapper=> Mapper.MapFrom(x => DateTime.Now))
                .ForMember(x => x.Emails, map => map.Ignore())
                .ForMember(x => x.Attachments, map => map.Ignore())
                .ForMember(x => x.STMMaterials, map => map.Ignore())
                .ForMember(x => x.Transfers, map => map.Ignore());

            CreateMap<STM, STMViewModel>()
                .ForMember(x => x.STM, map => map.MapFrom(x => x.Id))
                .ForMember(x => x.InstallationDestiny, map => map.MapFrom(x => x.InstallationDestiny.Name))
                .ForMember(x => x.InstallationSource, map => map.MapFrom(x => x.InstallationSource.Name))
                .ForMember(x => x.Approver, map => map.MapFrom(x => x.UserWithdraw.Name))
                .ForMember(x => x.Requester, map => map.MapFrom(x => x.UserRequester.Name))
                .ForMember(x => x.Status, map => map.MapFrom(x => x.SolicitationStatus.Description))
                .BeforeMap(BeforeMap);
        }

        private void BeforeMap(STM stm, STMViewModel viewModel)
        {
            foreach (Transfer transfer in stm.Transfers)
            {
                viewModel.Transfer = transfer.TransferStatusId;
            }
        }
    }
}