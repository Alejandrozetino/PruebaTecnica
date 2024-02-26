create database Acme_DB;

Use Acme_DB;


CREATE TABLE Usuarios
     ( Id                           INT           NOT NULL IDENTITY
     , NombreUsuario                VARCHAR(255)  NOT NULL 
     , CorreoElectronico            VARCHAR(255)  NOT NULL 
     , Contraseña                   VARCHAR(255)  NOT NULL
	 , FechaRegistro				DATETIME	  NOT NULL
	 ,CONSTRAINT PK_Usuarios        Primary Key   (Id)
	 )
go

CREATE TABLE Encuestas
     ( Id                           INT           NOT NULL IDENTITY
     , IdUsuario                    INT           NOT NULL Foreign Key References Usuarios(Id)
     , Nombre                       VARCHAR(255)  NOT NULL 
     , Descripcion                  VARCHAR(255)  NOT NULL
	 , Link          				VARCHAR(255)  NOT NULL
	 , FechaCreación                DATETIME      NOT NULL
	 , CONSTRAINT PK_Encuestas      Primary Key   (Id)  
	 )
go

CREATE TABLE CamposEncuestas
     ( Id                            INT           NOT NULL IDENTITY
     , IdEncuesta                    INT           NOT NULL Foreign Key References Encuestas(Id)
     , NombreCampo                   VARCHAR(255)  NOT NULL 
     , TituloCampo                   VARCHAR(255)  NOT NULL --Esté se mostrará en pantalla
	 , EsRequerido          	     Bit           NOT NULL
	 , TipoCampo                     VARCHAR(100)  NOT NULL
	 , CONSTRAINT PK_CamposEncuestas Primary Key   (Id)  
	 )
go

CREATE TABLE ResultadosEncuestas
     ( Id                                INT           NOT NULL IDENTITY
     , IdEncuesta                        INT           NOT NULL Foreign Key References Encuestas(Id)
	 , IdCamposEncuesta                  INT           NOT NULL Foreign Key References CamposEncuestas(Id)
     , Respuesta                         VARCHAR(Max)  Null 
	 , CONSTRAINT PK_ResultadosEncuestas Primary Key   (Id)  
	 )
go
