using System;
using System.Linq;
using System.Web.Hosting;
using System.Web.Mvc;
using PatientDataAdministration.Core;
using PatientDataAdministration.Data;
using PatientDataAdministration.Data.InterchangeModels;
using PatientDataAdministration.EnumLibrary;
using PatientDataAdministration.Web.Engines;
using PatientDataAdministration.Web.Models;

namespace PatientDataAdministration.Web.Areas.ServerCommunication.Controllers
{
    public class DataIntegrityController : BaseServerCommController
    {
        // GET: ServerCommunication/DataIntegrity
        private readonly Entities _entities = new Entities();

        public JsonResult CreateNew(int recordRow, string pepId)
        {
            try
            {
                var beforeData = Newtonsoft.Json.JsonConvert.SerializeObject(_entities.Patient_PatientInformation
                    .Where(x => x.PepId == pepId && !x.IsDeleted).ToList());

                var operationResponse = _entities.Sp_System_DataIntegrity_Opr_CreateNew(recordRow).FirstOrDefault() ??
                                        new Sp_System_DataIntegrity_Opr_CreateNew_Result();

                var messageBody =
                    System.IO.File.ReadAllText(
                        $"{HostingEnvironment.ApplicationPhysicalPath}Assets\\message\\di_createnew.html");

                messageBody = messageBody.Replace("{{patient_name}}", operationResponse.PatientName);
                messageBody = messageBody.Replace("{{last_pep}}", operationResponse.LastPepId);
                messageBody = messageBody.Replace("{{new_pep}}", operationResponse.NewPepId);

                Messaging.SendMail(SecurityModel.GetUserInSession.AdministrationStaffInformation.Email, null, null,
                    "Data Integrity Notification: Create as New", messageBody, null);

                var afterData = Newtonsoft.Json.JsonConvert.SerializeObject(_entities.Patient_PatientInformation
                    .Where(x => x.PepId == pepId && !x.IsDeleted).ToList());
                LogAction(ActionTypeDataIntegrity.CreateNew, beforeData, afterData);
                Reload();

                return
                    Json(
                        new ResponseData
                        {
                            Status = true,
                            Message = "Successful",
                            Data = null
                        },
                        JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                ActivityLogger.Log(ex);
                return Json(new ResponseData {Status = false, Message = ex.Message}, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult Preffered(int recordRow, string pepId)
        {
            try
            {
                var beforeData = Newtonsoft.Json.JsonConvert.SerializeObject(_entities.Patient_PatientInformation
                    .Where(x => x.PepId == pepId && !x.IsDeleted).ToList());

                var operationResponse = _entities.Sp_System_DataIntegrity_Opr_Preffered(recordRow).FirstOrDefault() ??
                                        new Sp_System_DataIntegrity_Opr_Preffered_Result();

                var messageBody =
                    System.IO.File.ReadAllText(
                        $"{HostingEnvironment.ApplicationPhysicalPath}Assets\\message\\di_preffered.html");

                messageBody = messageBody.Replace("{{patient_name}}", operationResponse.PatientName);
                messageBody = messageBody.Replace("{{last_pep}}", operationResponse.LastPepId);

                Messaging.SendMail(SecurityModel.GetUserInSession.AdministrationStaffInformation.Email, null, null,
                    "Data Integrity Notification: Select Preffered", messageBody, null);
                
                var afterData = Newtonsoft.Json.JsonConvert.SerializeObject(_entities.Patient_PatientInformation
                    .Where(x => x.PepId == pepId && !x.IsDeleted).ToList());
                LogAction(ActionTypeDataIntegrity.Preffered, beforeData, afterData);
                Reload();

                return
                    Json(
                        new ResponseData
                        {
                            Status = true,
                            Message = "Successful",
                            Data = null
                        },
                        JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                ActivityLogger.Log(ex);
                return Json(new ResponseData { Status = false, Message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult Delete(int recordRow, string pepId)
        {
            try
            {
                var beforeData = Newtonsoft.Json.JsonConvert.SerializeObject(_entities.Patient_PatientInformation
                    .Where(x => x.PepId == pepId && !x.IsDeleted).ToList());

                var operationResponse = _entities.Sp_System_DataIntegrity_Opr_Delete(recordRow).FirstOrDefault() ??
                                        new Sp_System_DataIntegrity_Opr_Delete_Result();

                var messageBody =
                    System.IO.File.ReadAllText(
                        $"{HostingEnvironment.ApplicationPhysicalPath}Assets\\message\\di_delete.html");

                messageBody = messageBody.Replace("{{patient_name}}", operationResponse.PatientName);
                messageBody = messageBody.Replace("{{last_pep}}", operationResponse.LastPepId);

                Messaging.SendMail(SecurityModel.GetUserInSession.AdministrationStaffInformation.Email, null, null,
                    "Data Integrity Notification: Delete", messageBody, null);

                var afterData = Newtonsoft.Json.JsonConvert.SerializeObject(_entities.Patient_PatientInformation
                    .Where(x => x.PepId == pepId && !x.IsDeleted).ToList());
                LogAction(ActionTypeDataIntegrity.Delete, beforeData, afterData);
                Reload();

                return
                    Json(
                        new ResponseData
                        {
                            Status = true,
                            Message = "Successful",
                            Data = null
                        },
                        JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                ActivityLogger.Log(ex);
                return Json(new ResponseData { Status = false, Message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult GetData()
        {
            try
            {
                if (DateTime.Now.Subtract(EngineDataIntegrity.DateGenerated).TotalSeconds > 60)
                {
                    EngineDataIntegrity.DataIntegrityPepId = _entities.Sp_System_DataIntegrity_PepId().ToList();
                    EngineDataIntegrity.DateGenerated = DateTime.Now;
                }

                return
                    Json(
                        new ResponseData
                        {
                            Status = true,
                            Message = "Successful",
                            Data = EngineDataIntegrity.DataIntegrityPepId
                        },
                        JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                ActivityLogger.Log(ex);
                return Json(new ResponseData {Status = false, Message = ex.Message}, JsonRequestBehavior.AllowGet);
            }
        }

        private static void Reload()
        {
            new EngineDataIntegrity();
        }

        private void LogAction(ActionTypeDataIntegrity actionTypeDataIntegrity, string dataBefore,
            string dataAfter)
        {
            try
            {
                var action = (int) actionTypeDataIntegrity;
                _entities.System_DataIntegrityActionLog.Add(new System_DataIntegrityActionLog()
                {
                    IsDeleted = false,
                    ActionDate = DateTime.Now, 
                    ActionReversedDate = DateTime.Now, 
                    ActionReversedUser = 0, 
                    ActionType = action, 
                    ActionUser = SecurityModel.GetUserInSession.AdministrationStaffInformation.Id, 
                    DataAfter = dataAfter, 
                    DataBefore = dataBefore, 
                    IsReversed = false
                });
                _entities.SaveChangesAsync();
            }
            catch (Exception e)
            {
                ActivityLogger.Log(e);
            }
        }
    }
}