using System;
using System.Data;
using System.Collections;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using JEncode;
using System.Drawing.Imaging;

/// <summary>
/// Summary description for ClaimsAccess
/// </summary>
public class ClaimsAccess 
{
    string sql;
    object login = null;
    string query = null;
    string db = null;
    object obj = null;
    string start = null;

    SqlConnection conn = null;

    public ClaimsAccess()
    {
    }

    public ClaimsAccess(string db) : this()
    {
       this.db = db;
       login = OpenConnection(this.db);
    }

    public ClaimsAccess(string q, string db) :this(db)
    {
        this.query = q.Replace("*", "%");
    }

    public ClaimsAccess(string db, object obj) :this(db)
    {
       this.obj = obj;
    }

    public object GetLoginStatus
    {
        get
        {
            return login;
        }
    }
 
    private Object OpenConnection(string database)
    {
       string connectionInfo;

       if (database.Equals("claimsDB"))
       {
          connectionInfo = System.Configuration.ConfigurationManager.AppSettings["ClaimsConnect"];
       }
       else
       {
          connectionInfo = System.Configuration.ConfigurationManager.AppSettings["GovernConnect"];
       }
        conn = new SqlConnection(connectionInfo);

        try
        {
            conn.Open();
        }
        catch (Exception e)
        {
            return e;
        }

        return (conn);
    }

    private Object GovWebConnection()
    {
        string connectionInfo = System.Configuration.ConfigurationManager.AppSettings["GovernConnect"];
        conn = new SqlConnection(connectionInfo);

        try
        {
            conn.Open();
        }
        catch (Exception e)
        {
            return e;
    }

        return (conn);
    }

    private string GetComboSearchType(string searchType, string query)
    {
       switch (Convert.ToInt32(searchType))
       {
          case 1:
                sql = "SELECT ID, DepartmentID, SupervisorID, LastName, Middle, FirstName, Address, City, State, Zip, JobTitle, EmployeeClass, DivisionCode, Email FROM Employee WHERE LastName LIKE '" + query + "'";
             break;
          case 2:
             sql = @"SELECT Incident.ID, Employee.LastName AS ELast, EmployeeID, CONVERT(VARCHAR(10),DateOfAccident,20) AS DoA, PrimaryInjury, SecondaryInjury, Cause, Description, Supervisor, CONVERT(VARCHAR(10),ReturnWork,20) AS RetWork,CONVERT(VARCHAR(10),StopWork,20) As SWork,CONVERT(VARCHAR(10),ReturnTransitional,20) AS TransWork, ClaimNumber,Location,Time, ClaimType,INService, DATEDIFF(day, StopWork, ReturnWork) AS DaysLost, DATEPART(year, DateOfAccident) AS Year,DATENAME(month,DateOfAccident) AS Month,Paid,FutureReserve,TotalIncurred FROM Employee,Incident WHERE Incident.EmployeeID = Employee.ID AND ClaimNumber LIKE '" + query + "'";
             break;
          case 3:
             sql = @"SELECT ID, IncidentID, CONVERT(VARCHAR(10), Date,20) AS ContactDate, CONVERT(VARCHAR(10), FollowUp,20) AS FollowDate, Notes, Type FROM ContactLog WHERE IncidentID ='" + query + "' ORDER BY IncidentID";
             break;
          case 4:
             sql = @"SELECT ID, IncidentID, Topic, CONVERT(VARCHAR(10), Date,20) AS INDate, Instructor, IsCompleted FROM INServiceLog WHERE IncidentID ='" + query + "' ORDER BY IncidentID";
             break;
          case 5:
             sql = @"SELECT ID, IncidentID, CONVERT(VARCHAR(10), DateApplied,20) AS DateApp, IssueDescription, CorrectiveAction, AdditionalAction, CONVERT(VARCHAR(10), DateAchieved,20) AS DateAch FROM CorrectiveAction WHERE IncidentID ='" + query + "' ORDER BY IncidentID";
             break;
          case 6:
             sql = @"SELECT ID, IncidentID, Name, Address, PracticeName, Town, Zip, Phone FROM Doctor WHERE IncidentID ='" + query + "' ORDER BY IncidentID";
             break;
          case 7:
             sql = @"SELECT ID, IncidentID, Description, CONVERT(VARCHAR(10), RequestDate,20) AS ReqDate, CONVERT(VARCHAR(10), DeliveryDate,20) AS DelDate, CONVERT(VARCHAR(10), ReceiptDate,20) AS RecDate FROM EquipmentDistribution WHERE IncidentID ='" + query + "' ORDER BY IncidentID";
             break;
          case 8:
             sql = @"SELECT ID, IncidentID, PolicyID, Description, CoverageType, PremiumCost, DeductibleLevel, Year FROM Insurance WHERE IncidentID ='" + query + "' ORDER BY IncidentID";
             break;
          case 9:
             sql = @"SELECT ID, IncidentID, CONVERT(VARCHAR(10), Date,20) AS InitDate, CONVERT(VARCHAR(10), FollowUp,20) AS FollowDate, Findings, Recommendations FROM Investigation WHERE IncidentID ='" + query + "' ORDER BY IncidentID";
             break;
          case 11:
             sql = @"SELECT ID, IncidentID, EQID, Cost, Description, Vendor FROM StandardPPE WHERE IncidentID ='" + query + "' ORDER BY IncidentID";
             break;
          case 12:
             sql = @"SELECT ID, IncidentID, Name, CONVERT(VARCHAR(10), Date,20) AS TrainingDate, Time, Location, Trainer FROM Training WHERE IncidentID ='" + query + "' ORDER BY IncidentID";
             break;
          case 13:
             sql = @"SELECT ID, IncidentID, Name, Address, Phone, Zip, Email FROM Witness WHERE IncidentID ='" + query + "' ORDER BY IncidentID";
             break;
       }

       return (sql);
    }

