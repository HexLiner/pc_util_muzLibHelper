using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using Microsoft.Data.Sqlite;
using static System.ComponentModel.Design.ObjectSelectorEditor;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrackBar;


namespace muzLibHelper
{
    public class TrackAutor
    {
        public long id = -1;
        public string name = "";


        public static bool AddAutorToDb(TrackAutor autor, DbReports dbReports)
        {
            dbReports.sqliteCommand.CommandText = "INSERT INTO main.Autors(Name) VALUES (" +
                                            "'" + autor.name + "'" +
                                            ")";
            try
            {
                dbReports.sqliteCommand.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                MessageBox.Show(dbReports.sqliteCommand.CommandText + "\n" + e.Message);
                return false;
            }
            return true;
        }

        public static bool IsAutorExists(string autorName, DbReports dbReports)
        {
            SqliteDataReader sqliteReader;

            dbReports.sqliteCommand.CommandText = "SELECT \"ID\" FROM Autors WHERE \"Name\"='" + autorName + "'";
            try
            {
                sqliteReader = dbReports.sqliteCommand.ExecuteReader();
            }
            catch (Exception e)
            {
                MessageBox.Show(dbReports.sqliteCommand.CommandText + "\n" + e.Message);
                return true;
            }

            if (sqliteReader.HasRows) return true;
            return false;
        }

        public static long GetAutorId(string autorName, DbReports dbReports)
        {
            SqliteDataReader sqliteReader;

            dbReports.sqliteCommand.CommandText = "SELECT \"ID\" FROM Autors WHERE \"Name\"='" + autorName + "'";
            try
            {
                sqliteReader = dbReports.sqliteCommand.ExecuteReader();
            }
            catch (Exception e)
            {
                MessageBox.Show(dbReports.sqliteCommand.CommandText + "\n" + e.Message);
                return -1;
            }

            if (!sqliteReader.HasRows)
            {
                dbReports.sqliteCommand.Dispose();
                return -1;
            }
            sqliteReader.Read();
            long autorId = sqliteReader.GetInt64(0);
            dbReports.sqliteCommand.Dispose();
            return autorId;
        }

        public static string GetAutorName(long autorId, DbReports dbReports)
        {
            SqliteDataReader sqliteReader;

            dbReports.sqliteCommand.CommandText = "SELECT \"Name\" FROM Autors WHERE \"ID\"='" + autorId + "'";
            try
            {
                sqliteReader = dbReports.sqliteCommand.ExecuteReader();
            }
            catch (Exception e)
            {
                MessageBox.Show(dbReports.sqliteCommand.CommandText + "\n" + e.Message);
                return "";
            }

            if (!sqliteReader.HasRows)
            {
                dbReports.sqliteCommand.Dispose();
                return "";
            }
            sqliteReader.Read();
            string autorName = sqliteReader.GetString(0);
            dbReports.sqliteCommand.Dispose();
            return autorName;
        }

        public static bool IsTableExists(DbReports dbReports)
        {
            dbReports.sqliteCommand.CommandText = "SELECT name FROM sqlite_master WHERE type = 'table' AND name = 'Autors'";
            if ((string)dbReports.sqliteCommand.ExecuteScalar() == "Autors") return true;
            return false;
        }

        public static void CreateTable(DbReports dbReports)
        {
            dbReports.sqliteCommand.CommandText = "CREATE TABLE \"Autors\" (\"ID\" INTEGER NOT NULL UNIQUE, \"Name\" TEXT, PRIMARY KEY(\"ID\" AUTOINCREMENT))";
            dbReports.sqliteCommand.ExecuteNonQuery();
        }
    }

    public class TrackGroup
    {
        public long id = -1;
        public string name = "";


        public static bool AddGroupToDb(TrackGroup group, DbReports dbReports)
        {
            dbReports.sqliteCommand.CommandText = "INSERT INTO main.Groups(Name) VALUES (" +
                                            "'" + group.name + "'" +
                                            ")";
            try
            {
                dbReports.sqliteCommand.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                MessageBox.Show(dbReports.sqliteCommand.CommandText + "\n" + e.Message);
                return false;
            }
            return true;
        }

