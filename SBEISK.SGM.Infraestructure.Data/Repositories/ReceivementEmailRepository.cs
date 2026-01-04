using System;
using System.Collections.Generic;
using System.Linq;
using SBEISK.SGM.CrossCutting.Utils.Merger;
using SBEISK.SGM.Domain.Entities;
using SBEISK.SGM.Domain.Repositories;
using SBEISK.SGM.Domain.TypeEmails;
using SBEISK.SGM.Infraestructure.Data.Context;
using SBEISK.SGM.Infraestructure.Data.Repositories.Base;

namespace SBEISK.SGM.Infraestructure.Data.Repositories
{
    public class ReceivementEmailRepository : Repository<ReceivementEmail>, IReceivementEmailRepository
    {
        private readonly IUserRepository userRepository;
        private readonly IReceivementInvoiceOrderRepository receivementInvoiceOrderRepository;
        private readonly ISTMRepository sTMRepository;
        private readonly ITransferRepository transferRepository;
        public ReceivementEmailRepository(SgmDataContext dataContext, IUserRepository userRepository, IReceivementInvoiceOrderRepository receivementInvoiceOrderRepository,
                                          ISTMRepository sTMRepository, ITransferRepository transferRepository) : base(dataContext)
        {
            this.userRepository = userRepository;
            this.receivementInvoiceOrderRepository = receivementInvoiceOrderRepository;
            this.sTMRepository = sTMRepository;
            this.transferRepository = transferRepository;
        }

        public IEnumerable<object> AddEmail(string emails, int type)
        {
            string[] emailsSplit = emails.Split(',');

            if (emails != null)
            {
                foreach (string email in emailsSplit)
                {
                    if (type.Equals(TypeEmails.isReceivementEmail.GetHashCode()))
                    {
                        ReceivementEmail mail = new ReceivementEmail();
                        mail.Email = email;
                        mail.ReceivementInvoiceOrderId = this.receivementInvoiceOrderRepository.LastIdReceiver();

                        yield return mail;
                    }
                    else if(type.Equals(TypeEmails.isSTMEmail.GetHashCode()))
                    {
                        STMEmail mail = new STMEmail();
                        mail.STMId = this.sTMRepository.LastIdSTM();
                        User user = this.userRepository.UserByEmail(email);

                        if(user != default(User))
                        {
                            mail.UserId = user.Id;
                        }

                        yield return mail;
                    }
                    else if(type.Equals(TypeEmails.isTransferEmail.GetHashCode()))
                    {
                        TransferEmail mail = new TransferEmail();
                        mail.TransferId = this.transferRepository.LastIdTransfer();
                        User user = this.userRepository.UserByEmail(email);

                        if(user != default(User))
                            mail.UserId = user.Id;

                        yield return mail;
                    }
                    else if(type.Equals(TypeEmails.isDirectExitReceiverEmail.GetHashCode()))
                    {
                        DirectExitReceiverEmail mail = new DirectExitReceiverEmail();
                        mail.Email = email;

                        yield return mail;
                    }
                    else if(type.Equals(TypeEmails.isDevolutionEmail.GetHashCode()))
                    {
                        ReceivementDevolutionEmail mail = new ReceivementDevolutionEmail();
                        mail.Email = email;

                        yield return mail;
                    }
                    else if(type.Equals(TypeEmails.isDirectExitEmail.GetHashCode()))
                    {
                        ExitEmail mail = new ExitEmail();
                        mail.Email = email;

                        yield return mail;
                    }
                }
            }
            yield break;
        }
        public void MergeReceivementsEmail(ICollection<ReceivementEmail> original, ICollection<ReceivementEmail> other, Action<ReceivementEmail, ReceivementEmail> updateStrategy)
        {
            Merger<ReceivementEmail> merger = new Merger<ReceivementEmail>((x, y) => x.Id == y.Id, (x, y) => x.ReceivementInvoiceOrderId == y.ReceivementInvoiceOrderId);
            MergeResult<ReceivementEmail> result = merger.Merge(original.ToList(), other.ToList());

            this.DataContext.ReceivementEmail.RemoveRange(result.ItemsToDelete);
            this.DataContext.ReceivementEmail.AddRange(result.ItemsToInsert);

            foreach (var item in result.ItemsToUpdate)
            {
                updateStrategy(item.Original, item.Modified);
            }
        }

    }
}