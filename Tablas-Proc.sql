USE DB_CARNE;




--TABLAS
CREATE TABLE Profesor_Transacciones
									(Cedula						CHAR(9) NOT NULL, 
									 Email						VARCHAR(50) NOT NULL, 
									 NombreP					VARCHAR(15) NOT NULL, 
									 Apellido1					VARCHAR(15) NOT NULL, 
									 Apellido2					VARCHAR(15) NOT NULL, 
									 PRIMARY KEY(Cedula)
									 );

CREATE TABLE Examen_Transacciones
									(SiglaExam					CHAR(5) NOT NULL,
									 Nombre						VARCHAR(15) NOT NULL, 
									 PRIMARY KEY(SiglaExam)
									 );

CREATE TABLE Aplica_Transacciones
									(Cedula						CHAR(9) NOT NULL,
									 SiglaExam					CHAR(5) NOT NULL,
									 Descripcion				VARCHAR(50),
									 FOREIGN KEY(Cedula) REFERENCES Profesor_Transacciones(Cedula) ON DELETE CASCADE,
									 FOREIGN KEY(SiglaExam) REFERENCES Examen_Transacciones(SiglaExam) ON DELETE CASCADE,
									 PRIMARY KEY(Cedula, SiglaExam)
									 );




--DATOS
INSERT Profesor_Transacciones( Cedula, Email, NombreP, Apellido1, Apellido2 ) VALUES( '111111111', '111111111@gmail.com', 'Profesor1', 'Apellido1', 'Apellido2' );
INSERT Profesor_Transacciones( Cedula, Email, NombreP, Apellido1, Apellido2 ) VALUES( '222222222', '222222222@gmail.com', 'Profesor2', 'Apellido1', 'Apellido2' );
INSERT Profesor_Transacciones( Cedula, Email, NombreP, Apellido1, Apellido2 ) VALUES( '333333333', '333333333@gmail.com', 'Profesor2', 'Apellido1', 'Apellido2' );
INSERT Examen_Transacciones( SiglaExam, Nombre ) VALUES( '11111', 'Examen1' );
INSERT Examen_Transacciones( SiglaExam, Nombre ) VALUES( '22222', 'Examen2' );
INSERT Examen_Transacciones( SiglaExam, Nombre ) VALUES( '33333', 'Examen3' );
INSERT Aplica_Transacciones( Cedula, SiglaExam ) VALUES( '111111111', '11111' );
INSERT Aplica_Transacciones( Cedula, SiglaExam ) VALUES( '222222222', '22222' );
INSERT Aplica_Transacciones( Cedula, SiglaExam ) VALUES( '333333333', '33333' );

--PROCS SIN TRANSACCIONES
GO
	CREATE PROCEDURE Aplicar_Examen_Sin_Transaccion ( @CedulaP CHAR(9), @Email VARCHAR(50), @NombreP VARCHAR(15), @Apellido1 VARCHAR(15), @Apellido2 VARCHAR(15), 
													  @SiglaExam CHAR(5), @NombreE VARCHAR(15),
													  @Descripcion VARCHAR(50),
													  @NumeroError int OUTPUT)
	AS BEGIN
		SET @NumeroError = 0;
		--Existe profe y examen
		IF @Email IS NULL AND @NombreE IS NULL BEGIN
			INSERT Aplica_Transacciones( Cedula, SiglaExam, Descripcion ) VALUES( @CedulaP, @SiglaExam, @Descripcion );
		END
		ELSE BEGIN
			--Existe profe
			IF @Email IS NOT NULL AND @NombreE IS NULL BEGIN
				INSERT Profesor_Transacciones( Cedula, Email, NombreP, Apellido1, Apellido2 ) VALUES( @CedulaP, @Email, @NombreP, @Apellido1, @Apellido2 );
				INSERT Aplica_Transacciones( Cedula, SiglaExam, Descripcion ) VALUES( @CedulaP, @SiglaExam, @Descripcion );
			END
			ELSE BEGIN
				--Existe examen
				IF @NombreE IS NOT NULL AND @Email IS NULL BEGIN
					INSERT Examen_Transacciones( SiglaExam, Nombre ) VALUES( @SiglaExam, @NombreE);
					INSERT Aplica_Transacciones( Cedula, SiglaExam, Descripcion ) VALUES( @CedulaP, @SiglaExam, @Descripcion );
				END
				ELSE BEGIN
					IF @Email IS NOT NULL AND @NombreE IS NOT NULL BEGIN
						INSERT Profesor_Transacciones( Cedula, Email, NombreP, Apellido1, Apellido2 ) VALUES( @CedulaP, @Email, @NombreP, @Apellido1, @Apellido2 );
						INSERT Examen_Transacciones( SiglaExam, Nombre ) VALUES( @SiglaExam, @NombreE);
						INSERT Aplica_Transacciones( Cedula, SiglaExam, Descripcion ) VALUES( @CedulaP, @SiglaExam, @Descripcion );
					END
					ELSE BEGIN
						SET @NumeroError = 1;
					END
				END
			END
		END
	END
GO