        public static long GetGroupId(string groupName, DbReports dbReports)
        {
            SqliteDataReader sqliteReader;

            dbReports.sqliteCommand.CommandText = "SELECT \"ID\" FROM Groups WHERE \"Name\"='" + groupName + "'";
            try
            {
                sqliteReader = dbReports.sqliteCommand.ExecuteReader();
            }
            catch (Exception e)
            {
                MessageBox.Show(dbReports.sqliteCommand.CommandText + "\n" + e.Message);
                return -1;
            }

            if (!sqliteReader.HasRows)
            {
                dbReports.sqliteCommand.Dispose();
                return -1;
            }
            sqliteReader.Read();
            long groupId = sqliteReader.GetInt64(0);
            dbReports.sqliteCommand.Dispose();
            return groupId;
        }

        public static string GetGroupName(long groupId, DbReports dbReports)
        {
            SqliteDataReader sqliteReader;

            dbReports.sqliteCommand.CommandText = "SELECT \"Name\" FROM Groups WHERE \"ID\"='" + groupId + "'";
            try
            {
                sqliteReader = dbReports.sqliteCommand.ExecuteReader();
            }
            catch (Exception e)
            {
                MessageBox.Show(dbReports.sqliteCommand.CommandText + "\n" + e.Message);
                return "";
            }

            if (!sqliteReader.HasRows)
            {
                dbReports.sqliteCommand.Dispose();
                return "";
            }
            sqliteReader.Read();
            string groupName = sqliteReader.GetString(0);
            dbReports.sqliteCommand.Dispose();
            return groupName;
        }

        public static bool IsTableExists(DbReports dbReports)
        {
            dbReports.sqliteCommand.CommandText = "SELECT name FROM sqlite_master WHERE type = 'table' AND name = 'Groups'";
            if ((string)dbReports.sqliteCommand.ExecuteScalar() == "Groups") return true;
            return false;
        }

        public static void CreateTable(DbReports dbReports)
        {
            dbReports.sqliteCommand.CommandText = "CREATE TABLE \"Groups\" (\"ID\" INTEGER NOT NULL UNIQUE, \"Name\" TEXT, PRIMARY KEY(\"ID\" AUTOINCREMENT))";
            dbReports.sqliteCommand.ExecuteNonQuery();
        }
    }

    public class Track
    {
        public long id = -1;
        public string name = "";
        public string file = "";
        public TrackAutor[] autors = new TrackAutor[3] {new TrackAutor(), new TrackAutor(), new TrackAutor()};
        public TrackGroup group = new TrackGroup();

