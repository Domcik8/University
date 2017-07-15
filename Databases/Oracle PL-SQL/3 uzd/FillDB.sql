INSERT INTO Rajonas VALUES ('Karoliniskes');
INSERT INTO Rajonas VALUES ('Justiniskies');
INSERT INTO Rajonas VALUES ('Pasilaiciai');

INSERT INTO Mokykla VALUES ('Zeminos Gimnazija', 'Pasilaiciu 1', 'Pasilaiciai');
INSERT INTO Mokykla VALUES ('Jono Pauliaus II Gimnazija', 'Karoliniskes 2','Karoliniskes');
INSERT INTO Mokykla VALUES ('Jono Pauliaus II Progimnazija', 'Justiniskies 3','Justiniskies');

INSERT INTO Tevai VALUES ('1', 'Karoliniskes', 'Jonas', 'Snow');
INSERT INTO Tevai VALUES ('2', 'Karoliniskes', 'Arya', 'Stark');
INSERT INTO Tevai VALUES ('3', 'Justiniskies', 'Robertas', 'Stark');
INSERT INTO Tevai VALUES ('4', 'Justiniskies', 'Sansa', 'Stark');
INSERT INTO Tevai VALUES ('5', 'Pasilaiciai', 'Bronas', 'Stark');

INSERT INTO Vaikas VALUES ('1', 'Jonas', 'Snow', null, null);
INSERT INTO Vaikas VALUES ('2', 'Arya', 'Stark', null, null);
INSERT INTO Vaikas VALUES ('4', 'Sansa', 'Stark', null, null);
INSERT INTO Vaikas VALUES ('6', 'Povilas', 'Stark', '2', '1');
INSERT INTO Vaikas VALUES ('7', 'Petras', 'Stark', '2', '1');
INSERT INTO Vaikas VALUES ('8', 'Magdalena', 'Stark', '4', '3');
INSERT INTO Vaikas VALUES ('9', 'Juliana', 'Stark', '4', '3');

INSERT INTO MokyklosVaikai VALUES ('Jono Pauliaus II Gimnazija', '1');
INSERT INTO MokyklosVaikai VALUES ('Jono Pauliaus II Progimnazija', '2');
INSERT INTO MokyklosVaikai VALUES ('Zeminos Gimnazija', '4');
INSERT INTO MokyklosVaikai VALUES ('Jono Pauliaus II Gimnazija', '6');
INSERT INTO MokyklosVaikai VALUES ('Jono Pauliaus II Gimnazija', '7');
INSERT INTO MokyklosVaikai VALUES ('Zeminos Gimnazija', '8');
INSERT INTO MokyklosVaikai VALUES ('Jono Pauliaus II Progimnazija', '9');