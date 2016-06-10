//************ Utils for showing info from Govern and Suffolk County Files ****************//

function getQueryType(query, taxmap) {
   var rowElem = [];
   var store = new dojox.data.QueryReadStore({ url: 'getDeedsJSON.ashx' });
   store.fetch({ serverQuery: { q: taxmap, db: "govern", queryType: query },

      onError: function (err) {
         //alert("Error on retrieving query");
         dojo.byId("govQuery").innerHTML = "No data was returned by the query";
      },

      onComplete: function (items, request) {
         for (var i = 0, length = items.length; i < length; i++) {
            for (var ind in items[i]) {
               if (ind === 'r') continue;
               for (var vals in items[i][ind]) {
                  if (items[i][ind][vals] !== "") {
                     if (vals === "id") continue;
                     var elem = vals + " = " + items[i][ind][vals];
                     rowElem.push(elem);
                  }
               }
            }
         }

         dojo.byId("govQuery").innerHTML = rowElem.join('<br>');
      }
   })
}

function getInfo(query) {

   var rowElem = [];
   var row = grid.menu.context.row ? grid.menu.context.row : grid.menu.context.cell ? grid.menu.context.cell.row : null;
   var tmCell = grid.menu.context.grid.cell(row.id, 0);
   var layout = new dijit.layout.BorderContainer({
      design: "headline",
      style: "width: 600px; height: 600px; background-color: #ccffff;"
   });

   var centerPane = new dijit.layout.ContentPane({
      region: "center",
      style: "font-face='Verdana'; background-color: #f9f6f4;",
      content: "<div id='govQuery'><table id='results'>" + getQueryType('initQuery', tmCell.data()) + "</table></div>"
   });

   var actionPane = new dijit.layout.ContentPane({
      region: "bottom",
      style: "height: 30px; background-color: #f9f6f4;"
   });

   var parcelSelect = new dijit.form.Button({
      label: "Parcel Info",
      onClick: function () {
         getQueryType('initQuery', tmCell.data());
      }
   }).placeAt(actionPane.containerNode);

   var ownerSelect = new dijit.form.Button({
      label: "Owner Info",
      onClick: function () {
         getQueryType('ownerQuery', tmCell.data());
      }
   }).placeAt(actionPane.containerNode);

   var mailingSelect = new dijit.form.Button({
      label: "Mailing Index",
      onClick: function () {
         getQueryType('mailingIndexQuery', tmCell.data());
      }
   }).placeAt(actionPane.containerNode);
   
   layout.addChild(centerPane);
   layout.addChild(actionPane);
   layout.startup();

   var dialog = new dijit.Dialog({
      hide: function () { dialog.destroy(); },
      title: "Govern Results: " + tmCell.data(),
      content: layout
   });


   dialog.containerNode.style.backgroundColor = "#ccffff";
   dialog.startup();
   dialog.show();

   }


  function showInfo(query)
  {
     getInfo(query);
  }