    private string GetSearchType(string searchType)
    {
        switch (Convert.ToInt32(searchType))
        {
            case 1:
                sql = "SELECT * FROM Department";
                break;
            case 2:
                sql = "SELECT ID, Title, First, Middle, Last, Email, Last + ', ' + First AS FullName FROM Supervisor";
                break;
            case 3:
                sql = "SELECT ID, Name, Description FROM EmployeeClass";
                break;
            case 4:
                sql = "SELECT ID, Name, Description FROM DivisionCode";
                break;
            case 5:
                sql = "SELECT ID, DepartmentID, SupervisorID, LastName, Middle, FirstName, Address, City, State, Zip, JobTitle, EmployeeClass, DivisionCode, Email FROM Employee";
                break;
            case 6:
                sql = "SELECT ID, Name, Description FROM ClaimType";
                break;
            case 7:
                sql = @"SELECT ID, EmployeeID,CONVERT(VARCHAR(10),DateOfAccident,20) AS DoA, PrimaryInjury, SecondaryInjury, Cause, Description, Supervisor, CONVERT(VARCHAR(10),ReturnWork,20) AS RetWork,CONVERT(VARCHAR(10),StopWork,20) As SWork,CONVERT(VARCHAR(10),ReturnTransitional,20) AS TransWork, ClaimNumber,Location,Time, ClaimType,INService, DATEDIFF(day, StopWork, ReturnWork) AS DaysLost, DATEPART(year, DateOfAccident) AS Year,DATENAME(month,DateOfAccident) AS Month,Paid,FutureReserve,TotalIncurred FROM Incident";
                break;
            case 8:
               //sql = @"SELECT INServiceLog.ID, ClaimNumber, Topic, CONVERT(VARCHAR(10), Date,20) AS INDate, Instructor, IsCompleted FROM Incident LEFT JOIN INServiceLog ON Incident.ClaimNumber = INServiceLog.IncidentID";
               //  sql = @"SELECT INServiceLog.ID, ClaimNumber, Topic, CONVERT(VARCHAR(10), Date,20) AS INDate, Instructor, IsCompleted FROM Incident INNER JOIN INServiceLog ON Incident.ClaimNumber = INServiceLog.IncidentID";
               //sql = @"SELECT ID, IncidentID, Topic, CONVERT(VARCHAR(10), Date,20) AS INDate, Instructor, IsCompleted FROM INServiceLog ORDER BY DATE";
               //sql = @"SELECT ID, ClaimNumber, CONVERT(VARCHAR(10), DateOfAccident,20) AS AccDate FROM Incident";
                //sql = @"SELECT INServiceLog.ID, IncidentID, ClaimNumber, Topic, CONVERT(VARCHAR(10), Date,20) AS INDate, Instructor, IsCompleted FROM Incident, INServiceLog WHERE INServiceLog.IncidentID = Incident.ID ORDER BY DATE";
                sql = @"SELECT ID, ClaimNumber, CONVERT(VARCHAR(10), DateOfAccident,20) AS AccidentDate FROM Incident ORDER BY DateOfAccident";
                break;
            case 9:
                sql = @"SELECT ID, IncidentID, Topic, Date, Instructor, IsCompleted FROM INServiceLog";
                break;
            case 10:
                //sql = @"SELECT ContactLog.ID, ClaimNumber, CONVERT(VARCHAR(10), Date,20) AS ContactDate, CONVERT(VARCHAR(10), FollowUp,20) AS FollowDate, Notes, Type FROM Incident LEFT JOIN ContactLog ON Incident.ClaimNumber = ContactLog.IncidentID";
                //sql = @"SELECT ID, IncidentID, CONVERT(VARCHAR(10), Date,20) AS ContactDate, CONVERT(VARCHAR(10), FollowUp,20) AS FollowDate, Notes, Type FROM ContactLog";
                //sql = @"SELECT DISTINCT IncidentID FROM ContactLog";
                //sql = @"SELECT ID, IncidentID FROM (SELECT ID, IncidentID,ROW_NUMBER() OVER (PARTITION BY IncidentID ORDER BY ID) AS RowNumber FROM ContactLog) AS a WHERE   a.RowNumber = 1";
                sql = @"SELECT ID, IncidentID, CONVERT(VARCHAR(10), Date,20) AS INDate,CONVERT(VARCHAR(10), FollowUp,20) AS FollowDate, Notes, Type FROM ContactLog ORDER BY Date";
                break;

           default:
                sql = @"SELECT TOP (11) PARCEL_ID, DSBL FROM  SDEadmin.TAX_PARCELS WHERE (PARCEL_ID LIKE '" + this.query + "') AND (NOT (PARCEL_ID IN (SELECT TOP (" + this.start + ") PARCEL_ID FROM SDEadmin.TAX_PARCELS WHERE (PARCEL_ID LIKE '" + this.query + "') ORDER BY PARCEL_ID))) ORDER BY PARCEL_ID";
                break;
        }        
        
        return (sql);
    }


    private string GetCurrentDate()
    {
       return(DateTime.Now.ToString("yyyy-MM-dd"));
     }

    public string RunSqlMethods(string action, string query)
    {
        SqlCommand command = new SqlCommand(query, conn);
        command.Connection = conn;
        string reader;

        if (action.Equals("1"))
        {
           try
           {
              reader = command.ExecuteScalar().ToString();
           }
           catch (Exception ex)
           {
              reader = "-1:" + ex.Message;
           }
        }
        else
        {
           try
           {
              reader = (string)command.ExecuteScalar();
           }
           catch (Exception ex)
           {
              reader = "-1:" + ex.Message;
           }           
        }

        if (reader == null)
        {
           reader = "0";
        }

       return (reader);
    }

    public string GetDepartment(string type)
    {
        string sql = this.GetSearchType(type);
        DataTable dt = new DataTable();
        SqlCommand command = new SqlCommand(sql, conn);
        SqlDataAdapter adapter = new SqlDataAdapter(command);
        adapter.Fill(dt);
        conn.Close();
        
        string[] tblJson = null;
        tblJson = new string[dt.Rows.Count];
        string returnString = "";
        string jsonString = null;
        string header = "[";
        string trailer = "]";

        if (dt.Rows.Count != 0)
        {
           int ncount = 0;
           foreach (DataRow row in dt.Rows)
           {
              Hashtable taxTable = new Hashtable();

              taxTable.Add("label", row["Name"].ToString());
              taxTable.Add("id", row["ID"].ToString());

              jsonString = JSON.JsonEncode(taxTable);

              tblJson[ncount] = jsonString;
              ncount = ncount + 1;
           }

           returnString = header + String.Join(",", tblJson) + trailer;

        }
        else
        {
           returnString = "[{\"id\": 0, \"label\": \"Enter Department...\"}]";
        }

        dt.Dispose();
        return returnString;
    }



