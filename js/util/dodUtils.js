//************ Utils for Dod functionality and testing ****************//

    function random(start, end){
		//include start but not end. e.g. 1-10, 1 is possible but not 10.
		return Math.floor(Math.random()*(end-start)) + start;
	}

	function getRowDetails(count,end,grid,rowId) {

       var hidden = ['id', 'ID', 'GOV_TAX_MAP', 'DATE', 'DATE_OF_SALE', 'LIBER', 'PAGE', 'STATUS', 'DEED_TYPE', 'CONS_AMOUNT', 'T_MESSAGE'];
       var row = grid.store.get(rowId);
	    var rowElem = [];

	    for (var k in row) {
	        if (dojo.indexOf(hidden, k) != -1) {
	            continue;
	        }
               
	        if (row[k] !== "") {
	            rowElem.push(k + ' = ' + row[k]);
	        }
	    }

	    rowElem.sort();
	    var details = rowElem.join("<hr>");

	    return (details);
	}

	function setContent(grid,rowId,node) {

	    node.innerHTML = '<div id="details" style="color: #777; padding: 5px">'
        + getRowDetails(20, 140, grid, rowId) + '</div>';
	 }

