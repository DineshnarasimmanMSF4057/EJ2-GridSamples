let userData=
[
  {
    "OrderID": "din",
    "CustomerID": "VINET",
    "OrderDate": "1996-07-04T00:00:00.000Z",
    "ShippedDate": "1996-07-16T00:00:00.000Z",
    "Freight": 32.38,
    "ShipName": "Vins et alcools Chevalier",
    "ShipAddress": "59 rue de l'Abbaye",
    "ShipCity": "Reims",
    "ShipRegion": null,
    "ShipCountry": "France"
  },
  {
    "OrderID": "min",
    "CustomerID": "TOMSP",
    "OrderDate": "1996-07-05T00:00:00.000Z",
    "ShippedDate": "1996-07-10T00:00:00.000Z",
    "Freight": 11.61,
    "ShipName": "Toms Spezialitäten",
    "ShipAddress": "Luisenstr. 48",
    "ShipCity": "Münster",
    "ShipRegion": null,
    "ShipCountry": "Germany"
  },
  {
    "OrderID": "sin",
    "CustomerID": "HANAR",
    "OrderDate": "1996-07-08T00:00:00.000Z",
    "ShippedDate": "1996-07-12T00:00:00.000Z",
    "Freight": 65.83,
    "ShipName": "Hanari Carnes",
    "ShipAddress": "Rua do Paço, 67",
    "ShipCity": "Rio de Janeiro",
    "ShipRegion": "RJ",
    "ShipCountry": "Brazil"
  }
 
]
function generateUUID() { // Public Domain/MIT
    var d = new Date().getTime();//Timestamp
    var d2 = ((typeof performance !== 'undefined') && performance.now && (performance.now()*1000)) || 0;//Time in microseconds since page-load or 0 if unsupported
    return 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, function(c) {
        var r = Math.random() * 16;//random number between 0 and 16
        if(d > 0){//Use timestamp until depleted
            r = (d + r)%16 | 0;
            d = Math.floor(d/16);
        } else {//Use microseconds since page-load if supported
            r = (d2 + r)%16 | 0;
            d2 = Math.floor(d2/16);
        }
        return (c === 'x' ? r : (r & 0x3 | 0x8)).toString(16);
    });
}
module.exports= userData