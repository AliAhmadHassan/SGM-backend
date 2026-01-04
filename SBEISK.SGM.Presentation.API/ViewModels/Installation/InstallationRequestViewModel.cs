
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using SBEISK.SGM.Presentation.API.ViewModels.UserProfileInstallation;

namespace SBEISK.SGM.Presentation.API.ViewModels.Installation
{
    public class InstallationRequestViewModel
    {
        [Required(ErrorMessage = "Campo Nome é obrigatório.", AllowEmptyStrings = false)]
        [StringLength(128, ErrorMessage = "Campo Nome suporta até 128 caracteres.")]
        public string Name { get; set; }
        
        [StringLength(256, ErrorMessage = "Campo Descrição suporta até 256 caracteres.")]
        public string Description { get; set; } 
        
        [Required(ErrorMessage = "Campo Nome é obrigatório.")]
        public bool ThirdMaterialPermission { get; set; }

        [Range(1,  Int32.MaxValue, ErrorMessage = "Campo tipo de instalação é obrigatório.")]             
        public int TypeId { get; set; }
        
        [Range(1,  Int32.MaxValue, ErrorMessage = "Campo projeto é obrigatório.")] 
        public int ProjectId { get; set; }

        [Range(1,  Int32.MaxValue, ErrorMessage = "Campo endereço é obrigatório.")] 
        public int AddressId { get; set; }
    }
}