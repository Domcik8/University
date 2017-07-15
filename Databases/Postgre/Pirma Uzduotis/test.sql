SELECT Stud.Knyga.ISBN, COUNT(*) AS Skaicius
FROM Stud.Knyga, Stud.Egzempliorius
WHERE Stud.Egzempliorius.ISBN = Stud.Knyga.ISBN
GROUP BY Stud.Knyga.ISBN