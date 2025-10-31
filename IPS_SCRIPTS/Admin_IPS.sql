CREATE OR REPLACE FUNCTION FN_EXISTE_GENERICO (
    p_tabla IN VARCHAR2,
    p_campo_id IN VARCHAR2,
    p_valor_id IN VARCHAR2
) RETURN NUMBER AS
    v_count NUMBER;
    v_query VARCHAR2(500);
BEGIN
    v_query := 'SELECT COUNT(*) FROM ' || p_tabla || ' WHERE ' || p_campo_id || ' = :1';
    EXECUTE IMMEDIATE v_query INTO v_count USING p_valor_id;
    RETURN v_count;
END;
/

CREATE OR REPLACE PROCEDURE SP_ELIMINAR_GENERICO (
    p_tabla IN VARCHAR2,
    p_campo_id IN VARCHAR2,
    p_valor_id IN VARCHAR2,
    p_resultado OUT NUMBER
) AS
    v_query VARCHAR2(500);
BEGIN
    v_query := 'DELETE FROM ' || p_tabla || ' WHERE ' || p_campo_id || ' = :1';
    EXECUTE IMMEDIATE v_query USING p_valor_id;
    
    IF SQL%ROWCOUNT > 0 THEN
        p_resultado := 1;
    ELSE
        p_resultado := 0;
    END IF;
    COMMIT;
EXCEPTION
    WHEN OTHERS THEN
        p_resultado := 0;
        ROLLBACK;
END;
/

CREATE OR REPLACE PROCEDURE SP_INSERTAR_CREDENCIALES (
    p_usuario_id IN NUMBER,
    p_nombre_usuario IN VARCHAR2,
    p_password IN VARCHAR2,
    p_tipo_usuario IN VARCHAR2,
    p_resultado OUT NUMBER
) AS
BEGIN
    INSERT INTO s_credenciales (usuario_id, nombre_usuario, password, tipo_usuario)
    VALUES (p_usuario_id, p_nombre_usuario, p_password, p_tipo_usuario);
    p_resultado := 1;
    COMMIT;
EXCEPTION
    WHEN OTHERS THEN
        p_resultado := 0;
        ROLLBACK;
END;
/

CREATE OR REPLACE PROCEDURE SP_ACTUALIZAR_CREDENCIALES (
    p_usuario_id IN NUMBER,
    p_nombre_usuario IN VARCHAR2,
    p_password IN VARCHAR2,
    p_tipo_usuario IN VARCHAR2,
    p_estado IN VARCHAR2,
    p_resultado OUT NUMBER
) AS
BEGIN
    UPDATE s_credenciales SET
        nombre_usuario = p_nombre_usuario,
        password = p_password,
        tipo_usuario = p_tipo_usuario,
        estado = p_estado
    WHERE usuario_id = p_usuario_id;
    
    IF SQL%ROWCOUNT > 0 THEN
        p_resultado := 1;
    ELSE
        p_resultado := 0;
    END IF;
    COMMIT;
EXCEPTION
    WHEN OTHERS THEN
        p_resultado := 0;
        ROLLBACK;
END;
/

CREATE OR REPLACE FUNCTION FN_VERIFICAR_CREDENCIALES (
    p_username IN VARCHAR2,
    p_password IN VARCHAR2
) RETURN NUMBER AS
    v_count NUMBER;
BEGIN
    SELECT COUNT(*)
    INTO v_count
    FROM s_credenciales
    WHERE nombre_usuario = p_username
    AND password = p_password
    AND estado = 'Activo';
    
    RETURN v_count;
END;
/

CREATE OR REPLACE FUNCTION FN_EXISTE_USUARIO (
    p_username IN VARCHAR2
) RETURN NUMBER AS
    v_count NUMBER;
BEGIN
    SELECT COUNT(*)
    INTO v_count
    FROM s_credenciales
    WHERE nombre_usuario = p_username;
    
    RETURN v_count;
END;
/