    public string GetSupervisor(string type)
    {
        string sql = this.GetSearchType(type);
        DataTable dt = new DataTable();
        SqlCommand command = new SqlCommand(sql, conn);
        SqlDataAdapter adapter = new SqlDataAdapter(command);
        adapter.Fill(dt);
        conn.Close();
        
        string[] tblJson = null;
        tblJson = new string[dt.Rows.Count];
        string returnString = "";
        string jsonString = null;
        string header = "[";
        string trailer = "]";

        if (dt.Rows.Count != 0)
        {
           int ncount = 0;
           foreach (DataRow row in dt.Rows)
           {
              Hashtable taxTable = new Hashtable();

              taxTable.Add("label", row["FullName"].ToString());
              taxTable.Add("id", row["ID"].ToString());
              taxTable.Add("title", row["Title"].ToString());
              taxTable.Add("first", row["First"].ToString());
              taxTable.Add("middle", row["Middle"].ToString());
              taxTable.Add("last", row["Last"].ToString());
              taxTable.Add("email", row["Email"].ToString());

              jsonString = JSON.JsonEncode(taxTable);

              tblJson[ncount] = jsonString;
              ncount = ncount + 1;
           }

           returnString = header + String.Join(",", tblJson) + trailer;

        }
        else
        {
           returnString = "[{\"id\": 0, \"label\": \"Enter Supervisor...\"}]";
        }

        dt.Dispose();
        return returnString;
    }


    public string GetEmployeeClass(string type)
    {
        string sql = this.GetSearchType(type);
        DataTable dt = new DataTable();
        SqlCommand command = new SqlCommand(sql, conn);
        SqlDataAdapter adapter = new SqlDataAdapter(command);
        adapter.Fill(dt);
        conn.Close();
        
        string[] tblJson = null;
        tblJson = new string[dt.Rows.Count];
        string returnString = "";
        string jsonString = null;
        string header = "[";
        string trailer = "]";

        if (dt.Rows.Count != 0)
        {
           int ncount = 0;
           foreach (DataRow row in dt.Rows)
           {
              Hashtable taxTable = new Hashtable();

              taxTable.Add("id", row["ID"].ToString());
              taxTable.Add("label", row["Name"].ToString());
              taxTable.Add("description", row["Description"].ToString());

              jsonString = JSON.JsonEncode(taxTable);

              tblJson[ncount] = jsonString;
              ncount = ncount + 1;
           }

           returnString = header + String.Join(",", tblJson) + trailer;

        }
        else
        {
           returnString = "[{\"id\": 0, \"label\": \"Enter Employee Class...\"}]";
        }

        dt.Dispose();
        return returnString;
    }

    public string GetClaimType(string type)
    {
        string sql = this.GetSearchType(type);
        DataTable dt = new DataTable();
        SqlCommand command = new SqlCommand(sql, conn);
        SqlDataAdapter adapter = new SqlDataAdapter(command);
        adapter.Fill(dt);
        conn.Close();
        
        string[] tblJson = null;
        tblJson = new string[dt.Rows.Count];
        string returnString = "";
        string jsonString = null;
        string header = "[";
        string trailer = "]";

        if (dt.Rows.Count != 0)
        {
           int ncount = 0;
           foreach (DataRow row in dt.Rows)
           {
              Hashtable taxTable = new Hashtable();

              taxTable.Add("id", row["ID"].ToString());
              taxTable.Add("label", row["Name"].ToString());
              taxTable.Add("description", row["Description"].ToString());

              jsonString = JSON.JsonEncode(taxTable);

              tblJson[ncount] = jsonString;
              ncount = ncount + 1;
           }

           returnString = header + String.Join(",", tblJson) + trailer;

        }
        else
        {
           returnString = "[{\"id\": 0, \"label\": \"Enter Claim Type...\"}]";
        }

        dt.Dispose();
        return returnString;
    }


   public string GetDivisionCode(string type)
    {
        string sql = this.GetSearchType(type);
        DataTable dt = new DataTable();
        SqlCommand command = new SqlCommand(sql, conn);
        SqlDataAdapter adapter = new SqlDataAdapter(command);
        adapter.Fill(dt);
        conn.Close();
        
        string[] tblJson = null;
        tblJson = new string[dt.Rows.Count];
        string returnString = "";
        string jsonString = null;
        string header = "[";
        string trailer = "]";

        if (dt.Rows.Count != 0)
        {
           int ncount = 0;
           foreach (DataRow row in dt.Rows)
           {
              Hashtable taxTable = new Hashtable();

              taxTable.Add("id", row["ID"].ToString());
              taxTable.Add("label", row["Name"].ToString());
              taxTable.Add("description", row["Description"].ToString());

              jsonString = JSON.JsonEncode(taxTable);

              tblJson[ncount] = jsonString;
              ncount = ncount + 1;
           }

           returnString = header + String.Join(",", tblJson) + trailer;

        }
        else
        {
           returnString = "[{\"id\": 0, \"label\": \"Enter Division Code...\"}]";
        }

        dt.Dispose();
        return returnString;
    }

