/**
 *
 * @author Dominik Gabriel Lisovski PS2 2014
 */

package muzika;

import java.io.BufferedReader;
import java.io.InputStreamReader;
import java.io.IOException;
import java.util.List;
import java.util.LinkedList;

public class UI {
    public void runUI() {
        WorkSQL db = new WorkSQL();
        BufferedReader bufRead = new BufferedReader(new InputStreamReader(System.in)); 
        int choice = 1;
        while (choice != 0) {
            try {
                printChoices();
                System.out.print(">");
                choice = Integer.parseInt(bufRead.readLine());
                
                switch (choice) {
                    case  0: break;
                    case  1: findAuthorSongs(bufRead, db);
                             break;
                    case  2: addAuthor(bufRead, db);
                             break;
                    case  3: updateAuthorInformation(bufRead, db);
                             break;
                    case  4: deleteAuthor(bufRead, db);
                             break;
                    case  5: switchAuthorNr(bufRead, db);
                             break;
                    default: System.out.println("Blogas pasirinkimas");
                             break;
                }
            } catch (IOException e) {
               System.out.println("Klaida skaitant ivesti");
            } catch(NumberFormatException e) {
               System.out.println("Netinkamas ivesties formatas");
            }
        }
        
        db.closeConnection();
    }
    
    private void printChoices() {
        System.out.println("Meniu:");
        System.out.println("[0] - baigti darba");
        System.out.println("[1] - parodyti autoriaus dainu sarasa");
        System.out.println("[2] - prideti nauja autoriu");
        System.out.println("[3] - pakeisti autoriaus duomenys");
        System.out.println("[4] - istrinti autoriu is duomenu bazes");
        System.out.println("[5] - sukeisti autoriu numerius");
    }
    
    private void findAuthorSongs(BufferedReader bufRead, WorkSQL db) {
        List<List> result = new LinkedList<List>();
      
        try {
            result = db.queryDb("SELECT * FROM Autorius");
            
            System.out.println("Autoriai:");
            for (int i = 0; i < result.size(); i++) {
                System.out.println((String) result.get(i).get(0) + " " + 
                        result.get(i).get(1) + " " + result.get(i).get(2)
                 + " " + result.get(i).get(3));
            }
            
            System.out.println("Iveskite autoriaus numeri");
            
            result = db.queryDb("SELECT Pavadinimas FROM Daina "
                    + "WHERE Autorius = '" + 
                    bufRead.readLine() + "'");
            
            if (result.isEmpty()) {
                System.out.println("Tokio autoriaus nera arba jis neturi dainu");
            } else {
                System.out.println("Autoriaus dainos:");
                for (int i = 0; i < result.size(); i++) {
                    System.out.println(result.get(i).get(0));
                }
            }
        } catch (Exception e) {
            System.out.println("Error: " + e.getMessage());
        }
    }
    
    private void addAuthor(BufferedReader bufRead, WorkSQL db) {
        System.out.println("Iveskite naujo autoriaus slapyvardi,"
                + " tautybe ir karjeros pradzia");
        
        try {
            db.queryDb("INSERT INTO Autorius(Slapyvardis, Tautybe, Karjeros_pradzia) VALUES "
                    + "('" + bufRead.readLine()
                    + "', '" + bufRead.readLine() + "', " + Integer.parseInt(bufRead.readLine()) + ")");
        } catch (Exception e) {
            System.out.println("Error: " + e.getMessage());
        }        
    }
    
    private void updateAuthorInformation(BufferedReader bufRead, WorkSQL db) {
        List<List> result = new LinkedList<List>();
        
        try {
            result = db.queryDb("SELECT * FROM Autorius");
            
            System.out.println("Autoriai:");
            for (int i = 0; i < result.size(); i++) {
                System.out.println((String) result.get(i).get(0) + " " + 
                        result.get(i).get(1) + " " + result.get(i).get(2) + 
                        " " + result.get(i).get(3));
            }
            
            System.out.println("Iveskite autoriaus numeri ir nauja slapyvardi"
                    + ", tautybe bei karjeros pradzia:");
            
            String nr = bufRead.readLine();
            
            result = db.queryDb("UPDATE Autorius SET slapyvardis = '" + 
                    bufRead.readLine() + "', tautybe = '" +
                    bufRead.readLine() + "', karjeros_pradzia = " +
                    bufRead.readLine() + "WHERE Nr = '" + nr + "'");
            
        } catch (Exception e) {
            System.out.println("Error: " + e.getMessage());
        }
    }
    
    private void deleteAuthor(BufferedReader bufRead, WorkSQL db) {
        List<List> result = new LinkedList<List>();
        
        try {
            result = db.queryDb("SELECT * FROM Autorius");
            
            System.out.println("Darbuotojai:");
            for (int i = 0; i < result.size(); i++) {
                System.out.println((String) result.get(i).get(0) + " " + 
                        result.get(i).get(1) + " " + result.get(i).get(2) + 
                        " " + result.get(i).get(3));
            }
            
            System.out.println("Iveskite autoriaus nr:");
            
            result = db.queryDb("DELETE FROM Autorius WHERE Nr = '" + 
                    bufRead.readLine() +  "'");
            
        } catch (Exception e) {
            System.out.println("Error: " + e.getMessage());
        }
    }
    
    private void switchAuthorNr(BufferedReader bufRead, WorkSQL db) {
            List<List> result = new LinkedList<List>();
        
        try {
            db.queryDb("BEGIN");
            
            
            result = db.queryDb("SELECT * FROM Autorius");
            
            System.out.println("Autorius (Autorius Slapyvardis):");
            for (int i = 0; i < result.size(); i++) {
                System.out.println((String) result.get(i).get(0) + " " + result.get(i).get(1));
            }
            
            System.out.println("Atitinkamai iveskite dvieju autoriu numerius" +
                    " ir slapyvardzius(Slapyvardis1, Nr1, Slapyvardis2, Nr2):");
            
            String pirmas = bufRead.readLine();
            int pirmoAutNr = Integer.parseInt(bufRead.readLine());
            String antras = bufRead.readLine();
            int antroAutNr = Integer.parseInt(bufRead.readLine());
            int random = 9998;
            
            db.queryDb("UPDATE Autorius SET Nr = " + 
                    random + " WHERE Nr = " + pirmoAutNr + 
                    " AND Slapyvardis = '" + pirmas + "'");
            
            db.queryDb("UPDATE Autorius SET Nr = " + 
                    pirmoAutNr + " WHERE Nr = " + antroAutNr + 
                    " AND Slapyvardis = '" + antras + "'");
            
            db.queryDb("UPDATE Autorius SET Nr = " + 
                    antroAutNr + " WHERE Nr = " + random + 
                    " AND Slapyvardis = '" + pirmas + "'");
            
            db.queryDb("COMMIT");
        } catch (Exception e) {
            System.out.println("Error:" + e.getMessage());
            try {
                db.queryDb("ROLLBACK");
            } catch (Exception ex) {
                System.out.println("Error:" + ex.getMessage());
            }
        }
    }
}