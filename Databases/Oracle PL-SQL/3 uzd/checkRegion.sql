create or replace 
FUNCTION checkRegion(child_r IN MokyklosVaikai%ROWTYPE) RETURN NUMBER
AS
  schoolRegion_v NVARCHAR2(255);
  childRegion_v NVARCHAR2(255);
BEGIN
  printText('Checking region');
  SELECT rajonas  INTO schoolRegion_v FROM Mokykla WHERE Mokykla.numeris = child_r.numeris;
  SELECT Tevai.rajonas INTO childRegion_v FROM Vaikas INNER JOIN Tevai ON Tevai.kodas = Vaikas.mamosKodas WHERE Vaikas.vaikoKodas = child_r.vaikoKodas;
  CASE WHEN (schoolRegion_v = childRegion_v) 
    THEN RETURN 0; 
  ELSE RETURN 1;
  END CASE;
  EXCEPTION WHEN OTHERS THEN RETURN 0;
END checkRegion;
/