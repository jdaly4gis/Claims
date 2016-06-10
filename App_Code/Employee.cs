using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Employee
/// </summary>
public class Employee:AdminFunctions
{
   private string _empID;
   private string _deptID;
   private string _supeID;
   private string _empClassID;
   private string _divCodeID;
   private string _title;
   private string _first;
   private string _middle;
   private string _last;
   private string _pareacode;
   private string _pareatelnum;
   private string _sareacode;
   private string _sareatelnum;
   private string _faxarea;
   private string _faxnum;
   private string _address;
   private string _city;
   private string _state;
   private string _zip;
   private string _email;
   private string _dob;
   private string _entrydate;
   private SqlCommand _query;
   private string _action;
   List<string>  _fields;
   private HttpContext _context;

   private string _result;


    public Employee()
	{
		//
		// TODO: Add constructor logic here
		//
	}

   public Employee(string empID)
	{
      this._empID = empID;
	}

	public Employee(string empID, string name)
	{

	}

	public Employee(HttpContext inVars)
	{

     List<string> _fields = new List<string>();

      this._deptID = inVars.Request["department"];
      this._supeID = inVars.Request["supervisor"];
      this._empID = inVars.Request["employee"];
      this._last = inVars.Request["last"];
      this._first = inVars.Request["first"];
      this._middle = inVars.Request["middle"];
      this._address = inVars.Request["address"];
      this._pareacode = inVars.Request["parea"];
      this._pareatelnum = inVars.Request["ptelnum"];
      this._sareacode = inVars.Request["sarea"];
      this._sareatelnum = inVars.Request["stelnum"];
      this._faxarea = inVars.Request["faxarea"];
      this._faxnum = inVars.Request["faxnum"];
      this._dob = inVars.Request["dob"];
      this._entrydate = inVars.Request["entrydate"];
      this._city = inVars.Request["city"];
      this._state = inVars.Request["state"];
      this._zip = inVars.Request["zip"];
      this._title = inVars.Request["title"];
      this._empClassID = inVars.Request["empclass"];
      this._divCodeID = inVars.Request["divcode"];
      this._email = inVars.Request["email"];
      this._action = inVars.Request["action"];
      this._context = inVars;


      if (this._action == "1")
      {
          Add();
      }
      
	}

    public string Result
    {
        get 
        {
            return _result;
        }

    }
   public int EmployeeID
   {
      get { return Convert.ToInt32(_empID); }
   }

   public string EmployeeName
   {
      get { return _last; }
   }

   public int EmployeeAction
   {
      get { return Convert.ToInt32(_action); }
   }

   public SqlCommand GetSQLQuery
   {
      get { return this._query; }
   }


   public override void Add()
   {

       ClaimsAccess access = new ClaimsAccess("claimsDB");
       SqlConnection login = (SqlConnection)access.GetLoginStatus;
       SqlCommand command  = new SqlCommand("dbo.EmployeeFunctions", login);

       if (login != null)
       {

           command.CommandType = CommandType.StoredProcedure;
           command.Parameters.Add("@status", SqlDbType.NVarChar, 1000).Value = "";
           command.Parameters.Add("@empID", SqlDbType.Int).Value = 1;
           command.Parameters.Add("@deptID", SqlDbType.Int).Value = 0; //this._deptID;
           command.Parameters.Add("@supeID", SqlDbType.Int).Value = 0; // this._supeID;
           command.Parameters.Add("@last", SqlDbType.NVarChar).Value = this._last;
           command.Parameters.Add("@first", SqlDbType.NVarChar).Value = this._first;
           command.Parameters.Add("@middle", SqlDbType.NVarChar).Value = this._middle;
           command.Parameters.Add("@parea", SqlDbType.NVarChar).Value = this._pareacode;
           command.Parameters.Add("@ptelnum", SqlDbType.NVarChar).Value = this._pareatelnum;
           command.Parameters.Add("@sarea", SqlDbType.NVarChar).Value = this._sareacode;
           command.Parameters.Add("@stelnum", SqlDbType.NVarChar).Value = this._sareatelnum;
           command.Parameters.Add("@faxarea", SqlDbType.NVarChar).Value = this._faxarea;
           command.Parameters.Add("@faxnum", SqlDbType.NVarChar).Value = this._faxnum;
           command.Parameters.Add("@address", SqlDbType.NVarChar).Value = this._address;
           command.Parameters.Add("@city", SqlDbType.NVarChar).Value = this._city;
           command.Parameters.Add("@state", SqlDbType.NVarChar).Value = "New York"; // this._state;
           command.Parameters.Add("@zip", SqlDbType.NVarChar).Value = this._zip;
           command.Parameters.Add("@title", SqlDbType.NVarChar).Value = "Title"; // this._title;
           command.Parameters.Add("@empclass", SqlDbType.Int).Value = 0; // this._empClassID;
           command.Parameters.Add("@divcode", SqlDbType.Int).Value = 0; // this._divCodeID;
           command.Parameters.Add("@email", SqlDbType.NVarChar).Value = this._email;
           command.Parameters.Add("@dob", SqlDbType.NVarChar).Value = this._dob;
           command.Parameters.Add("@action", SqlDbType.Int).Value = this._action;


           if (command.ExecuteNonQuery() == 1)
           {
               this._result = "The record has been added to the Employee table";
           }
           else
           {
               this._result = "There has been a problem entering the data";
           }
       }
   }

   public override void Add(string name)
   {
   }

   public override SqlCommand Update()
   {
/*
      return (@"UPDATE Employee SET  DepartmentID='"   + this._deptID + "'," +
                                    "SupervisorID='"  + this._supeID + "'," +
                                    "LastName='"  + this._last + "'," +
                                    "FirstName='" + this._first + "'," +
                                    "Middle='"   + this._middle + "'," +
                                    "Address='"   + this._address + "'," +
                                    "City='"   + this._city + "'," +
                                    "State='"   + this._state + "'," +
                                    "JobTitle='"   + this._title + "'," +
                                    "EmployeeClass='"   + this._empClassID + "'," +
                                    "DivisionCode='"   + this._divCodeID + "'," +
                                    "Email='"  + this._email + "'" +
                                    "WHERE ID ='" + this._empID + "'");
*/
       return null;
   }

   public override SqlCommand Update(int id, string name)
   {
      return (null);
   }

   public override SqlCommand Delete(int id)
   {
      return (null);
   }
}