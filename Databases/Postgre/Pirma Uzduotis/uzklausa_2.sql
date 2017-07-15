SELECT Skaitytojas.vardas, Skaitytojas.pavarde, Skaitytojas.nr 
FROM Stud.Skaitytojas, Stud.Egzempliorius, Stud.Autorius
WHERE Skaitytojas.nr = Egzempliorius.skaitytojas AND Egzempliorius.isbn = Autorius.isbn AND
((Autorius.vardas = 'Jonas' AND Autorius.pavarde = 'Jonaitis') OR
(Autorius.vardas = 'Pijus' AND Autorius.pavarde = 'Jonaitis'));