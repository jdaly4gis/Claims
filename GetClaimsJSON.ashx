<%@ WebHandler Language="C#" Class="GetClaimsJSON" %>

using System;
using System.Text;
using System.Data.SqlClient;
using System.Web;

public class GetClaimsJSON : IHttpHandler {

    ClaimsAccess access;

    public void ProcessRequest (HttpContext context) {

       string mode   = HttpUtility.UrlDecode(context.Request["mode"]);
       string entity = HttpUtility.UrlDecode(context.Request["ent"]);       
       string query  = HttpUtility.UrlDecode(context.Request["q"]);                     
       string id     = HttpUtility.UrlDecode(context.Request["id"]);              
       string start  = HttpUtility.UrlDecode(context.Request["start"]);                     
       string count  = HttpUtility.UrlDecode(context.Request["count"]);                     

       string outputToReturn = String.Empty;
       string results = String.Empty;

       if (mode.Equals("init"))
       {
          access = new ClaimsAccess("claimsDB");

          switch (entity)
          {
             case "dept":
                outputToReturn = access.GetDepartment("1");
                break;
             case "supe":
                outputToReturn = access.GetSupervisor("2");
                break;
             case "empclass":
                outputToReturn = access.GetEmployeeClass("3");
                break;
             case "divcode":
                outputToReturn = access.GetDivisionCode("4");
                break;
             case "emp":
                outputToReturn = access.GetEmployee("5", null);
                break;
             case "empfilter":
                outputToReturn = access.GetEmployee("1", query);
                break;
             case "claim":
                outputToReturn = access.GetClaimType("6");
                break;
             case "incident":
                outputToReturn = access.GetIncident("7", null);
                break;
             case "incfilter":
                outputToReturn = access.GetIncident("2", query);
                break;
             case "INService":
                outputToReturn = access.GetINService("4", query);
                break;
             case "caction":
                outputToReturn = access.GetCorrectiveAction("5", query);
                break;
             case "INList":
                outputToReturn = access.GetINList("9");
                break;
             case "ContactLog":
                outputToReturn = access.GetContactLog("10", null);
                break;
             case "condate":
                outputToReturn = access.GetContactLog("3", query);
                break;
             case "Grid":
                outputToReturn = access.GetGridData("7", null);
                break;
             case "doctor":
                outputToReturn = access.GetDoctor("6", query);
                break;
             case "equipdist":
                outputToReturn = access.EquipmentDistribution("7", query);
                break;
             case "insurance":
                outputToReturn = access.Insurance("8", query);
                break;
             case "investigation":
                outputToReturn = access.Investigation("9", query);
                break;
             case "standardPPE":
                outputToReturn = access.StandardPPE("11", query);
                break;
             case "training":
                outputToReturn = access.Training("12", query);
                break;
             case "witness":
                outputToReturn = access.Witness("13", query);
                break;
          }                
       }
       else if (mode.Equals("admin"))
       {
          switch (entity)
          {
             case "emp":
                Employee  empTypeRecord = new Employee(context);
                access = new ClaimsAccess("claimsDB", empTypeRecord);
                outputToReturn = empTypeRecord.Result;
                  
                break;                                                
/*
             case "dept":
                Department dTypeRecord = new Department(context.Request["id"], context.Request["name"], context.Request["action"]);
                access = new ClaimsAccess("claimsDB", dTypeRecord);
                outputToReturn = access.RunSqlMethods(context.Request["action"], dTypeRecord.GetSQLQuery);
                break;
             case "supe":
                Supervisor  sTypeRecord = new Supervisor(context);
                access = new ClaimsAccess("claimsDB", sTypeRecord);
                outputToReturn = access.RunSqlMethods(context.Request["action"], sTypeRecord.GetSQLQuery);
                break;
             case "empclass":
                EmployeeClass  ecTypeRecord = new EmployeeClass(context);
                access = new ClaimsAccess("claimsDB", ecTypeRecord);
                outputToReturn = access.RunSqlMethods(context.Request["action"], ecTypeRecord.GetSQLQuery);
                break;                
             case "claim":
                ClaimType  ctTypeRecord = new ClaimType(context);
                access = new ClaimsAccess("claimsDB", ctTypeRecord);
                outputToReturn = access.RunSqlMethods(context.Request["action"], ctTypeRecord.GetSQLQuery);
                break;                                
             case "divcode":
                DivisionCode  dcTypeRecord = new DivisionCode(context);
                access = new ClaimsAccess("claimsDB", dcTypeRecord);
                outputToReturn = access.RunSqlMethods(context.Request["action"], dcTypeRecord.GetSQLQuery);
                break;                                

             case "incident":
                Incident  iTypeRecord = new Incident(context);
                access = new ClaimsAccess("claimsDB", iTypeRecord);
                outputToReturn = access.RunSqlMethods(context.Request["action"], iTypeRecord.GetSQLQuery);
                string [] parts = outputToReturn.Split(':');
                if (parts.Length != 2)
                {
                   int action = Convert.ToInt32(context.Request["action"]);
                   iTypeRecord.DoLog(action);
                }                                      
                break;                                                                
             case "INService":
                INService  INTypeRecord = new INService(context);
                access = new ClaimsAccess("claimsDB", INTypeRecord);
                outputToReturn = access.RunSqlMethods(context.Request["action"], INTypeRecord.GetSQLQuery);
                break;                                                                                
             case "ContactLog":
                ContactLog  CLogTypeRecord = new ContactLog(context);
                access = new ClaimsAccess("claimsDB", CLogTypeRecord);
                outputToReturn = access.RunSqlMethods(context.Request["action"], CLogTypeRecord.GetSQLQuery);
                break;                                                                                                
             case "CorrectiveAction":
                CorrectiveAction  CActionTypeRecord = new CorrectiveAction(context);
                access = new ClaimsAccess("claimsDB", CActionTypeRecord);
                outputToReturn = access.RunSqlMethods(context.Request["action"], CActionTypeRecord.GetSQLQuery);
                break;                                                                                                                
             case "Doctor":
                Doctor  DocActionTypeRecord = new Doctor(context);
                access = new ClaimsAccess("claimsDB", DocActionTypeRecord);
                outputToReturn = access.RunSqlMethods(context.Request["action"], DocActionTypeRecord.GetSQLQuery);
                break;                                                                                                                                
             case "Equip":
                EquipmentDistribution  EQActionTypeRecord = new EquipmentDistribution(context);
                access = new ClaimsAccess("claimsDB", EQActionTypeRecord);
                outputToReturn = access.RunSqlMethods(context.Request["action"], EQActionTypeRecord.GetSQLQuery);
                break;                                                                                                                                                
             case "Insurance":
                Insurance  InsActionTypeRecord = new Insurance(context);
                access = new ClaimsAccess("claimsDB", InsActionTypeRecord);
                outputToReturn = access.RunSqlMethods(context.Request["action"], InsActionTypeRecord.GetSQLQuery);
                break;                                                                                                                                                                
             case "Investigation":
                Investigation  InvActionTypeRecord = new Investigation(context);
                access = new ClaimsAccess("claimsDB", InvActionTypeRecord);
                outputToReturn = access.RunSqlMethods(context.Request["action"], InvActionTypeRecord.GetSQLQuery);
                break;                                                                                                                                                                                
             case "SPPE":
                StandardPPE  PPEActionTypeRecord = new StandardPPE(context);
                access = new ClaimsAccess("claimsDB", PPEActionTypeRecord);
                outputToReturn = access.RunSqlMethods(context.Request["action"], PPEActionTypeRecord.GetSQLQuery);
                break;                                                                                                                                                                                       
             case "Train":
                Training  TrainActionTypeRecord = new Training(context);
                access = new ClaimsAccess("claimsDB", TrainActionTypeRecord);
                outputToReturn = access.RunSqlMethods(context.Request["action"], TrainActionTypeRecord.GetSQLQuery);
                break;                                                                                                                                                                                       
             case "PostWitness":
                Witness  WitnessActionTypeRecord = new Witness(context);
                access = new ClaimsAccess("claimsDB", WitnessActionTypeRecord);
                outputToReturn = access.RunSqlMethods(context.Request["action"], WitnessActionTypeRecord.GetSQLQuery);
                break;                      
*/
          }                
       }

       if(!string.IsNullOrEmpty(results))
       {
          outputToReturn = results;
       }
       
       context.Response.ContentType = "application/json";
       context.Response.ContentEncoding = Encoding.UTF8;
       context.Response.Write(outputToReturn);
       
       }

       public bool IsReusable {
       get {
          return false;
          }
     }
}


