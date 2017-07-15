package muzika;

import java.sql.Connection;
import java.sql.DriverManager;
import java.sql.SQLException;
import java.sql.ResultSet;
import java.sql.Statement;
import java.util.List;
import java.util.LinkedList;

public class WorkSQL {
   private String dbURL = "jdbc:postgresql://pgsql2.mif/studentu";
    private String dbUsername = "doli1680";
    private String dbPassword = "doli1680";
    private Connection postGresConn = null;
    
    public WorkSQL() {
        loadDriver();
        getConnection();
    }
    
    private void loadDriver() {
        try {
            Class.forName("org.postgresql.Driver");
        } catch (ClassNotFoundException e) {
            System.out.println("Couldn't find driver class!");
            System.exit(1);
        }
    }
    
    private void getConnection() {
        try {
            postGresConn = DriverManager.getConnection(dbURL, dbUsername, dbPassword) ;
        } catch (SQLException e) {
            System.out.println("Couldn't connect to database");
            System.exit(1);
        }
    }
    
    public void closeConnection() {
        try {
            postGresConn.close(); 
        } catch (SQLException e) {
            System.out.println("Error while closing connection to database");
        }
    }
    
    public List<List> queryDb(String query) throws Exception{
        Statement stmt = null;
        ResultSet rs = null;
        List<List> result = new LinkedList<List>();
                
        try {
            stmt = postGresConn.createStatement();
            rs = stmt.executeQuery(query);
            
            while (rs.next()) {
                List<String> row = new LinkedList<String>();
                
                for (int i = 1; i < rs.getMetaData().getColumnCount() + 1; i++) {
                    row.add(rs.getString(i));
                }
                
                result.add(row);
            }
        } catch (SQLException e) {
            if (!e.getMessage().equals("No results were returned by the query.")) {
                throw new SQLException(e.getMessage());
            }  
        } finally {
            try {
                if(null != rs) {
                    rs.close();
                }
                if(null != stmt) {
                    stmt.close();
                }
            } catch (SQLException e) {
                System.out.println("Unexpected SQL Error");
            }
        }
        
        return result;
    }
    
    public String getDbUsername() {
        return dbUsername;
    }
}