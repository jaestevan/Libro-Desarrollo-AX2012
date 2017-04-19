using System;
using System.Collections.Generic;
using System.Security.Permissions;
using System.Data;
using Microsoft.Dynamics.Framework.Reports;
public partial class JAEEEjemploBusinessLogic
{
    [DataMethod(), PermissionSet(SecurityAction.Assert, Name = "FullTrust")]
    public static IEnumerable<DataRow> GetDataTable()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add(new DataColumn("C�digo", Type.GetType("System.String")));
        dt.Columns.Add(new DataColumn("Descripci�n", Type.GetType("System.String")));

        DataRow row = dt.NewRow();
        row.ItemArray = new object[] { "C1", "Prueba 1"};
        yield return row;

        row = dt.NewRow();
        row.ItemArray = new object[] { "C2", "Prueba 2" };
        yield return row;
    }


}
