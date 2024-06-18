using System.IO;
using Umbraco.Cms.Core;
using Umbraco.Cms.Core.IO;

namespace Srtream.Umbraco.Core

{
    public class UniqueMediaPathScheme : IMediaPathScheme
    {
        private const int DirectoryLength = 8;
        public string GetDeleteDirectory(MediaFileManager fileSystem, string filepath) => null;

        public string GetFilePath(MediaFileManager fileManager, Guid itemGuid, Guid propertyGuid, string filename)
        {
            Guid combinedGuid = GuidUtils.Combine(itemGuid, propertyGuid);
            var directory = GuidUtils.ToBase32String(combinedGuid, DirectoryLength);

            return Path.Combine(directory, filename).Replace('\\', '/');
        }
    }
}
