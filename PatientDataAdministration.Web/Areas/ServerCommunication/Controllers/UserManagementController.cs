using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using PatientDataAdministration.Core;
using PatientDataAdministration.Data;
using PatientDataAdministration.Data.InterchangeModels;
using PatientDataAdministration.EnumLibrary;
using PatientDataAdministration.EnumLibrary.Dictionary;

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

                    var password = Encryption.GetUniqueKey(6);
                    administrationStaffInformation.PasswordSalt = Encryption.GetUniqueKey(10);
                    administrationStaffInformation.PasswordData = Encryption.SaltEncrypt(password,
                        administrationStaffInformation.PasswordSalt);
                    administrationStaffInformation.DateRegistered = DateTime.Now;
                    administrationStaffInformation.AuthenticationState = (int) Status.Active;

                    _db.Administration_StaffInformation.Add(administrationStaffInformation);
                    var msg = $"Dear {administrationStaffInformation.Surname} {administrationStaffInformation.FirstName},<br />";
                    msg += $"You have been profiled as a Site Administrator. Your Credentials are Below:<br />";
                    msg += $"<strong>Username</strong>: {administrationStaffInformation.Email}<br />";
                    msg += $"<strong>Password</strong>: {password}<br />";

                    Core.Messaging.SendMail(administrationStaffInformation.Email, null, null,
                        "Authentication Credential", msg, null);
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
                    existing.RoleId = administrationStaffInformation.RoleId;

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
                                        Role = ((UserRole)u.RoleId).DisplayName(),
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

        public JsonResult GetUserSingle(int userId)
        {
            try
            {
                var user = _db.Administration_StaffInformation.FirstOrDefault(x => !x.IsDeleted && x.Id == userId);

                if (user == null)
                    return Json(new ResponseData { Status = true, Message = "No User Found" },
                        JsonRequestBehavior.AllowGet);
                {
                    var siteInfo = _db.Administration_SiteInformation.FirstOrDefault(x => x.Id == user.SiteId);
                    var userData = new
                    {
                        UserInformation = user,
                        SiteInformation = siteInfo ??
                                          new Administration_SiteInformation(),
                        Role = ((UserRole) user.RoleId).DisplayName()
                    };

                    return Json(new ResponseData { Status = true, Message = "Successful", Data = userData },
                        JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                ActivityLogger.Log(ex);
                return Json(new ResponseData { Status = false, Message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult SendUserPassword(int userId)
        {
            try
            {
                var administrationStaffInformation =
                    _db.Administration_StaffInformation.FirstOrDefault(x => x.Id == userId);

                if (administrationStaffInformation == null)
                    return Json(new ResponseData { Status = false, Message = "User not Found" },
                        JsonRequestBehavior.AllowGet);

                var msg = $"Dear {administrationStaffInformation.Surname} {administrationStaffInformation.FirstName},<br />";
                msg += $"You Requested that you be reminded of your Credentials. Find below:<br />";
                msg += $"<strong>Username</strong>: {administrationStaffInformation.Email}<br />";
                msg += $"<strong>Password</strong>: {Encryption.SaltDecrypt(administrationStaffInformation.PasswordData, administrationStaffInformation.PasswordSalt)}<br />";
                
                Messaging.SendMail(administrationStaffInformation.Email, null, null,
                    "Password Request", msg, null);

                return Json(new ResponseData { Status = true, Message = "Password Retrieval Successful"},
                    JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                ActivityLogger.Log(ex);
                return Json(new ResponseData { Status = false, Message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult DeleteUser(int userId)
        {
            try
            {
                var user = _db.Administration_StaffInformation.FirstOrDefault(x => !x.IsDeleted);

                if (user == null)
                    return Json(new ResponseData { Status = true, Message = "No User Found" },
                        JsonRequestBehavior.AllowGet);
                {
                    user.IsDeleted = true;
                    _db.Entry(user).State = EntityState.Modified;
                    _db.SaveChanges();

                    return Json(new ResponseData { Status = true, Message = "Successful" },
                        JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                ActivityLogger.Log(ex);
                return Json(new ResponseData { Status = false, Message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult AuthenticateUser(string username, string password)
        {
            try
            {
                var staffInformation =
                    _db.Administration_StaffInformation.FirstOrDefault(x => !x.IsDeleted && x.Email == username);

                if (staffInformation == null)
                    return
                        Json(
                            new ResponseData
                            {
                                Status = false,
                                Message = "This User does not Exist"
                            },
                            JsonRequestBehavior.AllowGet);
                else
                {
                    if (Encryption.IsSaltEncryptValid(password, staffInformation.PasswordData,
                        staffInformation.PasswordSalt))
                        return
                            Json(
                                new ResponseData
                                {
                                    Status = true,
                                    Message = "Successful Log On"
                                },
                                JsonRequestBehavior.AllowGet);
                    else
                        return
                            Json(
                                new ResponseData
                                {
                                    Status = false,
                                    Message = "Invalid Password"
                                },
                                JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                ActivityLogger.Log(ex);
                return Json(new ResponseData { Status = false, Message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult PasswordChange(string oldPassword, string newPassword, string email)
        {
            try
            {
                var staffInformation =
                    _db.Administration_StaffInformation.FirstOrDefault(x => !x.IsDeleted && x.Email == email);

                if (staffInformation == null)
                    return
                        Json(
                            new ResponseData
                            {
                                Status = false,
                                Message = "This User does not Exist"
                            },
                            JsonRequestBehavior.AllowGet);


                if (!Encryption.IsSaltEncryptValid(oldPassword, staffInformation.PasswordData,
                    staffInformation.PasswordSalt))
                    return
                        Json(
                            new ResponseData
                            {
                                Status = false,
                                Message = "Invalid Existing Password"
                            },
                            JsonRequestBehavior.AllowGet);

                staffInformation.PasswordData = Encryption.SaltEncrypt(newPassword, staffInformation.PasswordSalt);
                _db.Entry(staffInformation).State = EntityState.Modified;
                _db.SaveChanges();

                return
                    Json(
                        new ResponseData
                        {
                            Status = true,
                            Message = "Successful Modification"
                        },
                        JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                ActivityLogger.Log(ex);
                return Json(new ResponseData { Status = false, Message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}