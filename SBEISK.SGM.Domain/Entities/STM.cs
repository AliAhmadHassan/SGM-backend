using System;
using System.Collections.Generic;
using SBEISK.SGM.Domain.Entities.Base;

namespace SBEISK.SGM.Domain.Entities
{
    public class STM : Entity, IUserModel, ITimestampedModel
    {
        public DateTime EmissionDate { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public int UserId { get; set; }
        public int UserIdWithdraw { get; set; }
        public int UserIdRequester { get; set; }
        public int InstallationSourceId { get; set; }
        public int InstallationDestinyId { get; set; }
        public int SolicitationStatusId { get; set; }
        public bool IsDraft { get; set; }
        public User UserWithdraw { get; set; }
        public User UserRequester { get; set; }
        public Installation InstallationSource { get; set; }
        public Installation InstallationDestiny { get; set; }
        public SolicitationStatus SolicitationStatus { get; set; }
        public IList<STMAttachment> Attachments { get; set; }
        public IList<STMEmail> Emails { get; set; }
        public IList<STMMaterial> STMMaterials { get; set; }
        public IList<Transfer> Transfers { get; set; }
    }
}