CREATE OR REPLACE FUNCTION FN_OBTENER_TIPO_USUARIO (
    p_username IN VARCHAR2
) RETURN VARCHAR2 AS
    v_tipo_usuario VARCHAR2(50);
BEGIN
    SELECT tipo_usuario
    INTO v_tipo_usuario
    FROM s_credenciales
    WHERE nombre_usuario = p_username;
    
    RETURN v_tipo_usuario;
EXCEPTION
    WHEN NO_DATA_FOUND THEN
        RETURN NULL;
END;
/

CREATE OR REPLACE PROCEDURE SP_CAMBIAR_ESTADO_USUARIO (
    p_username IN VARCHAR2,
    p_estado IN VARCHAR2,
    p_resultado OUT NUMBER
) AS
BEGIN
    UPDATE s_credenciales 
    SET estado = p_estado 
    WHERE nombre_usuario = p_username;
    
    IF SQL%ROWCOUNT > 0 THEN
        p_resultado := 1;
    ELSE
        p_resultado := 0;
    END IF;
    COMMIT;
EXCEPTION
    WHEN OTHERS THEN
        p_resultado := 0;
        ROLLBACK;
END;
/

CREATE OR REPLACE PROCEDURE SP_CAMBIAR_PASSWORD (
    p_username IN VARCHAR2,
    p_nueva_password IN VARCHAR2,
    p_resultado OUT NUMBER
) AS
BEGIN
    UPDATE s_credenciales 
    SET password = p_nueva_password 
    WHERE nombre_usuario = p_username;
    
    IF SQL%ROWCOUNT > 0 THEN
        p_resultado := 1;
    ELSE
        p_resultado := 0;
    END IF;
    COMMIT;
EXCEPTION
    WHEN OTHERS THEN
        p_resultado := 0;
        ROLLBACK;
END;
/

CREATE OR REPLACE PROCEDURE SP_INSERTAR_PACIENTE (
    p_documento IN VARCHAR2,
    p_primer_nombre IN VARCHAR2,
    p_segundo_nombre IN VARCHAR2,
    p_primer_apellido IN VARCHAR2,
    p_segundo_apellido IN VARCHAR2,
    p_telefono IN VARCHAR2,
    p_correo IN VARCHAR2,
    p_edad IN NUMBER,
    p_sexo IN CHAR,
    p_tipo_sangre IN VARCHAR2,
    p_rh IN CHAR,
    p_ciudad_id IN NUMBER,
    p_eps_id IN NUMBER,
    p_documento_responsable IN VARCHAR2,
    p_usuario_id IN NUMBER,
    p_resultado OUT NUMBER
) AS
BEGIN
    INSERT INTO pacientes (
        documentoid, primer_nombre, segundo_nombre, primer_apellido,
        segundo_apellido, telefono, correo, edad, sexo,
        tipo_sangre, rh, ciudad_id, eps_id, documento_responsable, usuario_id
    ) VALUES (
        p_documento, p_primer_nombre, p_segundo_nombre, p_primer_apellido,
        p_segundo_apellido, p_telefono, p_correo, p_edad, p_sexo,
        p_tipo_sangre, p_rh, p_ciudad_id, p_eps_id, p_documento_responsable, p_usuario_id
    );
    p_resultado := 1;
    COMMIT;
EXCEPTION
    WHEN OTHERS THEN
        p_resultado := 0;
        ROLLBACK;
END;
/

