create or replace 
PROCEDURE insertSchoolChild(schoolID IN VARCHAR2, childID IN VARCHAR2)
AS
  child_r child_c%ROWTYPE;
  does_not_live_in_this_region_exception EXCEPTION;
  parents_did_not_learn_in_this_school_exception EXCEPTION;
  no_data_about_child_or_school EXCEPTION;
  PRAGMA EXCEPTION_INIT
  (does_not_live_in_this_region_exception, -1);
  PRAGMA EXCEPTION_INIT
  (parents_did_not_learn_in_this_school_exception, -2);
  PRAGMA EXCEPTION_INIT
  (no_data_about_child_or_school, -3);
BEGIN
  IF (schoolID = null || schoolID = '' || childID = null || childID = '') THEN
     RAISE no_data_about_child_or_school;
  END IF;
  child_r.numeris = schoolID;
  child_r.vaikoKodas = childID;
  
  IF (checkForeignKeys(child_r) = 1) THEN
    RAISE no_data_about_child_or_school;
  END IF;
  
  IF (checkRegion(child_r) = 1) THEN
    RAISE does_not_live_in_this_region_exception;
  END IF;
  
  IF (checkParents(child_r) = 1) THEN
    RAISE parents_did_not_learn_in_this_school_exception
  END IF;
  
  INSERT INTO MokyklosVaikai VALUES (schoolID, childID);
  printText('Vaikas sekmingai uzregistruotas');
  
  EXCEPTION WHEN does_not_live_in_this_region_exception  printText('Rajonas, kuriame gyvena vaikas, nesutampa su mokyklos rajonu');
            WHEN parents_did_not_learn_in_this_school_exception printText('Ne vienas is tevu nera lankes momyklos, kuria lanko vaikas');
END insertSchoolChild;
/