   public string GetEmployee(string type, string query)
    {
       string sql;
       if (query != null)
       {
          sql = this.GetComboSearchType(type, query);
       }
       else
       {
          sql = this.GetSearchType(type);
       }

        DataTable dt = new DataTable();
        SqlCommand command = new SqlCommand(sql, conn);
        SqlDataAdapter adapter = new SqlDataAdapter(command);
        adapter.Fill(dt);
        conn.Close();
        
        string[] tblJson = null;
        tblJson = new string[dt.Rows.Count];
        string returnString = "";
        string jsonString = null;
        string header = "{\"identifier\":\"id\", \"label\":\"LastName\", \"items\":[";
        string trailer = "]}";

        if (dt.Rows.Count != 0)
       { 
           int ncount = 0;
           foreach (DataRow row in dt.Rows)
           {
              Hashtable taxTable = new Hashtable();

              taxTable.Add("id", row["ID"].ToString());
              taxTable.Add("DepartmentID", row["DepartmentID"].ToString());
              taxTable.Add("SupervisorID", row["SupervisorID"].ToString());
              taxTable.Add("FullName", row["LastName"].ToString() + ", " + row["FirstName"].ToString() + " " + row["Middle"].ToString());
              taxTable.Add("LastName", row["LastName"].ToString());
              taxTable.Add("Middle", row["Middle"].ToString());
              taxTable.Add("FirstName", row["FirstName"].ToString());
              taxTable.Add("Address", row["Address"].ToString());
              taxTable.Add("City", row["City"].ToString());
              taxTable.Add("State", row["State"].ToString());
              taxTable.Add("Zip", row["Zip"].ToString());
              taxTable.Add("JobTitle", row["JobTitle"].ToString());
              taxTable.Add("EmployeeClass", row["EmployeeClass"].ToString());
              taxTable.Add("DivisionCode", row["DivisionCode"].ToString());
              taxTable.Add("Email", row["Email"].ToString());

              jsonString = JSON.JsonEncode(taxTable);

              tblJson[ncount] = jsonString;
              ncount = ncount + 1;
           }

           returnString = header + String.Join(",", tblJson) + trailer;

        }
        else
        {
           returnString = "[{\"id\": \"0\", \"LastName\": \"Enter Employee...\"}]";
        }

        dt.Dispose();
        return returnString;
    }




   public string GetIncident(string type, string query)
    {
       string sql, header, trailer;

       if (query != null)
       {
          sql = this.GetComboSearchType(type, query);
          header = "{\"identifier\":\"id\", \"label\":\"ClaimNumber\", \"items\":[";
          trailer = "]}";
       }
       else
       {
          sql = this.GetSearchType(type);
          header = "[";
          trailer = "]";
       }

        DataTable dt = new DataTable();
        SqlCommand command = new SqlCommand(sql, conn);
        SqlDataAdapter adapter = new SqlDataAdapter(command);
        adapter.Fill(dt);
        conn.Close();
        
        string[] tblJson = null;
        tblJson = new string[dt.Rows.Count];
        string returnString = "";
        string jsonString = null;

         if (dt.Rows.Count != 0)
        {
           int ncount = 0;
           foreach (DataRow row in dt.Rows)
           {
              Hashtable taxTable = new Hashtable();

              taxTable.Add("id", row["ID"].ToString());
              taxTable.Add("EmployeeID", row["EmployeeID"].ToString());
              taxTable.Add("DateOfAccident", row["DoA"].ToString());
              taxTable.Add("PrimaryInjury", row["PrimaryInjury"].ToString());
              taxTable.Add("ELast", row["Elast"].ToString());
              taxTable.Add("SecondaryInjury", row["SecondaryInjury"].ToString());
              taxTable.Add("Cause", row["Cause"].ToString());
              taxTable.Add("Description", row["Description"].ToString());
              taxTable.Add("Supervisor", row["Supervisor"].ToString());
              taxTable.Add("ReturnWork", row["RetWork"].ToString());
              taxTable.Add("StopWork", row["SWork"].ToString());
              taxTable.Add("ReturnTransitional", row["TransWork"].ToString());
              taxTable.Add("ClaimNumber", row["ClaimNumber"].ToString());
              taxTable.Add("Location", row["Location"].ToString());
              taxTable.Add("Time", row["Time"].ToString());
              taxTable.Add("ClaimType", row["ClaimType"].ToString());
              taxTable.Add("INService", row["INService"].ToString());
              taxTable.Add("DaysLost", row["DaysLost"].ToString());
              taxTable.Add("Year", row["Year"].ToString());
              taxTable.Add("Month", row["Month"].ToString());
              taxTable.Add("Paid", row["Paid"].ToString());
              taxTable.Add("FutureReserve", row["FutureReserve"].ToString());
              taxTable.Add("TotalIncurred", row["TotalIncurred"].ToString());

              jsonString = JSON.JsonEncode(taxTable);

              tblJson[ncount] = jsonString;
              ncount = ncount + 1;
           }

           returnString = header + String.Join(",", tblJson) + trailer;

        }
        else
        {
           returnString = "[{\"id\": \"0\", \"LastName\": \"Enter Incident...\"}]";
        }

        dt.Dispose();
        return returnString;
    }

   public string GetGridData(string type, string query)
    {
       string sql, header, trailer;

       if (query != null)
       {
          sql = this.GetComboSearchType(type, query);
          header = "{\"identifier\":\"id\", \"label\":\"ClaimNumber\", \"items\":[";
          trailer = "]}";
       }
       else
       {
          sql = this.GetSearchType(type);
          header = "[";
          trailer = "]";
       }

        DataTable dt = new DataTable();
        SqlCommand command = new SqlCommand(sql, conn);
        SqlDataAdapter adapter = new SqlDataAdapter(command);
        adapter.Fill(dt);
        conn.Close();
        
        string[] tblJson = null;
        tblJson = new string[dt.Rows.Count];
        string returnString = "";
        string jsonString = null;

         if (dt.Rows.Count != 0)
        {
           int ncount = 0;
           foreach (DataRow row in dt.Rows)
           {
              Hashtable taxTable = new Hashtable();

              taxTable.Add("id", row["ID"].ToString());
              taxTable.Add("EmployeeID", row["EmployeeID"].ToString());
              taxTable.Add("DateOfAccident", row["DoA"].ToString());
              taxTable.Add("PrimaryInjury", row["PrimaryInjury"].ToString());
              taxTable.Add("SecondaryInjury", row["SecondaryInjury"].ToString());
              taxTable.Add("Cause", row["Cause"].ToString());
              taxTable.Add("Description", row["Description"].ToString());
              taxTable.Add("Supervisor", row["Supervisor"].ToString());
              taxTable.Add("ReturnWork", row["RetWork"].ToString());
              taxTable.Add("StopWork", row["SWork"].ToString());
              taxTable.Add("ReturnTransitional", row["TransWork"].ToString());
              taxTable.Add("ClaimNumber", row["ClaimNumber"].ToString());
              taxTable.Add("Location", row["Location"].ToString());
              taxTable.Add("Time", row["Time"].ToString());
              taxTable.Add("ClaimType", row["ClaimType"].ToString());
              taxTable.Add("INService", row["INService"].ToString());
              taxTable.Add("DaysLost", row["DaysLost"].ToString());
              taxTable.Add("Year", row["Year"].ToString());
              taxTable.Add("Month", row["Month"].ToString());
              taxTable.Add("Paid", row["Paid"].ToString());
              taxTable.Add("FutureReserve", row["FutureReserve"].ToString());
              taxTable.Add("TotalIncurred", row["TotalIncurred"].ToString());

              jsonString = JSON.JsonEncode(taxTable);

              tblJson[ncount] = jsonString;
              ncount = ncount + 1;
           }

           returnString = header + String.Join(",", tblJson) + trailer;

        }
        else
        {
           returnString = "[{\"id\": \"0\", \"LastName\": \"Enter Incident...\"}]";
        }

        dt.Dispose();
        return returnString;
    }