CREATE OR REPLACE PROCEDURE SP_ACTUALIZAR_PACIENTE (
    p_documento IN VARCHAR2,
    p_primer_nombre IN VARCHAR2,
    p_segundo_nombre IN VARCHAR2,
    p_primer_apellido IN VARCHAR2,
    p_segundo_apellido IN VARCHAR2,
    p_telefono IN VARCHAR2,
    p_correo IN VARCHAR2,
    p_edad IN NUMBER,
    p_sexo IN CHAR,
    p_tipo_sangre IN VARCHAR2,
    p_rh IN CHAR,
    p_ciudad_id IN NUMBER,
    p_eps_id IN NUMBER,
    p_documento_responsable IN VARCHAR2,
    p_resultado OUT NUMBER
) AS
BEGIN
    UPDATE pacientes SET
        primer_nombre = p_primer_nombre,
        segundo_nombre = p_segundo_nombre,
        primer_apellido = p_primer_apellido,
        segundo_apellido = p_segundo_apellido,
        telefono = p_telefono,
        correo = p_correo,
        edad = p_edad,
        sexo = p_sexo,
        tipo_sangre = p_tipo_sangre,
        rh = p_rh,
        ciudad_id = p_ciudad_id,
        eps_id = p_eps_id,
        documento_responsable = p_documento_responsable
    WHERE documentoid = p_documento;
    
    IF SQL%ROWCOUNT > 0 THEN
        p_resultado := 1;
    ELSE
        p_resultado := 0;
    END IF;
    COMMIT;
EXCEPTION
    WHEN OTHERS THEN
        p_resultado := 0;
        ROLLBACK;
END;
/

CREATE OR REPLACE PROCEDURE SP_INSERTAR_DOCTOR (
    p_documento IN VARCHAR2,
    p_numero_licencia IN VARCHAR2,
    p_primer_nombre IN VARCHAR2,
    p_segundo_nombre IN VARCHAR2,
    p_primer_apellido IN VARCHAR2,
    p_segundo_apellido IN VARCHAR2,
    p_telefono IN VARCHAR2,
    p_correo IN VARCHAR2,
    p_especialidad_id IN NUMBER,
    p_hora_atencion IN VARCHAR2,
    p_usuario_id IN NUMBER,
    p_resultado OUT NUMBER
) AS
BEGIN
    INSERT INTO doctores (
        documentoid, numero_licencia, primer_nombre, segundo_nombre,
        primer_apellido, segundo_apellido, telefono, correo,
        especialidad_id, horaatencion, usuario_id
    ) VALUES (
        p_documento, p_numero_licencia, p_primer_nombre, p_segundo_nombre,
        p_primer_apellido, p_segundo_apellido, p_telefono, p_correo,
        p_especialidad_id, p_hora_atencion, p_usuario_id
    );
    p_resultado := 1;
    COMMIT;
EXCEPTION
    WHEN OTHERS THEN
        p_resultado := 0;
        ROLLBACK;
END;
/

CREATE OR REPLACE PROCEDURE SP_ACTUALIZAR_DOCTOR (
    p_documento IN VARCHAR2,
    p_numero_licencia IN VARCHAR2,
    p_primer_nombre IN VARCHAR2,
    p_segundo_nombre IN VARCHAR2,
    p_primer_apellido IN VARCHAR2,
    p_segundo_apellido IN VARCHAR2,
    p_telefono IN VARCHAR2,
    p_correo IN VARCHAR2,
    p_especialidad_id IN NUMBER,
    p_hora_atencion IN VARCHAR2,
    p_resultado OUT NUMBER
) AS
BEGIN
    UPDATE doctores SET
        numero_licencia = p_numero_licencia,
        primer_nombre = p_primer_nombre,
        segundo_nombre = p_segundo_nombre,
        primer_apellido = p_primer_apellido,
        segundo_apellido = p_segundo_apellido,
        telefono = p_telefono,
        correo = p_correo,
        especialidad_id = p_especialidad_id,
        horaatencion = p_hora_atencion
    WHERE documentoid = p_documento;
    
    IF SQL%ROWCOUNT > 0 THEN
        p_resultado := 1;
    ELSE
        p_resultado := 0;
    END IF;
    COMMIT;
EXCEPTION
    WHEN OTHERS THEN
        p_resultado := 0;
        ROLLBACK;
END;
/

CREATE OR REPLACE PROCEDURE SP_OBTENER_DOCTORES_ESP (
    p_especialidad_id IN NUMBER,
    p_cursor OUT SYS_REFCURSOR
) AS
BEGIN
    OPEN p_cursor FOR
        SELECT * FROM doctores 
        WHERE especialidad_id = p_especialidad_id 
        ORDER BY primer_nombre;
