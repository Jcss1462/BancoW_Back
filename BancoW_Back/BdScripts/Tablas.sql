--Create Database BancoWDb;

--Drop Table Simulacion;
--Drop Table TerminoPago;
--Drop Table Usuario;

-- Crear tabla Usuario
CREATE TABLE Usuario (
    idUsuario INT IDENTITY(1,1) PRIMARY KEY,
    email NVARCHAR(255) NOT NULL UNIQUE,
    password NVARCHAR(255) NOT NULL -- Aquí se espera que el password esté en formato hash
);

-- Crear tabla TerminoPago
CREATE TABLE TerminoPago (
    idTerminoDePago INT PRIMARY KEY,
    descripcion NVARCHAR(50) NOT NULL -- Mensual o Anual
);

-- Crear tabla Simulación
CREATE TABLE Simulacion (
    idSimulacion INT IDENTITY(1,1) PRIMARY KEY,
    titulo NVARCHAR(255) NULL,
    monto DECIMAL(18, 2) NOT NULL,
    termino_pago_id INT NOT NULL,
    fecha_inicio DATE NOT NULL,
    fecha_fin DATE NOT NULL,
    tasa DECIMAL(5, 2) NOT NULL,
    usuario_id INT NOT NULL,
    
    CONSTRAINT FK_Simulacion_TerminoPago FOREIGN KEY (termino_pago_id)
    REFERENCES TerminoPago(idTerminoDePago),

    CONSTRAINT FK_Simulacion_Usuario FOREIGN KEY (usuario_id)
    REFERENCES Usuario(idUsuario)
);
