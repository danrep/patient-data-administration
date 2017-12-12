using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PatientDataAdministration.Core;
using PatientDataAdministration.Data;
using PatientDataAdministration.Data.InterchangeModels;
using PatientDataAdministration.EnumLibrary;

namespace PatientDataAdministration.Web.Areas.ServerCommunication.Controllers
{
    public class UserManagementController : BaseServerCommController
    {
        private readonly Entities _db = new Entities();

        public JsonResult SaveUser(Administration_StaffInformation administrationStaffInformation)
        {
            try
            {
                if (administrationStaffInformation.Id == 0)
                {
                    if (
                        _db.Administration_StaffInformation.FirstOrDefault(
                            x =>
                                !x.IsDeleted &&
                                (x.Email == administrationStaffInformation.Email ||
                                 x.PhoneNumber == administrationStaffInformation.PhoneNumber)) != null)
                        return
                            Json(
                                new ResponseData
                                {
                                    Status = false,
                                    Message = "There is a User having the same Email/Phone Number. Please check"
                                },
                                JsonRequestBehavior.AllowGet);

                    var password = administrationStaffInformation.PhoneNumber;
                    administrationStaffInformation.PasswordSalt = Encryption.GetUniqueKey(10);
                    administrationStaffInformation.PasswordData = Encryption.SaltEncrypt(password,
                        administrationStaffInformation.PasswordSalt);
                    administrationStaffInformation.DateRegistered = DateTime.Now;
                    administrationStaffInformation.AuthenticationState = (int) Status.Active;

                    _db.Administration_StaffInformation.Add(administrationStaffInformation);
                }
                else
                {
                    var existing = _db.Administration_StaffInformation.FirstOrDefault(
                        x =>
                            !x.IsDeleted && x.Id == administrationStaffInformation.Id);

                    if (existing == null)
                        return
                            Json(
                                new ResponseData
                                {
                                    Status = false,
                                    Message = "This user does not exist."
                                },
                                JsonRequestBehavior.AllowGet);

                    existing.FirstName = administrationStaffInformation.FirstName;
                    existing.Surname = administrationStaffInformation.Surname;
                    existing.SiteId = administrationStaffInformation.SiteId;

                    _db.Entry(existing).State = EntityState.Modified;
                }

                _db.SaveChanges();
                return Json(new ResponseData { Status = true, Message = "Successful" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                ActivityLogger.Log(ex);
                return Json(new ResponseData { Status = false, Message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult GetUser()
        {
            try
            {
                var users = _db.Administration_StaffInformation.Where(x => !x.IsDeleted).ToList();

                if (!users.Any())
                    return Json(new ResponseData {Status = true, Message = "No User Found", Data = users},
                        JsonRequestBehavior.AllowGet);
                {
                    var siteInfo = _db.Administration_SiteInformation.Where(x => !x.IsDeleted).ToList();
                    var userData =
                        users.Select(
                                u =>
                                    new
                                    {
                                        UserInformation = u,
                                        SiteInformation =
                                        siteInfo.FirstOrDefault(x => x.Id == u.SiteId) ??
                                        new Administration_SiteInformation()
                                    })
                            .OrderBy(x => x.UserInformation.Surname)
                            .ThenBy(x => x.UserInformation.FirstName)
                            .ToList();

                    return Json(new ResponseData {Status = true, Message = "Successful", Data = userData},
                        JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                ActivityLogger.Log(ex);
                return Json(new ResponseData {Status = false, Message = ex.Message}, JsonRequestBehavior.AllowGet);
            }
        }
    }
}