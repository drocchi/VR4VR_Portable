using UnityEngine;
using System.Collections;
using System.IO;

using MySql.Data;
using MySql.Data.MySqlClient;

public class saverS : MonoBehaviour
{

    public static int levelScore;
    public static int completionTime;
    public static int result; //1 success, 2 fail, 3 terminated
    public static int instanceCount;
    public static int successCount;
    public static string successTimes;
    public static int failCount;
    public static string failTimes;
    public static int promptCount;
    public static string promptTimes;
    public static int distracterCount;
    public static string distracterOrder;
    public static string distracterTimes;
    public static string sessionTime;

    public GUIStyle ScoreStyle;

    private static StreamWriter writer;
    private static string msg;

    private static bool sessionDataCreated = false;

    private static bool scoreActive = false;
    private static string scoreLabel;
    private static float scoreStartTime;
    private static float scoreTime = 4.0f;
    private static MySqlConnection conn=null;
  
    public static void StartSaveSession()
    {
        if (!sessionDataCreated)
        {
            ResetData();

            //string log_name = sessionTime + ".vr4vr";
            string log_name = sessionTime + ".txt";

            //if (Application.isEditor)
            //{
            //	Directory.CreateDirectory(Application.dataPath + "/../../Logs");
            //	writer = new StreamWriter(Application.dataPath + "/../../Logs/" + log_name);
            //}
            //else
            //{
            //	Directory.CreateDirectory(Application.dataPath + "/../Logs");
            //	writer = new StreamWriter(Application.dataPath + "/../Logs/" + log_name);
            //}

            Directory.CreateDirectory(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop) + "/VR4VR Results");
            writer = new StreamWriter(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop) + "/VR4VR Results/" + log_name);

            sessionDataCreated = true;
            SaveText(log_name);
            SaveText("Skill: " + generalManagerS.ActiveSkill.ToString() + ", Subtask: " + generalManagerS.ActiveSubTask.ToString() + ", Level: " + generalManagerS.ActiveLevel.ToString());
            Debug.Log("Skill: " + generalManagerS.ActiveSkill.ToString() + ", Subtask: " + generalManagerS.ActiveSubTask.ToString() + ", Level: " + generalManagerS.ActiveLevel.ToString());
            msg = "MySQL Session Started...";
        }
    }
    public static void SaveText(string _text)
    {
        if (!sessionDataCreated) StartSaveSession();

        if (Application.isEditor)
        {
            writer.WriteLine(Time.timeSinceLevelLoad.ToString("F2") + ": " + _text);
        }
        //else
        //{
        //    string encryptedText = Common.Cryptography.AES.Encrypt(Time.timeSinceLevelLoad.ToString("F2") + ": " + _text, "vr4vr@kdfD6e!v*");
        //    writer.WriteLine(encryptedText);
        //}

        writer.Flush();
    }

    public static void SaveDataBase()
    {
        //if (!sessionDataCreated) StartSaveSession();
        if (sessionDataCreated)
        {
            //string connStr = "server=localhost;user=root;database=vr4vr;port=3306;password=vrvr";
            string connStr = "server=localhost;user=RRT;database=vr4vr;port=3306;password=carrt_RRT;";
            conn = new MySqlConnection(connStr);
            try
            {

                conn.Open();

                    levelScore = CalculateScore();
                    completionTime = (int)(Time.timeSinceLevelLoad - timerS.levelStartTime);

                    //string sql = "INSERT INTO sessions (JobCoachID, UserID, SkillNo, SubtaskNo, LevelNo, LevelScore, CompletionTime, Result, InstanceCount, SuccessCount, SuccessTimes, FailCount, FailTimes, PromptCount, PromptTimes, DistracterCount, DistracterOrder, DistracterTimes, SessionTime) VALUES (2, 1, 1, 1, 1, 20, 121, 1, 1, 1, '121-', 0, '', 0, '', 0, '', '', '" + sessionTime + "')";
                    string variables = generalManagerS.jobCoachID.ToString() + "," +
                    generalManagerS.userID.ToString() + "," +
                        ((int)generalManagerS.ActiveSkill).ToString() + "," +
                        ((int)generalManagerS.ActiveSubTask).ToString() + "," +
                        ((int)generalManagerS.ActiveLevel).ToString() + "," +
                        levelScore.ToString() + "," +
                        completionTime.ToString() + "," +
                        result.ToString() + "," +
                        instanceCount.ToString() + "," +
                        successCount.ToString() + "," +
                        "'" + successTimes + "'," +
                        failCount.ToString() + "," +
                        "'" + failTimes + "'," +
                        promptCount.ToString() + "," +
                        "'" + promptTimes + "'," +
                        distracterCount.ToString() + "," +
                        "'" + distracterOrder + "'," +
                        "'" + distracterTimes + "'," +
                        "'" + sessionTime + "'";

                    string sql = "INSERT INTO sessions (JobCoachID, UserID, SkillNo, SubtaskNo, LevelNo, LevelScore, CompletionTime, Result, InstanceCount, SuccessCount, SuccessTimes, FailCount, FailTimes, PromptCount, PromptTimes, DistracterCount, DistracterOrder, DistracterTimes, SessionTime) VALUES (" + variables + ")";

                    //if (OpenConnection() == true)
                    //{
                    //create command and assign the query and connection from the constructor
                    MySqlCommand cmd = new MySqlCommand(sql, conn);

                    //Execute command
                    cmd.ExecuteNonQuery();

                    //close connection
                    //    CloseConnection();
                    //}

                    SaveText(variables);
                    if (writer != null) writer.Close();
                    //sessionDataCreated = false;
                    //msg = "MySQL Data Inserted...";
                
            }
            catch(UnityException ex)
            {
                msg = ex.ToString();
            }
        conn.Close();

        sessionDataCreated = false;
    }
    }

    private static void ResetData()
    {
        levelScore = 0;
        completionTime = 0;
        result = 0;
        instanceCount = 0;
        successCount = 0;
        successTimes = "";
        failCount = 0;
        failTimes = "";
        promptCount = 0;
        promptTimes = "";
        distracterCount = 0;
        distracterOrder = "";
        distracterTimes = "";
        sessionTime = System.DateTime.Now.ToString("yyyy-MM-dd HH-mm-ss");
    }
    private static int CalculateScore()
    {
        int score = 0;
        if (instanceCount > 0)
        {
            if (result == 1)
                score = 100 - (failCount * 10) - (promptCount * 5);
            else
                score = (int)((100.0f / (float)instanceCount) * (float)successCount) - (failCount * 10) - (promptCount * 5);

            if (score < 0) score = 0;
            if (score > 100) score = 100;
            scoreLabel = "Score: " + score.ToString();

            scoreStartTime = Time.time;
            scoreActive = true;
        }
        return score;
    }

    void OnGUI()
    {

        GUI.Label(new Rect(720, 20, 250, 20), msg);

        if (scoreActive)
        {
            userInterfaceS.drawText(new Vector2(0.375f, 0.6f), new Vector2(0.25f, 0.1f), scoreLabel, ScoreStyle);

            if (Time.time > scoreStartTime + scoreTime) scoreActive = false;
        }
    }
}

