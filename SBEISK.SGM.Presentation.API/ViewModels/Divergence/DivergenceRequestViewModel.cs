
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using SBEISK.SGM.Presentation.API.ViewModels.Divergence;

namespace SBEISK.SGM.Presentation.API.ViewModels.Divergence
{
    public class DivergenceRequestViewModel
    {
        [Required(ErrorMessage = "Campo obrigatório.")]
        public int StatusId { get; set; }
        
        [Required(ErrorMessage = "Campo obrigatório.")]
        public int ReceivementId { get; set; }
        
       [StringLength(256, ErrorMessage = "Campo Descrição suporta até 255 caracteres.")]
        public string Description { get; set; }

        [StringLength(256, ErrorMessage = "Campo Descrição suporta até 255 caracteres.")]
        public string Note { get; set; }
        public IFormFileCollection File { get; set; }
    }
}