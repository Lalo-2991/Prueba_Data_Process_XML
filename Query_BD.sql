CREATE DATABASE DataProcess
GO

USE DataProcess
GO

CREATE TABLE Encabezado
(
	id int PRIMARY KEY IDENTITY,
	factura varchar(10) not null,
	emisor varchar(50) not null,
	folioFiscal varchar(32)not null,
	fechaEmision datetime not null,
	fechaCertificacion datetime not null,
	lugarExpedicion varchar(5) not null,
	idMetodoPago int not null,
	idFormaPago int not null,
	idMoneda int not null,
	idEfectoComprobante int not null,
	CONSTRAINT FK_Encabezado_metodoPago FOREIGN KEY (idMetodoPago) REFERENCES MetodoPago(idMetodoPago)
		ON UPDATE CASCADE 
		ON DELETE CASCADE,
	CONSTRAINT FK_Encabezado_formaPago FOREIGN KEY (idFormaPago) REFERENCES FormaPago(idFormaPago)
		ON UPDATE CASCADE 
		ON DELETE CASCADE,
	CONSTRAINT FK_Encabezado_moneda FOREIGN KEY (idMoneda) REFERENCES Moneda(idMoneda)
		ON UPDATE CASCADE 
		ON DELETE CASCADE,
	CONSTRAINT FK_Encabezado_efectoComprobante FOREIGN KEY (idEfectoComprobante) REFERENCES efectoComprobante(idEfectoComprobante)
		ON UPDATE CASCADE 
		ON DELETE CASCADE
)
GO

CREATE TABLE MetodoPago
(
	idMetodoPago int PRIMARY KEY IDENTITY,
	nombreMetodoPago varchar(50) not null
)
GO
INSERT INTO MetodoPago VALUES
('Pago en una sola exhibici�n'),
('Pago en parcialidades o diferido')
GO

CREATE TABLE FormaPago
(
	idFormaPago int PRIMARY KEY IDENTITY,
	nombreFormaPago varchar(50) not null

)
GO
INSERT INTO FormaPago VALUES
('01 Efectivo'),
('02 Cheque nominativo'),
('03 Transferencia electr�nica de fondos'),
('04 Tarjeta de cr�dito'),
('05 Monedero electr�nico'),
('06 Dinero electr�nico'),
('08 Vales de despensa'),
('12 Daci�n en pago'),
('13 Pago por subrogaci�n'),
('14 Pago por consignaci�n'),
('15 Condonaci�n'),
('17 Compensaci�n'),
('23 Novaci�n'),
('24 Confusi�n'),
('25 Remisi�n de deuda'),
('26 Prescripci�n o caducidad'),
('27 A satisfacci�n del acreedor'),
('28 Tarjeta de d�bito'),
('29 Tarjeta de servicios'),
('30 Aplicaci�n de anticipos'),
('31 Intermediario pagos'),
('99 Por definir Opcional');
GO

CREATE TABLE Moneda
(
	idMoneda int PRIMARY KEY IDENTITY,
	nombreMoneda varchar(50) not null
)
GO
INSERT INTO Moneda VALUES
('GBP Libra Esterlina'),
('JPY Yen'),
('MXN Peso Mexicano'),
('USD D�lar americano'),
('VEF Bol�var')
GO



CREATE TABLE EfectoComprobante
(
	idEfectoComprobante int PRIMARY KEY IDENTITY,
	nombreEfectoComprobante varchar(50) not null
)
GO
INSERT INTO EfectoComprobante VALUES
('I - Ingreso'),
('E - Egreso'),
('T - Traslado'),
('N - N�mina'),
('P - Pago')
GO


INSERT INTO Encabezado VALUES
('ADQRO18838', 'SUBWAY','7F541F5F031D46268B738D3E8B85019E', '2023-08-13T21:52:29','2023-08-13T21:52:30','54600', 1,1,1,1),
('BEPRO19843', 'Casa de To�o','10F225E51D7644968CB8AFD0C67056BF', '2023-08-13T22:45:21','2023-08-13T21:45:23','03800', 1,1,1,1)
GO
SELECT * FROM Encabezado
SELECT * FROM EfectoComprobante
truncate table encabezado
DROP TABLE Encabezado