   public string GetINService(string type, string query)
    {
       string sql, header, trailer;

       if (query != null)
        {
           sql = this.GetComboSearchType(type, query);
           header = "{\"identifier\":\"id\", \"label\":\"incidentNo\", \"items\":[";
           trailer = "]}";
        }
        else
        {
           sql = this.GetSearchType(type);
           header = "[";
           trailer = "]";
        }
        DataTable dt = new DataTable();
        SqlCommand command = new SqlCommand(sql, conn);
        SqlDataAdapter adapter = new SqlDataAdapter(command);
        adapter.Fill(dt);
        conn.Close();
        
        string[] tblJson = null;
        tblJson = new string[dt.Rows.Count];
        string returnString = "";
        string jsonString = null;

        if (dt.Rows.Count != 0)
        {
           int ncount = 0;
           foreach (DataRow row in dt.Rows)
           {
              Hashtable taxTable = new Hashtable();

              taxTable.Add("id", row["ID"].ToString());
              taxTable.Add("incidentNo", row["IncidentID"].ToString());
              taxTable.Add("Topic", row["Topic"].ToString());
              taxTable.Add("INDate", row["INDate"].ToString());
              taxTable.Add("Instructor", row["Instructor"].ToString());
              taxTable.Add("IsCompleted", row["IsCompleted"].ToString());

              jsonString = JSON.JsonEncode(taxTable);

              tblJson[ncount] = jsonString;
              ncount = ncount + 1;
           }

           returnString = header + String.Join(",", tblJson) + trailer;
        }
        else
        {

           string date = GetCurrentDate();
           returnString = "{\"identifier\":\"id\", \"label\":\"incidentNo\", \"items\":[{\"id\": \"0\", \"incidentNo\": \"0\", \"Topic\": \"\", \"INDate\": \"" + date + "\", \"Instructor\": \"\", \"IsCompleted\":\"0\"}]}";
        }

        dt.Dispose();
        return returnString;
    }

   public string GetINList(string type)
    {
        string sql = this.GetSearchType(type);
        DataTable dt = new DataTable();
        SqlCommand command = new SqlCommand(sql, conn);
        SqlDataAdapter adapter = new SqlDataAdapter(command);
        adapter.Fill(dt);
        conn.Close();
        
        string[] tblJson = null;
        tblJson = new string[dt.Rows.Count];
        string returnString = "";
        string jsonString = null;
        string header = "[";
        string trailer = "]";

        if (dt.Rows.Count != 0)
        {
           int ncount = 0;
           foreach (DataRow row in dt.Rows)
           {
              Hashtable taxTable = new Hashtable();

              taxTable.Add("id", row["ID"].ToString());
              taxTable.Add("IncidentID", row["IncidentID"].ToString());
              taxTable.Add("Topic", row["Topic"].ToString());
              taxTable.Add("Date", row["INDate"].ToString());
              taxTable.Add("Instructor", row["Instructor"].ToString());
              taxTable.Add("IsCompleted", row["IsCompleted"].ToString());

              jsonString = JSON.JsonEncode(taxTable);

              tblJson[ncount] = jsonString;
              ncount = ncount + 1;
           }

           returnString = header + String.Join(",", tblJson) + trailer;

        }
        else
        {
           returnString = "[{\"id\": \"0\", \"LastName\": \"Enter Incident...\"}]";
        }

        dt.Dispose();
        return returnString;
    }


   public string GetContactLog(string type, string query)
    {
       string sql, header, trailer;

      if (query != null)
       {
          sql = this.GetComboSearchType(type, query);
          header = "{\"identifier\":\"id\", \"label\":\"ContactDate\", \"items\":[";
          trailer = "]}";
       }
       else
       {
          sql = this.GetSearchType(type);
          header = "[";
          trailer = "]";
       }

        DataTable dt = new DataTable();
        SqlCommand command = new SqlCommand(sql, conn);
        SqlDataAdapter adapter = new SqlDataAdapter(command);
        adapter.Fill(dt);
        conn.Close();
        
        string[] tblJson = null;
        tblJson = new string[dt.Rows.Count];
        string returnString = "";
        string jsonString = null;

        if (dt.Rows.Count != 0)
        {
           int ncount = 0;
           foreach (DataRow row in dt.Rows)
           {
              Hashtable taxTable = new Hashtable();
              if (query != null)
              {
                 taxTable.Add("id", row["ID"].ToString());
                 taxTable.Add("incidentNo", row["IncidentID"].ToString());
                 taxTable.Add("ContactDate", row["ContactDate"].ToString());
                 taxTable.Add("FollowDate", row["FollowDate"].ToString());
                 taxTable.Add("Notes", row["Notes"].ToString());
                 taxTable.Add("Type", row["Type"].ToString());
              }
              else
              {
                 taxTable.Add("id", row["ID"].ToString());
                 taxTable.Add("incidentNo", row["IncidentID"].ToString());
              }


              jsonString = JSON.JsonEncode(taxTable);

              tblJson[ncount] = jsonString;
              ncount = ncount + 1;
           }

           returnString = header + String.Join(",", tblJson) + trailer;

        }
        else
        {

           string date = GetCurrentDate();
           returnString = "{\"identifier\":\"id\", \"label\":\"ContactDate\", \"items\":[{\"id\": \"0\", \"incidentNo\": \"0\", \"ContactDate\": \"" + date + "\", \"FollowDate\": \"" + date+ "\", \"Notes\": \"\", \"Type\": \"\"}]}";
        }

        dt.Dispose();
        return returnString;
    }

