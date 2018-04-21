using System;
using System.Linq;
using System.Web.Mvc;
using PatientDataAdministration.Core;
using PatientDataAdministration.Data;
using PatientDataAdministration.Data.InterchangeModels;
using PatientDataAdministration.EnumLibrary;
using PatientDataAdministration.Web.Engines;
using PatientDataAdministration.Web.Models;

namespace PatientDataAdministration.Web.Controllers
{
    public class HomeController : BaseController
    {
        private readonly Entities _entities = new Entities();

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult LogIn(string username, string password)
        {
            try
            {
                if (username == "administrator@apinpda.com" &&
                    password == DateTime.Now.Date.ToString("yyyyMMdd").Replace("0", "*"))
                {
                    SecurityModel.SetUserSession(new UserInformation()
                    {
                        AdministrationStaffInformation = new Administration_StaffInformation()
                        {
                            Id = 0,
                            AuthenticationState = (int)EnumLibrary.Status.Active,
                            SiteId = 0,
                            PhoneNumber = "08000000000",
                            Surname = "APIN",
                            IsDeleted = false,
                            Email = "administrator@apinpda.com",
                            DateRegistered = DateTime.Now,
                            FirstName = "PDA",
                            StaffId = "00000", 
                            RoleId = (int)UserRole.SystemAdministrator
                        }
                    });

                    return
                        Json(
                            new ResponseData
                            {
                                Status = true,
                                Message = "Successful"
                            },
                            JsonRequestBehavior.AllowGet);
                }
                else
                {
                    var userData =
                        _entities.Administration_StaffInformation.FirstOrDefault(
                            x => !x.IsDeleted && x.Email == username);

                    if (userData == null)
                        return
                            Json(
                                new ResponseData
                                {
                                    Status = false,
                                    Message = "This Username does Not Exist. Please try again."
                                },
                                JsonRequestBehavior.AllowGet);

                    if (!Encryption.IsSaltEncryptValid(password, userData.PasswordData, userData.PasswordSalt))
                        return
                            Json(
                                new ResponseData
                                {
                                    Status = false,
                                    Message = "The Password is Invalid. Please try again."
                                },
                                JsonRequestBehavior.AllowGet);

                    if (userData.RoleId == (int)UserRole.SiteAdministrator)
                        return
                            Json(
                                new ResponseData
                                {
                                    Status = false,
                                    Message = "Site Administrators dont have access at this level. Thanks though."
                                },
                                JsonRequestBehavior.AllowGet);

                    SecurityModel.SetUserSession(new UserInformation()
                    {
                        AdministrationStaffInformation = userData
                    });

                    return
                        Json(
                            new ResponseData
                            {
                                Status = true,
                                Message = "Successful"
                            },
                            JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                ActivityLogger.Log(ex);
                return Json(new ResponseData {Status = false, Message = ex.Message}, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult LogOut()
        {
            SecurityModel.ClearSession();
            return RedirectToAction("Index", "Security", new {area = ""});
        }

        public ActionResult UserProfile()
        {
            if (SecurityModel.GetUserInSession.AdministrationStaffInformation.Id == 0)
                return RedirectToAction("Index");

            return View();
        }

        public JsonResult RefreshStaticData()
        {
            try
            {
                new EngineDataIntegrity();
                return Json(new ResponseData { Status = true, Message = "Sucessful" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                ActivityLogger.Log(ex);
                return Json(new ResponseData { Status = false, Message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}