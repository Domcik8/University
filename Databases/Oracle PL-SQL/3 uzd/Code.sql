DECLARE
  CURSOR c_companies IS SELECT * FROM MokyklosVaikai;
BEGIN
    checkSchoolChildren();
    dropIncorrectData();
    insertSchoolChild('numeris', 'vaikoKodas');
END;