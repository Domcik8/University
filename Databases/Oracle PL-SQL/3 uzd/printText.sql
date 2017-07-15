create or replace 
PROCEDURE printText(text IN NVARCHAR2)
AS
BEGIN
  DBMS_OUTPUT.PUT_LINE(text);
END printText;
/