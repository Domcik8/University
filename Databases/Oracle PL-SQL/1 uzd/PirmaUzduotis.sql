DECLARE
  n PLS_INTEGER; -- sekos ilgis
  TYPE numberArray IS TABLE OF PLS_INTEGER
  INDEX BY PLS_INTEGER;
  v_fibonacciArray numberArray;

PROCEDURE readLine(n OUT PLS_INTEGER)
AS
BEGIN
  n := &n;
END readLine;

PROCEDURE getFibonacciArray(n IN PLS_INTEGER, v_fibonacciArray OUT numberArray)
AS
BEGIN
  CASE WHEN n = 0 THEN NULL;
       WHEN n = 1 THEN v_fibonacciArray(1) := 0;
       WHEN n = 2 THEN v_fibonacciArray(1) := 0; v_fibonacciArray(2) := 1;
       ELSE 
        v_fibonacciArray(1) := 0; v_fibonacciArray(2) := 1;
        FOR counter IN 3 .. n LOOP
          v_fibonacciArray(counter) := v_fibonacciArray(counter - 2) + v_fibonacciArray(counter - 1);
        END LOOP;
  END CASE;
END getFibonacciArray;

PROCEDURE printNumber(n IN PLS_INTEGER)
AS
BEGIN
  DBMS_OUTPUT.PUT_LINE(n);
END printNumber;

PROCEDURE printArray(v_fibonacciArray IN numberArray)
AS
v_i PLS_INTEGER := 1;
BEGIN
  WHILE v_i <= v_fibonacciArray.Count LOOP
    printNumber(v_fibonacciArray(v_i));
    v_i := v_i + 1;
  END LOOP;
END printArray;

BEGIN
  readLine(n);
  getFibonacciArray(n, v_fibonacciArray);
  printArray(v_fibonacciArray);
END;