        public static List<Track> SearchInDb(Track track, DbReports dbReports)
        {
            SqliteDataReader sqliteReader;
            Track foundTrack;
            List<Track> tracks = new List<Track>();
            int i;
            string dbCommand;
            bool isFirstProperties = true;


            dbCommand = "SELECT " +
                        "A1.Name as 'Autor1', " +
                        "A2.Name as 'Autor2', " +
                        "A3.Name as 'Autor3', " +
                        "Tracks.Name, " +
                        "Tracks.File, " +
                        "Groups.Name as 'Group' " +
                        "FROM Tracks " +
                        "LEFT JOIN Autors as A1 ON A1.ID = Tracks.Autor1_ID " +
                        "LEFT JOIN Autors as A2 ON A2.ID = Tracks.Autor2_ID " +
                        "LEFT JOIN Autors as A3 ON A3.ID = Tracks.Autor3_ID " +
                        "LEFT JOIN Groups ON Groups.ID = Tracks.Group_ID " +
                        "WHERE ";

            if (track.name != "")
            {
                dbCommand += "Tracks.Name='" + track.name + "'";
                isFirstProperties = false;
            }

            for (i = 0; i < track.autors.Length; i++)
            {
                if (track.autors[i].name != "")
                {
                    if (!isFirstProperties) dbCommand += " AND ";
                    dbCommand += "(A1.Name ='" + track.autors[i].name +
                                 "' OR A2.Name ='" + track.autors[i].name +
                                 "' OR A3.Name ='" + track.autors[i].name +
                                 "') ";
                    isFirstProperties = false;
                }
            }

            if (track.file != "")
            {
                if (!isFirstProperties) dbCommand += " AND ";
                dbCommand += "Track.File='" + track.file + "' ";
                isFirstProperties = false;
            }

            if (track.group.name != "")
            {
                if (!isFirstProperties) dbCommand += " AND ";
                dbCommand += "Groups.Name='" + track.group.name + "' ";
                isFirstProperties = false;
            }

            if (isFirstProperties) return null;


            dbReports.sqliteCommand.CommandText = dbCommand;
            try
            {
                sqliteReader = dbReports.sqliteCommand.ExecuteReader();
            }
            catch (Exception e)
            {
                MessageBox.Show(dbReports.sqliteCommand.CommandText + "\n" + e.Message);
                return null;
            }


            if (sqliteReader.HasRows)
            {
                while (sqliteReader.Read())
                {
                    tracks.Add(new Track());
                    foundTrack = tracks[tracks.Count - 1];
                    if (!sqliteReader.IsDBNull(0)) foundTrack.autors[0].name = sqliteReader.GetString(0);
                    if (!sqliteReader.IsDBNull(1)) foundTrack.autors[1].name = sqliteReader.GetString(1);
                    if (!sqliteReader.IsDBNull(2)) foundTrack.autors[2].name = sqliteReader.GetString(2);
                    foundTrack.name = sqliteReader.GetString(3);
                    foundTrack.file = sqliteReader.GetString(4);
                    if (!sqliteReader.IsDBNull(5)) foundTrack.group.name = sqliteReader.GetString(5);
                }
                dbReports.sqliteCommand.Dispose();
            }
            else
            {
                dbReports.sqliteCommand.Dispose();
                return null;
            }
            dbReports.sqliteCommand.Dispose();

            return tracks;
        }


        public static bool AddTrackToDb(Track track, DbReports dbReports)
        {
            if (track.name == "") return false;
            if (track.autors[0].name == "") return false;
            if ((track.autors[0].name == track.autors[1].name) || ((track.autors[1].name != "" && (track.autors[1].name == track.autors[2].name)))) return false;
            if (track.file == "") return false;


            foreach (var autor in track.autors)
            {
                if (autor.name == "") continue;

                autor.id = TrackAutor.GetAutorId(autor.name, dbReports);
                if (autor.id < 0)
                {
                    // Add new autor
                    if (!TrackAutor.AddAutorToDb(autor, dbReports)) return false;
                    autor.id = TrackAutor.GetAutorId(autor.name, dbReports);
                    if (autor.id < 0) return false;
                }
            }

            if (track.group.name != "")
            {
                track.group.id = TrackGroup.GetGroupId(track.group.name, dbReports);
                if (track.group.id < 0)
                {
                    // Add new group
                    if (!TrackGroup.AddGroupToDb(track.group, dbReports)) return false;
                    track.group.id = TrackGroup.GetGroupId(track.group.name, dbReports);
                    if (track.group.id < 0) return false;
                }
            }

            // Add new track
            dbReports.sqliteCommand.CommandText = "INSERT INTO main.Tracks(Autor1_ID, Autor2_ID, Autor3_ID, Name, File, Group_ID) VALUES (" +
                                            ((track.autors[0].name != "") ? track.autors[0].id : "NULL") + "," +
                                            ((track.autors[1].name != "") ? track.autors[1].id : "NULL") + "," +
                                            ((track.autors[2].name != "") ? track.autors[2].id : "NULL") + "," +
                                            "'" + track.name + "'," +
                                            "'" + track.file + "'," +
                                            ((track.group.name != "") ? track.group.id : "NULL") +
                                            ")";
            try
            {
                dbReports.sqliteCommand.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                MessageBox.Show(dbReports.sqliteCommand.CommandText + "\n" + e.Message);
                return false;
            }

            return true;
        }

        public static bool IsTableExists(DbReports dbReports)
        {
            dbReports.sqliteCommand.CommandText = "SELECT name FROM sqlite_master WHERE type = 'table' AND name = 'Tracks'";
            if ((string)dbReports.sqliteCommand.ExecuteScalar() == "Tracks") return true;
            return false;
        }

