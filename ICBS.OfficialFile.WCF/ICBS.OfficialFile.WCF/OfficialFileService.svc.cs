using System;
using System.IO;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Web.Caching;
using ICBS.OfficialFile.WCFDAL.Models;
using ICBS.OfficialFile.WCFDAL.OfficialFileContentEntity;
using ICBS.OfficialFile.WCF.Helper;
namespace ICBS.OfficialFile.WCF
{

   
    public class OfficialFileService : IOfficialFileService
    {
       static List<FileContentModel> ListFileContent = new List<FileContentModel>();


        public bool CreatCacheFileContent(byte[] content, Guid guid, long? length, bool result)
        {
            try
            {
                int count = 0;

                if (ListFileContent.Count > 0)
                {
                    foreach (var item in ListFileContent)
                    {
                        if (item.Id == guid)
                        {

                            count++;
                            item.ArryContent = item.ArryContent.Concat(content).ToArray();

                        }
                       
                    }

                    if (guid != null && count == 0)
                    {
                        ListFileContent.Add(new FileContentModel
                        {
                            Id = guid,
                            ArryContent = content,
                            DateNow = DateTime.Now
                        });

                    }

                    if (result == true)
                        {
                        foreach (var item in ListFileContent)
                        {
                            if (item.Id == guid)
                            {
                                if (item.ArryContent.Length == length)
                                {
                                    return true;
                                }
                            }

                        }
                    }

                }
                else
                {

                    if (guid != null)
                    {
                        ListFileContent.Add(new FileContentModel
                        {
                            Id = guid,
                            ArryContent = content,
                            DateNow = DateTime.Now
                        });

                    }

                    if (result == true)
                    {
                       
                        var item = ListFileContent.Find(o => o.Id == guid && o.ArryContent.Length == length);
                        if (item != null)
                        {
                            return true;
                        }
                                
                              
                        

                    }
                }

                return false;
            }
            catch (Exception ex)
            {

                return false;
            }
        }
        public bool CreatFileContent(OfficialFileContentModelSVC fileData)
        {
            
            try
            {
                var newFile = new OfficialFileContent
                {
                            FileName = fileData.FileName,
                            SystemFileName = fileData.SystemFileName,
                            SystemFileType = fileData.SystemFileType,
                            CreationDate = fileData.CreationDate,
                            UpdateDate = fileData.UpdateDate,
                            FileSize = fileData.FileSize,
                            SubjectType = fileData.SubjectType,
                            Description = fileData.Description,
                            CreatorUserName = fileData.CreatorUserName,
                            UpdateUserName = fileData.UpdateUserName,
                            FileSerial = fileData.FileSerial,
                            //FileContent = fileData.FileContent,
                };
                    
                using (var db = new OfficialFileEntity())
                {
                    db.OfficialFileContents.Add(newFile);

                    db.SaveChanges();
                }

                foreach (var item in ListFileContent)
                {
                    if (item.Id == fileData.FileSerial)
                    {
                        
                        AddFileContent(item.Id, item.ArryContent);
                    }

                }
                return true;
            }
            catch (Exception ex)
            {

                return false;
            }
        }


