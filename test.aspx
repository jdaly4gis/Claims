<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default" %>

<!DOCTYPE html>
<html>
<head>
    <link rel="Stylesheet" type="text/css" href="js/dojo/resources/dojo.css" />
    <link rel="Stylesheet" type="text/css" href="js/dijit/themes/claro/claro.css" />
    <link rel="Stylesheet" type="text/css" href="js/dijit/themes/claro/document.css" />
    <link rel="Stylesheet" type="text/css" href="js/gridx/resources/claro/Gridx.css" />
    <link rel="Stylesheet" type="text/css" href="js/dojox/layout/resources/ExpandoPane.css" />

	<style type="text/css">
     html, body {
         width: 100%;
         height: 100%;
         margin: 0;
   }

#borderContainer {
  width: 100%;
  height: 100%;
}
	</style>
	<script>dojoConfig = {parseOnLoad: true}</script>
	<script src='js/dojo/dojo.js'></script>
	
	<script>
		dojo.require("dijit.layout.ContentPane");
		dojo.require("dijit.layout.BorderContainer");
		dojo.require("dojox.layout.ExpandoPane");
	</script>
</head>
<body class="claro">
       <div data-dojo-type="dijit/layout/BorderContainer" style="width:100%; height:100%;">
          <div data-dojo-type="dojox/layout/ExpandoPane" id="leftPane" region="left" splitter="true" style="width:30%;height:100%">
             <div data-dojo-type="dijit/layout/AccordionContainer"data-dojo-props="region:'leading', splitter:true, minSize:20, style:'height:100%; width:100%;'" id="leftAccordion"> 
                            <!--div data-dojo-type="dojox/layout/ContentPane" data-dojo-props="region:'top', title:'Incident'" href="addIncident.htm" style="margin:0px;background-color:#ffffee"></div> 
                            <div data-dojo-type="dojox/layout/ContentPane" data-dojo-props="region:'top', title:'Contact Log'" href="addContactLog.htm" style="margin:0px;background-color:#ffffee"></div>
                            <div data-dojo-type="dojox/layout/ContentPane" data-dojo-props="region:'top', title:'IN Service Log'" href="addINService.htm" style="margin:0px;background-color:#ffffee"></div>
                            <div data-dojo-type="dojox/layout/ContentPane" data-dojo-props="region:'top', title:'Corrective Action'" href="addCorrectiveAction.htm" style="margin:0px;background-color:#ffffee"></div>
                            <div data-dojo-type="dojox/layout/ContentPane" data-dojo-props="region:'top', title:'Doctor'" href="addDoctor.htm" style="margin:0px;background-color:#ffffee"></div>
                            <div data-dojo-type="dojox/layout/ContentPane" data-dojo-props="region:'top', title:'Equipment'" href="addEquipment.htm" style="margin:0px;background-color:#ffffee"></div>
                            <div data-dojo-type="dojox/layout/ContentPane" data-dojo-props="region:'top', title:'Insurance'" href="addInsurance.htm" style="margin:0px;background-color:#ffffee"></div>
                            <div data-dojo-type="dojox/layout/ContentPane" data-dojo-props="region:'top', title:'Investigation'" href="addInvestigation.htm" style="margin:0px;background-color:#ffffee"></div>
                            <div data-dojo-type="dojox/layout/ContentPane" data-dojo-props="region:'top', title:'Standard PPE'" href="addStandardPPE.htm" style="margin:0px;background-color:#ffffee"></div>
                            <div data-dojo-type="dojox/layout/ContentPane" data-dojo-props="region:'top', title:'Training'" href="addTraining.htm" style="margin:0px;background-color:#ffffee"></div>
                            <div data-dojo-type="dojox/layout/ContentPane" data-dojo-props="region:'top', title:'Witness'" href="addWitness.htm" style="margin:0px;background-color:#ffffee"></div-->
             </div>
</div> 
             <div data-dojo-type="dijit/layout/TabContainer" data-dojo-props="region:'center'">
                           <div data-dojo-type="dojox/layout/ContentPane" data-dojo-props="region:'top', title:'Incident'" href="addIncident.htm" style="margin:0px;background-color:#ffffee"></div> 
                            <div data-dojo-type="dojox/layout/ContentPane" data-dojo-props="region:'top', title:'Contact Log'" href="addContactLog.htm" style="margin:0px;background-color:#ffffee"></div>
                            <div data-dojo-type="dojox/layout/ContentPane" data-dojo-props="region:'top', title:'IN Service Log'" href="addINService.htm" style="margin:0px;background-color:#ffffee"></div>
                            <div data-dojo-type="dojox/layout/ContentPane" data-dojo-props="region:'top', title:'Corrective Action'" href="addCorrectiveAction.htm" style="margin:0px;background-color:#ffffee"></div>
                            <div data-dojo-type="dojox/layout/ContentPane" data-dojo-props="region:'top', title:'Doctor'" href="addDoctor.htm" style="margin:0px;background-color:#ffffee"></div>
                            <div data-dojo-type="dojox/layout/ContentPane" data-dojo-props="region:'top', title:'Equipment'" href="addEquipment.htm" style="margin:0px;background-color:#ffffee"></div>
                            <div data-dojo-type="dojox/layout/ContentPane" data-dojo-props="region:'top', title:'Insurance'" href="addInsurance.htm" style="margin:0px;background-color:#ffffee"></div>
                            <div data-dojo-type="dojox/layout/ContentPane" data-dojo-props="region:'top', title:'Investigation'" href="addInvestigation.htm" style="margin:0px;background-color:#ffffee"></div>
                            <div data-dojo-type="dojox/layout/ContentPane" data-dojo-props="region:'top', title:'Standard PPE'" href="addStandardPPE.htm" style="margin:0px;background-color:#ffffee"></div>
                            <div data-dojo-type="dojox/layout/ContentPane" data-dojo-props="region:'top', title:'Training'" href="addTraining.htm" style="margin:0px;background-color:#ffffee"></div>
                            <div data-dojo-type="dojox/layout/ContentPane" data-dojo-props="region:'top', title:'Witness'" href="addWitness.htm" style="margin:0px;background-color:#ffffee"></div>
             </div>
             <div data-dojo-type="dijit/layout/ContentPane" data-dojo-props="region:'trailing'">Trailing pane</div>
             <div data-dojo-type="dijit/layout/ContentPane" data-dojo-props="region:'bottom'">Bottom pane</div>
</div>
</body>
</html>