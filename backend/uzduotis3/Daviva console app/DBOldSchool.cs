using MySql.Data.MySqlClient;
using System.Data;


public class DBOldSchool
{

    private MySqlConnection connection;
    private string server;
    private string database;
    private string uid;
    private string password;
    private string connectionString;

    //Constructor
    public DBOldSchool()
    {
        Initialize();
    }
    public DBOldSchool(string server, string database, string uid, string password)
    {
        Initialize(server, database, uid, password);
    }

    //Initialize values
    private void Initialize()
    {

        server = "178.128.202.96";
        database = "Testas";
        uid = "TestUser";
        password = "TestasTesta5";
        connectionString = "SERVER=" + server + ";port=3306;" + "DATABASE=" +
        database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";Convert Zero Datetime=true;default command timeout=999;";

        connection = new MySqlConnection(connectionString);

    }
    private void Initialize(string server, string database, string uid, string password)
    {
        connectionString = "SERVER=" + server + ";" + "DATABASE=" +
        database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";Convert Zero Datetime=true;default command timeout=999;";

        connection = new MySqlConnection(connectionString);

    }

    //open connection to database
    private bool OpenConnection()
    {
        try
        {
            connection.Open();
            return true;
        }
        catch
        {
            return false;
        }
    }
    public void OpenConnectionlong()
    {
        try
        {
            connection.Open();
        }
        catch
        {

        }
    }

    public string[] SearchData(string search)
    {

        string[] cols = { "ID", "AutoKategorija", "Spalva", "Serija", "VariklioDarbinisTuris", "Kurotipas", "AutomobilioPagaminimoMetai", "KebuloTipas", "GreiciuDezesTipas", "PavaruSkaicius", "PavaruDezesKodas", "VariklioGalia", "VaromejiRatai", "Marke", "Modelis", "Variantas", "MetuIntervalas", "Tipas" };

        string colsList = "";
        string whereList = "";
        string[] separate = search.Split(',', '.', ':');
        string[] words = separate[0].Split(' ', '\t');

        for (int i = 0; i < cols.Length; i++)
        {
            colsList += cols[i] + (i != (cols.Length - 1) ? ", " : "");
        }

        whereList += ($" (CONCAT({ colsList }) LIKE '%{ words[0] }%')");
        if (words.Length > 1)
        {
            for (int m = 1; m < words.Length; m++)
            {
                whereList += ($" AND (CONCAT({ colsList }) LIKE '%{ words[m] }%') ");
            }
        }

        if (separate.Length > 1)
        {
            for (int s = 1; s < separate.Length; s++)
            {
                string[] sepwords = separate[s].Split(' ', '\t');
                whereList += ($" OR (CONCAT({ colsList }) LIKE '%{ sepwords[0] }%')");
                if (sepwords.Length > 1)
                {
                    for (int sw = 1; sw < sepwords.Length; sw++)
                    {
                        whereList += ($" AND (CONCAT({ colsList }) LIKE '%{ sepwords[sw] }%') ");
                    }
                }
            }
        }

        string query = $"SELECT {colsList} FROM `Automobilis` WHERE ( {whereList} )";

        MySqlDataAdapter adpt = new MySqlDataAdapter(query, connection);
        DataSet dset = new DataSet();
        adpt.Fill(dset);
        string[] data = new string[dset.Tables[0].Rows.Count];
       
        for (int i = 0; i < dset.Tables[0].Rows.Count; i++)
        {
            data[i] = $"{ dset.Tables[0].Rows[i][0].ToString() }";
            for (int k = 1; k < dset.Tables[0].Columns.Count; k++)
            {
                data[i] += $" { dset.Tables[0].Rows[i][k].ToString() }";
            }
        }
        return data;
    }

    public string[] Getmarkes()
    {
        string query = "SELECT `Marke` FROM `Automobilis` GROUP BY `Marke`";

        MySqlDataAdapter adpt = new MySqlDataAdapter(query, connection);
        DataSet dset = new DataSet();
        adpt.Fill(dset);

        string[] markes = new string[dset.Tables[0].Rows.Count];
        for (int i = 0; i < dset.Tables[0].Rows.Count; i++)
        {
            markes[i] = dset.Tables[0].Rows[i][0].ToString();
        }
        return markes;
    }

    public DataSet SelectAll(string table)
    {
        string query = "SELECT * FROM " + table;

        MySqlDataAdapter adpt = new MySqlDataAdapter(query, connection);
        DataSet dset = new DataSet();
        adpt.Fill(dset);
        return dset;
    }

}