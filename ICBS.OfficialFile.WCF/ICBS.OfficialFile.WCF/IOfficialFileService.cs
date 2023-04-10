using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using ICBS.OfficialFile.WCFDAL.Models;
namespace ICBS.OfficialFile.WCF
{
    [ServiceContract]
    public interface IOfficialFileService
    {
        [OperationContract]
         bool CreatCacheFileContent(byte[] content, Guid guid,long? length, bool result);

        [OperationContract]
        bool CreatFileContent(OfficialFileContentModelSVC fileData);

        [OperationContract]
        bool EditeFileContent(OfficialFileContentModelSVC fileData);

        [OperationContract]
        bool DeleteFileContent(long id);

        [OperationContract]
        List<OfficialFileContentModelSVC> GetAllFileContent(string fileType);

        [OperationContract]
        OfficialFileContentModelSVC GetAttachment(long id);

        [OperationContract]
        bool AddFileContent(Guid id, byte[] fileData);

        [OperationContract]
        bool CreatFileAccess(AccessFileContentModelSVC fileData);

        [OperationContract]
        bool EditeFileAccess(AccessFileContentModelSVC fileData);

        [OperationContract]
        bool DeleteFileAccess(long id);

        [OperationContract]
        List<AccessFileContentModelSVC> GetAllFileAccess();

        [OperationContract]
        byte[] DownloadFileContent(long id, Guid serial, long? length, int i);

    }

}


