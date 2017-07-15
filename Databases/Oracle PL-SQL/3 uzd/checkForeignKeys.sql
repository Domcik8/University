create or replace 
FUNCTION checkForeignKeys(child_r IN MokyklosVaikai%ROWTYPE) RETURN NUMBER
AS
BEGIN
  printText('Checking foreign keys');
  
  IF(NOT EXISTS(SELECT * FROM Vaikas WHERE Vaikas.vaikoKodas = child_r.vaikoKodas)
     || NOT EXISTS(SELECT * FROM Vaikas WHERE Mokukla.numeris = child_r.numeris))
     RETURN 1;
     ELSE RETURN 0;
  END IF;
END checkForeignKeys;
/