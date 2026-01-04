using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Http;
using SBEISK.SGM.CrossCutting.Utils.Merger;
using SBEISK.SGM.Domain.Entities;
using SBEISK.SGM.Domain.Repositories;
using SBEISK.SGM.Infraestructure.Data.Context;
using SBEISK.SGM.Infraestructure.Data.Repositories.Base;

namespace SBEISK.SGM.Infraestructure.Data.Repositories
{
    public class ReceivementFileRepository : Repository<ReceivementPhoto>, IReceivementFileRepository
    {
        private readonly IReceivementInvoiceOrderRepository receivementInvoiceOrderRepository;
        private readonly IReceivementAttachmentRepository receivementAttachmentRepository;
        private readonly ITransferRepository transferRepository;
        private readonly ISTMRepository sTMRepository;
        public ReceivementFileRepository(SgmDataContext dataContext, IReceivementInvoiceOrderRepository receivementInvoiceOrderRepository,
                                        IReceivementAttachmentRepository receivementAttachmentRepository,
                                        ITransferRepository transferRepository, ISTMRepository sTMRepository) : base(dataContext)
        {
            this.receivementInvoiceOrderRepository = receivementInvoiceOrderRepository;
            this.receivementAttachmentRepository = receivementAttachmentRepository;
            this.transferRepository = transferRepository;
            this.sTMRepository = sTMRepository;
        }

        public IEnumerable<object> UploadFile(IList<IFormFile> files, object generic)
        {
            foreach (IFormFile file in files)
            {
                string fileName = file.FileName;
                string extension = Path.GetExtension(fileName);

                using (Stream stream = file.OpenReadStream())
                {
                    using (MemoryStream ms = new MemoryStream())
                    {
                        stream.CopyTo(ms);
                        byte[] arrayB = ms.ToArray();

                        Type type = generic.GetType();
                        if (type.Equals(typeof(ReceivementPhoto)))
                        {
                            if (extension.Equals(".jpg") || extension.Equals(".png"))
                            {
                                ReceivementPhoto photo = new ReceivementPhoto();
                                photo.Photo = arrayB;
                                photo.ReceivementInvoiceOrderId = this.receivementInvoiceOrderRepository.LastIdReceiver();
                                yield return photo;
                            }
                            else
                                yield return default(ReceivementPhoto);
                        }
                        else if (type.Equals(typeof(ReceivementAttachment)))
                        {
                            ReceivementAttachment attach = new ReceivementAttachment();
                            attach.Document = arrayB;
                            attach.ReceivementInvoiceOrderId = this.receivementInvoiceOrderRepository.LastIdReceiver();
                            yield return attach;
                        }
                        else if (type.Equals(typeof(TransferAttachment)))
                        {
                            TransferAttachment transferAttachment = new TransferAttachment();
                            transferAttachment.Document = arrayB;
                            transferAttachment.Fiscal = true;
                            transferAttachment.TransferId = this.transferRepository.LastIdTransfer();
                            yield return transferAttachment;
                        }
                        else if (type.Equals(typeof(TransferPhoto)))
                        {
                            TransferPhoto photo = new TransferPhoto();
                            photo.Photo = arrayB;
                            photo.TransferId = this.transferRepository.LastIdTransfer();

                            yield return photo;
                        }
                        else if (type.Equals(typeof(STMAttachment)))
                        {
                            STMAttachment stmAttachment = new STMAttachment();
                            stmAttachment.Document = arrayB;
                            stmAttachment.STMId = this.sTMRepository.LastIdSTM();
                            yield return stmAttachment;
                        }
                        else if (type.Equals(typeof(DirectExitReceiverAttachment)))
                        {
                            DirectExitReceiverAttachment attachment = new DirectExitReceiverAttachment();
                            attachment.Document = arrayB;

                            yield return attachment;
                        }
                        else if(type.Equals(typeof(ReceivementDevolutionAttachment)))
                        {
                            ReceivementDevolutionAttachment attachment = new ReceivementDevolutionAttachment();
                            attachment.Document = arrayB;

                            yield return attachment;
                        }
                        else if(type.Equals(typeof(ExitAttachment)))
                        {
                            ExitAttachment attachment = new ExitAttachment();
                            attachment.Document = arrayB;

                            yield return attachment;
                        }
                        else if(type.Equals(typeof(ExitPhotoBoarding)))
                        {
                            if(extension.Equals(".jpg") || extension.Equals(".png"))
                            {
                                ExitPhotoBoarding photo = new ExitPhotoBoarding();
                                photo.Photo = arrayB;

                                yield return photo;
                            }
                            else yield return default(ExitPhotoBoarding);
                        }
                    }
                }
            }
        }
        public void MergePhotos(ICollection<ReceivementPhoto> original, ICollection<ReceivementPhoto> other, Action<ReceivementPhoto, ReceivementPhoto> updateStrategy)
        {
            Merger<ReceivementPhoto> merger = new Merger<ReceivementPhoto>((x, y) => x.Id == y.Id, (x, y) => x.ReceivementInvoiceOrderId == y.ReceivementInvoiceOrderId);
            MergeResult<ReceivementPhoto> result = merger.Merge(original.ToList(), other.ToList());

            this.DataContext.ReceivementPhotos.RemoveRange(result.ItemsToDelete);
            this.DataContext.ReceivementPhotos.AddRange(result.ItemsToInsert);

            foreach (var item in result.ItemsToUpdate)
            {
                updateStrategy(item.Original, item.Modified);
            }
        }

    }
}