using SportStore.Application.Services;

namespace SportStore.Infrastructure.Services;

public class FileUploadService : IFileUploadService
{
    public void UploadFile(Stream input, string filepath)
    {
        using var output = new FileStream(filepath, FileMode.Create);
        input.CopyTo(output);
    }
}