END;
/

CREATE OR REPLACE PROCEDURE SP_INSERTAR_CITA (
    p_cita_id IN NUMBER,
    p_paciente_doc IN VARCHAR2,
    p_doctor_doc IN VARCHAR2,
    p_fecha IN DATE,
    p_hora IN VARCHAR2,
    p_estado IN VARCHAR2,
    p_especialidad_id IN NUMBER,
    p_resultado OUT NUMBER
) AS
BEGIN
    INSERT INTO s_citas (
        cita_id, paciente_documentoid, doctor_documentoid, 
        fecha, hora, estado, especialidad_id
    ) VALUES (
        p_cita_id, p_paciente_doc, p_doctor_doc,
        p_fecha, p_hora, p_estado, p_especialidad_id
    );
    p_resultado := 1;
    COMMIT;
EXCEPTION
    WHEN OTHERS THEN
        p_resultado := 0;
        ROLLBACK;
END;
/

CREATE OR REPLACE PROCEDURE SP_ACTUALIZAR_CITA (
    p_cita_id IN NUMBER,
    p_paciente_doc IN VARCHAR2,
    p_doctor_doc IN VARCHAR2,
    p_fecha IN DATE,
    p_hora IN VARCHAR2,
    p_especialidad_id IN NUMBER,
    p_estado IN VARCHAR2,
    p_resultado OUT NUMBER
) AS
BEGIN
    UPDATE s_citas SET
        paciente_documentoid = p_paciente_doc,
        doctor_documentoid = p_doctor_doc,
        fecha = p_fecha,
        hora = p_hora,
        especialidad_id = p_especialidad_id,
        estado = p_estado
    WHERE cita_id = p_cita_id;
    
    IF SQL%ROWCOUNT > 0 THEN
        p_resultado := 1;
    ELSE
        p_resultado := 0;
    END IF;
    COMMIT;
EXCEPTION
    WHEN OTHERS THEN
        p_resultado := 0;
        ROLLBACK;
END;
/

CREATE OR REPLACE FUNCTION FN_SIGUIENTE_ID_CITA
RETURN NUMBER AS
    v_siguiente_id NUMBER;
BEGIN
    SELECT NVL(MAX(cita_id), 0) + 1 
    INTO v_siguiente_id
    FROM s_citas;
    RETURN v_siguiente_id;
END;
/

CREATE OR REPLACE FUNCTION FN_EXISTE_CITA_HORARIO (
    p_doctor_doc IN VARCHAR2,
    p_fecha IN DATE,
    p_hora IN VARCHAR2
) RETURN NUMBER AS
    v_count NUMBER;
BEGIN
    SELECT COUNT(*)
    INTO v_count
    FROM s_citas
    WHERE doctor_documentoid = p_doctor_doc
    AND TRUNC(fecha) = TRUNC(p_fecha)
    AND hora = p_hora
    AND estado != 'Cancelada';
    
    RETURN v_count;
END;
/

CREATE OR REPLACE PROCEDURE SP_CAMBIAR_ESTADO_CITA (
    p_cita_id IN NUMBER,
    p_estado IN VARCHAR2,
    p_resultado OUT NUMBER
) AS
BEGIN
    UPDATE s_citas 
    SET estado = p_estado 
    WHERE cita_id = p_cita_id;
    
    IF SQL%ROWCOUNT > 0 THEN
        p_resultado := 1;
    ELSE
        p_resultado := 0;
    END IF;
    COMMIT;
EXCEPTION
    WHEN OTHERS THEN
        p_resultado := 0;
        ROLLBACK;
END;
/

CREATE OR REPLACE PROCEDURE SP_OBTENER_CITAS_PACIENTE (
    p_paciente_doc IN VARCHAR2,
    p_cursor OUT SYS_REFCURSOR
) AS
BEGIN
    OPEN p_cursor FOR
        SELECT * FROM s_citas 
        WHERE paciente_documentoid = p_paciente_doc 
        ORDER BY fecha DESC, hora DESC;
