CREATE TABLE Clientes(
	idCliente    	INT            NOT NULL IDENTITY PRIMARY KEY,
	nome         	NVARCHAR(100)  NOT NULL,
	username       NVARCHAR(100)   NOT NULL,
	password       VARCHAR(300)            NOT NULL,
	morada       	NVARCHAR(100),
	codigoPostal 	NVARCHAR(8)    CHECK (codigoPostal LIKE '____-___'),
	localidade   	NVARCHAR(30),
	telefone     	NVARCHAR(9),
	fax          	NVARCHAR(9),
	telemovel    	NVARCHAR(9), 
	email        	NVARCHAR(100)  UNIQUE CHECK(email LIKE('%@%.%')), 
	site         	NVARCHAR(100), 
	representante  	NVARCHAR(100),
	observacoes  	TEXT, 
	dataCriacao  	DATE 		   NOT NULL DEFAULT getdate(),
	ultimoUpdate   	DATE 		   NOT NULL DEFAULT getdate(),
	ativo           BIT			   DEFAULT(1)
)

CREATE TABLE Maquinas(
	idMaquina      INT             NOT NULL IDENTITY PRIMARY KEY,
	idCliente      INT             NOT NULL REFERENCES Clientes(idCliente),
	descricao      TEXT		       NOT NULL,
	ip             NVARCHAR(100),
	loginAcesso    NVARCHAR(50),
	passwordAcesso NVARCHAR(100),
	dataCriacao    DATE 		   NOT NULL DEFAULT getdate(),
	ultimoUpdate   DATE 		   NOT NULL DEFAULT getdate(),
	ativo          BIT			   DEFAULT(1)
)

CREATE TABLE Funcionarios(
	idFuncionario  INT             NOT NULL IDENTITY PRIMARY KEY,
	nome           NVARCHAR(50)    NOT NULL,
	username       NVARCHAR(100)   NOT NULL,
	password       VARCHAR(300)            NOT NULL,
	dataNascimento DATE,
	foto           NVARCHAR(500),
	nCC            NVARCHAR(12), 
	telefone       NVARCHAR(9), 
	email          NVARCHAR(100)   UNIQUE CHECK(email LIKE('%@%.%')), 
	observacoes    TEXT,
	dataCriacao    DATE 		   NOT NULL DEFAULT getdate(),
	ultimoUpdate   DATE 		   NOT NULL DEFAULT getdate(), 
	ativo          BIT			   DEFAULT(1)
)

CREATE TABLE Assistencias(
	idAssistencia  INT             NOT NULL IDENTITY PRIMARY KEY,
	idCliente      INT     		   NOT NULL REFERENCES Clientes(idCliente),
	idMaquina      INT    		   NOT NULL REFERENCES Maquinas(idMaquina),
	idFuncionario  INT     		   NOT NULL REFERENCES Funcionarios(idFuncionario),
	dataInicio     DATE    		   DEFAULT getdate(),
	dataFim        DATE    		   DEFAULT getdate(),
	horaInicio     TIME    		   DEFAULT CONVERT(TIME, getdate()),
	horaFim        TIME    		   DEFAULT CONVERT(TIME, getdate()),
	concluida      BIT,
	preco          MONEY,
	observacoes    TEXT,
	dataCriacao    DATE 		   NOT NULL DEFAULT getdate(),
	ultimoUpdate   DATE 		   NOT NULL DEFAULT getdate(),
	ativo          BIT			   DEFAULT(1)
)