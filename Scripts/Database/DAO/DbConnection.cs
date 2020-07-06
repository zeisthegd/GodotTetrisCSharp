using System;
using Godot;
using System.Data;
using System.Data.SqlClient;

namespace Database
{
    public class DbConnection : Node
    {

        public static bool SaveFileExists()
        {
            File playersData = new File();

            if (playersData.FileExists(AutoLoad.SavePath))
                return true;

            return false;
        }

        public static void WriteToFile(string[] players)
        {
            System.IO.File.WriteAllText(AutoLoad.SavePathForCSharp, String.Empty);

            using (System.IO.StreamWriter file =
            new System.IO.StreamWriter(AutoLoad.SavePathForCSharp))
            {

                for (int i = 0; i < players.Length; i++)
                {
                    if (i != players.Length - 1)
                        file.WriteLine(players[i]);
                    else file.Write(players[i]);
                }
            }
        }
    }
}
