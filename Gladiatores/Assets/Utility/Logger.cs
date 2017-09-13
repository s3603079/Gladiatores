//  @note   http://nanoappli.com/blog/archives/6511

using System.Collections;
using UnityEngine;

public class Logger : MonoBehaviour
{
    // ログの記録
    static Hashtable logMsg_ = new Hashtable();
    GUIStyle style_ = new GUIStyle();

    public static void Log(object argKey, string argMeg)
    {
        logMsg_.Add(argKey, argMeg);
    }
    public static void RemoveLog(object argKey)
    {
        logMsg_.Remove(argKey);
    }

    // 記録されたログを画面出力する
    void OnGUI()
    {
        Rect rect = new Rect(0, 0, Screen.width, Screen.height);

        // フォントサイズを10px,白文字にする。
        // styleのnewは遅いため、本来はStart()で実施すべき...
        style_.fontSize = 20;
        style_.fontStyle = FontStyle.Normal;
        style_.normal.textColor = Color.white;

        // 出力された文字列を改行でつなぐ
        string outMessage = "";
        foreach (string msg in logMsg_.Values)
        {
            outMessage += msg + System.Environment.NewLine;
        }

        // 改行でつないだログメッセージを画面に出す
        GUI.Label(rect, outMessage, style_);
    }
}
