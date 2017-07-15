INSERT INTO Atlikejas VALUES('Eminem', 1996, 'Ameriketis');
INSERT INTO Atlikejas VALUES('Rihanna', 2005, 'Barbadosete');
INSERT INTO Atlikejas VALUES('Skrillex', 2002, 'Ameriketis');
INSERT INTO Atlikejas VALUES('Polina Gagarina', 2003, 'Ruse'); 


INSERT INTO Albumas VALUES('Curtain Call', 77.55, 'Eminem');
INSERT INTO Albumas VALUES('Encore', 77.06, 'Eminem');
INSERT INTO Albumas VALUES('Hotness', 58.08, 'Rihanna');
INSERT INTO Albumas VALUES('Recess', 46.34, 'Skrillex');
INSERT INTO Albumas VALUES('Shards', null, 'Polina Gagarina');

/*INSERT INTO Albumas VALUES('test6', null, 'Polina Gagarina');
INSERT INTO Albumas VALUES('test7', null, 'Polina Gagarina');
INSERT INTO Albumas VALUES('test8', null, 'Polina Gagarina');
INSERT INTO Albumas VALUES('test9', null, 'Polina Gagarina');
INSERT INTO Albumas VALUES('test10', null, 'Polina Gagarina');
INSERT INTO Albumas VALUES('test11', null, 'Polina Gagarina');
INSERT INTO Albumas VALUES('test12', null, 'Polina Gagarina');
INSERT INTO Albumas VALUES('test13', null, 'Polina Gagarina');
INSERT INTO Albumas VALUES('test14', null, 'Polina Gagarina');
INSERT INTO Albumas VALUES('test15', null, 'Polina Gagarina');
INSERT INTO Albumas VALUES('test16', null, 'Polina Gagarina');
INSERT INTO Albumas VALUES('test17', null, 'Polina Gagarina');
INSERT INTO Albumas VALUES('test18', null, 'Polina Gagarina');
INSERT INTO Albumas VALUES('test19', null, 'Polina Gagarina');
INSERT INTO Albumas VALUES('test20', null, 'Polina Gagarina');
INSERT INTO Albumas VALUES('test21', null, 'Polina Gagarina');
INSERT INTO Albumas VALUES('test22', null, 'Polina Gagarina');
INSERT INTO Albumas VALUES('test23', null, 'Polina Gagarina');
INSERT INTO Albumas VALUES('test24', null, 'Polina Gagarina');
INSERT INTO Albumas VALUES('test25', null, 'Polina Gagarina');
INSERT INTO Albumas VALUES('test26!', null, 'Polina Gagarina');
*/

INSERT INTO Autorius(Slapyvardis, Tautybe, Karjeros_pradzia) VALUES('Konstantin Meladze', 'Rusas', 1990);
INSERT INTO Autorius(Slapyvardis, Tautybe, Karjeros_pradzia) VALUES('Eminem', 'Ameriketis', 1996);
INSERT INTO Autorius(Slapyvardis, Tautybe, Karjeros_pradzia) VALUES('Rihanna', 'Barbadosete', 2005);
INSERT INTO Autorius(Slapyvardis, Tautybe, Karjeros_pradzia) VALUES('Skrillex', 'Ameriketis', 2002);
INSERT INTO Autorius(Slapyvardis) VALUES('Notker Balbulus');

INSERT INTO Daina(Pavadinimas, Trukme_Min, Autorius)
VALUES ('We As Americans', null, 2);
INSERT INTO Daina VALUES('Love You More', 1.46, 'Rap', 2);
INSERT INTO Daina VALUES('Evel Deeds', 4.19, 'Rap', 2);
INSERT INTO Daina VALUES('Ricky Ticky Toc', 2.49, 'Rap', 2);
INSERT INTO Daina VALUES('Never Enough', 2.39, 'Rap', 2);
INSERT INTO Daina VALUES('Yellow Brick Road', 5.46, 'Rap', 2);
INSERT INTO Daina VALUES('Like Toy Soldiers', 4.56, 'Rap', 1);
INSERT INTO Daina VALUES('Mosh', 5.17, 'Rap', 2);
INSERT INTO Daina VALUES('Puke', 4.07, 'Rap', 2);
INSERT INTO Daina VALUES('My 1st Single', 5.02, 'Rap', 2);
INSERT INTO Daina VALUES('Rain Man', 5.13, 'Rap', 2);
INSERT INTO Daina VALUES('Big Weenie', 4.26, 'Rap', 2);
INSERT INTO Daina VALUES('Just Lose It', 4.08, 'Rap', 2);
INSERT INTO Daina VALUES('Ass Like That', 4.25, 'Rap', 2);
INSERT INTO Daina VALUES('Spend Some Time', 5.10, 'Rap', 2);
INSERT INTO Daina VALUES('Mockingbird', 4.10, 'Rap', 2);
INSERT INTO Daina VALUES('Encore, Curtains Down', 4.02, 'Rap', 2);
INSERT INTO Daina VALUES('Curtains Up', 0.46, 'Rap', 2);
INSERT INTO Daina VALUES('One Shot 2 Shot', 4.26, 'Rap', 2);
INSERT INTO Daina VALUES('Final Thought', 0.30, 'Rap', 2);
INSERT INTO Daina VALUES('20 tasis testinis', null, null, 2);
INSERT INTO Daina VALUES('21 tasis testinis', null, null, 2);

