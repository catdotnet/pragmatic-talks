var path = require('path');
var express = require('express');
var history = require('connect-history-api-fallback');
var server = express(); 
var folder = path.join(__dirname, "..", "dist");
var staticFileMiddleware = express.static(folder);

server.use(staticFileMiddleware);
server.use(history({
  disableDotRule: true,
  verbose: true
}));
server.use(staticFileMiddleware);

server.listen(3000, function () {
    console.log('listening ' + folder + ' on port 3000!');
});