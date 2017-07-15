DROP TABLE MokyklosVaikai;
DROP TABLE Vaikas;
DROP TABLE Tevai;
DROP TABLE Mokykla;
DROP TABLE Rajonas;
DROP TABLE BlogiDuomenys;
/

CREATE TABLE Rajonas
(
  pavadinimas NVARCHAR2(255),
  CONSTRAINT rajonas_pavadimas_pk PRIMARY KEY (pavadinimas)
);

CREATE TABLE Mokykla
(
  numeris NVARCHAR2(255),
  adresas NVARCHAR2(255),
  rajonas NVARCHAR2(255),
  CONSTRAINT Mokykla_numeris_pk PRIMARY KEY (numeris),
  CONSTRAINT Mokykla_rajonas_fk FOREIGN KEY (rajonas) REFERENCES Rajonas
);

CREATE TABLE Tevai
(
  kodas NVARCHAR2(255),
  rajonas NVARCHAR2(255),
  vardas NVARCHAR2(255),
  pavarde NVARCHAR2(255),
  CONSTRAINT Tevai_kodas_pk PRIMARY KEY (kodas),
  CONSTRAINT Tevai_rajonas_fk FOREIGN KEY (rajonas) REFERENCES Rajonas
);

CREATE TABLE Vaikas
(
  vaikoKodas NVARCHAR2(255),
  vardas NVARCHAR2(255),
  pavarde NVARCHAR2(255),
  mamosKodas NVARCHAR2(255),
  tecioKodas NVARCHAR2(255),
  CONSTRAINT Vaikai_vaikoKodas_pk PRIMARY KEY (vaikoKodas),
  CONSTRAINT Vaikai_mamosKodas_fk FOREIGN KEY (mamosKodas) REFERENCES Tevai,
  CONSTRAINT Vaikai_tecioKodas_fk FOREIGN KEY (tecioKodas) REFERENCES Tevai
);

CREATE TABLE MokyklosVaikai
(
  numeris NVARCHAR2(255),
  vaikoKodas NVARCHAR2(255),
  CONSTRAINT MokyklosVaikai_numeris_fk FOREIGN KEY (numeris) REFERENCES Mokykla,
  CONSTRAINT MokyklosVaikai_vaikoKodas_fk FOREIGN KEY (vaikoKodas) REFERENCES Vaikas
);

CREATE TABLE BlogiDuomenys
(
  vaikoKodas NVARCHAR2(255),
  priezastis NVARCHAR2(255)
);