   public string GetCorrectiveAction(string type, string query)
    {
       string sql, header, trailer;

      if (query != null)
       {
          sql = this.GetComboSearchType(type, query);
          header = "{\"identifier\":\"id\", \"label\":\"incidentNo\", \"items\":[";
          trailer = "]}";
       }
       else
       {
          sql = this.GetSearchType(type);
          header = "[";
          trailer = "]";
       }

        DataTable dt = new DataTable();
        SqlCommand command = new SqlCommand(sql, conn);
        SqlDataAdapter adapter = new SqlDataAdapter(command);
        adapter.Fill(dt);
        conn.Close();
        
        string[] tblJson = null;
        tblJson = new string[dt.Rows.Count];
        string returnString = "";
        string jsonString = null;

        if (dt.Rows.Count != 0)
        {
           int ncount = 0;
           foreach (DataRow row in dt.Rows)
           {
              Hashtable taxTable = new Hashtable();
              if (query != null)
              {
                 taxTable.Add("id", row["ID"].ToString());
                 taxTable.Add("incidentNo", row["IncidentID"].ToString());
                 taxTable.Add("DateApplied", row["DateApp"].ToString());
                 taxTable.Add("IssueDescription", row["IssueDescription"].ToString());
                 taxTable.Add("CorrectiveAction", row["CorrectiveAction"].ToString());
                 taxTable.Add("AdditionalAction", row["AdditionalAction"].ToString());
                 taxTable.Add("DateAchieved", row["DateAch"].ToString());
              }
              else
              {
                 taxTable.Add("id", row["ID"].ToString());
                 taxTable.Add("incidentNo", row["IncidentID"].ToString());
              }


              jsonString = JSON.JsonEncode(taxTable);

              tblJson[ncount] = jsonString;
              ncount = ncount + 1;
           }

           returnString = header + String.Join(",", tblJson) + trailer;
        }
        else
        {
           string date = GetCurrentDate();
           returnString = "{\"identifier\":\"id\", \"label\":\"incidentNo\", \"items\":[{\"id\": \"0\", \"incidentNo\": \"0\", \"DateApplied\": \"" + date + "\", \"IssueDescription\": \"\", \"CorrectiveAction\": \"\", \"AdditionalAction\": \"\", \"DateAchieved\": \"" + date + "\"}]}";
        }

        dt.Dispose();
        return returnString;
    }


    public string GetDoctor(string type, string query)
    {
       string sql, header, trailer;

      if (query != null)
       {
          sql = this.GetComboSearchType(type, query);
          header = "{\"identifier\":\"id\", \"label\":\"incidentNo\", \"items\":[";
          trailer = "]}";
       }
       else
       {
          sql = this.GetSearchType(type);
          header = "[";
          trailer = "]";
       }

        DataTable dt = new DataTable();
        SqlCommand command = new SqlCommand(sql, conn);
        SqlDataAdapter adapter = new SqlDataAdapter(command);
        adapter.Fill(dt);
        conn.Close();
        
        string[] tblJson = null;
        tblJson = new string[dt.Rows.Count];
        string returnString = "";
        string jsonString = null;

        if (dt.Rows.Count != 0)
        {
           int ncount = 0;
           foreach (DataRow row in dt.Rows)
           {
              Hashtable taxTable = new Hashtable();
              if (query != null)
              {
                 taxTable.Add("id", row["ID"].ToString());
                 taxTable.Add("incidentNo", row["IncidentID"].ToString());
                 taxTable.Add("Name", row["Name"].ToString());
                 taxTable.Add("Address", row["Address"].ToString());
                 taxTable.Add("PracticeName", row["PracticeName"].ToString());
                 taxTable.Add("Town", row["Town"].ToString());
                 taxTable.Add("Zip", row["Zip"].ToString());
                 taxTable.Add("Phone", row["Phone"].ToString());
              }
              else
              {
                 taxTable.Add("id", row["ID"].ToString());
                 taxTable.Add("incidentNo", row["IncidentID"].ToString());
              }



              jsonString = JSON.JsonEncode(taxTable);

              tblJson[ncount] = jsonString;
              ncount = ncount + 1;
           }

           returnString = header + String.Join(",", tblJson) + trailer;
        }
        else
        {
           string date = GetCurrentDate();
           returnString = "{\"identifier\":\"id\", \"label\":\"incidentNo\", \"items\":[{\"id\": \"0\", \"incidentNo\": \"0\", \"Name\": \"\", \"Address\": \"\", \"PracticeName\": \" \", \"Town\": \"\", \"Zip\": \"\", \"Phone\": \"\"}]}";
        }

        dt.Dispose();
        return returnString;
    }


