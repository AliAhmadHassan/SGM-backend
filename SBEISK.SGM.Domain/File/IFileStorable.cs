namespace SBEISK.SGM.Domain.File
{
    public interface IFileStorable
    {
        FileData GetFileData(string extension);
    }
}
