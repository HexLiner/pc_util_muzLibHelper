﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
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
        public string name = "";


        public static long AddNewAutor(TrackAutor autor, DbManager dbManager)
        {
            if ((autor == null) || (autor.name == null) || (autor.name == "")) return -1;

            return dbManager.AddRowToTable("Autors", "Name", "'" + autor.name + "'");
        }

        public static long ConnetctTrackToAutor(long trackID, string autorName, DbManager dbManager)
        {
            if ((autorName == null) || (trackID < 1)) return -1;

            return dbManager.AddRowToTable("TracksAutors", "TrackID, AutorID", "'" + trackID + "', (SELECT ID FROM Autors WHERE Name='" + autorName + "')");
        }

        public static List<Track> GetAutorTracks(string autorName, DbManager dbManager)
        {
            SqliteDataReader sqliteReader;
            List<Track> tracks = new List<Track>();


            string cmd = "SELECT " +
                         "Tracks.Name " +
                         "FROM TracksAutors " +
                         "JOIN Autors ON Autors.ID = TracksAutors.AutorID " +
                         "JOIN Tracks ON Tracks.ID = TracksAutors.TrackID " +
                         "WHERE " +
                         "Autors.Name='" + autorName + "'";
            sqliteReader = dbManager.ExecuteDbSelectCmd(cmd);
            if (sqliteReader == null)
            {
                return null;
            }

            if (sqliteReader.HasRows)
            {
                while (sqliteReader.Read())
                {
                    tracks.Add(new Track() { name = sqliteReader.GetString(0) });
                }
            }
            else
            {
                sqliteReader.Close();
                return null;
            }

            sqliteReader.Close();
            return tracks;
        }

        public static List<TrackAutor> GetTrackAutors(string trackName, DbManager dbManager)
        {
            SqliteDataReader sqliteReader;
            List<TrackAutor> autors = new List<TrackAutor>();


            string cmd = "SELECT " +
                         "Autors.Name " +
                         "FROM TracksAutors " +
                         "JOIN Autors ON Autors.ID = TracksAutors.AutorID " +
                         "JOIN Tracks ON Tracks.ID = TracksAutors.TrackID " +
                         "WHERE " +
                         "Tracks.Name='" + trackName + "' " +
                         "ORDER BY Autors.Name";
            sqliteReader = dbManager.ExecuteDbSelectCmd(cmd);
            if (sqliteReader == null)
            {
                return null;
            }

            if (sqliteReader.HasRows)
            {
                while (sqliteReader.Read())
                {
                    autors.Add(new TrackAutor() { name = sqliteReader.GetString(0) });
                }
            }
            else
            {
                sqliteReader.Close();
                return null;
            }

            sqliteReader.Close();
            return autors;
        }

        public static bool IsExists(string autorName, DbManager dbManager)
        {
            SqliteDataReader sqliteReader;
            bool result;

            string cmd = "SELECT ID FROM Autors WHERE Name='" + autorName + "'";
            sqliteReader = dbManager.ExecuteDbSelectCmd(cmd);
            if (sqliteReader == null)
            {
                return true;
            }

            result = sqliteReader.HasRows;
            sqliteReader.Close();
            return result;
        }

        public static bool IsTableExists(DbManager dbManager)
        {
            if (!dbManager.IsTableExists("Autors")) return false;
            if (!dbManager.IsTableExists("TracksAutors")) return false;
            return true;
        }

        public static void CreateTable(DbManager dbManager)
        {
            dbManager.CreateTable("Autors", "ID INTEGER NOT NULL UNIQUE, Name TEXT, PRIMARY KEY(ID AUTOINCREMENT)");
            dbManager.CreateTable("TracksAutors", "ID INTEGER NOT NULL UNIQUE, TrackID INTEGER, AutorID INTEGER, FOREIGN KEY(TrackID) REFERENCES Tracks(ID) ON DELETE RESTRICT, FOREIGN KEY(AutorID) REFERENCES Autors(ID) ON DELETE RESTRICT, PRIMARY KEY(ID AUTOINCREMENT)");
        }
    }

    public class TrackGroup
    {
        public string name = "";


        public static long AddNewGroup(TrackGroup group, DbManager dbManager)
        {
            if ((group == null) || (group.name == null) || (group.name == "")) return -1;

            return dbManager.AddRowToTable("Groups", "Name", "'" + group.name + "'");
        }

        public static bool IsExists(string groupName, DbManager dbManager)
        {
            SqliteDataReader sqliteReader;
            bool result;

            string cmd = "SELECT ID FROM Groups WHERE Name='" + groupName + "'";
            sqliteReader = dbManager.ExecuteDbSelectCmd(cmd);
            if (sqliteReader == null)
            {
                return true;
            }

            result = sqliteReader.HasRows;
            sqliteReader.Close();
            return result;
        }

        public static bool IsTableExists(DbManager dbManager)
        {
            return dbManager.IsTableExists("Groups");
        }

        public static void CreateTable(DbManager dbManager)
        {
            dbManager.CreateTable("Groups", "ID INTEGER NOT NULL UNIQUE, Name TEXT, PRIMARY KEY(ID AUTOINCREMENT)");
        }
    }

    public class TrackFile
    {
        public string name = "";
        public string instrName = "";


        public static long AddToDb(long trackID, TrackFile file, DbManager dbManager)
        {
            if (trackID < 1) return -1;
            if (file == null) return -1;
            if (((file.name == null) || (file.name == "")) && ((file.instrName == null) || (file.instrName == ""))) return -1;

            string rowValues = "'" + trackID + "'," +
                               (((file.name == null) || (file.name == "")) ? "NULL" : "'" + file.name + "',") +
                               (((file.instrName == null) || (file.instrName == "")) ? "NULL" : "'" + file.instrName + "'");
            return dbManager.AddRowToTable("TracksFiles", "TrackID, Name, InstrName", rowValues);
        }

        public static List<TrackFile> GetTrackFiles(string trackName, DbManager dbManager)
        {
            SqliteDataReader sqliteReader;
            List<TrackFile> files = new List<TrackFile>();


            string cmd = "SELECT " +
                         "TracksFiles.Name, " +
                         "TracksFiles.InstrName" +
                         "FROM TracksFiles " +
                         "JOIN Tracks ON Tracks.ID = TracksFiles.TrackID " +
                         "WHERE " +
                         "Tracks.Name='" + trackName + "' " +
                         "ORDER BY TracksFiles.Name";

            sqliteReader = dbManager.ExecuteDbSelectCmd(cmd);
            if (sqliteReader == null)
            {
                return null;
            }

            if (sqliteReader.HasRows)
            {
                while (sqliteReader.Read())
                {
                    TrackFile file = new TrackFile();
                    if (!sqliteReader.IsDBNull(0)) file.name = sqliteReader.GetString(0);
                    if (!sqliteReader.IsDBNull(1)) file.name = sqliteReader.GetString(1);
                    files.Add(file);
                }
            }
            else
            {
                sqliteReader.Close();
                return null;
            }

            sqliteReader.Close();
            return files;
        }

        public static bool IsTableExists(DbManager dbManager)
        {
            return dbManager.IsTableExists("TracksFiles");
        }

        public static void CreateTable(DbManager dbManager)
        {
            dbManager.CreateTable("TracksFiles", "ID INTEGER NOT NULL UNIQUE, TrackID INTEGER, Name TEXT, InstrName TEXT, FOREIGN KEY(TrackID) REFERENCES Tracks(ID) ON DELETE RESTRICT, PRIMARY KEY(ID AUTOINCREMENT)");
        }
    }

    public class Track
    {
        public string name = "";
        public string version = "";
        public TrackGroup group = new TrackGroup();
        public List<TrackAutor> autors = new List<TrackAutor>();
        public TrackFile file = new TrackFile();


        public static List<Track> SearchInDb(Track track, DbManager dbManager)
        {
            SqliteDataReader sqliteReader;
            Track foundTrack;
            List<Track> tracks = new List<Track>();
            int i;
            string dbCommand, dbCommandConditions;
            bool isFirstCondition = true;


            dbCommand = "SELECT " +
                        "Tracks.Name, " +
                        "Tracks.Version, " +
                        "Groups.Name as 'Group', " +
                        "TracksFiles.Name as 'FileName', " +
                        "TracksFiles.InstrName as 'InstrFileName' " +
                        "FROM Tracks ";
            if ((track.autors != null) && (track.autors.Count > 0))
            {
                dbCommand += "JOIN Autors ON Autors.ID = TracksAutors.AutorID " +
                             "JOIN TracksAutors ON Tracks.ID = TracksAutors.TrackID ";
            }
            dbCommand += "JOIN TracksFiles ON Tracks.ID = TracksFiles.TrackID ";
            dbCommand += "LEFT JOIN Groups ON Groups.ID = Tracks.GroupID ";

            dbCommandConditions = "WHERE ";

            if (track.name != "")
            {
                dbCommandConditions += "Tracks.Name='" + track.name + "'";
                isFirstCondition = false;
            }

            if (track.version != "")
            {
                if (!isFirstCondition) dbCommandConditions += " AND ";
                dbCommandConditions += "Tracks.Version='" + track.version + "' ";
                isFirstCondition = false;
            }

            if ((track.autors != null) && (track.autors.Count > 0))
            {
                bool isFirstAutor = true;
                if (!isFirstCondition) dbCommandConditions += " AND ";
                isFirstCondition = false;
                dbCommandConditions += "(";

                foreach (var autor in track.autors)
                {
                    if (!isFirstAutor) dbCommandConditions += " OR ";
                    if ((autor.name != null) && (autor.name != ""))
                    {
                        dbCommandConditions += "Autors.Name='" + autor.name + "'";
                        isFirstCondition = false;
                    }
                    else
                    {
                        return null;   ////
                    }
                    isFirstAutor = false;
                }

                dbCommandConditions += ") ";
            }

            if ((track.file != null) && (((track.file.name != null) && (track.file.name != "")) || (track.file.instrName != null) && (track.file.instrName != "")))
            {
                bool isFirstFile = true;
                if (!isFirstCondition) dbCommandConditions += " AND ";
                isFirstCondition = false;
                dbCommandConditions += "(";

                if ((track.file.name != null) && (track.file.name != ""))
                {
                    isFirstFile = false;
                    isFirstCondition = false;
                    dbCommandConditions += "TracksFiles.Name='" + track.file.name + "'";
                }

                if ((track.file.instrName != null) && (track.file.instrName != ""))
                {
                    if (!isFirstFile) dbCommandConditions += " AND ";
                    isFirstFile = false;
                    isFirstCondition = false;
                    dbCommandConditions += "TracksFiles.InstrName='" + track.file.instrName + "'";
                }

                dbCommandConditions += ") ";
            }

            if (track.group.name != "")
            {
                if (!isFirstCondition) dbCommandConditions += " AND ";
                dbCommandConditions += "Groups.Name='" + track.group.name + "' ";
                isFirstCondition = false;
            }


            if (!isFirstCondition) dbCommand += dbCommandConditions;
            dbCommand += "Order by Tracks.Name";

            sqliteReader = dbManager.ExecuteDbSelectCmd(dbCommand);
            if (sqliteReader == null)
            {
                return null;
            }


            if (sqliteReader.HasRows)
            {
                while (sqliteReader.Read())
                {
                    tracks.Add(new Track());
                    foundTrack = tracks[tracks.Count - 1];

                    foundTrack.name = sqliteReader.GetString(0);
                    if (!sqliteReader.IsDBNull(1)) foundTrack.version = sqliteReader.GetString(1);
                    if (!sqliteReader.IsDBNull(2)) foundTrack.group.name = sqliteReader.GetString(2);
                    if (!sqliteReader.IsDBNull(3)) foundTrack.file.name = sqliteReader.GetString(3);
                    if (!sqliteReader.IsDBNull(4)) foundTrack.file.instrName = sqliteReader.GetString(4);
                }
                sqliteReader.Close();
            }
            else
            {
                sqliteReader.Close();
                return null;
            }

            // Get all track autors
            foreach(var tr in tracks)
            {
                tr.autors = TrackAutor.GetTrackAutors(tr.name, dbManager);
            }

            return tracks;
        }

        public static long AddToDb(Track track, DbManager dbManager)
        {
            long trackID = -1;

            if (track.name == "") return -1;
            if (track.autors == null) return -1;
            foreach(var autor in track.autors)
            {
                if ((autor.name == null) || (autor.name == "")) return -1;
            }
            if ((track.file == null) || (((track.file.name == null) || (track.file.name == "")) && ((track.file.instrName == null) || (track.file.instrName == "")))) return -1;


            foreach (var autor in track.autors)
            {
                if (!TrackAutor.IsExists(autor.name, dbManager))
                {
                    // Add new autor
                    if (TrackAutor.AddNewAutor(autor, dbManager) == -1) return -1;
                }
            }

            if (track.group.name != "")
            {
                if (!TrackGroup.IsExists(track.group.name, dbManager))
                {
                    // Add new group
                    if (TrackGroup.AddNewGroup(track.group, dbManager) == -1) return -1;
                }
            }

            // Add new track
            string rowValues = "'" + track.name + "'," +
                               ((track.version != "") ? ("'" + track.version + "'") : "NULL") + "," +
                               ((track.group.name != "") ? ("(SELECT ID FROM Groups WHERE Name='" + track.group.name + "')") : "NULL");
            trackID = dbManager.AddRowToTable("Tracks", "Name, Version, GroupID", rowValues);
            if (trackID == -1)
            {
                return -1;
            }


            // Add track to autors connections
            foreach (var autor in track.autors)
            {
                TrackAutor.ConnetctTrackToAutor(trackID, autor.name, dbManager);
            }

            // Add track to files connections
            TrackFile.AddToDb(trackID, track.file, dbManager);

            return trackID;
        }

        public static bool IsTableExists(DbManager dbManager)
        {
            return dbManager.IsTableExists("Tracks");
        }

        public static void CreateTable(DbManager dbManager)
        {
            dbManager.CreateTable("Tracks", "ID INTEGER NOT NULL UNIQUE, Name TEXT, Version TEXT, GroupID INTEGER, FOREIGN KEY(GroupID) REFERENCES Groups(ID) ON DELETE RESTRICT, PRIMARY KEY(ID AUTOINCREMENT)");
        }
    }

    public class TrackRelationship
    {
        /*
        public static bool ConnectTracks(long parentTrackId, long childrenTrackId, string relationshipType, dbManager dbManager)
        {
            if ((parentTrackId == -1) || (childrenTrackId == -1)) return false;

            dbManager.sqliteCommand.CommandText = "INSERT INTO main.TracksRelationships(ParentTrackID, ChildrenTrackID, TypeID) VALUES (" +
                                            "'" + parentTrackId + "'," +
                                            "'" + childrenTrackId + "'," +
                                            "(SELECT ID FROM RelationshipsTypes WHERE Type='" + relationshipType + "')" +
                                            ");";
            try
            {
                dbManager.sqliteCommand.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                MessageBox.Show(dbManager.sqliteCommand.CommandText + "\n" + e.Message);
                return false;
            }

            return true;
        }

        public static List<Track> GetFirstTrackChildrens(long trackId, dbManager dbManager) {
            if (trackId == -1) return null;

            ////

            return null;
        }

        public static List<Track> GetAllTrackChildrens(long trackId, dbManager dbManager)
        {
            if (trackId == -1) return null;

            ////

            return null;
        }

        public static List<Track> GetTrackParents(long trackId, dbManager dbManager)
        {
            if (trackId == -1) return null;

            ////

            return null;
        }

        public static List<Track> GetAllTrackParents(long trackId, dbManager dbManager)
        {
            if (trackId == -1) return null;

            ////

            return null;
        }

        public static List<Track> GetAllTrackRelationships(long trackId, dbManager dbManager)
        {
            if (trackId == -1) return null;

            ////

            return null;
        }

        public static bool IsTableExists(dbManager dbManager)
        {
            dbManager.sqliteCommand.CommandText = "SELECT name FROM sqlite_master WHERE type = 'table' AND name = 'TracksRelationships'";
            if ((string)dbManager.sqliteCommand.ExecuteScalar() != "TracksRelationships") return false;

            dbManager.sqliteCommand.CommandText = "SELECT name FROM sqlite_master WHERE type = 'table' AND name = 'RelationshipsTypes'";
            if ((string)dbManager.sqliteCommand.ExecuteScalar() != "RelationshipsTypes") return false;
            //// check rows
            return true;
        }

        public static void CreateTable(dbManager dbManager)
        {
            dbManager.sqliteCommand.CommandText = "CREATE TABLE RelationshipsTypes (ID INTEGER NOT NULL UNIQUE, Type TEXT, PRIMARY KEY(ID AUTOINCREMENT))";
            dbManager.sqliteCommand.ExecuteNonQuery();
            dbManager.sqliteCommand.CommandText = "INSERT INTO main.RelationshipsTypes(Type) VALUES " +
                                                                                        "('Remix'), " +
                                                                                        "('Format'), " +
                                                                                        "('NewVision')";
            dbManager.sqliteCommand.ExecuteNonQuery();

            dbManager.sqliteCommand.CommandText = "CREATE TABLE TracksRelationships (ID INTEGER NOT NULL UNIQUE, ParentTrackID INTEGER, ChildrenTrackID INTEGER, TypeID INTEGER, FOREIGN KEY(ParentTrackID) REFERENCES Tracks(ID) ON DELETE RESTRICT, FOREIGN KEY(ChildrenTrackID) REFERENCES Tracks(ID) ON DELETE RESTRICT, FOREIGN KEY(TypeID) REFERENCES RelationshipsTypes(ID) ON DELETE RESTRICT, PRIMARY KEY(ID AUTOINCREMENT))";
            dbManager.sqliteCommand.ExecuteNonQuery();
        }
        */
    }



    public class DbReports
    {
        private const int DB_VERSION = 4;


        public static bool CheckDb(DbManager dbManager)
        {
            bool isCorrectDB = true;

            // Check DB empty
            if (!dbManager.isEmptyDB)
            {
                string tableCheckResult;
                int tableCheckCnt = 0;

                // Check tables exists
                tableCheckResult = dbManager.ExecuteScalarDbSelectCmd("SELECT name FROM sqlite_master WHERE type = 'table' AND name = 'muzLib_Settings'");
                if (tableCheckResult == "muzLib_Settings") tableCheckCnt++;

                if (TrackAutor.IsTableExists(dbManager)) tableCheckCnt++;
                if (TrackGroup.IsTableExists(dbManager)) tableCheckCnt++;
                if (Track.IsTableExists(dbManager)) tableCheckCnt++;
                if (TrackFile.IsTableExists(dbManager)) tableCheckCnt++;
                ////if (TrackRelationship.IsTableExists(dbManager)) tableCheckCnt++;

                else if (tableCheckCnt != 6)
                {
                    isCorrectDB = false;
                    MessageBox.Show("Incorrect DB file!");
                    return false;
                }

                // Check DB version
                tableCheckResult = dbManager.ExecuteScalarDbSelectCmd("SELECT Value FROM muzLib_Settings WHERE Setting='DB_Version'");
                int real_db_version = Convert.ToInt32(tableCheckResult);
                if (real_db_version != DB_VERSION)
                {
                    isCorrectDB = false;
                    MessageBox.Show("Unsupported DB version!");
                    return false;
                }

            }


            if (dbManager.isEmptyDB && isCorrectDB)
            {
                dbManager.CreateTable("muzLib_Settings", "Setting TEXT NOT NULL UNIQUE, Value TEXT, PRIMARY KEY(Setting)");
                // Add DB version
                dbManager.AddRowToTable("muzLib_Settings", "Setting, Value", "'DB_Version', '" + DB_VERSION.ToString() + "'");

                TrackAutor.CreateTable(dbManager);
                TrackGroup.CreateTable(dbManager);
                Track.CreateTable(dbManager);
                TrackFile.CreateTable(dbManager);
                ////TrackRelationship.CreateTable(dbManager);
            }

            return isCorrectDB;
        }

    }

}
