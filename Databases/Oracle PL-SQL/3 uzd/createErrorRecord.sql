create or replace 
PROCEDURE createErrorRecord(child_r IN MokyklosVaikai%ROWTYPE, reason IN VARCHAR2)
AS
BEGIN
  printText('Creating error record: ' || reason);
  INSERT INTO BlogiDuomenys VALUES (child_r.vaikoKodas, reason);
END createErrorRecord;
/