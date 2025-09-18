using Microsoft.VisualBasic.ApplicationServices;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrackBar;

namespace muzLibHelper
{
    public partial class Form1 : Form
    {
        private DbManager dbManager;

        public Form1()
        {
            InitializeComponent();

            dbManager = new DbManager();
            dbManager.OpenDb("C:\\Users\\Dmitry\\Downloads\\testMDb.db");
            DbReports.CheckDb(dbManager);

            helpRichTextBox.Text = "Search prompt:\r\n" +
                                   "(NAME [(VERSION)] | ..)  (AUTOR1[, AUTOR2, AUTOR3] | ..)  [FILE | ..]  [GROUP | ..]\r\n" +
                                   "\r\nAdd prompt:\r\n" +
                                   "NAME [(VERSION)]  AUTOR1[, AUTOR2, AUTOR3]  FILE  [GROUP]";
        }

        private void searchButton_Click(object sender, EventArgs e)
        {
            List<Track> tracks;
            bool isFirstAutor;
            string autors;
            string trackNameAndVersion;

            searchResultGridView.Rows.Clear();
            Track track = parseCmdString(inputTextBox.Text);
            if (track != null)
            {
                operationResultLabel.ForeColor = SystemColors.ControlText;

                tracks = Track.SearchInDb(track, dbManager);
                if (tracks == null)
                {
                    operationResultLabel.Text = "Empty";
                    return;
                }

                foreach (var tr in tracks)
                {
                    isFirstAutor = true;
                    autors = "";
                    if (tr.autors.Count > 0)
                    {
                        foreach (var autor in tr.autors)
                        {
                            if (!isFirstAutor) autors += ", ";
                            autors += autor.name;
                            isFirstAutor = false;
                        }
                    }

                    trackNameAndVersion = tr.name;
                    if ((tr.version != null) && (tr.version != "")) trackNameAndVersion += " (" + tr.version + ")";

                    searchResultGridView.Rows.Add(trackNameAndVersion,
                                                 autors,
                                                 tr.file.name,
                                                 //// instrName
                                                 tr.group.name
                                                 );
                }

                operationResultLabel.Text = tracks.Count.ToString() + " tracks";
            }
            else
            {
                operationResultLabel.ForeColor = Color.IndianRed;
                operationResultLabel.Text = "Error!";
            }
        }


        private Track parseCmdString(string cmd)
        {
            string[] nameAndVersion;
            string autorName, name, version, file, group;
            Track track = new Track();


            if (cmd == null) return null;

            string[] cmdParts = cmd.Split("  ");
            if (cmdParts.Length < 2) return null;
            string[] autors = cmdParts[1].Split(",");
            if ((autors.Length < 1) || (autors.Length > 3)) return null;


            nameAndVersion = cmdParts[0].Trim().Split("(");
            name = nameAndVersion[0].Trim();
            if (nameAndVersion.Length > 1) version = nameAndVersion[1].Trim(')').Trim();
            else version = "";
            if (name == "..") name = "";
            track.name = name;
            track.version = version;

            foreach (var autor in autors)
            {
                autorName = autor.Trim();
                if (autorName != "..") track.autors.Add(new TrackAutor() { name = autorName });
            }

            if (cmdParts.Length >= 3)
            {
                file = cmdParts[2].Trim();
                //// instrName
                if (file != "..") track.file.name = file;
            }

            if (cmdParts.Length >= 4)
            {
                group = cmdParts[3].Trim();
                if (group == "..") group = "";
                track.group.name = group;
            }

            return track;
        }





        private void addButton_Click(object sender, EventArgs e)
        {
            Track track = parseCmdString(inputTextBox.Text);
            dbManager.newHistoryGroup();
            Track.AddToDb(track, dbManager);
            if (Track.AddToDb(track, dbManager) >= 0)
            {
                inputTextBox.Clear();
                operationResultLabel.ForeColor = SystemColors.ControlText;
                operationResultLabel.Text = "\"" + track.name + "\" added";

                /*
                foreach (var logStr in dbManager.cmdHistory[dbManager.cmdHistory.Count - 1].cmds)
                {
                    logRichTextBox.Text += logStr;
                    logRichTextBox.Text += "\n";
                }
                logRichTextBox.Text += "\n";*/
            }
            else
            {
                operationResultLabel.ForeColor = Color.IndianRed;
                operationResultLabel.Text = "Error!";
            }
        }

        private void massAddButton_Click(object sender, EventArgs e)
        {
            string[] cmds = massAddRichTextBox.Text.Split("\n");
            if ((cmds == null) || (cmds.Length == 0))
            {
                massAddReasultLabel.ForeColor = Color.IndianRed;
                massAddReasultLabel.Text = "Input error!";
                return;
            }

            massAddReasultLabel.ForeColor = SystemColors.ControlText;
            massAddReasultLabel.Text = "Process...";

            foreach (var cmd in cmds)
            {
                Track track = parseCmdString(cmd);
                dbManager.newHistoryGroup();
                if (Track.AddToDb(track, dbManager) == -1)
                {
                    MessageBox.Show("Incorrect cmd:\r\n" + cmd);
                    massAddReasultLabel.ForeColor = Color.IndianRed;
                    massAddReasultLabel.Text = "Error!";
                    return;
                }

                /*
                foreach (var logStr in dbManager.cmdHistory[dbManager.cmdHistory.Count - 1].cmds)
                {
                    logRichTextBox.Text += logStr;
                    logRichTextBox.Text += "\n";
                }
                logRichTextBox.Text += "\n";*/
            }

            massAddReasultLabel.ForeColor = SystemColors.ControlText;
            massAddReasultLabel.Text = "Done";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            dbManager.ApplyDbChanges();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            dbManager.historyUndo();
        }
    }
}
