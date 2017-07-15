CREATE TABLE doli1680.Atlikejas(
	Slapyvardis			CHAR(25)	NOT NULL 	PRIMARY KEY,
	Karjeros_pradzia	SMALLINT	NOT NULL,
	Tautybe				CHAR(15)	NOT NULL
);

CREATE TABLE doli1680.Albumas(
	Pavadinimas			CHAR(25)	NOT NULL	PRIMARY KEY,
	Turis_Min			DECIMAL(5,2),
	Atlikejas			CHAR(25)	NOT NULL,
	FOREIGN KEY(Atlikejas) REFERENCES Atlikejas
	ON DELETE SET NULL
);

CREATE TABLE doli1680.Autorius(
	Nr					SERIAL		NOT NULL	 PRIMARY KEY,
	Slapyvardis			CHAR(25)	NOT NULL,
	Tautybe				CHAR(15),
	Karjeros_pradzia	SMALLINT
);

CREATE TABLE doli1680.Daina(
	Pavadinimas			CHAR(100)	NOT NULL,
	Trukme_Min			DECIMAL(5,2)
	CONSTRAINT  DainosTrukme CHECK(Trukme_Min > 0 AND Trukme_Min < 999),
	Zanras				CHAR(15)	DEFAULT 'Nenustatyta',
	Autorius			SMALLINT	NOT NULL,
	PRIMARY KEY (PAVADINIMAS),
	FOREIGN KEY(Autorius) REFERENCES Autorius
	ON DELETE SET NULL
);

CREATE TABLE doli1680.Priklauso(
	Dainos_Pavadinimas	CHAR(100)	NOT NULL,
	Albumo_Pavadinimas 	CHAR(50)	NOT NULL,
	PRIMARY KEY(Dainos_Pavadinimas, Albumo_Pavadinimas),
	FOREIGN KEY(Dainos_Pavadinimas) 
	REFERENCES Daina
	ON DELETE SET NULL,
	FOREIGN KEY(Albumo_Pavadinimas) 
	REFERENCES Albumas
	ON DELETE SET NULL
);

ALTER TABLE Albumas
	ADD CONSTRAINT  TurioDidis CHECK(Turis_Min > 0 AND Turis_Min < 999);
		
ALTER TABLE Autorius
	ALTER Tautybe SET DEFAULT 'Nezinoma';
	
CREATE UNIQUE INDEX index_autorius
	ON Autorius (Slapyvardis);
	
ALTER TABLE Daina 
	ALTER Zanras DROP DEFAULT;
	
ALTER TABLE Daina
	ALTER Zanras SET DEFAULT 'Klasika';

ALTER TABLE Daina
	ADD CONSTRAINT ZanroTipas CHECK (Zanras IN ('R&B', 'Rock', 'POP', 'Electro', 'Rap', 'Classic'));
	
CREATE INDEX index_daina
	ON daina(zanras);
	
CREATE VIEW Dainos_atlikejas
	AS SELECT Daina.Pavadinimas, Zanras, Slapyvardis AS Atlikejas, Tautybe AS "Atlikejo_tautybe"
	FROM Daina, Atlikejas, Albumas, Priklauso
	GROUP BY Dainos_Pavadinimas, Albumo_Pavadinimas, 
	Daina.Pavadinimas, Albumas.Pavadinimas, Atlikejas.Slapyvardis
	HAVING ((Dainos_Pavadinimas = Daina.Pavadinimas) 
	AND (Albumo_Pavadinimas = Albumas.Pavadinimas)
	AND (Atlikejas = Slapyvardis));
	
CREATE VIEW Albumo_ilgis
	AS SELECT Albumas.Pavadinimas, 
	Sum(Trukme_Min) AS Trukme_Min, 
	Count(*) AS Dainu_skaicius
	FROM Daina, Priklauso AS A, Albumas
	WHERE A.Dainos_Pavadinimas = Daina.Pavadinimas
	AND Albumas.Pavadinimas = A.Albumo_Pavadinimas
	GROUP BY Albumas.Pavadinimas;
	
CREATE FUNCTION DainuSkaiciusAlbume() 
	RETURNS "trigger" AS $$
	BEGIN
	IF (SELECT COUNT(*) 
	FROM Priklauso AS A
	WHERE A.Albumo_Pavadinimas = NEW.Albumo_Pavadinimas) > 20
	THEN RAISE EXCEPTION 'Virsytas albumo dainu skaicius';
	END IF;
	RETURN NEW;
	END;
	$$
	LANGUAGE plpgsql;

CREATE TRIGGER DainuSkaiciusAlbume
	BEFORE INSERT ON Priklauso
	FOR EACH ROW 
	EXECUTE PROCEDURE DainuSkaiciusAlbume();
	
CREATE FUNCTION AlbumuSkaicius()
	RETURNS "trigger" AS $$
	BEGIN
	IF(SELECT COUNT(*) FROM Albumas) >= 25
	THEN RAISE EXCEPTION 'Virsytas albumu skaicius';
	END IF;
	RETURN NEW;
	END;
	$$
	LANGUAGE plpgsql;
	
CREATE TRIGGER AlbumuSkaicius
	BEFORE INSERT ON Albumas
	FOR EACH ROW
	EXECUTE PROCEDURE AlbumuSkaicius();