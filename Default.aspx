<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default" %>
    <html>
    <head>
    <meta http-equiv="X-UA-Compatible" content="IE=8, IE=9, IE=10, IE=11" />
    <title>Claims Functions</title>
   <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    
          <link rel="Stylesheet" type="text/css" href="js/dojo/resources/dojo.css" />
          <link rel="Stylesheet" type="text/css" href="js/dijit/themes/claro/claro.css" />
          <link rel="Stylesheet" type="text/css" href="js/dijit/themes/claro/document.css" />
          <link rel="Stylesheet" type="text/css" href="js/gridx/resources/claro/Gridx.css" />
          <link rel="Stylesheet" type="text/css" href="js/dojox/layout/resources/ExpandoPane.css" />
          
          <style type="text/css">
           body
            {
              width: 100%;
              height: 100%;
              background-color:#ccffff;
            }
          
          	.gridx {
              position: absolute;
              top:0px;
              left:0px;
              height: 700px;
              width:  1600px;
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

          /* pre-loader specific stuff to prevent unsightly flash of unstyled content */
          #loader {
           padding: 0;
           margin: 0;
           position: absolute;
           top: 0;
           left: 0;
           width: 100%;
           height: 100%;
           background: #ededed;
           z-index: 999;
           vertical-align: middle;
           }

           #loaderInner {
            padding: 5px;
            position: relative;
            left: 0;
            top: 0;
            width: 175px;
            background: #3c3;
            color: #fff;
          }
          </style>

          <script src = "js/util/dodUtils.js"></script>
          <script src = "js/util/selectInfo.js"></script>
          <script src = "js/util/loader.js"></script>

          <script data-dojo-config="async:1, isDebug: true, parseOnLoad:false" src="js/dojo/dojo.js"></script>
          <script>
             require([
             'dojo/dom',
             'dojo/_base/fx',
             'dojo/parser',
             'dojo/request',
             'dojo/store/Memory',
             'dojox/widget/Standby',
             'dojox/layout/ExpandoPane',
             'dojox/layout/ContentPane',
             'dojox/data/QueryReadStore',
             'dijit/form/NumberTextBox',
             'dijit/form/DateTextBox',
             'dijit/form/TextArea',
             'dijit/form/CurrencyTextBox',
             'dijit/layout/BorderContainer',
             'dijit/layout/ContentPane',
             'dijit/layout/AccordionContainer',
             'dijit/layout/TabContainer'
             ], function (dom, baseFx, parser) {

                parser.parse(dom.byId('container')).then(function () {
                   dom.byId('loaderInner').innerHTML += " done.";
                   setTimeout(function hideLoader() {
                      baseFx.fadeOut({
                         node: 'loader',
                         duration: 500,
                         onEnd: function (n) {
                            n.style.display = "none";
                         }
                      }).play();
                   }, 250);
                })
             });
		    </script>
    </head>
    <body class="claro">
   	<div id="loader">
   		<div id="loaderInner" style="direction:ltr;">Loading ...</div>
   	</div>
       <div data-dojo-type="dijit/layout/BorderContainer" style="width:100%; height:100%;">
          <div data-dojo-type="dojox/layout/ExpandoPane" id="leftPane" region="left" splitter="true" style="width:30%;height:100%">
             <div data-dojo-type="dijit/layout/AccordionContainer" data-dojo-props="region:'leading', splitter:true, minSize:20, style:'height:100%; width:100%;'" id="leftAccordion"> 
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
                            <div data-dojo-type="dojox/layout/ContentPane" data-dojo-props="region:'top', title:'Witness'"  href="addWitness.htm" style="margin:0px;background-color:#ffffee"></div>
             </div>
             <div data-dojo-type="dijit/layout/ContentPane" data-dojo-props="region:'trailing'">Trailing pane</div>
             <div data-dojo-type="dijit/layout/ContentPane" data-dojo-props="region:'bottom'">Bottom pane</div>
</div>
  </body>
 </html>
                  