create or replace 
PROCEDURE checkSchoolChildren
AS
  CURSOR child_c IS SELECT * FROM MokyklosVaikai;
  child_r child_c%ROWTYPE;
BEGIN
  IF child_c%ISOPEN THEN
    CLOSE child_c;
  END IF;
  
  OPEN child_c;
  
  LOOP
  FETCH child_c INTO child_r;
  EXIT WHEN child_c%NOTFOUND;
    checkSchoolChild(child_r);
  END LOOP;
  
  CLOSE child_c;
END checkSchoolChildren;
/