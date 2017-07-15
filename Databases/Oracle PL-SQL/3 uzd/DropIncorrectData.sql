create or replace 
PROCEDURE dropIncorrectData
AS
BEGIN
  printText('');
  printText('Ismetame neteisingus duomenys');
  DELETE MokyklosVaikai WHERE EXISTS(SELECT * FROM BlogiDuomenys WHERE MokyklosVaikai.vaikoKodas = BlogiDuomenys.vaikoKodas);
  printText('Buvo ismesta ' || sql%rowcount || ' irasu is lenteles BlogiDuomenys');
END dropIncorrectData;
/
 