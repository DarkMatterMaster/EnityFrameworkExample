CREATE DATABASE EntradaSimple

USE EntradaSimple

-------------------------------------------------------

CREATE TABLE Personal (
IdPersonal INT PRIMARY KEY IDENTITY (1,1),
Nombre VARCHAR(60) NOT NULL,
Departamento VARCHAR(50) NOT NULL
);

INSERT INTO Personal(Nombre, Departamento)
VALUES ('Anibal','Ventas'),
('Faustino','Visitante'),
('Alberto','Inspecion');

--******
CREATE OR ALTER PROCEDURE spAgregarPersonal(
@Nombre VARCHAR(60),
@Departamento VARCHAR(50)
)
AS 
BEGIN
	INSERT INTO Personal(Nombre, Departamento)
	VALUES (@Nombre, @Departamento);
END

EXECUTE spAgregarPersonal @Nombre='Ursus', @Departamento='Finanzas'

--******
CREATE OR ALTER PROCEDURE spObtenerPersonal
AS
BEGIN
	SELECT IdPersonal, Nombre, Departamento FROM Personal;
END

EXECUTE spObtenerPersonal

SELECT * FROM Personal;
--------------------------------------------------------

CREATE TABLE Accesos(
IdAcceso INT PRIMARY KEY IDENTITY(1,1),
Nombre VARCHAR(100) NOT NULL,
Ubicacion VARCHAR(50) NOT NULL
);

INSERT INTO Accesos(Nombre, Ubicacion)
VALUES ('Puerta 1','Norte'),
('Puerta 2','Sur'),
('Puerta 3','Oeste');

--******
CREATE OR ALTER PROCEDURE spObtenerAccesos
AS
BEGIN
	SELECT IdAcceso, Nombre, Ubicacion FROM Accesos;
END

EXECUTE spObtenerAccesos


SELECT * FROM Accesos;

--------------------------------------------------------

CREATE TABLE RegistroAccesos(
IdRegistro INT PRIMARY KEY IDENTITY(1,1),
IdPersonal INT NOT NULL,
IdAcceso INT NOT NULL,
FechaHora DATETIME NOT NULL,
TipoMovimiento CHAR(1) NOT NULL,
FOREIGN KEY (IdPersonal) REFERENCES Personal(IdPersonal),
FOREIGN KEY (IdAcceso) REFERENCES Accesos(IdAcceso)
);

INSERT INTO RegistroAccesos(IdPersonal, IdAcceso, FechaHora, TipoMovimiento)
VALUES
(1, 1, '2025-06-21 08:00:00', 'E'),
(1, 2, '2025-06-21 10:00:00', 'S'),
(2, 2, '2025-06-21 08:15:00', 'E'),
(2, 3, '2025-06-21 17:19:00', 'S'),
(3, 3, '2025-06-21 11:33:00', 'E'),
(3, 1, '2025-06-21 12:07:00', 'S');

--*****************
CREATE OR ALTER PROCEDURE spObtenerRegistros
AS
BEGIN
	SELECT RegistroAccesos.IdRegistro, Personal.Nombre, Accesos.Nombre AS Acceso, Accesos.Ubicacion, RegistroAccesos.FechaHora, RegistroAccesos.TipoMovimiento
    FROM RegistroAccesos
    INNER JOIN Personal ON RegistroAccesos.IdPersonal = Personal.IdPersonal
    INNER JOIN Accesos ON RegistroAccesos.IdAcceso = Accesos.IdAcceso
    ORDER BY RegistroAccesos.FechaHora DESC
END

EXECUTE spObtenerRegistros


--*****************
CREATE OR ALTER PROCEDURE spObtenerRegistrospPorPersonal
@IdPersonal INT
AS
BEGIN
	SELECT RegistroAccesos.IdRegistro, Personal.Nombre, Accesos.Nombre AS Acceso, Accesos.Ubicacion, RegistroAccesos.FechaHora, RegistroAccesos.TipoMovimiento
    FROM RegistroAccesos
    INNER JOIN Personal ON RegistroAccesos.IdPersonal = Personal.IdPersonal
    INNER JOIN Accesos ON RegistroAccesos.IdAcceso = Accesos.IdAcceso
	WHERE RegistroAccesos.IdPersonal = @IdPersonal
    ORDER BY RegistroAccesos.FechaHora
END

EXECUTE spObtenerRegistrospPorPersonal @IdPersonal = 1
--*****************
CREATE OR ALTER PROCEDURE spAgregarRegistro(
@IdPersonal INT,
@IdAcceso INT,
@FechaHora DATETIME,
@TipoMovimiento CHAR(1)
)
AS 
BEGIN
	INSERT INTO RegistroAccesos(IdPersonal, IdAcceso, FechaHora, TipoMovimiento)
	VALUES (@IdPersonal, @IdAcceso, @FechaHora, @TipoMovimiento);
END

EXECUTE spAgregarRegistro @IdPersonal = 1, @IdAcceso = 2, @FechaHora = '2025-06-22 08:40:00', @TipoMovimiento = 'E'

SELECT * FROM RegistroAccesos;


--------------------------------------------------------