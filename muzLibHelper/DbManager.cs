using Microsoft.Data.Sqlite;
using muzLibHelper;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class DbManagerAddRowHistory
{
    public List<string> tables = new List<string>();
    public List<string> rowIds = new List<string>();
}


public class DbManager
{
    private SqliteConnection sqlite;
    private SqliteCommand sqliteCommand;
    public bool isEmptyDB = false;
    public List<DbManagerAddRowHistory> addRowHistory = new List<DbManagerAddRowHistory>();

    private string dbFile;
    private string tmpFile;
    private bool isTmpFileChanged;


    public DbManager()
    {
        sqlite = new SqliteConnection();
        sqliteCommand = new SqliteCommand
        {
            Connection = sqlite
        };

        addRowHistory.Add(new DbManagerAddRowHistory());
    }


    public bool OpenDb(string dbFile)
    {
        if (!File.Exists(dbFile)) isEmptyDB = true;
        else isEmptyDB = false;
        this.dbFile = dbFile;

        this.CreateNewTmpFile();

        sqlite.Close();
        sqlite.ConnectionString = "Data Source=" + tmpFile;
        sqlite.Open();

        if (isEmptyDB) MessageBox.Show("Is empty data base! Created new data dase.");

        addRowHistory.Clear();
        addRowHistory.Add(new DbManagerAddRowHistory());

        return true;
    }

    public void CloseDb()
    {
        sqlite.Close();

        if (isTmpFileChanged)
        {
            //// check file busy
            File.Delete(dbFile);
            File.Move(tmpFile, dbFile);   // Rename
        }
    }

    public void ApplyDbChanges()
    {
        if (!isTmpFileChanged) return;

        this.CloseDb();
        this.CreateNewTmpFile();
    }

    private void CreateNewTmpFile()
    {
        tmpFile = Path.GetDirectoryName(dbFile) + "/" + Path.GetFileName(dbFile) + "_tmp" + Path.GetExtension(dbFile);
        if (File.Exists(tmpFile)) File.Delete(tmpFile);
        File.Copy(dbFile, tmpFile);
        isTmpFileChanged = false;
    }


    private bool ExecuteDbInsertCmd(string cmd)
    {
        sqliteCommand.CommandText = cmd;
        try
        {
            sqliteCommand.ExecuteNonQuery();
        }
        catch (Exception e)
        {
            MessageBox.Show(sqliteCommand.CommandText + "\n" + e.Message);
            return false;
        }

        isTmpFileChanged = true;

        return true;
    }

    public bool CreateTable(string tableName, string columns)
    {
        string cmd = "CREATE TABLE " + tableName + "(" + columns + ")";
        return this.ExecuteDbInsertCmd(cmd);
    }

    public long AddRowToTable(string tableName, string rowColumns, string rowValues)
    {
        string cmd = "INSERT INTO " + tableName +
                     "(" + rowColumns + ")" +
                     " VALUES(" + rowValues + "); " +
                     "SELECT last_insert_rowid()";

        string rowIdStr = this.ExecuteScalarDbSelectCmd(cmd);
        if (rowIdStr == null) return -1;
        long rowId = Convert.ToInt64(rowIdStr);

        addRowHistory[addRowHistory.Count - 1].tables.Add(tableName);
        addRowHistory[addRowHistory.Count - 1].rowIds.Add(rowIdStr);

        return rowId;
    }

    public SqliteDataReader ExecuteDbSelectCmd(string cmd)
    {
        SqliteDataReader sqliteReader;

        sqliteCommand.CommandText = cmd;
        try
        {
            sqliteReader = sqliteCommand.ExecuteReader();
        }
        catch (Exception e)
        {
            MessageBox.Show(sqliteCommand.CommandText + "\n" + e.Message);
            return null;
        }

        return sqliteReader;
    }

    public string ExecuteScalarDbSelectCmd(string cmd)
    {
        string result = null;

        sqliteCommand.CommandText = cmd;
        try
        {
            result = sqliteCommand.ExecuteScalar().ToString();   ////
        }
        catch (Exception e)
        {
            MessageBox.Show(sqliteCommand.CommandText + "\n" + e.Message);
        }

        return result;
    }

    public bool IsTableExists(string tableName)
    {
        string result;

        result = this.ExecuteScalarDbSelectCmd("SELECT name FROM sqlite_master WHERE type = 'table' AND name = '" + tableName + "'");
        if (result != tableName) return false;
        return true;
    }

    public void newHistoryGroup()
    {
        if (addRowHistory[addRowHistory.Count - 1].tables.Count == 0) return;
        addRowHistory.Add(new DbManagerAddRowHistory());
    }

    public void historyUndo()
    {
        if (addRowHistory[addRowHistory.Count - 1].tables.Count == 0) return;

        int lastHystoryIndex = addRowHistory.Count - 1;
        int lastHystoryGroupIndex = addRowHistory[addRowHistory.Count - 1].tables.Count - 1;

        // Delete for DB
        string cmd = "DELETE FROM " +
                     addRowHistory[lastHystoryIndex].tables[lastHystoryGroupIndex] +
                     " WHERE " +
                     "ID = " + addRowHistory[lastHystoryIndex].rowIds[lastHystoryGroupIndex];
        this.ExecuteDbInsertCmd(cmd);

        // Delete from history
        if (lastHystoryGroupIndex == 0)
        {
            addRowHistory.RemoveAt(lastHystoryIndex);
        }
        else
        {
            addRowHistory[lastHystoryIndex].tables.RemoveAt(lastHystoryGroupIndex);
            addRowHistory[lastHystoryIndex].rowIds.RemoveAt(lastHystoryGroupIndex);
        }
    }

}

