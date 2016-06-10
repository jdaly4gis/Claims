    <%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default" %>

    <!DOCTYPE HTML">
    <html>
    <head>
    <meta http-equiv="X-UA-Compatible" content="IE=8, IE=9, IE=10, IE=11"/>
    <title>Claims Admin</title>
   <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    
          <link rel="Stylesheet" type="text/css" href="../js/dojo/resources/dojo.css" />
          <link rel="Stylesheet" type="text/css" href="../js/dijit/themes/claro/claro.css" />
          <link rel="Stylesheet" type="text/css" href="../js/dijit/themes/claro/document.css" />
          <link rel="Stylesheet" type="text/css" href="../js/gridx/resources/claro/Gridx.css" />
          
          <style type="text/css">

            body  {
              background-color:#ccffff;
            }
          
          	.gridx {
               height: 700px;
               width:  1600px;
               margin-left:200px;
               margin-right:200px;  
               margin-top:40px;                   
               border:2px solid;
               border-radius:6px;
            }

            #govQuery  {
               font-family:Verdana;
               font-size:12px;               
               font-weight:bold;                              
               color:#333366;
            }

            h1 {
              color:brown;
              font-family:Arial;
              font-size:36px;
              line-height:36px;
              text-align:center;
           }

           hr {
              border: 0;
              height: 0;
              border-top: 1px solid rgba(0, 0, 0, 0.1);
              border-bottom: 1px solid rgba(255, 255, 255, 0.3);
           }

           #details {
             font-size:8pt;
          }

           #config  {
 			    width: 300px; 
			    height: 400px; 
			    float: left;
          }

          </style>

          <script src = "../js/util/dodUtils.js"></script>
          <script src = "../js/util/selectInfo.js"></script>

          <script data-dojo-config="async:1, isDebug: true, parseOnLoad:true" src="../js/dojo/dojo.js"></script>
          <script>
             require([
             'dojo/dom',
             'dojo/parser',
             'dojo/request',
             'dojo/store/Memory',
             'dojox/widget/Standby',
             'dojox/data/QueryReadStore',
             'gridx/allModules',
             'dijit/registry',
             'dijit/layout/BorderContainer',
             'dojox/layout/ContentPane',
             'dijit/Dialog',
             'gridx/Grid',
             'gridx/core/model/cache/Sync',
             'gridx/core/model/cache/Async',
             'dijit/Menu',
             'dijit/MenuItem',
             'dijit/PopupMenuItem',
             'dijit/CheckedMenuItem',
             'dijit/MenuSeparator',
             'dijit/form/CheckBox',
             'dojo/domReady!'
             ], function (dom, parser, request, Memory, Standby, QueryReadStore, modules, registry) {
                var layout = new dijit.layout.BorderContainer({
                   design: "headline",
                   style: "width: 1000px; height: 700px; background-color: #ccffff;"
                });

                var centerPane = new dojox.layout.ContentPane({
                   region: "center",
                   style: "font-face='Verdana'; background-color: #f9f6f4;top:100px"
                });

                var actionPane = new dojox.layout.ContentPane({
                   region: "bottom",
                   style: "height: 30px; background-color: #f9f6f4;"
                });

                var deptSelect = new dijit.form.Button({
                   label: "Department",
                   onClick: function () {
                      centerPane.set('href', "forms/addDepartment.htm");
                   }
                }).placeAt(actionPane.containerNode);

                var supervisorSelect = new dijit.form.Button({
                   label: "Supervisor",
                   onClick: function () {
                      centerPane.set('href', "forms/addSupervisor.htm");
                   }
                }).placeAt(actionPane.containerNode);

                var employeeClass = new dijit.form.Button({
                   label: "Employee Class",
                   onClick: function () {
                      centerPane.set('href', "forms/addEmployeeClass.htm");
                   }
                }).placeAt(actionPane.containerNode);

                var divisionCode = new dijit.form.Button({
                   label: "Division Code",
                   onClick: function () {
                      centerPane.set('href', "forms/addDivisionCode.htm");
                   }
                }).placeAt(actionPane.containerNode);

                var employee = new dijit.form.Button({
                   label: "Employee",
                   onClick: function () {
                      centerPane.set('href', "forms/addEmployee.htm");
                   }
                }).placeAt(actionPane.containerNode);

                /*
                var incident = new dijit.form.Button({
                   label: "Incident",
                   onClick: function () {
                      centerPane.set('href', "forms/addIncident.htm");
                   }
                }).placeAt(actionPane.containerNode);
                */
                var ctype = new dijit.form.Button({
                   label: "Claim Type",
                   onClick: function () {
                      centerPane.set('href', "forms/addClaimType.htm");
                   }
                }).placeAt(actionPane.containerNode);
                /*
                var INtype = new dijit.form.Button({
                   label: "IN Service",
                   onClick: function () {
                      centerPane.set('href', "forms/addINService.htm");
                   }
                }).placeAt(actionPane.containerNode);

                var CLogtype = new dijit.form.Button({
                   label: "Contact Log",
                   onClick: function () {
                      centerPane.set('href', "forms/addContactLog.htm");
                   }
                }).placeAt(actionPane.containerNode);
                */

                layout.addChild(centerPane);
                layout.addChild(actionPane);
                layout.startup();

                var dialog = new dijit.Dialog({
                   hide: function () { dialog.destroy(); },
                   title: "Administration Panel",
                   content: layout
                });

                centerPane.set('href', "forms/addDepartment.htm");

                dialog.containerNode.style.backgroundColor = "#ccffff";
                dialog.startup();
                dialog.show();
             });

		    </script>
    </head>
    <body class="claro">
	<table border='0' cellspacing='0' cellpadding='0'><tr>
		<td>
			<div id='adminContainer'></div>
		</td>
		<td id='ctrlPane'></td>
	</tr>
   </table>
    </body>
    </html>                 