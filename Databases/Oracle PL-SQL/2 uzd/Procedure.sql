create or replace 
PACKAGE FibonacciPackage
AS
  TYPE numberArray IS TABLE OF PLS_INTEGER
  INDEX BY PLS_INTEGER;
END FibonacciPackage;
/

create or replace 
PACKAGE FibonacciExceptions
AS  negative_array EXCEPTION;
    too_big_array EXCEPTION;
    null_array EXCEPTION;
    PRAGMA EXCEPTION_INIT
    (negative_array, -1);
    PRAGMA EXCEPTION_INIT
    (too_big_array, -2);
    PRAGMA EXCEPTION_INIT
    (null_array, -3);
    
    PROCEDURE RecordError;
    FUNCTION GetErrorMessage(num IN Number) RETURN VARCHAR2;
END FibonacciExceptions;
/

create or replace 
PROCEDURE readLine(n OUT PLS_INTEGER)
AS
BEGIN
  n := 26;
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

create or replace 
PROCEDURE printNumber(n IN PLS_INTEGER)
AS
BEGIN
  DBMS_OUTPUT.PUT_LINE(n);
END printNumber;

create or replace 
PROCEDURE printArray(v_fibonacciArray IN FibonacciPackage.numberArray)
AS
v_i PLS_INTEGER := 1;
BEGIN
  WHILE v_i <= v_fibonacciArray.Count LOOP
    printNumber(v_fibonacciArray(v_i  /*+ 10*/));
    v_i := v_i + 1;
    /*v_i := 5 / 0;*/
  END LOOP;
  
  EXCEPTION 
      WHEN ZERO_DIVIDE THEN FibonacciExceptions.RecordError();  RAISE;
      WHEN NO_DATA_FOUND THEN FibonacciExceptions.RecordError();  RAISE;
      WHEN OTHERS THEN FibonacciExceptions.RecordError();  RAISE;
END printArray;
/

create or replace 
PROCEDURE getFibonacciArray(n IN PLS_INTEGER, v_fibonacciArray OUT FibonacciPackage.numberArray)
AS
BEGIN
  CASE WHEN n = 0 THEN RAISE FibonacciExceptions.null_array;
       WHEN n = 1 THEN v_fibonacciArray(1) := 0;
       WHEN n = 2 THEN v_fibonacciArray(1) := 0; v_fibonacciArray(2) := 1;
       ELSE 
        v_fibonacciArray(1) := 0; v_fibonacciArray(2) := 1;
        FOR counter IN 3 .. n LOOP
          v_fibonacciArray(counter) := v_fibonacciArray(counter - 2) + v_fibonacciArray(counter - 1);
        END LOOP;
  END CASE;

  EXCEPTION 
    WHEN FibonacciExceptions.null_array THEN FibonacciExceptions.RecordError();  RAISE;
    WHEN OTHERS THEN FibonacciExceptions.RecordError();  RAISE;
END getFibonacciArray;
/