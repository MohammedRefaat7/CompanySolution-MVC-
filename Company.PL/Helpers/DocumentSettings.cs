namespace Company.PL.Helpers
{
    public static class DocumentSettings
    {
        public static string UploadFile(IFormFile file , string FolderName)
        {
            // CurrentDirectory\\wwwroot\\Files\\FolderName    (FilePath)
            string FolderPath = Path.Combine(Directory.GetCurrentDirectory(),"wwwroot\\Files", FolderName);

            // Unique FileName
            string FileName = $"{Guid.NewGuid()}{file.FileName}";

            // FilePath(FolderPath , UFileName)
            string FilePath = Path.Combine(FolderPath, FileName);

            // Save file As STREAM 
            using var Fs = new FileStream(FilePath, FileMode.Create);
            file.CopyTo(Fs);

            return FileName;
        }
    }
}