        public static void CreateTable(DbReports dbReports)
        {
            dbReports.sqliteCommand.CommandText = "CREATE TABLE \"Tracks\" (\"ID\" INTEGER NOT NULL UNIQUE, \"Autor1_ID\" INTEGER, \"Autor2_ID\" INTEGER, \"Autor3_ID\" INTEGER, \"Name\" TEXT, \"File\" TEXT, \"Group_ID\" INTEGER, FOREIGN KEY(\"Autor1_ID\") REFERENCES \"Autors\"(\"ID\") ON DELETE RESTRICT, FOREIGN KEY(\"Autor2_ID\") REFERENCES \"Autors\"(\"ID\") ON DELETE RESTRICT, FOREIGN KEY(\"Autor3_ID\") REFERENCES \"Autors\"(\"ID\") ON DELETE RESTRICT, FOREIGN KEY(\"Group_ID\") REFERENCES \"Groups\"(\"ID\") ON DELETE RESTRICT, PRIMARY KEY(\"ID\" AUTOINCREMENT))";
            dbReports.sqliteCommand.ExecuteNonQuery();
        }
    }


    public class DbReports
    {
        private const int DB_VERSION = 2;

        private SqliteConnection sqlite;
        public SqliteCommand sqliteCommand;


        public DbReports()
        {
            sqlite = new SqliteConnection();
            sqliteCommand = new SqliteCommand
            {
                Connection = sqlite
            };
        }


        public bool ChangeDbFile(string filePath)
        {
            bool isEmptyDB = false;
            bool isCorrectDB = true;


            if (!File.Exists(filePath)) isEmptyDB = true;

            sqlite.Close();
            sqlite.ConnectionString = "Data Source=" + filePath;
            sqlite.Open();


            // Check DB empty
            if (!isEmptyDB)
            {
                string tableCheckResult;
                int tableCheckCnt = 0;

                try
                {
                    // Check tables exists
                    sqliteCommand.CommandText = "SELECT name FROM sqlite_master WHERE type = 'table' AND name = 'muzLib_Settings'";
                    tableCheckResult = (string)sqliteCommand.ExecuteScalar();
                    if (tableCheckResult == "muzLib_Settings") tableCheckCnt++;

                    if (TrackAutor.IsTableExists(this)) tableCheckCnt++;
                    if (TrackGroup.IsTableExists(this)) tableCheckCnt++;
                    if (Track.IsTableExists(this)) tableCheckCnt++;

                    if (tableCheckCnt == 0) isEmptyDB = true;
                    else if (tableCheckCnt != 4)
                    {
                        isCorrectDB = false;
                        MessageBox.Show("Incorrect DB file!");
                    }


                    // Check DB version
                    sqliteCommand.CommandText = "SELECT VALUE FROM muzLib_Settings WHERE SETTING=\"DB_Version\"";
                    int real_db_version = Convert.ToInt32(sqliteCommand.ExecuteScalar());
                    if (real_db_version != DB_VERSION)
                    {
                        isCorrectDB = false;
                        MessageBox.Show("Unsupported DB version!");
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show(sqliteCommand.CommandText + "\n" + e.Message);
                    return true;
                }

            }


            if (isEmptyDB && isCorrectDB)
            {
                try
                {
                    sqliteCommand.CommandText = "CREATE TABLE \"muzLib_Settings\" (\"Setting\" TEXT NOT NULL UNIQUE, \"Value\" TEXT, PRIMARY KEY(\"Setting\"))";
                    sqliteCommand.ExecuteNonQuery();
                    // Add DB version
                    sqliteCommand.CommandText = "INSERT INTO \"main\".\"muzLib_Settings\"(\"Setting\", \"Value\") VALUES (\'DB_Version\', \'" + DB_VERSION.ToString() + "\')";
                    sqliteCommand.ExecuteNonQuery();

                    TrackAutor.CreateTable(this);
                    TrackGroup.CreateTable(this);
                    Track.CreateTable(this);

                    MessageBox.Show("Is empty data base! Created new data dase.");
                }
                catch (Exception e)
                {
                    MessageBox.Show(sqliteCommand.CommandText + "\n" + e.Message);
                    return true;
                }
            }

            return isCorrectDB;
        }

    }

}
