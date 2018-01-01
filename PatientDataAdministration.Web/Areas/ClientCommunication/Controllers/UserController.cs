using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PatientDataAdministration.Core;
using PatientDataAdministration.Data;
using PatientDataAdministration.Data.InterchangeModels;

namespace PatientDataAdministration.Web.Areas.ClientCommunication.Controllers
{
    public class UserController : BaseClientCommController
    {
        private readonly Entities _entities = new Entities();

        public JsonResult Authenticate(string authData)
        {
            try
            {
                if (string.IsNullOrEmpty(authData))
                    return Json(new ResponseData { Status = false, Message = "No Query Received" }, JsonRequestBehavior.AllowGet);
                
                var authDataParts = authData.Split('*');
                var username = authDataParts[0];
                var password = authDataParts[1];

                var userInformation =
                    _entities.Administration_StaffInformation.FirstOrDefault(
                        x => !x.IsDeleted && x.Email == username);

                if (userInformation == null)
                    return Json(new ResponseData { Status = false, Message = "User Not Found"}, JsonRequestBehavior.AllowGet);
                else
                {
                    if (Encryption.IsSaltEncryptValid(password, userInformation.PasswordData, userInformation.PasswordSalt))
                    {
                        _entities.Administration_ClientLog.Add(new Administration_ClientLog()
                        {
                            ChipData = string.Empty, 
                            ClientId = 0,
                            CurrentUserId = userInformation.Id,
                            DateLog = DateTime.Now,
                            IsDeleted = false,
                            LocationLat = string.Empty, 
                            LocationLong = string.Empty, 
                            ClientIpAddress = Request.UserHostAddress ?? ""
                        });
                        _entities.SaveChanges();

                        return Json(new
                            {
                                Status = true,
                                Message = "Successful",
                                Data = userInformation
                        },
                            JsonRequestBehavior.AllowGet);
                    }
                    else
                        return Json(new ResponseData {Status = false, Message = "Incorrect Password"},
                            JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                ActivityLogger.Log(ex);
                return Json(new ResponseData { Status = false, Message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}