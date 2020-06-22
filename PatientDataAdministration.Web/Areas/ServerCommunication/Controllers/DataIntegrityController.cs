using System;
using System.Linq;
using System.Web.Hosting;
using System.Web.Mvc;
using PatientDataAdministration.Core;
using PatientDataAdministration.Data;
using PatientDataAdministration.Data.InterchangeModels;
using PatientDataAdministration.EnumLibrary;
using PatientDataAdministration.EnumLibrary.Dictionary;
using PatientDataAdministration.Web.Engines.EngineDataIntegrity;
using PatientDataAdministration.Web.Models;

namespace PatientDataAdministration.Web.Areas.ServerCommunication.Controllers
{
    public class DataIntegrityController : BaseServerCommController
    {
        // GET: ServerCommunication/DataIntegrity
        private readonly Entities _entities = new Entities();

        public JsonResult CreateNewPepIdIntegrity(int recordRow, string pepId)
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
                LogActionPepIdIntegrity(ActionTypeDataIntegrity.CreateNew, beforeData, afterData);
                ReloadPepIdIntegrity();

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

        public JsonResult PrefferedPepIdIntegrity(int recordRow, string pepId)
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
                LogActionPepIdIntegrity(ActionTypeDataIntegrity.Preffered, beforeData, afterData);
                ReloadPepIdIntegrity();

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

        public JsonResult DeletePepIdIntegrity(int recordRow, string pepId)
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
                LogActionPepIdIntegrity(ActionTypeDataIntegrity.Delete, beforeData, afterData);
                ReloadPepIdIntegrity();

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

        public JsonResult GetDataPepIdIntegrity()
        {
            try
            {
                var task = EngineDataIntegrity.Tasks
                    .FirstOrDefault(x =>
                        x.ThreadEngine.Name == DataIntegrityIssue.DupPepId.DisplayName());

                if (DateTime.Now
                        .Subtract(task?.DateGenerated ?? DateTime.Now).TotalSeconds > 60)
                {
                    _entities.Database.CommandTimeout = 0;
                    EngineDuplicatePepId.DataIntegrityPepId = _entities.Sp_System_DataIntegrity_PepId().ToList();

                    if (task != null)
                        task.DateGenerated = DateTime.Now;
                }

                return
                    Json(
                        new ResponseData
                        {
                            Status = true,
                            Message = "Successful",
                            Data = EngineDuplicatePepId.DataIntegrityPepId
                        },
                        JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                ActivityLogger.Log(ex);
                return Json(new ResponseData {Status = false, Message = ex.Message}, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult GetBioDataIntegrity()
        {
            try
            {
                return
                    Json(
                        new ResponseData
                        {
                            Status = true,
                            Message = "Successful",
                            Data = EngineDuplicateBioData.BioDataIntegrityCases
                        },
                        JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                ActivityLogger.Log(ex);
                return Json(new ResponseData { Status = false, Message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult GetBioDataIntegritySuspects(int caseId)
        {
            try
            {
                var suspectInformation = _entities.Patient_PatientBiometricIntegrityCaseMember
                    .Where(x => !x.IsDeleted &&
                                !x.IsTreated &&
                                x.PatientBiometricIntegrityCaseId == caseId &&
                                x.MemberTreatmentTypeId == (int) CaseMemberStatus.Undecided).ToList();
                return
                    Json(
                        new ResponseData
                        {
                            Status = true,
                            Message = "Successful",
                            Data = suspectInformation.Select(x => new
                            {
                                SuspectData = x, 
                                PatientInformation = _entities.Patient_PatientInformation.FirstOrDefault(y => y.PepId == x.SuspectPepId),
                                BiometricInformation = _entities.Patient_PatientBiometricData.FirstOrDefault(y => y.PepId == x.SuspectPepId)
                            }).ToList()
                        },
                        JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                ActivityLogger.Log(ex);
                return Json(new ResponseData { Status = false, Message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        private static void ReloadPepIdIntegrity()
        {
            EngineDuplicatePepId.ProcessDataIntegrityPepId();

            EngineDuplicateBioData.ProcessDataIntegrityBiometric();
        }

        private void LogActionPepIdIntegrity(ActionTypeDataIntegrity actionTypeDataIntegrity, string dataBefore,
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