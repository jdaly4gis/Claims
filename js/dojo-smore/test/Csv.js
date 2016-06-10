define(["doh", "dojo/request", "dojo/promise/all", "dojo-smore/Csv"],
function(doh, request, all, Csv) {
	var xhrBase = require.toUrl("dojo-smore/test/data"),
		csvs = {}, // holds retrieved raw CSV data
		stores = window.stores = {}, // holds stores created after CSV data is retrieved
		readyPromise = all([
			// Load CSV data.  The referenced data has various inconsistencies
			// throughout on purpose, to test that the implementation responds
			// properly to things like extra whitespace, blank values/lines, etc.
			request(xhrBase + "/noquote.csv").then(function(data) {
				csvs.noQuote = data;
				stores.noQuoteNoHeader = new Csv({
					data: data,
					fieldNames: ["id", "last", "first", "born", "died"]
				});
				stores.noQuoteWithHeader = new Csv({
					data: data
					// No fieldNames; first row will be treated as header row.
				});
				stores.noQuoteTrim = new Csv({
					data: data,
					trim: true
				});
				stores.noQuoteNoHeaderAutoId = new Csv({
					data: data,
					fieldNames: ["id", "last", "first", "born", "died"],
					trim: true,
					idProperty: "" // Tell store to auto-generate IDs
				});
				stores.noQuoteAutoId = new Csv({
					data: data,
					trim: true,
					idProperty: "" // Tell store to auto-generate IDs
				});
			}),
			request(xhrBase + "/quote.csv").then(function(data) {
				csvs.quote = data;
				stores.quoteNoHeader = new Csv({
					data: data,
					fieldNames: ["id", "name", "quote"]
				});
				stores.quoteWithHeader = new Csv({
					data: data
					// No fieldNames; first row will be treated as header row.
				});
			}),
			request(xhrBase + "/contributors.csv").then(function(data) {
				csvs.contributors = data;
				stores.contributors = new Csv({
					data: data
				});
			})
		]);
	
	readyPromise.then(function() {
		doh.register("Csv tests", [
			function noQuote(t) {
				var noHeader = stores.noQuoteNoHeader,
					withHeader = stores.noQuoteWithHeader,
					trim = stores.noQuoteTrim,
					item, trimmedItem;
				
				// Test header vs. no header...
				t.is(4, noHeader.data.length,
					"Store with fieldNames should have 4 items.");
				t.is(3, withHeader.data.length,
					"Store using header row should have 3 items.");
				t.t(noHeader.get("id"),
					"First line should be considered an item when fieldNames are set");
				t.t(!!withHeader.get("1").first,
					"Field names picked up from header row should be trimmed.");
				t.is(noHeader.get("1").last, withHeader.get("1").last,
					"Item with id of 1 should have the same data in both stores.");
				
				// Test trim vs. no trim...
				item = withHeader.get("2");
				trimmedItem = trim.get("2");
				t.is(" Nikola ", item.first,
					"Leading/trailing spaces should be preserved if trim is false.");
				t.is("Nikola", trimmedItem.first,
					"Leading/trailing spaces should be trimmed if trim is true.");
				t.is(" ", item.middle,
					"Strings containing only whitespace should remain intact if trim is false.");
				t.is("", trimmedItem.middle,
					"Strings containing only whitespace should be empty if trim is true.");
				
				// Test data integrity...
				item = withHeader.get("1");
				t.t(item.middle === "", "Test blank value.");
				t.is("1879-03-14", item.born, "Test value after blank value.");
				t.t(withHeader.get("3").died === "", "Test blank value at end of line.");
			},
			function quote(t) {
				var noHeader = stores.quoteNoHeader,
					withHeader = stores.quoteWithHeader;
				
				// Test header vs. no header...
				t.is(5, noHeader.data.length,
					"Store with fieldNames should have 5 items.");
				t.is(4, withHeader.data.length,
					"Store using header row should have 4 items.");
				t.t(noHeader.get("id"),
					"First line should be considered an item when fieldNames are set");
				t.is(noHeader.get("1").name, withHeader.get("1").name,
					"Item with id of 1 should have the same data in both stores.");
				
				// Test data integrity...
				t.is('""', withHeader.get("3").quote,
					"Value consisting of two double-quotes should pick up properly.");
				t.is(" S, P, ...ace! ", withHeader.get("4").quote,
					"Leading/trailing spaces within quotes should be preserved.");
				t.t(/^Then[\s\S]*"Nevermore\."$/.test(withHeader.get("2").quote),
					"Multiline value should remain intact.");
				t.t(/smiling,\r\n/.test(withHeader.get("2").quote),
					"Multiline value should use same newline format as input.");
			},
			function autoId(t) {
				var noQuoteAutoId = stores.noQuoteAutoId,
					noQuoteNoHeaderAutoId = stores.noQuoteNoHeaderAutoId;
				console.log(noQuoteAutoId.data, noQuoteNoHeaderAutoId.data);
				t.is(noQuoteNoHeaderAutoId.get("1").last, "last",
					"Item 1 should be the first line");
				t.is(noQuoteNoHeaderAutoId.get("2").last, "Hawking",
					"Item 2 should be the second line");
				t.is(noQuoteNoHeaderAutoId.get("3").last, "Einstein",
					"Item 3 should be the third line");
				t.is(noQuoteNoHeaderAutoId.get("4").last, "Tesla",
					"Item 4 should be the fourth line");
				
				t.is(noQuoteAutoId.get("1").last, "Hawking",
					"Item 1 should be the first non-header line");
				t.is(noQuoteAutoId.get("2").last, "Einstein",
					"Item 2 should be the second non-header line");
				t.is(noQuoteAutoId.get("3").last, "Tesla",
					"Item 3 should be the third non-header line");
			},
			function importExport(t) {
				t.is(csvs.contributors, stores.contributors.toCsv(),
					"toCsv() should generate data matching original if it is well-formed");
			}
		]);
	});
});