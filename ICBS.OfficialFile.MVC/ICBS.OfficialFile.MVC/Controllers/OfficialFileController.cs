using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Web.Mvc;
using System.Web.Routing;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using ICBS.OfficialFile.BLL.OfficialFileServiceReference;
using ICBS.OfficialFile.MVC.Models;
using ICBS.OfficialFile.BLL.Helper;
using System.Linq;

namespace ICBS.OfficialFile.MVC.Controllers
{
    public class OfficialFileController : Controller
    {
        static List<FileContentModel> ListFileContent = new List<FileContentModel>();
        #region FileContent
        public ActionResult FileContentIndex()
        {

            var mdl = new OfficialFileContentModel();
            return View("FileContentIndex", mdl);
        }
        public ActionResult GetFileContentById(long id = 0)
        {
            var res = new OfficialFileContentModelSVC();
            if (id != 0)
            {
                res = OfficialFileHelper.GetAllFileContent(null).First(o => o.Id == id);
            }
            var mdl = new OfficialFileContentModel
            {
                Id = res.Id,
                FileName = res.FileName,
                FileSize = res.FileSize,
                Description = res.Description,
                SystemFileType = res.SystemFileType,
                SubjectType = res.SubjectType,
                CreationDate = res.CreationDate,
                UpdateDate = res.UpdateDate,
                UpdateUserName = res.UpdateUserName,
                CreatorUserName = res.CreatorUserName,
                //FileContent = res.FileContent
            };


            return Json(mdl, JsonRequestBehavior.AllowGet);
        }
        public ActionResult CreatFileContent(OfficialFileContentModel fileData)
        {
            var mdl = new OfficialFileContentModelSVC
            {
                Id = fileData.Id,
                CreationDate = DateTime.Now,
                UpdateDate = DateTime.Now,
                //FileContent = fileData.FileContent,
                SystemFileType = fileData.SystemFileType,
                FileName = fileData.FileName,
                SystemFileName = fileData.SystemFileName,
                SubjectType = fileData.SubjectType,
                FileSize = fileData.FileSize,
                Description = fileData.Description,
                FileSerial = fileData.FileSerial,
#if DEBUG
                CreatorUserName = "Debug",
                UpdateUserName = "Debug",
#endif

            };
            var res = OfficialFileHelper.CreatFileContent(mdl);
            return Json(new { res }, JsonRequestBehavior.AllowGet);
        }


        public ActionResult Async_Save(HttpPostedFileBase file)
        {

            if (file != null)
            {
                Session["Async_Save" + User.Identity.Name] = file;
            }

            return Content("");
        }

        public ActionResult Async_Delete(string[] fileNames)
        {


            if (fileNames != null)
            {
                foreach (var fullName in fileNames)
                {
                    var fileName = Path.GetFileName(fullName);
                    var physicalPath = Path.Combine(Server.MapPath("~/App_Data"), fileName);

                    // TODO: Verify user permissions

                    if (System.IO.File.Exists(physicalPath))
                    {
                        // The files are not actually removed in this demo
                        // System.IO.File.Delete(physicalPath);
                    }
                }
            }
            Session["Async_Save" + User.Identity.Name] = null;
            // Return an empty string to signify success
            return Content("");
        }
        
