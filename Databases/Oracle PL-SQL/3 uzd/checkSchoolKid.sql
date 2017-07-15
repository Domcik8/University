create or replace 
PROCEDURE checkSchoolChild(child_r MokyklosVaikai%ROWTYPE)
AS
BEGIN
  printText('');
  printText('Checking number: ' || child_r.numeris || ' child come: ' || child_r.vaikoKodas);
  IF (checkRegion(child_r) = 1) THEN
    createErrorRecord(child_r, 'Rajonas, kuriame gyvena vaikas, nesutampa su mokyklos rajonu');
  END IF;
  IF (checkParents(child_r) = 1) THEN
    createErrorRecord(child_r, 'Ne vienas is tevu nera lankes momyklos, kuria lanko vaikas');
  END IF;
END checkSchoolChild;
/