END;
/

CREATE OR REPLACE PROCEDURE SP_OBTENER_CITAS_DOCTOR (
    p_doctor_doc IN VARCHAR2,
    p_cursor OUT SYS_REFCURSOR
) AS
BEGIN
    OPEN p_cursor FOR
        SELECT * FROM s_citas 
        WHERE doctor_documentoid = p_doctor_doc 
        ORDER BY fecha DESC, hora DESC;
END;
/

CREATE OR REPLACE PROCEDURE SP_OBTENER_CITAS_FECHA (
    p_fecha IN DATE,
    p_cursor OUT SYS_REFCURSOR
) AS
BEGIN
    OPEN p_cursor FOR
        SELECT * FROM s_citas 
        WHERE TRUNC(fecha) = TRUNC(p_fecha) 
        ORDER BY hora;
END;
/

CREATE OR REPLACE PROCEDURE SP_OBTENER_CITAS_ESTADO (
    p_estado IN VARCHAR2,
    p_cursor OUT SYS_REFCURSOR
) AS
BEGIN
    OPEN p_cursor FOR
        SELECT * FROM s_citas 
        WHERE estado = p_estado 
        ORDER BY fecha DESC, hora DESC;
END;
/

CREATE OR REPLACE PROCEDURE SP_OBTENER_CITAS_PENDIENTES_PACIENTE (
    p_paciente_doc IN VARCHAR2,
    p_cursor OUT SYS_REFCURSOR
) AS
BEGIN
    OPEN p_cursor FOR
        SELECT * FROM s_citas
        WHERE paciente_documentoid = p_paciente_doc 
        AND estado = 'Pendiente'
        AND fecha >= TRUNC(SYSDATE)
        ORDER BY fecha, hora;
END;
/

CREATE OR REPLACE PROCEDURE SP_INSERTAR_HISTORIA (
    p_historia_id IN NUMBER,
    p_paciente_doc IN VARCHAR2,
    p_doctor_doc IN VARCHAR2,
    p_cita_id IN NUMBER,
    p_especialidad_id IN NUMBER,
    p_diagnostico IN VARCHAR2,
    p_tratamiento IN VARCHAR2,
    p_observaciones IN VARCHAR2,
    p_resultado OUT NUMBER
) AS
BEGIN
    INSERT INTO s_historias_clinicas (
        historia_id, paciente_documentoid, doctor_documentoid, 
        cita_id, especialidad_id, diagnostico, tratamiento, observaciones
    ) VALUES (
        p_historia_id, p_paciente_doc, p_doctor_doc,
        p_cita_id, p_especialidad_id, p_diagnostico, p_tratamiento, p_observaciones
    );
    p_resultado := 1;
    COMMIT;
EXCEPTION
    WHEN OTHERS THEN
        p_resultado := 0;
        ROLLBACK;
END;
/

CREATE OR REPLACE PROCEDURE SP_ACTUALIZAR_HISTORIA (
    p_historia_id IN NUMBER,
    p_diagnostico IN VARCHAR2,
    p_tratamiento IN VARCHAR2,
    p_observaciones IN VARCHAR2,
    p_resultado OUT NUMBER
) AS
BEGIN
    UPDATE s_historias_clinicas
    SET diagnostico = p_diagnostico,
        tratamiento = p_tratamiento,
        observaciones = p_observaciones
    WHERE historia_id = p_historia_id;
    
    IF SQL%ROWCOUNT > 0 THEN
        p_resultado := 1;
    ELSE
        p_resultado := 0;
    END IF;
    COMMIT;
EXCEPTION
    WHEN OTHERS THEN
        p_resultado := 0;
        ROLLBACK;
END;
/

CREATE OR REPLACE FUNCTION FN_SIGUIENTE_ID_HISTORIA
RETURN NUMBER AS
    v_siguiente_id NUMBER;
