CREATE OR REPLACE PROCEDURE readLine(n OUT PLS_INTEGER)
AS
BEGIN
  n := &n;
  if(n > 20) THEN
     RAISE FibonacciExceptions.too_big_array;
  END if;
  if(n < 0) THEN 
    RAISE FibonacciExceptions.negative_array;
  END if;
  
  EXCEPTION 
    WHEN FibonacciExceptions.too_big_array THEN FibonacciExceptions.RecordError(); DBMS_OUTPUT.PUT_LINE('N can not be bigger than 20'); n := 20;
    WHEN FibonacciExceptions.negative_array THEN FibonacciExceptions.RecordError(); RAISE;
    WHEN OTHERS THEN FibonacciExceptions.RecordError(); RAISE;
END readLine;
/

DECLARE
  n PLS_INTEGER; -- sekos ilgis
  v_fibonacciArray FibonacciPackage.numberArray;
BEGIN
    readLine(n);
    getFibonacciArray(n, v_fibonacciArray);
    printArray(v_fibonacciArray);
END;