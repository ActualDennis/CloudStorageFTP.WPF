using CloudStorage.Server.Di;
using CloudStorage.Server.Exceptions;
using CloudStorage.Server.Helpers;
using System.IO;

namespace CloudStorage.Server.FileSystem
{
    public class CloudStorageUnixFileSystemProvider : FtpUnixFileSystemProvider, ICloudStorageFileSystemProvider
    {
        public CloudStorageUnixFileSystemProvider()
        {
            
        }

        public override FileStream CreateNewFile(string path)
        {
            var storageInfo = DiContainer.Provider.Resolve<DatabaseHelper>().GetStorageInformation(UserName);

            if(storageInfo.BytesOccupied >= storageInfo.BytesTotal)
                throw new UserOutOfSpaceException("Can't copy the files because your cloud storage limit exceeded.");

            return base.CreateNewFile(path);
        }

        public override FileStream CreateNewFileorOverwrite(string path)
        {
            var storageInfo = DiContainer.Provider.Resolve<DatabaseHelper>().GetStorageInformation(UserName);

            if (storageInfo.BytesOccupied >= storageInfo.BytesTotal)
                throw new UserOutOfSpaceException("Can't copy the files because your cloud storage limit exceeded.");

            return base.CreateNewFileorOverwrite(path);
        }

    }
}
