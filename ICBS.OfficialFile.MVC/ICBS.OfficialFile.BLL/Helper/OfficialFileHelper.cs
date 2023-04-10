using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ICBS.OfficialFile.BLL.OfficialFileServiceReference;
namespace ICBS.OfficialFile.BLL.Helper
{
    public class OfficialFileHelper
    {
        public static List<OfficialFileContentModelSVC> GetAllFileContent(string fileType)
        {
            using (var clProxy = new OfficialFileServiceClient())
            {
                return clProxy.GetAllFileContent(fileType);

            }
        }
        public static bool CreatCacheFileContent(byte[] content, Guid guid, long? length, bool result)
        {
            using (var clProxy = new OfficialFileServiceClient())
            {
                return clProxy.CreatCacheFileContent(content, guid, length, result);

            }
        }
        public static OfficialFileContentModelSVC GetAttachment(long id)
        {
            using (var clProxy = new OfficialFileServiceClient())
            {
                return clProxy.GetAttachment(id);
            }
        }
        public static bool CreatFileContent(OfficialFileContentModelSVC fileData)
        {
            using (var clProxy = new OfficialFileServiceClient())
            {
                return clProxy.CreatFileContent(fileData);
            }
        }

        public static bool DeleteFileContent(long id)
        {
            using (var clProxy = new OfficialFileServiceClient())
            {
                return clProxy.DeleteFileContent(id);
            }
        }

        public static bool EditeFileContent(OfficialFileContentModelSVC fileData)
        {
            using (var clProxy = new OfficialFileServiceClient())
            {
                return clProxy.EditeFileContent(fileData);
            }
        }


        public static bool CreatFileAccess(AccessFileContentModelSVC fileData)
        {
            using (var clProxy = new OfficialFileServiceClient())
            {
                return clProxy.CreatFileAccess(fileData);
            }
        }

        public static bool EditeFileAccess(AccessFileContentModelSVC fileData)
        {
            using (var clProxy = new OfficialFileServiceClient())
            {
                return clProxy.EditeFileAccess(fileData);
            }
        }

        public static bool DeleteFileAccess(long id)
        {
            using (var clProxy = new OfficialFileServiceClient())
            {
                return clProxy.DeleteFileAccess(id);
            }
        }

        public static List<AccessFileContentModelSVC> GetAllFileAccess()
        {
            using (var clProxy = new OfficialFileServiceClient())
            {
                return clProxy.GetAllFileAccess();

            }
        }
        public static byte[] DownloadFileContent(long id, Guid serial, long? length, int i)
        {
            using (var clProxy = new OfficialFileServiceClient())
            {
                return clProxy.DownloadFileContent(id, serial, length, i);

            }
        }
    }
}
