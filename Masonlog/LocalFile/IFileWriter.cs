namespace Masonlog.LocalFile
{
    public interface IFileWriter : IDisposable
    {
        void Writer(FileEntry fileEntry);
    }
}