    public string EquipmentDistribution(string type, string query)
    {
       string sql, header, trailer;

      if (query != null)
       {
          sql = this.GetComboSearchType(type, query);
          header = "{\"identifier\":\"id\", \"label\":\"incidentNo\", \"items\":[";
          trailer = "]}";
       }
       else
       {
          sql = this.GetSearchType(type);
          header = "[";
          trailer = "]";
       }

        DataTable dt = new DataTable();
        SqlCommand command = new SqlCommand(sql, conn);
        SqlDataAdapter adapter = new SqlDataAdapter(command);
        adapter.Fill(dt);
        conn.Close();
        
        string[] tblJson = null;
        tblJson = new string[dt.Rows.Count];
        string returnString = "";
        string jsonString = null;

        if (dt.Rows.Count != 0)
        {
           int ncount = 0;
           foreach (DataRow row in dt.Rows)
           {
              Hashtable taxTable = new Hashtable();
              if (query != null)
              {
                 taxTable.Add("id", row["ID"].ToString());
                 taxTable.Add("incidentNo", row["IncidentID"].ToString());
                 taxTable.Add("Description", row["Description"].ToString());
                 taxTable.Add("RequestDate", row["ReqDate"].ToString());
                 taxTable.Add("DeliveryDate", row["DelDate"].ToString());
                 taxTable.Add("ReceiptDate", row["RecDate"].ToString());
              }
              else
              {
                 taxTable.Add("id", row["ID"].ToString());
                 taxTable.Add("incidentNo", row["IncidentID"].ToString());
              }

              jsonString = JSON.JsonEncode(taxTable);

              tblJson[ncount] = jsonString;
              ncount = ncount + 1;
           }

           returnString = header + String.Join(",", tblJson) + trailer;
        }
        else
        {
           string date = GetCurrentDate();
           returnString = "{\"identifier\":\"id\", \"label\":\"incidentNo\", \"items\":[{\"id\": \"0\", \"incidentNo\": \"0\", \"Description\": \"\", \"RequestDate\": \"" + date + "\",\"DeliveryDate\": \"" + date + "\",\"ReceiptDate\": \"" + date + "\"}]}";
        }

        dt.Dispose();
        return returnString;
    }

   public string Insurance(string type, string query)
    {
       string sql, header, trailer;

      if (query != null)
       {
          sql = this.GetComboSearchType(type, query);
          header = "{\"identifier\":\"id\", \"label\":\"incidentNo\", \"items\":[";
          trailer = "]}";
       }
       else
       {
          sql = this.GetSearchType(type);
          header = "[";
          trailer = "]";
       }

        DataTable dt = new DataTable();
        SqlCommand command = new SqlCommand(sql, conn);
        SqlDataAdapter adapter = new SqlDataAdapter(command);
        adapter.Fill(dt);
        conn.Close();
        
        string[] tblJson = null;
        tblJson = new string[dt.Rows.Count];
        string returnString = "";
        string jsonString = null;

        if (dt.Rows.Count != 0)
        {
           int ncount = 0;
           foreach (DataRow row in dt.Rows)
           {
              Hashtable taxTable = new Hashtable();
              if (query != null)
              {
                 taxTable.Add("id", row["ID"].ToString());
                 taxTable.Add("incidentNo", row["IncidentID"].ToString());
                 taxTable.Add("PolicyID", row["PolicyID"].ToString());
                 taxTable.Add("Description", row["Description"].ToString());
                 taxTable.Add("CoverageType", row["CoverageType"].ToString());
                 taxTable.Add("PremiumCost", row["PremiumCost"].ToString());
                 taxTable.Add("DeductibleLevel", row["DeductibleLevel"].ToString());
                 taxTable.Add("Year", row["Year"].ToString());
              }
              else
              {
                 taxTable.Add("id", row["ID"].ToString());
                 taxTable.Add("incidentNo", row["IncidentID"].ToString());
              }

              jsonString = JSON.JsonEncode(taxTable);

              tblJson[ncount] = jsonString;
              ncount = ncount + 1;
           }

           returnString = header + String.Join(",", tblJson) + trailer;
        }
        else
        {
           string date = GetCurrentDate();
           returnString = "{\"identifier\":\"id\", \"label\":\"incidentNo\", \"items\":[{\"id\": \"0\", \"incidentNo\": \"0\", \"incidentNo\": \"0\", \"PolicyID\": \" \", \"Description\": \"\", \"CoverageType\": \" \", \"PremiumCost\": \"\", \"DeductibleLevel\": \"\", \"Year\": \"\"}]}";
        }

        dt.Dispose();
        return returnString;
    }

  public string Investigation(string type, string query)
    {
       string sql, header, trailer;

      if (query != null)
       {
          sql = this.GetComboSearchType(type, query);
          header = "{\"identifier\":\"id\", \"label\":\"Date\", \"items\":[";
          trailer = "]}";
       }
       else
       {
          sql = this.GetSearchType(type);
          header = "[";
          trailer = "]";
       }

        DataTable dt = new DataTable();
        SqlCommand command = new SqlCommand(sql, conn);
        SqlDataAdapter adapter = new SqlDataAdapter(command);
        adapter.Fill(dt);
        conn.Close();
        
        string[] tblJson = null;
        tblJson = new string[dt.Rows.Count];
        string returnString = "";
        string jsonString = null;

        if (dt.Rows.Count != 0)
        {
           int ncount = 0;
           foreach (DataRow row in dt.Rows)
           {
              Hashtable taxTable = new Hashtable();
              if (query != null)
              {
                 taxTable.Add("id", row["ID"].ToString());
                 taxTable.Add("incidentNo", row["IncidentID"].ToString());
                 taxTable.Add("Date", row["InitDate"].ToString());
                 taxTable.Add("FollowUp", row["FollowDate"].ToString());
                 taxTable.Add("Findings", row["Findings"].ToString());
                 taxTable.Add("Recommendations", row["Recommendations"].ToString());
              }
              else
              {
                 taxTable.Add("id", row["ID"].ToString());
                 taxTable.Add("incidentNo", row["IncidentID"].ToString());
              }

              jsonString = JSON.JsonEncode(taxTable);

              tblJson[ncount] = jsonString;
              ncount = ncount + 1;
           }

           returnString = header + String.Join(",", tblJson) + trailer;
        }
        else
        {
           string date = GetCurrentDate();
           returnString = "{\"identifier\":\"id\", \"label\":\"Date\", \"items\":[{\"id\": \"0\", \"incidentNo\": \"0\", \"Date\": \"" + date + "\", \"FollowUp\": \"" + date + "\", \"Findings\": \"\", \"Recommendations\": \"\"}]}";
        }

        dt.Dispose();
        return returnString;
    }

