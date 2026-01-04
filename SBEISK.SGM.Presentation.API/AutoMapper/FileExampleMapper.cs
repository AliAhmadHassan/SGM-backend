using AutoMapper;
using SBEISK.SGM.Domain.Entities;
using SBEISK.SGM.Presentation.API.ViewModels.FileExample;

namespace SBEISK.SGM.Presentation.API.AutoMapper
{
    public class FileExampleMapper : Profile
    {
        public FileExampleMapper()
        {
            CreateMap<FileExampleViewModel, FileExample>();
        }
    }
}
