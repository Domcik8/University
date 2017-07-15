WITH Columns(Quantity, Table_Schema, Table_Name)
	AS (SELECT Count(*), Table_Schema, Table_Name
		FROM Information_Schema.Columns
		GROUP BY Table_Schema, Table_Name)

SELECT Table_Schema, Table_Name, Quantity
FROM Columns
WHERE (Quantity <= 20) AND (Quantity >= 10)
ORDER BY 1, 2