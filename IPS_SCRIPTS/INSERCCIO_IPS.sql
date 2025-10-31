INSERT INTO Ciudades (ciudad_id, nombre, departamento, pais) 
VALUES (1, 'Valledupar', 'Cesar', 'Colombia');

INSERT INTO Ciudades (ciudad_id, nombre, departamento, pais) 
VALUES (2, 'Aguachica', 'Cesar', 'Colombia');

INSERT INTO Ciudades (ciudad_id, nombre, departamento, pais) 
VALUES (3, 'La Paz', 'Cesar', 'Colombia');

INSERT INTO Ciudades (ciudad_id, nombre, departamento, pais) 
VALUES (4, 'Agustín Codazzi', 'Cesar', 'Colombia');

INSERT INTO Ciudades (ciudad_id, nombre, departamento, pais) 
VALUES (5, 'Bosconia', 'Cesar', 'Colombia');

-- Ciudades cercanas (Costa Caribe)
INSERT INTO Ciudades (ciudad_id, nombre, departamento, pais) 
VALUES (6, 'Santa Marta', 'Magdalena', 'Colombia');

INSERT INTO Ciudades (ciudad_id, nombre, departamento, pais) 
VALUES (7, 'Barranquilla', 'Atlántico', 'Colombia');

INSERT INTO Ciudades (ciudad_id, nombre, departamento, pais) 
VALUES (8, 'Riohacha', 'La Guajira', 'Colombia');

INSERT INTO Ciudades (ciudad_id, nombre, departamento, pais) 
VALUES (9, 'Bogotá', 'Cundinamarca', 'Colombia');

INSERT INTO Ciudades (ciudad_id, nombre, departamento, pais) 
VALUES (10, 'Bucaramanga', 'Santander', 'Colombia');



INSERT INTO EPS (eps_id, nombre, nit, telefono, correo, direccion, tipo_regimen)
VALUES (1, 'Nueva EPS', '900156264-1', '018000952020', 'contacto@nuevaeps.com.co', 
        'Calle 26 #69D-91', 'Contributivo');

INSERT INTO EPS (eps_id, nombre, nit, telefono, correo, direccion, tipo_regimen)
VALUES (2, 'Sanitas EPS', '800251440-6', '6018000519519', 'servicioalcliente@keralty.com', 
        'Av. El Dorado #69-76', 'Contributivo');

INSERT INTO EPS (eps_id, nombre, nit, telefono, correo, direccion, tipo_regimen)
VALUES (3, 'SURA EPS', '800088702-7', '6014441234', 'atencioneps@sura.com.co', 
        'Calle 16 Sur #43A-49', 'Contributivo');

INSERT INTO EPS (eps_id, nombre, nit, telefono, correo, direccion, tipo_regimen)
VALUES (4, 'Salud Total EPS', '800130907-1', '6013077000', 'servicioalcliente@saludtotal.com.co', 
        'Calle 127A #53A-45', 'Contributivo');

INSERT INTO EPS (eps_id, nombre, nit, telefono, correo, direccion, tipo_regimen)
VALUES (5, 'Compensar EPS', '860066942-7', '6014441234', 'contactenos@compensar.com', 
        'Av. Calle 68 #49A-47', 'Contributivo');

INSERT INTO EPS (eps_id, nombre, nit, telefono, correo, direccion, tipo_regimen)
VALUES (6, 'Famisanar EPS', '830003564-5', '6015139000', 'servicioalafiliado@famisanar.com.co', 
        'Cra. 13 #32-76', 'Contributivo');

INSERT INTO EPS (eps_id, nombre, nit, telefono, correo, direccion, tipo_regimen)
VALUES (7, 'Coosalud EPS', '892300082-2', '6053853535', 'atencionalusuario@coosalud.com', 
        'Cra. 54 #59-129', 'Contributivo');

INSERT INTO EPS (eps_id, nombre, nit, telefono, correo, direccion, tipo_regimen)
VALUES (8, 'Mutual SER EPS', '830037950-0', '6013078000', 'contactenos@mutualser.com', 
        'Calle 67 #7-94', 'Contributivo');

INSERT INTO EPS (eps_id, nombre, nit, telefono, correo, direccion, tipo_regimen)
VALUES (9, 'Comfamiliar Cesar', '892300016-1', '6055742525', 'info@comfacesar.com', 
        'Calle 16 #9-52 Valledupar', 'Subsidiado');

INSERT INTO EPS (eps_id, nombre, nit, telefono, correo, direccion, tipo_regimen)
VALUES (10, 'Cajacopi EPS', '860028415-2', '6018000122122', 'servicio@cajacopi.com', 
        'Cra. 7 #24-89', 'Subsidiado');



INSERT INTO Especialidades (especialidad_id, nombre)
VALUES (1, 'Medicina General');

INSERT INTO Especialidades (especialidad_id, nombre)
VALUES (2, 'Odontología');

INSERT INTO Especialidades (especialidad_id, nombre)
VALUES (3, 'Psicología');

INSERT INTO Especialidades (especialidad_id, nombre)
VALUES (4, 'Enfermería');

INSERT INTO Especialidades (especialidad_id, nombre)
VALUES (5, 'Nutrición y Dietética');

INSERT INTO Especialidades (especialidad_id, nombre)
VALUES (6, 'Ginecología');

INSERT INTO Especialidades (especialidad_id, nombre)
VALUES (7, 'Pediatría');

INSERT INTO Especialidades (especialidad_id, nombre)
VALUES (8, 'Dermatología');

INSERT INTO Especialidades (especialidad_id, nombre)
VALUES (9, 'Traumatología');

INSERT INTO Especialidades (especialidad_id, nombre)
VALUES (10, 'Oftalmología');

INSERT INTO Especialidades (especialidad_id, nombre)
VALUES (11, 'Medicina Interna');

INSERT INTO Especialidades (especialidad_id, nombre)
VALUES (12, 'Fisioterapia');

COMMIT;

-- ============================================
-- VERIFICAR DATOS INSERTADOS
-- ============================================

PROMPT '============================================';
PROMPT 'DATOS INSERTADOS CORRECTAMENTE';
PROMPT '============================================';

-- Contar ciudades
SELECT 'Ciudades insertadas: ' || COUNT(*) AS total FROM Ciudades;

-- Contar EPS
SELECT 'EPS insertadas: ' || COUNT(*) AS total FROM EPS;

-- Contar Especialidades
SELECT 'Especialidades insertadas: ' || COUNT(*) AS total FROM Especialidades;

PROMPT '============================================';
PROMPT 'Verificación de datos:';
PROMPT '============================================';

-- Ver ciudades del Cesar
SELECT ciudad_id, nombre, departamento FROM Ciudades WHERE departamento = 'Cesar';

-- Ver todas las EPS
SELECT eps_id, nombre, tipo_regimen FROM EPS;

-- Ver todas las especialidades
SELECT especialidad_id, nombre FROM Especialidades;

PROMPT '============================================';
PROMPT 'FIN DEL SCRIPT';
PROMPT '============================================';