create or replace 
FUNCTION checkParents(child_r IN MokyklosVaikai%ROWTYPE) RETURN NUMBER
AS
  schoolRegion_v NVARCHAR2(255);
  mamosMokyklosRegionas_v NVARCHAR2(255);
  tecioMokyklosRegionas_v NVARCHAR2(255);
BEGIN
  printText('Checking parents');
  SELECT rajonas INTO schoolRegion_v FROM Mokykla WHERE Mokykla.numeris = child_r.numeris;
  
  BEGIN
    SELECT Mokykla.rajonas INTO mamosMokyklosRegionas_v FROM Vaikas 
      INNER JOIN Tevai ON Tevai.kodas = Vaikas.mamosKodas 
      INNER JOIN MokyklosVaikai ON MokyklosVaikai.vaikoKodas = Tevai.kodas
      INNER JOIN Mokykla ON Mokykla.numeris = MokyklosVaikai.numeris
      WHERE Vaikas.vaikoKodas = child_r.vaikoKodas;
    EXCEPTION WHEN OTHERS THEN mamosMokyklosRegionas_v := 'Nera duomenu';
  END;
    
  BEGIN
    SELECT Mokykla.rajonas INTO tecioMokyklosRegionas_v FROM Vaikas 
      INNER JOIN Tevai ON Tevai.kodas = vaikas.teciokodas
      INNER JOIN MokyklosVaikai ON MokyklosVaikai.vaikoKodas = Tevai.kodas
      INNER JOIN Mokykla ON Mokykla.numeris = MokyklosVaikai.numeris
      WHERE Vaikas.vaikoKodas = child_r.vaikoKodas;
    EXCEPTION WHEN OTHERS THEN tecioMokyklosRegionas_v := 'Nera duomenu';
  END;
    
  CASE WHEN (mamosMokyklosRegionas_v = 'Nera duomenu' AND tecioMokyklosRegionas_v = 'Nera duomenu') THEN RETURN 0;
       WHEN (schoolRegion_v != mamosMokyklosRegionas_v AND schoolRegion_v != tecioMokyklosRegionas_v) THEN RETURN 1; 
  ELSE RETURN 0;
  END CASE;
END checkParents;
/