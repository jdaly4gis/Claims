using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for AdminFunctions
/// </summary>
abstract public class AdminFunctions
{
	public AdminFunctions()
	{
		//
		// TODO: Add constructor logic here
		//
	}

   abstract public void Add();
   abstract public void Add(string name);
   abstract public SqlCommand Update();
   abstract public SqlCommand  Update(int id, string name);
   abstract public SqlCommand Delete(int id);

}