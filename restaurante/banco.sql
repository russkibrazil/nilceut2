CREATE DATABASE IF NOT EXISTS restaurante;
USE restaurante;

DROP TABLE IF EXISTS endereco;
DROP TABLE IF EXISTS compra;
DROP TABLE IF EXISTS cardapio;
DROP TABLE IF EXISTS telefone;
DROP TABLE IF EXISTS servidor;
DROP TABLE IF EXISTS aluno;
DROP TABLE IF EXISTS pessoa;
DROP TABLE IF EXISTS departamento;
DROP TABLE IF EXISTS refeicao;

CREATE TABLE pessoa (
	CPF BIGINT(11), 
    Nome VARCHAR(50) NOT NULL, 
    Dnasc DATETIME NOT NULL, 
    TipoUsuario ENUM('ALUNO', 'FUNCIONARIO', 'PROFESSOR'),
	PRIMARY KEY (CPF)
);

CREATE TABLE aluno (
	CPF BIGINT(11), 
    RA VARCHAR(15),
	FOREIGN KEY (CPF) REFERENCES pessoa(CPF) ON DELETE RESTRICT ON UPDATE CASCADE
);

CREATE TABLE departamento (Nome VARCHAR(50), 
	PRIMARY KEY (Nome));


CREATE TABLE servidor (
	CPF BIGINT(11), 
    Departamento VARCHAR(50),
	FOREIGN KEY (CPF) REFERENCES pessoa(CPF) ON DELETE RESTRICT ON UPDATE CASCADE,
	FOREIGN KEY (Departamento) REFERENCES departamento(Nome) ON DELETE RESTRICT ON UPDATE CASCADE
);


CREATE TABLE telefone (
	CPF BIGINT(11), 
    Telefone BIGINT(11) NOT NULL,
	UNIQUE KEY `telefone_usuario` (CPF, Telefone),
    FOREIGN KEY (CPF) REFERENCES pessoa(CPF) ON DELETE RESTRICT ON UPDATE CASCADE
);


CREATE TABLE endereco (
	CPF BIGINT(11), 
    Lograduro ENUM('Aeroporto', 'Alameda', 'Área', 'Avenida', 'Campo', 'Chácara', 'Colônia', 'Condomínio','Conjunto', 'Distrito','Esplanada', 'Estação', 'Estrada', 'Favela', 'Feira','Jardim',    'Ladeira', 'Lago','Lagoa','Largo','Loteamento','Morro','Núcleo','Parque','Passarela','Pátio','Praça','Quadra','Recanto','Residencial','Rodovia','Rua','Setor','Sítio','Travessa','Trecho','Trevo','Vale','Vereda','Via','Viaduto','Viela','Vila'), 
    Identificador VARCHAR(100) NOT NULL, 
    Numero INTEGER(5),
     UNIQUE KEY `logrUNIQUE` (CPF, Lograduro),
     FOREIGN KEY (CPF) REFERENCES pessoa(CPF) ON DELETE RESTRICT ON UPDATE CASCADE
);


CREATE TABLE refeicao (Id INTEGER AUTO_INCREMENT, 
    Base VARCHAR(50) NOT NULL,
	Guarnicao VARCHAR(50) NOT NULL, 
    Salada VARCHAR(50) NOT NULL, 
    Sobremesa VARCHAR(50) NOT NULL, 
    Suco VARCHAR(50) NOT NULL,
     PRIMARY KEY (Id),
     UNIQUE KEY `refeicao_UNIQUE` (Base, Guarnicao, Salada, Sobremesa, Suco)
);


CREATE TABLE cardapio (
	DtPreparo DATE, 
    QtPreparada INTEGER NOT NULL, 
    QtDisponivel INTEGER, 
    IdRefeicao INTEGER,
    PRIMARY KEY (DtPreparo),
    FOREIGN KEY (IdRefeicao) REFERENCES refeicao(Id) ON DELETE RESTRICT
);


CREATE TABLE compra(
	Dt DATE,
    CPF BIGINT(11),
    FOREIGN KEY (CPF) REFERENCES pessoa(CPF) ON DELETE RESTRICT ON UPDATE CASCADE,
    FOREIGN KEY (Dt) REFERENCES cardapio(DtPreparo) ON DELETE RESTRICT
);

DROP PROCEDURE IF EXISTS Dec_compra;
DELIMITER $$
CREATE PROCEDURE `Dec_compra` (IN quando DATE)
BEGIN
	UPDATE cardapio SET QtDisponivel = (QtDisponivel - 1) WHERE DtPreparo = quando;
END$$
DELIMITER ;

CREATE TRIGGER `cardapio`.`gerar_refeicao_disponivel` AFTER INSERT ON `restaurante`.`cardapio` FOR EACH ROW
BEGIN
    UPDATE cardapio SET QtDisponivel = new.QtPreparada WHERE DtPreparo = new.DtPreparo;
END

CREATE TRIGGER `compra`.`incremento_compra_trigger` AFTER INSERT ON `restaurante`.`compra` FOR EACH ROW
	CALL Dec_compra(Dt);
