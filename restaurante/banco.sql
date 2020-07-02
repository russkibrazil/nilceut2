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
	CPF BIGINT(11) NOT NULL, 
    Nome VARCHAR(50) NOT NULL, 
    Dnasc DATETIME NOT NULL, 
    TipoUsuario ENUM('ALUNO', 'FUNCIONARIO', 'PROFESSOR'),
	PRIMARY KEY (CPF)
);

CREATE TABLE aluno (
	CPF BIGINT(11) NOT NULL, 
    RA VARCHAR(15) NOT NULL,
	FOREIGN KEY (CPF) REFERENCES pessoa(CPF) ON DELETE RESTRICT ON UPDATE CASCADE
);

CREATE TABLE departamento (Nome VARCHAR(50) NOT NULL, 
	PRIMARY KEY (Nome));


CREATE TABLE servidor (
	CPF BIGINT(11) NOT NULL, 
    Departamento VARCHAR(50),
	FOREIGN KEY (CPF) REFERENCES pessoa(CPF) ON DELETE RESTRICT ON UPDATE CASCADE,
	FOREIGN KEY (Departamento) REFERENCES departamento(Nome) ON DELETE RESTRICT ON UPDATE CASCADE
);

CREATE TABLE telefone (
	CPF BIGINT(11) NOT NULL, 
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

CREATE TABLE refeicao (Id INTEGER AUTO_INCREMENT NOT NULL, 
    Base VARCHAR(50) NOT NULL,
	Guarnicao VARCHAR(50) NOT NULL, 
    Salada VARCHAR(50) NOT NULL, 
    Sobremesa VARCHAR(50), 
    Suco VARCHAR(50),
     PRIMARY KEY (Id),
     UNIQUE KEY `refeicao_UNIQUE` (Base, Guarnicao, Salada, Sobremesa, Suco)
);

CREATE TABLE cardapio (
	DtPreparo DATE NOT NULL, 
    QtPreparada INTEGER NOT NULL, 
    QtDisponivel INTEGER, 
    IdRefeicao INTEGER NOT NULL,
    PRIMARY KEY (DtPreparo),
    FOREIGN KEY (IdRefeicao) REFERENCES refeicao(Id) ON DELETE RESTRICT
);

CREATE TABLE compra(
	Dt DATE NOT NULL,
    CPF BIGINT(11) NOT NULL,
    FOREIGN KEY (CPF) REFERENCES pessoa(CPF) ON DELETE RESTRICT ON UPDATE CASCADE,
    FOREIGN KEY (Dt) REFERENCES cardapio(DtPreparo) ON DELETE RESTRICT
);

DROP PROCEDURE IF EXISTS Dec_compra;
DROP FUNCTION IF EXISTS ehAluno;
DELIMITER $$
CREATE PROCEDURE `Dec_compra` (IN quando DATE)
BEGIN
	UPDATE cardapio SET QtDisponivel = (QtDisponivel - 1) WHERE DtPreparo = quando;
END $$

CREATE FUNCTION ehAluno (cpf BIGINT(11))
	RETURNS BOOLEAN DETERMINISTIC
    BEGIN
		DECLARE v BOOLEAN;
        SET v = FALSE;
		IF (exists(SELECT TipoUsuario FROM Pessoa WHERE CPF = cpf AND TipoUsuario = 'ALUNO')) THEN
			SET v = TRUE;
		END IF;
	RETURN v;
    END
$$

CREATE TRIGGER `restaurante`.`gerar_refeicao_disponivel` AFTER INSERT ON `restaurante`.`cardapio` FOR EACH ROW
BEGIN
    UPDATE cardapio SET QtDisponivel = new.QtPreparada WHERE DtPreparo = new.DtPreparo;
END $$

CREATE TRIGGER `restaurante`.`incremento_compra_trigger` AFTER INSERT ON `restaurante`.`compra` FOR EACH ROW
	CALL Dec_compra(Dt);$$
/*	
CREATE TRIGGER `aluno`.`verifica_validade` BEFORE INSERT ON `restaurante`.`aluno` FOR EACH ROW
BEGIN
	IF ((SELECT ehAluno(new.CPF)) = FALSE) THEN
		ROLLBACK;
		SELECT 'Não é um aluno!';
	END IF;
END $$

CREATE TRIGGER `servidor`.`verifica_validade` BEFORE INSERT ON `restaurante`.`servidor` FOR EACH ROW
BEGIN
	IF (SELECT ehAluno(new.CPF) = TRUE) THEN
		ROLLBACK;
		SELECT 'Não é um servidor!';
	END IF;
END $$*/
DELIMITER ;
COMMIT;
INSERT INTO pessoa VALUES 
	(12345678901, 'João da Silva', str_to_date('10/10/1980', '%d/%m/%Y'), 'ALUNO'),
    (54818378461, 'João Marcelo Porto', str_to_date('20/12/1984', '%d/%m/%Y'), 'ALUNO'),
    (67877803540, 'Raul Osvaldo da Paz', str_to_date('12/11/1970', '%d/%m/%Y'), 'FUNCIONARIO'),
    (86500145690, 'Bento Lucca Theo Freitas', str_to_date('13/03/1947', '%d/%m/%Y'), 'PROFESSOR'),
    (34851174802, 'Carlos Marcos Vinicius Galvão', str_to_date('13/04/1957', '%d/%m/%Y'), 'FUNCIONARIO'),
    (37535505236, 'Malu Cecília Eduarda Lopes', str_to_date('02/11/1984', '%d/%m/%Y'), 'ALUNO'),
    (20482601523, 'Débora Marina Luiza Fernandes', str_to_date('10/01/1970', '%d/%m/%Y'), 'PROFESSOR'),
    (44532299411, 'Luís Emanuel Vieira', str_to_date('18/02/1969', '%d/%m/%Y'), 'FUNCIONARIO'),
    (77769267951, 'Letícia Mariane Adriana Rezende', str_to_date('09/02/1988', '%d/%m/%Y'), 'ALUNO'),
    (52759442179, 'Danilo Felipe da Paz', str_to_date('06/08/1965', '%d/%m/%Y'), 'FUNCIONARIO')
;

INSERT INTO aluno VALUES
	(12345678901, '12345678901'),
    (54818378461, '54818378461'),
    (37535505236, '37535505236'),
    (77769267951, '77769267951')
;
INSERT INTO departamento VALUES
	('RH'),
    ('Limpeza'),
    ('Engenharia Mecânica'),
    ('Biblioteca')
;
INSERT INTO servidor VALUES
	(67877803540, 'RH'),
    (86500145690, 'Engenharia Mecânica'),
    (34851174802, 'Limpeza'),
    (20482601523, 'Engenharia Mecânica'),
    (44532299411, 'Biblioteca'),
    (52759442179, 'Biblioteca')
;
INSERT INTO endereco VALUES
	(54818378461, 'Rua', 'Aristóteles', 635),
    (67877803540, 'Rua', 'Mendes Ribeiro', 949),
    (86500145690, 'Rua', 'Jadir da Silva', 407),
    (34851174802, 'Travessa', 'Venezuela', 698),
    (37535505236, 'Rua', 'A-1', 984),
    (20482601523, 'Rua', 'Guia Lopes', 938),
    (44532299411, 'Rua', 'do Coco', 678),
    (77769267951, 'Rua', 'Irmã Maria das Dores de Jesus', 535),
    (52759442179, 'Avenida', 'José Turquinho', 920)
;
INSERT INTO telefone VALUES
	(54818378461, 86997199367),
    (67877803540, 83981135344),
    (86500145690, 47988216213),
    (34851174802, 9537378466),
    (34851174802, 95983070606),
    (37535505236, 66988521289),
    (20482601523, 6736543627),
    (44532299411, 6825409701),
    (44532299411, 68999814986),
    (77769267951, 19985067827)
;
INSERT INTO refeicao (Base, Guarnicao, Salada, Sobremesa, Suco) VALUES
	('Arroz e Feijão', 'Filé de frango grelhado', 'Alface', 'Banana', 'Laranja'),
    ('Arroz e Feijão', 'Purê de Batata com ovos mexidos', 'Rúcula', 'Uvas', 'Uva'),
    ('Arroz integral e grão de bico', 'PTS', 'Acelga', 'Laranja', 'Limão')
;
COMMIT;