        public ActionResult Save(OfficialFileContentModel attachmentViewModel)
        {

            var isSaved = false;
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
                var errorKeys = ModelState.Where(x => x.Value.Errors.Count > 0)
                    .Select(kvp => kvp.Key

                    );

                var allKeys = ModelState.Keys.ToList();
                return Json(new { IsSaved = false, IsValid = false, errors, errorKeys, allKeys }, JsonRequestBehavior.AllowGet);
            }
            try
            {
                if (attachmentViewModel.Id == 0)
                {
                    if (Session["Async_Save" + User.Identity.Name] != null)
                    {
                        var file = ((HttpPostedFileBase)Session["Async_Save" + User.Identity.Name]);
                        using (Stream str = file.InputStream)
                        {

                            Guid Serial = Guid.NewGuid();
                            attachmentViewModel.SystemFileType = file.ContentType;
                            attachmentViewModel.FileSize = file.ContentLength;
                            attachmentViewModel.SystemFileName = file.FileName;
                            attachmentViewModel.FileSerial = Serial;

                            var LengthDivided = str.Length / 20000000;
                            var remaining = str.Length % 20000000;
                            if (remaining > 0) LengthDivided++;


                            bool res = false;
                            byte[] content = new byte[str.Length];
                            int n = str.Read(content, 0, content.Length);
                          
                            for (int i = 1; i <= LengthDivided; i++)
                            {
                                var divContent = new byte[20000000];
                                var startB = (i - 1) * 20000000;
                                bool result = false;
                                if (i == LengthDivided)
                                {
                                    result = true;
                                    divContent = new byte[remaining];

                                }

                                divContent = content.SubArray(startB, divContent.Length);
                                res = OfficialFileHelper.CreatCacheFileContent(divContent, Serial, str.Length, result);


                            }
                            if (res == true)
                            {

                                var attachmentsvc = CreatFileContent(attachmentViewModel);


                                Session["Async_Save" + User.Identity.Name] = null;

                                return Json(new
                                {
                                    IsSaved = true,
                                    IsValid = true,
                                    Message = "فایل با موفقیت ذخیره شد",
                                }, JsonRequestBehavior.AllowGet);
                            }


                            /*  byte[] content = new byte[str.Length];
                              str.Read(content, 0, content.Length);
                              attachmentViewModel.FileContent = content;
                              attachmentViewModel.SystemFileType = file.ContentType;
                              attachmentViewModel.FileSize = file.ContentLength;
                              attachmentViewModel.SystemFileName = file.FileName;
                              attachmentViewModel.FileSerial = Serial;*/



                        }


                        /*var attachmentsvc = CreatFileContent(attachmentViewModel);

                    
                        Session["Async_Save" + User.Identity.Name] = null;

                        return Json(new
                        {
                            IsSaved = true,
                            IsValid = true,
                            Message = "فایل با موفقیت ذخیره شد",
                        }, JsonRequestBehavior.AllowGet);*/
                        return Json(new
                        {
                            IsSaved = true,
                            IsValid = true,
                            Message = "فایل ذخیره نشد",
                        }, JsonRequestBehavior.AllowGet);
                    }

                    return Json(
                        new
                        {
                            IsSaved = false,
                            IsValid = false,
                            Message = "فایلی برای ذخیره کردن یافت نشد",
                        }, JsonRequestBehavior.AllowGet);

                }
                else
                {
                    return Json(
                        new
                        {
                            IsSaved = false,
                            IsValid = false,
                            Message = "فایلی برای ذخیره کردن یافت نشد"
                        }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(
                    new
                    {
                        IsSaved = false,
                        IsValid = false,
                        ex.Message,
                    }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult EditeFileContent(OfficialFileContentModel fileData)
        {
            var mdl = new OfficialFileContentModelSVC
            {
                Id = fileData.Id,
                UpdateDate = DateTime.Now,
                FileName = fileData.FileName,
                Description = fileData.Description,
                SubjectType = fileData.SubjectType,
                UpdateUserName = fileData.UpdateUserName
            };
            var res = OfficialFileHelper.EditeFileContent(mdl);

            return Json(res, JsonRequestBehavior.AllowGet);

        }

        public ActionResult DeleteFileContent(long id)
        {
            var res = OfficialFileHelper.DeleteFileContent(id);
            return Json(res, JsonRequestBehavior.AllowGet);
        }

        #endregion
        public ActionResult GetAllFileContentLearnIndex()
        {
            var mdl = new OfficialFileContentModel();
            return View("GetAllFileContentLearnIndex", mdl);
        }
        public ActionResult GetAllFileContentTechIndex()
        {
            var mdl = new OfficialFileContentModel();
            return View("GetAllFileContentTechIndex", mdl);
        }

        public ActionResult GetAllFileContent([DataSourceRequest] DataSourceRequest request, string fileType)
        {

            var res = OfficialFileHelper.GetAllFileContent(fileType).Select(o => new OfficialFileContentModel
            {
                Id = o.Id,
                FileName = o.FileName,
                SystemFileName = o.SystemFileName,
                SystemFileType = o.SystemFileType,
                CreationDate = o.CreationDate,
                UpdateDate = o.UpdateDate,
                FileSize = o.FileSize,
                Description = o.Description,
                CreatorUserName = o.CreatorUserName,
                UpdateUserName = o.UpdateUserName,
                // FileContent = o.FileContent,
                SubjectType = o.SubjectType
            });
            return Json(res.ToDataSourceResult(request));
        }

        [HttpGet]
        public FileResult GetAttachedFile(long attachmentId)
        {
            var res = OfficialFileHelper.GetAttachment(attachmentId);

            var LengthDivided = res.FileSize / 20000000;
            var remaining = res.FileSize % 20000000;
            if (remaining > 0) LengthDivided++;
            bool result = false;
            byte[] fileResult = { 0 };

            for (int i = 1; i <= LengthDivided; i++)
            {


                byte[] file = OfficialFileHelper.DownloadFileContent(attachmentId, res.FileSerial, res.FileSize, i);
                try
                {
                    int count = 0;

                if (ListFileContent.Count > 0)
                {
                    foreach (var item in ListFileContent)
                    {
                        if (item.Id == res.FileSerial)
                        {
                            count++;
                            item.ArryContent = item.ArryContent.Concat(file).ToArray();

                        }

                    }

                    if (res.FileSerial != null && count == 0)
                    {
                        ListFileContent.Add(new FileContentModel
                        {
                            Id = res.FileSerial,
                            ArryContent = file,
                            DateNow = DateTime.Now
                        });

                    }

                        if (i == LengthDivided) { 

                            foreach (var item in ListFileContent)
                            {
                               if (item.Id == res.FileSerial)
                               {
                                 if (item.ArryContent.Length == res.FileSize)
                                 {
                                        result = true;
                                        fileResult = item.ArryContent;
                                        
                                    }
                               }

                            }
                        }


                    }
                else
                {

                    if (res.FileSerial != null)
                    {
                        ListFileContent.Add(new FileContentModel
                        {
                            Id = res.FileSerial,
                            ArryContent = file,
                            DateNow = DateTime.Now
                        });

                    }

                    if (i == LengthDivided)

                    {
                        foreach (var item in ListFileContent)
                        {
                            if (item.Id == res.FileSerial)
                            {

                                if (item.ArryContent.Length == res.FileSize)
                                {
                                        result = true;
                                        fileResult = item.ArryContent;
                                        //ListFileContent.Remove(item);

                                }
                            }

                        }

                    }
                }

               
                }
           
                catch (Exception ex) { }
          
        }
            //if (!string.IsNullOrEmpty(fileName)) return File(res.FileContent, res.FileType, fileName);
            // FileResult f = new FileContentResult(res.FileContent, res.FileType);
            // return File(res.FileContent, res.SystemFileType, res.FileName);

            if(result == true)
            {
                var removeItem = ListFileContent.Find(o => o.Id == res.FileSerial);
                ListFileContent.Remove(removeItem);


            }
                return File(fileResult, res.SystemFileType, res.FileName);
        }

        public ActionResult FileType()
        {
            var req = new List<SelectListItem>
            {
                new SelectListItem {Text="آموزش", Value="Learn"},
                new SelectListItem {Text="فنی", Value="Tech"}
            };
            return Json(req, JsonRequestBehavior.AllowGet);
        }
    }
}