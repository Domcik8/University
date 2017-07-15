SELECT Skaitytojas.nr, Skaitytojas.vardas, Skaitytojas.pavarde, 
	COUNT(*) AS "paimta", 
	Sum(CASE WHEN Knyga.verte is NULL  THEN 10 ELSE Knyga.verte END) AS "vertė" 
FROM Stud.Skaitytojas, Stud.Egzempliorius, Stud.Knyga
WHERE Skaitytojas.nr = Egzempliorius.skaitytojas AND 
	Egzempliorius.isbn = Knyga.isbn
GROUP BY Skaitytojas.nr HAVING 
Sum(CASE WHEN Knyga.verte is NULL  THEN 10 ELSE Knyga.verte END) > 30