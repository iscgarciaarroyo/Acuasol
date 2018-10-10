var express = require('express');
var sql = require('mssql');
var conf = require('./config');
//var mailCtrl = require('./mailCtrl');
var bodyParser = require('body-parser');
const cors = require('cors');
var multer  = require('multer');

// configura bd 
var config = conf.Datos;
// instanciar
var app = express();
// para datos en mensaje
app.use(bodyParser.json()); // support json encoded bodies
app.use(bodyParser.urlencoded({ extended: true })); // support encoded bodies
app.use(cors());

function regreso(id, mensaje, res) {
	//console.log(id+' - '+mensaje);
	var SendObj = { "idUsr": id, "Msj": mensaje };
	var stringData = JSON.stringify(SendObj);

	// Indicamos el tipo de contenido a devolver en las cabeceras de nuestra respuesta
	res.contentType('application/json');
	res.send(stringData);
}

app.get('/', function(req, res) {
	res.send("Servicios Listos");
 });

app.get('/spNoticias', function (req, res) {
	var dbConn = new sql.Connection(config);
	dbConn.connect().then(function () {
		var request = new sql.Request(dbConn);
		request
			.input('NotIdNoticia', req.body.idNoticia)
			.input('NotDescripcion', req.body.descripcion)
			.input('NotFechaVigencia', req.body.fechaVig)
			//.input('NotImagen', req.body.image)
			.input('NotURLImagen', req.body.URLImage)
			.input('Accion', req.body.accion)
			.execute("[dbo].[spNoticias]").then(function (recordSet) {
				var msj = JSON.stringify(recordSet[0]);
				dbConn.close();
				res.contentType('application/json');
				res.send(msj);
			}).catch(function (err) {
				dbConn.close();
				regreso('0', 'Err1:' + err.message, res);
			});
	}).catch(function (err) {
		dbConn.close();
		regreso('0', 'Err2:' + err.message, res);
	});
});

app.post('/insPagoDetalle', function (req, res) {
	var dbConn = new sql.Connection(config);
	dbConn.connect().then(function () {
		var request = new sql.Request(dbConn);
		request

			.input('anio', req.body.anio)
			.input('idMarca', req.body.idMarca)
			.input('idSubmarca', req.body.idSubmarca)
			.input('spec', req.body.spec)
			.input('pTenencia', req.body.pTenencia)
			.input('pSeguro', req.body.pSeguro)
			.input('pVerificacion', req.body.pVerificacion)
			.input('pPlacasTarjeta', req.body.pPlacasTarjeta)
			.execute("[dbo].[INS_PAGO_DETALLE_SP]").then(function (recordSet) {
				var msj = JSON.stringify(recordSet[0][0]);
				dbConn.close();
				res.contentType('application/json');
				res.send(msj);
			}).catch(function (err) {
				dbConn.close();
				regreso('0', 'Err1:' + err.message, res);
			});
	}).catch(function (err) {
		dbConn.close();
		regreso('0', 'Err2:' + err.message, res);
	});
});

// escuchar
app.listen(conf.Port);
console.log("Servidor Back ACUASOL http://localhost:" + conf.Port);