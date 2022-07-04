using Newtonsoft.Json;
using PatientDataAdministration.Core;
using PatientDataAdministration.Core.PubSub;
using PatientDataAdministration.Data.InterchangeModels;
using PatientDataAdministration.EnumLibrary;
using PatientDataAdministration.EnumLibrary.Dictionary;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace PatientDataAdministration.Web.Areas.ClientCommunication.Controllers
{
    public class InstantBioCheckController : BaseClientCommController
    {
        [HttpPost]
        public JsonResult PostNewVerification(DuplicationSubmission patientInformation)
        {
            try
            {
                var operationId = Guid.NewGuid().ToString().ToUpper(); 

                Core.PubSub.Redis.Operations.Publish(PubSubAction.InstaDedupClientSub.NormalizeDisplayName(),
                    new StackExchange.Redis.RedisValue(JsonConvert.SerializeObject(new CommunicationModel()
                    {
                        Data = JsonConvert.SerializeObject(new DedupSubmission()
                        {
                            OperationId = operationId, 
                            PatientDataSubmitted = new List<PatientData> { 
                                new PatientData() { 
                                    BioDataSource = 0, 
                                    FingerPosition = FingerPrintPosition.LeftThumb, 
                                    FingerPrintData = patientInformation.FingerPrimary, 
                                    FingerPrintStore = FingerPrintStore.Primary, 
                                    PepId = patientInformation.PepId, 
                                    RowId = 0
                                },
                                new PatientData() {
                                    BioDataSource = 0,
                                    FingerPosition = FingerPrintPosition.RightThumb,
                                    FingerPrintData = patientInformation.FingerSecondary,
                                    FingerPrintStore = FingerPrintStore.Primary,
                                    PepId = patientInformation.PepId,
                                    RowId = 0
                                } }
                        }),
                        PubSubAction = PubSubAction.InstaDedupClientSub
                    })));

                Core.InMemory.Redis.Operations.SaveData(operationId, new InstantDudupModel
                {
                    OperationId = operationId,
                    DuplicationSuspects = new List<DuplicationSuspect>(),
                    ProcessingStatus = ProcessingStatus.Submitted
                });

                return
                    Json(
                        new ResponseData
                        {
                            Status = true,
                            Message = $"Task Started Succesfully", 
                            Data = operationId 
                        }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                ActivityLogger.Log(ex);
                return Json(new ResponseData { Status = false, Message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult CheckOperationStatus(string operationGuid)
        {
            try
            {
                return
                    Json(
                        new ResponseData
                        {
                            Status = true,
                            Message = $"Message", 
                            Data = Core.InMemory.Redis.Operations.ReadData<InstantDudupModel>(operationGuid)
                        }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                ActivityLogger.Log(ex);
                return Json(new ResponseData { Status = false, Message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}