BEGIN
    SELECT NVL(MAX(historia_id), 0) + 1 
    INTO v_siguiente_id
    FROM s_historias_clinicas;
    RETURN v_siguiente_id;
END;
/

CREATE OR REPLACE FUNCTION FN_EXISTE_HISTORIA_CITA (
    p_cita_id IN NUMBER
) RETURN NUMBER AS
    v_count NUMBER;
BEGIN
    SELECT COUNT(*)
    INTO v_count
    FROM s_historias_clinicas
    WHERE cita_id = p_cita_id;
    RETURN v_count;
END;
/

CREATE OR REPLACE PROCEDURE SP_OBTENER_HISTORIAS_ESP (
    p_paciente_doc IN VARCHAR2,
    p_especialidad_id IN NUMBER,
    p_cursor OUT SYS_REFCURSOR
) AS
BEGIN
    OPEN p_cursor FOR
        SELECT h.* 
        FROM s_historias_clinicas h
        INNER JOIN s_citas c ON h.cita_id = c.cita_id
        WHERE h.paciente_documentoid = p_paciente_doc
        AND h.especialidad_id = p_especialidad_id
        ORDER BY c.fecha DESC;
END;
/

CREATE OR REPLACE PROCEDURE SP_OBTENER_HISTORIA_CITA (
    p_cita_id IN NUMBER,
    p_cursor OUT SYS_REFCURSOR
) AS
BEGIN
    OPEN p_cursor FOR
        SELECT * FROM s_historias_clinicas
        WHERE cita_id = p_cita_id;
END;
/

CREATE OR REPLACE PROCEDURE SP_OBTENER_HISTORIAS_DOCTOR (
    p_doctor_doc IN VARCHAR2,
    p_cursor OUT SYS_REFCURSOR
) AS
BEGIN
    OPEN p_cursor FOR
        SELECT h.* 
        FROM s_historias_clinicas h
        INNER JOIN s_citas c ON h.cita_id = c.cita_id
        WHERE h.doctor_documentoid = p_doctor_doc
        ORDER BY c.fecha DESC;
END;
/

CREATE OR REPLACE PROCEDURE SP_OBTENER_HISTORIAS_PACIENTE (
    p_paciente_doc IN VARCHAR2,
    p_cursor OUT SYS_REFCURSOR
) AS
BEGIN
    OPEN p_cursor FOR
        SELECT h.* 
        FROM s_historias_clinicas h
        INNER JOIN s_citas c ON h.cita_id = c.cita_id
        WHERE h.paciente_documentoid = p_paciente_doc
        ORDER BY c.fecha DESC;
END;
/

CREATE OR REPLACE PROCEDURE SP_GUARDAR_HISTORIA (
    p_cita_id IN NUMBER,
    p_diagnostico IN VARCHAR2,
    p_tratamiento IN VARCHAR2,
    p_observaciones IN VARCHAR2,
    p_resultado OUT NUMBER
) AS
    v_count NUMBER;
    v_historia_id NUMBER;
    v_paciente_doc VARCHAR2(20);
    v_doctor_doc VARCHAR2(20);
    v_especialidad_id NUMBER;
BEGIN
    SELECT COUNT(*) INTO v_count
    FROM s_historias_clinicas
    WHERE cita_id = p_cita_id;
    
    IF v_count > 0 THEN
        UPDATE s_historias_clinicas
        SET diagnostico = p_diagnostico,
            tratamiento = p_tratamiento,
            observaciones = p_observaciones
        WHERE cita_id = p_cita_id;
    ELSE
        SELECT paciente_documentoid, doctor_documentoid, especialidad_id
        INTO v_paciente_doc, v_doctor_doc, v_especialidad_id
        FROM s_citas
        WHERE cita_id = p_cita_id;
        
        SELECT NVL(MAX(historia_id), 0) + 1
        INTO v_historia_id
        FROM s_historias_clinicas;
        
        INSERT INTO s_historias_clinicas (
            historia_id, paciente_documentoid, doctor_documentoid,
            cita_id, especialidad_id, diagnostico, tratamiento, observaciones
        ) VALUES (
            v_historia_id, v_paciente_doc, v_doctor_doc,
            p_cita_id, v_especialidad_id, p_diagnostico, p_tratamiento, p_observaciones
        );
    END IF;
    
    p_resultado := 1;
    COMMIT;
