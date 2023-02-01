namespace SharpMason.Logging.DiskFile
{
    public interface IFileWriter:IDisposable
    {
       void Writer(FileEntry fileEntry);
    }
}