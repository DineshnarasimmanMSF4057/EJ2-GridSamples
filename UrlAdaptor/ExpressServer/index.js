const express = require("express");
const http = require("http");
const bodyParser = require("body-parser");
const cors = require("cors");

let data=require("./data");

const host = "0.0.0.0"; // causes req.socket.remote to use the IPv4 format
const port = 4001;

const app = express();
app.use(cors());
app.use(bodyParser.json());

// reading all records
app.post("/orders", function (req, res) {
  console.log("POST | Reading all records...");
  if (req.body.skip != undefined && req.body.take !=undefined) {
    return res.json({
      result: data.slice(req.body.skip, req.body.skip + req.body.take),
      count: data.length,
    });
  }
  return res.json({ result: data, count: data.length });
});

// reading all records
app.get("/", function (req, res) {
  console.log("GET  | Requesting the homepage from a browser...");
  return res.send("Expess listening...");
});

// creating a new record
app.post("/orders/insert", function (req, res) {
  data.splice(0, 0, req.body.value);
  console.log("POST | Creating record:", req.body.value);

  return res.status(200).send("Row inserted...");
});

// updating an exising record
app.post("/orders/update", function (req, res) {
  const index = data.findIndex((x) => x.OrderID === req.body.value.OrderID);
  console.log(index, req.body.value);
  data.splice(index, 1, req.body.value);
  console.log("POST | Updating record...");

  return res.status(200).send("Row updated...");
});

// deleting an existing record
app.post("/orders/delete", function (req, res) {

if(req.body.key===undefined)
{
  data=data.filter(user =>
    !req.body.deleted.some(toRemove => toRemove.OrderID === user.OrderID)
  );
  console.log(data)
}
else
{
  data=  data.filter((x) => x.OrderID != req.body.key);
}
  

  console.log("POST | Deleting record:", req.body.key);
  console.log(res)
  return res.status(200).send("Row deleted...");
});

function onListening() {
  console.log(`Express listening on port ${port}...`);
}

app.listen(port, host, onListening);