EXCEPTION
    WHEN OTHERS THEN
        p_resultado := 0;
        ROLLBACK;
END;
/

CREATE OR REPLACE PROCEDURE SP_INSERTAR_ESPECIALIDAD (
    p_especialidad_id IN NUMBER,
    p_nombre IN VARCHAR2,
    p_resultado OUT NUMBER
) AS
BEGIN
    INSERT INTO especialidades (especialidad_id, nombre)
    VALUES (p_especialidad_id, p_nombre);
    p_resultado := 1;
    COMMIT;
EXCEPTION
    WHEN OTHERS THEN
        p_resultado := 0;
        ROLLBACK;
END;
/

CREATE OR REPLACE PROCEDURE SP_ACTUALIZAR_ESPECIALIDAD (
    p_especialidad_id IN NUMBER,
    p_nombre IN VARCHAR2,
    p_resultado OUT NUMBER
) AS
BEGIN
    UPDATE especialidades SET
        nombre = p_nombre
    WHERE especialidad_id = p_especialidad_id;
    
    IF SQL%ROWCOUNT > 0 THEN
        p_resultado := 1;
    ELSE
        p_resultado := 0;
    END IF;
    COMMIT;
EXCEPTION
    WHEN OTHERS THEN
        p_resultado := 0;
        ROLLBACK;
END;
/

CREATE OR REPLACE PROCEDURE SP_INSERTAR_EPS (
    p_eps_id IN NUMBER,
    p_nombre IN VARCHAR2,
    p_telefono IN VARCHAR2,
    p_correo IN VARCHAR2,
    p_resultado OUT NUMBER
) AS
BEGIN
    INSERT INTO eps (eps_id, nombre, telefono, correo)
    VALUES (p_eps_id, p_nombre, p_telefono, p_correo);
    p_resultado := 1;
    COMMIT;
EXCEPTION
    WHEN OTHERS THEN
        p_resultado := 0;
        ROLLBACK;
END;
/

CREATE OR REPLACE PROCEDURE SP_ACTUALIZAR_EPS (
    p_eps_id IN NUMBER,
    p_nombre IN VARCHAR2,
    p_telefono IN VARCHAR2,
    p_correo IN VARCHAR2,
    p_resultado OUT NUMBER
) AS
BEGIN
    UPDATE eps SET
        nombre = p_nombre,
        telefono = p_telefono,
        correo = p_correo
    WHERE eps_id = p_eps_id;
    
    IF SQL%ROWCOUNT > 0 THEN
        p_resultado := 1;
    ELSE
        p_resultado := 0;
    END IF;
    COMMIT;
EXCEPTION
    WHEN OTHERS THEN
        p_resultado := 0;
        ROLLBACK;
END;
/

CREATE OR REPLACE PROCEDURE SP_INSERTAR_CIUDAD (
    p_ciudad_id IN NUMBER,
    p_nombre IN VARCHAR2,
    p_departamento IN VARCHAR2,
    p_pais IN VARCHAR2,
    p_resultado OUT NUMBER
) AS
BEGIN
    INSERT INTO ciudades (ciudad_id, nombre, departamento, pais)
    VALUES (p_ciudad_id, p_nombre, p_departamento, p_pais);
    p_resultado := 1;
    COMMIT;
EXCEPTION
    WHEN OTHERS THEN
        p_resultado := 0;
        ROLLBACK;
END;
/