        public bool DeleteFileContent(long id)
        {
            string connectionString =
            "data source=srv-developer;initial catalog=Cr24.OfficialFile;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework";
            string queryString =
                "DELETE FROM [FileContentTbl] WHERE IdOfficialFileContent =" + id;
            using (SqlConnection connection =
                 new SqlConnection(connectionString))
            {
                SqlCommand command = connection.CreateCommand();
                command.CommandText = queryString;

                try
                {
                    connection.Open();

                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        Console.WriteLine("\t{0}\t{1}",
                            reader[0], reader[1]);
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            try
            {
                using (var db = new OfficialFileEntity())
                {
                    var contObj = db.OfficialFileContents.First(o => o.Id == id);
                   // var file = contObj.FileContentTbls.First(o => o.IdOfficialFileContent == id);
                    db.OfficialFileContents.Remove(contObj);
                   // db.FileContentTbls.Remove(file);
                    db.SaveChanges();
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        public bool EditeFileContent(OfficialFileContentModelSVC fileData)
               { 
            try
            {
                using (var db = new OfficialFileEntity())
                {
                    var contObj = db.OfficialFileContents.First(o => o.Id == fileData.Id);
                    contObj.FileName = fileData.FileName;
                    contObj.UpdateDate = DateTime.Now;
                    contObj.SubjectType = fileData.SubjectType;
                    contObj.Description = fileData.Description;
                    contObj.UpdateUserName = fileData.UpdateUserName;

                    db.SaveChanges();
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public List<OfficialFileContentModelSVC> GetAllFileContent(string fileType)
        {

            using (var db = new OfficialFileEntity())
            {


                return db.OfficialFileContents.Where(o => string.IsNullOrEmpty(fileType) || o.SubjectType.Contains(fileType)).Select(o => new OfficialFileContentModelSVC
                {
                    Id = o.Id,
                    FileName = o.FileName,
                    SystemFileName = o.SystemFileName,
                    SystemFileType = o.SystemFileType,
                    CreationDate = o.CreationDate,
                    UpdateDate = o.UpdateDate,
                    FileSize = o.FileSize,
                    SubjectType = o.SubjectType,
                    Description = o.Description,
                    CreatorUserName = o.CreatorUserName,
                    UpdateUserName = o.UpdateUserName
                }).ToList();

            }
        }



        public bool AddFileContent(Guid id,byte[] fileData)
        {
            try
            {

                using (var db = new OfficialFileEntity())
                {
                    var contObj = db.OfficialFileContents.First(o => o.FileSerial == id);

                        contObj.FileContentTbls.Add(new FileContentTbl
                        {
                            FileContent = fileData,
                        });
                    
                    db.SaveChanges();

                }
                return true;

            }
            catch (Exception ex)
            {
                return false;
            }
        }

        
        public bool CreatFileAccess(AccessFileContentModelSVC fileData)
        {
            try
            {
                var newFile = new AccessFileContent
                {
                    Status = fileData.Status,
                    Link = fileData.Link,
                    UserList = fileData.UserList,


                };

                using (var db = new OfficialFileEntity())
                {
                    db.AccessFileContents.Add(newFile);
                    db.SaveChanges();
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool DeleteFileAccess(long id)
        {
            try
            {
                using (var db = new OfficialFileEntity())
                {
                    var contObj = db.AccessFileContents.First(o => o.Id == id);
                    db.AccessFileContents.Remove(contObj);
                    db.SaveChanges();
                }
                return true;
            }
            catch
            {
                return false;
            }

        }


        public bool EditeFileAccess(AccessFileContentModelSVC fileData)
        {
            try
            {
                using (var db = new OfficialFileEntity())
                {
                    var contObj = db.AccessFileContents.First(o => o.Id == fileData.Id);
                    contObj.Status = fileData.Status;
                    contObj.Link = fileData.Link;
                    contObj.UserList = fileData.UserList;


                    db.SaveChanges();
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        public List<AccessFileContentModelSVC> GetAllFileAccess()
        {
            using (var db = new OfficialFileEntity())
            {
                return db.AccessFileContents.Select(o => new AccessFileContentModelSVC
                {
                    Id = o.Id,
                    Status = o.Status,
                    Link = o.Link,
                    UserList = o.UserList

                }).ToList();
            }
        }

    

        public byte[] DownloadFileContent(long id, Guid serial, long? length,int i)
        {

            using (var db = new OfficialFileEntity())
            {
                var att = db.OfficialFileContents.First(o => o.Id == id);
                var file = db.FileContentTbls.First(o => o.IdOfficialFileContent == id);

                var LengthDivided = att.FileSize / 20000000;
                var remaining = file.FileContent.Length % 20000000;
                if (remaining > 0) LengthDivided++;

                var divContent = new byte[20000000];

                var startB = (i - 1) * 20000000;

                if(i == LengthDivided)
                {
                    divContent = new byte[remaining];
                   divContent = file.FileContent.SubArray(startB, divContent.Length);
                }
                else
                {
                    divContent = file.FileContent.SubArray(startB, divContent.Length);
                }

                return divContent;
            }
                
        }
        
        public OfficialFileContentModelSVC GetAttachment(long id)
        {
            try
            {
                using (var db = new OfficialFileEntity())
                {
                    var att = db.OfficialFileContents.First(o => o.Id == id);
                    if (att == null)
                    {
                        var err = "فایلی وجود ندارد";
                        throw new Exception(err);
                    }
                   
                        var resA = new OfficialFileContentModelSVC
                        {
                            Id = att.Id,
                            FileName = att.FileName,
                            SystemFileType = att.SystemFileType,
                            FileSize = att.FileSize,
                            Description = att.Description,
                            SubjectType = att.SubjectType,
                            FileSerial = att.FileSerial
                            //FileContent = content,
                        
                        };
                        return resA;
                   
                }
            }

            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }
    }

}

