CREATE TEMP TABLE KnygosEgzemplioriai(isbn, "Egzemplioriu paimta")
	AS (SELECT Egzempliorius.isbn, COUNT(Egzempliorius.paimta) AS "Egzemplioriu paimta"
		FROM Stud.Egzempliorius
		GROUP BY Stud.Egzempliorius.isbn);

CREATE TEMP TABLE PopuliariausiaLeidykla(leidykla, "Egzemplioriu paimta")
	AS (SELECT A.leidykla, SUM(B."Egzemplioriu paimta") AS "Egzemplioriu paimta"
		FROM Stud.Knyga AS A, KnygosEgzemplioriai AS B
		WHERE A.isbn = B.isbn
		GROUP BY A.leidykla);
CREATE TEMP TABLE MAXEgz(MAXEgzemplioriu)
	AS (SELECT MIN(A."Egzemplioriu paimta")
		FROM PopuliariausiaLeidykla AS A);

SELECT A.leidykla, A."Egzemplioriu paimta"
FROM PopuliariausiaLeidykla AS A, MAXEgz AS B
WHERE A."Egzemplioriu paimta" = B.MAXEgzemplioriu