INSERT INTO Daina(Pavadinimas, Trukme_Min, Zanras, Autorius)
VALUES ('We As Americans', null, null, 2);

INSERT INTO Daina VALUES('Stan', null, 'Rap', 2);
/*INSERT INTO Daina VALUES('Lose Yourself', null, 'Rap', 2);*/
/*INSERT INTO Daina VALUES('Like Toy Soldiers', 4.56, 'Rap', 2);*/
INSERT INTO Daina VALUES('Just Lose It', null, 'Rap', 2);

INSERT INTO Daina VALUES('No', 3.56, 'Pop', 1);
INSERT INTO Daina VALUES('For ever', null, 'Pop', 1);
INSERT INTO Daina VALUES('The play is over', 3.59, 'Pop', 1);

INSERT INTO Daina VALUES('Bring it back', null, 'R&B', 3);

INSERT INTO Priklauso (Dainos_Pavadinimas, Albumo_Pavadinimas)
VALUES ('We As Americans', 'Encore');
INSERT INTO Priklauso VALUES('Love You More', 'Encore');
INSERT INTO Priklauso VALUES('Evel Deeds', 'Encore');
INSERT INTO Priklauso VALUES('Ricky Ticky Toc', 'Encore');
INSERT INTO Priklauso VALUES('Never Enough', 'Encore');
INSERT INTO Priklauso VALUES('Yellow Brick Road', 'Encore');
INSERT INTO Priklauso VALUES('Like Toy Soldiers', 'Encore');
INSERT INTO Priklauso VALUES('Mosh', 'Encore');
INSERT INTO Priklauso VALUES('Puke', 'Encore');
INSERT INTO Priklauso VALUES('My 1st Single', 'Encore');
INSERT INTO Priklauso VALUES('Rain Man', 'Encore');
INSERT INTO Priklauso VALUES('Big Weenie', 'Encore');
INSERT INTO Priklauso VALUES('Just Lose It', 'Encore');
INSERT INTO Priklauso VALUES('Ass Like That', 'Encore');
INSERT INTO Priklauso VALUES('Spend Some Time', 'Encore');
INSERT INTO Priklauso VALUES('Mockingbird', 'Encore');
INSERT INTO Priklauso VALUES('Encore, Curtains Down', 'Encore');
INSERT INTO Priklauso VALUES('One Shot 2 Shot', 'Encore');
INSERT INTO Priklauso VALUES('Curtains Up', 'Encore');
INSERT INTO Priklauso VALUES('Final Thought', 'Encore');

/*INSERT INTO Priklauso VALUES('20 tasis testinis', 'Encore');*/
/*INSERT INTO Priklauso VALUES('21 tasis testinis', 'Encore');*/

INSERT INTO Priklauso VALUES('Stan', 'Curtain Call');
INSERT INTO Priklauso VALUES('Lose Yourself', 'Curtain Call');
INSERT INTO Priklauso VALUES('Like Toy Soldiers', 'Curtain Call');
INSERT INTO Priklauso VALUES('Just Lose It', 'Curtain Call');

/*INSERT INTO Priklauso VALUES('21 tasis testinis', 'Hotness');*/



INSERT INTO Priklauso VALUES('No', 'Shards');
INSERT INTO Priklauso VALUES('For ever', 'Shards');
INSERT INTO Priklauso VALUES('The play is over', 'Shards');

INSERT INTO Priklauso VALUES('Bring it back', 'Hotness');

Blogi duomenys:

INSERT INTO Albumas VALUES('Shards', 999, 'Mikutavicius')

INSERT INTO Priklauso VALUES('No', 'Shards');
INSERT INTO Priklauso VALUES('For ever', 'Shards');
INSERT INTO Priklauso VALUES('The play is over', 'Shards');