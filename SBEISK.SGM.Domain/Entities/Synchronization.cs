using SBEISK.SGM.Domain.Entities.Base;
using System;

namespace SBEISK.SGM.Domain.Entities
{
    public class Synchronization : Entity
    {
        public string Target { get; set; }
        public DateTime? LastSuccess { get; set; }
        public bool IsRunning { get; set; }
        public DateTime? LastTrigger { get; set; }
        public string Command { get; set; }
    }
}