   public string StandardPPE(string type, string query)
    {
       string sql, header, trailer;

      if (query != null)
       {
          sql = this.GetComboSearchType(type, query);
          header = "{\"identifier\":\"id\", \"label\":\"EQID\", \"items\":[";
          trailer = "]}";
       }
       else
       {
          sql = this.GetSearchType(type);
          header = "[";
          trailer = "]";
       }

        DataTable dt = new DataTable();
        SqlCommand command = new SqlCommand(sql, conn);
        SqlDataAdapter adapter = new SqlDataAdapter(command);
        adapter.Fill(dt);
        conn.Close();
        
        string[] tblJson = null;
        tblJson = new string[dt.Rows.Count];
        string returnString = "";
        string jsonString = null;

        if (dt.Rows.Count != 0)
        {
           int ncount = 0;
           foreach (DataRow row in dt.Rows)
           {
              Hashtable taxTable = new Hashtable();

              taxTable.Add("id", row["ID"].ToString());
              taxTable.Add("incidentNo", row["IncidentID"].ToString());
              taxTable.Add("EQID", row["EQID"].ToString());
              taxTable.Add("Cost", row["Cost"].ToString());
              taxTable.Add("Description", row["Description"].ToString());
              taxTable.Add("Vendor", row["Vendor"].ToString());

              jsonString = JSON.JsonEncode(taxTable);

              tblJson[ncount] = jsonString;
              ncount = ncount + 1;
           }

           returnString = header + String.Join(",", tblJson) + trailer;
        }
        else
        {
           string date = GetCurrentDate();
           returnString = "{\"identifier\":\"id\", \"label\":\"EQID\", \"items\":[{\"id\": \"0\", \"incidentNo\": \"0\", \"EQID\": \" \", \"Cost\": \"\", \"Description\": \"\", \"Vendor\": \"\"}]}";
        }

        dt.Dispose();
        return returnString;
    }

  public string Training(string type, string query)
    {
       string sql, header, trailer;

      if (query != null)
       {
          sql = this.GetComboSearchType(type, query);
          header = "{\"identifier\":\"id\", \"label\":\"Name\", \"items\":[";
          trailer = "]}";
       }
       else
       {
          sql = this.GetSearchType(type);
          header = "[";
          trailer = "]";
       }

        DataTable dt = new DataTable();
        SqlCommand command = new SqlCommand(sql, conn);
        SqlDataAdapter adapter = new SqlDataAdapter(command);
        adapter.Fill(dt);
        conn.Close();
        
        string[] tblJson = null;
        tblJson = new string[dt.Rows.Count];
        string returnString = "";
        string jsonString = null;

        if (dt.Rows.Count != 0)
        {
           int ncount = 0;
           foreach (DataRow row in dt.Rows)
           {
              Hashtable taxTable = new Hashtable();
              if (query != null)
              {
                 taxTable.Add("id", row["ID"].ToString());
                 taxTable.Add("incidentNo", row["IncidentID"].ToString());
                 taxTable.Add("Name", row["Name"].ToString());
                 taxTable.Add("Date", row["TrainingDate"].ToString());
                 taxTable.Add("Time", row["Time"].ToString());
                 taxTable.Add("Location", row["Location"].ToString());
                 taxTable.Add("Trainer", row["Trainer"].ToString());
              }
              else
              {
                 taxTable.Add("id", row["ID"].ToString());
                 taxTable.Add("incidentNo", row["IncidentID"].ToString());
              }

              jsonString = JSON.JsonEncode(taxTable);

              tblJson[ncount] = jsonString;
              ncount = ncount + 1;
           }

           returnString = header + String.Join(",", tblJson) + trailer;
        }
        else
        {
           string date = GetCurrentDate();
           returnString = "{\"identifier\":\"id\", \"label\":\"Name\", \"items\":[{\"id\": \"0\", \"incidentNo\": \"0\", \"Name\": \" \", \"Date\": \"" + date + "\", \"Time\": \"\", \"Location\": \"\", \"Trainer\": \"\"}]}";
        }

        dt.Dispose();
        return returnString;
    }

   public string Witness(string type, string query)
    {
       string sql, header, trailer;

      if (query != null)
       {
          sql = this.GetComboSearchType(type, query);
          header = "{\"identifier\":\"id\", \"label\":\"Name\", \"items\":[";
          trailer = "]}";
       }
       else
       {
          sql = this.GetSearchType(type);
          header = "[";
          trailer = "]";
       }

        DataTable dt = new DataTable();
        SqlCommand command = new SqlCommand(sql, conn);
        SqlDataAdapter adapter = new SqlDataAdapter(command);
        adapter.Fill(dt);
        conn.Close();
        
        string[] tblJson = null;
        tblJson = new string[dt.Rows.Count];
        string returnString = "";
        string jsonString = null;

        if (dt.Rows.Count != 0)
        {
           int ncount = 0;
           foreach (DataRow row in dt.Rows)
           {
              Hashtable taxTable = new Hashtable();
              if (query != null)
              {
                 taxTable.Add("id", row["ID"].ToString());
                 taxTable.Add("incidentNo", row["IncidentID"].ToString());
                 taxTable.Add("Name", row["Name"].ToString());
                 taxTable.Add("Address", row["Address"].ToString());
                 taxTable.Add("Phone", row["Phone"].ToString());
                 taxTable.Add("Zip", row["Zip"].ToString());
                 taxTable.Add("Email", row["Email"].ToString());
              }
              else
              {
                 taxTable.Add("id", row["ID"].ToString());
                 taxTable.Add("incidentNo", row["IncidentID"].ToString());
              }

              jsonString = JSON.JsonEncode(taxTable);

              tblJson[ncount] = jsonString;
              ncount = ncount + 1;
           }

           returnString = header + String.Join(",", tblJson) + trailer;
        }
        else
        {
           string date = GetCurrentDate();
           returnString = "{\"identifier\":\"id\", \"label\":\"Name\", \"items\":[{\"id\": \"0\", \"incidentNo\": \"0\", \"Name\": \" \", \"Address\": \"\", \"Phone\": \"\", \"Zip\": \"\", \"Email\": \"\" }]}";
        }

        dt.Dispose();
        return returnString;
    }

}
 