using FluentFTP;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace HealthTek_Web_V3.Helpers
{
    public class UploadFileHelper
    {
        private readonly IWebHostEnvironment _hostEnv;

        private readonly string _uploadFileBasePath;
        private static string Host = "198.71.61.248";
        private static string UserId = "healthtek520183810";
        private static string Password = "Jji2232m!!Sadaq@22!";
        public UploadFileHelper(IWebHostEnvironment hostEnv)
        {
            this._hostEnv = hostEnv;
            this._uploadFileBasePath = Path.Combine("FilePool");
        }
        public async Task<string> UploadFileAsync(IFormFile formFile, string uploadFolder, bool renameFile = true, string? newFileName = null)
        {
            if (formFile == null)
            {
                return string.Empty;
            }

            ////Create Directory FilePool 
            if (!Directory.Exists(Path.Combine(this._hostEnv.WebRootPath, this._uploadFileBasePath, uploadFolder.ToLower())))
            {
                Directory.CreateDirectory(Path.Combine(this._hostEnv.WebRootPath, this._uploadFileBasePath, uploadFolder.ToLower()));
            }

            var fileName = formFile.FileName;
            if (renameFile)
            {
                var fileExtention = Path.GetExtension(formFile.FileName);
                fileName = newFileName + fileExtention;
            }

            var partialPath = Path.Combine(this._uploadFileBasePath, uploadFolder.ToLower(), fileName);
            var path = Path.Combine(this._hostEnv.WebRootPath, partialPath);
            using (var stream = new FileStream(path, FileMode.Create))
            {
                await formFile.CopyToAsync(stream);
                await stream.FlushAsync();
                await stream.DisposeAsync();
            }

            // Create and connect the ftp client
            FtpClient client = new FtpClient(Host, UserId, Password);
            client.EncryptionMode = FtpEncryptionMode.Auto;
            client.ValidateAnyCertificate = true;
            await client.ConnectAsync();
            if (client.IsConnected)
            {
                if (File.Exists(path))
                {
                    var fileInfo = new FileInfo(formFile.FileName);
                    await client.UploadFileAsync(path, $"/VM769F7BF/{fileInfo.Name}", existsMode: FtpRemoteExists.Overwrite, createRemoteDir: true);
                    partialPath = fileInfo.Name;
                }
            }
            // Delete file from FilePool
            File.Delete(path);
            // return the address
            return partialPath;
        }
        public async Task<string> ReadFile(string path)
        {
            string contents = path;
            using (var conn = new FtpClient(Host, UserId, Password))
            {
                path = $"/VM769F7BF/{path}";
                conn.EncryptionMode = FtpEncryptionMode.Auto;
                conn.ValidateAnyCertificate = true;
                await conn.ConnectAsync();
                if (!conn.Download(out byte[] bytes, path))
                {
                    throw new Exception("Cannot read file");
                }
                var token = new CancellationToken();
                var partialpath = Path.Combine(this._hostEnv.WebRootPath, _uploadFileBasePath, contents);
                try
                {
                    await File.WriteAllBytesAsync(partialpath, bytes, cancellationToken: token);
                }
                catch (Exception ex)
                {

                }
                contents = partialpath;
            }
            return contents;
        }
        public async Task DeleteFile(string path)
        {
            using (var conn = new FtpClient(Host, UserId, Password))
            {
                path = $"/VM769F7BF/{path}";
                conn.EncryptionMode = FtpEncryptionMode.Auto;
                conn.ValidateAnyCertificate = true;
                await conn.ConnectAsync();
                await conn.DeleteFileAsync(path);
            }
        }
        public async Task DownloadFileAsync(string path)
        {
            var localpath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            var token = new CancellationToken();
            localpath = $"{localpath}\\Downloads\\{path}";
            path = $"/VM769F7BF/{path}";
            using (var ftp = new FtpClient(Host, UserId, Password))
            {
                ftp.EncryptionMode = FtpEncryptionMode.Auto;
                ftp.ValidateAnyCertificate = true;
                await ftp.ConnectAsync();
                // download a file and ensure the local directory is created
                await ftp.DownloadFileAsync(localpath, path, FtpLocalExists.Overwrite, FtpVerify.Retry, token: token);
            }
        }

    }
}