CREATE OR REPLACE PROCEDURE SP_ACTUALIZAR_CIUDAD (
    p_ciudad_id IN NUMBER,
    p_nombre IN VARCHAR2,
    p_departamento IN VARCHAR2,
    p_pais IN VARCHAR2,
    p_resultado OUT NUMBER
) AS
BEGIN
    UPDATE ciudades SET
        nombre = p_nombre,
        departamento = p_departamento,
        pais = p_pais
    WHERE ciudad_id = p_ciudad_id;
    
    IF SQL%ROWCOUNT > 0 THEN
        p_resultado := 1;
    ELSE
        p_resultado := 0;
    END IF;
    COMMIT;
EXCEPTION
    WHEN OTHERS THEN
        p_resultado := 0;
        ROLLBACK;
END;
/

CREATE OR REPLACE PROCEDURE SP_INSERTAR_RESPONSABLE (
    p_documento IN VARCHAR2,
    p_primer_nombre IN VARCHAR2,
    p_segundo_nombre IN VARCHAR2,
    p_primer_apellido IN VARCHAR2,
    p_segundo_apellido IN VARCHAR2,
    p_telefono IN VARCHAR2,
    p_correo IN VARCHAR2,
    p_parentesco IN VARCHAR2,
    p_direccion IN VARCHAR2,
    p_barrio IN VARCHAR2,
    p_calle IN VARCHAR2,
    p_ciudad_id IN NUMBER,
    p_ocupacion IN VARCHAR2,
    p_resultado OUT NUMBER
) AS
BEGIN
    INSERT INTO responsables (
        documentoid, primer_nombre, segundo_nombre, primer_apellido,
        segundo_apellido, telefono, correo, parentesco, direccion, barrio,
        calle, ciudad_id, ocupacion
    ) VALUES (
        p_documento, p_primer_nombre, p_segundo_nombre, p_primer_apellido,
        p_segundo_apellido, p_telefono, p_correo, p_parentesco, p_direccion,
        p_barrio, p_calle, p_ciudad_id, p_ocupacion
    );
    p_resultado := 1;
    COMMIT;
EXCEPTION
    WHEN OTHERS THEN
        p_resultado := 0;
        ROLLBACK;
END;
/

CREATE OR REPLACE PROCEDURE SP_ACTUALIZAR_RESPONSABLE (
    p_documento IN VARCHAR2,
    p_primer_nombre IN VARCHAR2,
    p_segundo_nombre IN VARCHAR2,
    p_primer_apellido IN VARCHAR2,
    p_segundo_apellido IN VARCHAR2,
    p_telefono IN VARCHAR2,
    p_correo IN VARCHAR2,
    p_parentesco IN VARCHAR2,
    p_direccion IN VARCHAR2,
    p_barrio IN VARCHAR2,
    p_calle IN VARCHAR2,
    p_ciudad_id IN NUMBER,
    p_ocupacion IN VARCHAR2,
    p_resultado OUT NUMBER
) AS
BEGIN
    UPDATE responsables SET
        primer_nombre = p_primer_nombre,
        segundo_nombre = p_segundo_nombre,
        primer_apellido = p_primer_apellido,
        segundo_apellido = p_segundo_apellido,
        telefono = p_telefono,
        correo = p_correo,
        parentesco = p_parentesco,
        direccion = p_direccion,
        barrio = p_barrio,
        calle = p_calle,
        ciudad_id = p_ciudad_id,
        ocupacion = p_ocupacion
    WHERE documentoid = p_documento;
    
    IF SQL%ROWCOUNT > 0 THEN
        p_resultado := 1;
    ELSE
        p_resultado := 0;
    END IF;
    COMMIT;
EXCEPTION
    WHEN OTHERS THEN
        p_resultado := 0;
        ROLLBACK;
END;
/


-- ==========================================
-- VERIFICAR QUE TODO SE CREÓ CORRECTAMENTE
-- ==========================================

SELECT object_name, object_type, status 
FROM user_objects 
WHERE object_type IN ('PROCEDURE', 'FUNCTION')
ORDER BY object_type, object_name;