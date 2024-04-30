const express = require("express");
const http = require("http");
const bodyParser = require("body-parser");
const cors = require("cors");
let data=require("./data");
// let data = require("./dataSource.json");

const host = "0.0.0.0"; // causes req.socket.remote to use the IPv4 format
const port = 4003;

const app = express();
app.use(cors());
app.use(bodyParser.json());

// reading all records
app.get("/orders", function (req, res) {
  console.log("POST | Reading all records...");
  if (req.body.skip != undefined && req.body.take !=undefined) {
    return res.json({
      result: data.slice(req.body.skip, req.body.skip + req.body.take),
      count: data.length,
    });
  }
  return res.json({ result: data, count: data.length });
});
JSON.stringify


// creating a new record


// updating an exising record
app.put("/orders(:id)", function (req, res) {
    //getting index
  const index = data.findIndex((x) => x.OrderID === req.body.OrderID);
  data.splice(index, 1, req.body);

  return res.status(200).send("Row updated...");
});


function onListening() {
  console.log(`Express listening on port ${port}...`);
}

app.listen(port, host, onListening);
