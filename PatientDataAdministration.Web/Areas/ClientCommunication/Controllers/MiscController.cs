using PatientDataAdministration.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using PatientDataAdministration.Core;
using PatientDataAdministration.Data.InterchangeModels;
using PatientDataAdministration.Web.Engines;

namespace PatientDataAdministration.Web.Areas.ClientCommunication.Controllers
{
    public class MiscController : BaseClientCommController
    {
        private readonly Entities _entities = new Entities();

        public JsonResult GetUpdateData()
        {
            try
            {
                return
                    Json(
                        new ResponseData
                        {
                            Status = true,
                            Message = "Successful",
                            Data = _entities.System_Update.FirstOrDefault(x => !x.IsDeleted && x.IsNew)
                        },
                        JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                ActivityLogger.Log(ex);
                return Json(new ResponseData {Status = false, Message = ex.Message}, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult Ping(string clientId = "", string appVersion = "2.0.1", long currentUserId = 0)
        {
            try
            {
                var currentUsers = LocalCache.Get<List<System_ClientPulse>>("System_ClientPulse");

                if (string.IsNullOrEmpty(clientId))
                    clientId = Request.UserHostAddress;

                appVersion = appVersion.Replace('-', '.');

                var currentUser = currentUsers.FirstOrDefault(x => x.ClientId == clientId);
                if (currentUser != null)
                {
                    if (DateTime.Now.Subtract(currentUser.CheckInPeriod).TotalSeconds > 1800)
                    {
                        currentUsers.Remove(currentUser);
                        ActivityLogger.Log("CLIENT_PULSE", $"{clientId}:{Request.UserHostAddress}:{appVersion}");
                    }
                }
                else
                    ActivityLogger.Log("CLIENT_PULSE", $"{clientId}:{Request.UserHostAddress}:{appVersion}");

                currentUser = new System_ClientPulse()
                {
                    AppVersion = appVersion,
                    CheckInPeriod = DateTime.Now,
                    ClientId = clientId,
                    IsDeleted = false,
                    UserId = currentUserId
                };

                currentUsers.Add(currentUser);
                LocalCache.Set("System_ClientPulse", currentUsers);

                return
                    Json(
                        new ResponseData
                        {
                            Status = true,
                            Message = "Successful",
                        },
                        JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                ActivityLogger.Log(ex);
                return Json(new ResponseData { Status = false, Message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        //Register Devices

        //Confirm Devices
    }
}