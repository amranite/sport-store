namespace SportStore.Application.Services;

public interface IFileUploadService
{
    void UploadFile(Stream input, string filepath);
}
