using System;
using System.IO;
using System.Collections;
using System.Text;

using UnityEngine;
using UnityEngine.UI;

using SimpleJSON;

namespace Mogoo.Utils
{
    public class Utils
    {
        // 读取对象Object
        public static UnityEngine.Object LoadResources(string path)
        {
            UnityEngine.Object obj = Resources.Load(path);
            if (obj == null) return null;
            UnityEngine.Object go = UnityEngine.Object.Instantiate(obj);
            return path != null ? go : null;
        }

        // 读取音效
        public static AudioClip LoadAudio(string path)
        {
            AudioClip audio = null;// = new AudioClip();

            try
            {
                audio = (AudioClip)Resources.Load(path, typeof(AudioClip));
            }
            catch (System.Exception ex)
            {
                Debug.LogError("!!!! audio  = Null  path =" + path);
                return null;
            }
            return audio;
        }

        // 返回text
        public static Text GetUGUI_Text(GameObject go, string path)
        {
            if (go == null || path == null) { return null; }

            Text text;
            if (path == "")
            {
                text = go.GetComponent<Text>();
            }
            else
                text = go.transform.Find(path).GetComponent<Text>();

            return text;
        }


        // 秒转小时、分、秒
        public static string FormatTime_H(int seconds)
        {
            int intH = seconds / 3600;
            string strH = intH < 10 ? "0" + intH.ToString() : intH.ToString();
            int intM = (seconds % 3600) / 60;
            string strM = intM < 10 ? "0" + intM.ToString() : intM.ToString();
            int intS = seconds % 3600 % 60;
            string strS = intS < 10 ? "0" + intS.ToString() : intS.ToString();
            return strH + ":" + strM + ":" + strS;
        }

        // 秒转分、秒
        public static string FormatTime_M(int seconds)
        {
            int intM = seconds / 60;
            string strM = intM < 10 ? "0" + intM.ToString() : intM.ToString();
            int intS = seconds % 60;
            string strS = intS < 10 ? "0" + intS.ToString() : intS.ToString();
            return strM + ":" + strS;
        }

        // 颜色字符（0xffffffff）转换 color
        public static Color ColorFromString(string colorstring)
        {

            int r = VFromChar(colorstring[0]) * 16 + VFromChar(colorstring[1]);
            int g = VFromChar(colorstring[2]) * 16 + VFromChar(colorstring[3]);
            int b = VFromChar(colorstring[4]) * 16 + VFromChar(colorstring[5]);
            int a = VFromChar(colorstring[6]) * 16 + VFromChar(colorstring[7]);
            return new UnityEngine.Color(r * 1f / 255, g * 1f / 255, b * 1f / 255, a * 1f / 255);
        }

        static int VFromChar(int c)
        {
            if (c >= '0' && c <= '9')
            {
                return c - '0';
            }
            else if (c >= 'A' && c <= 'F')
            {
                return c - 'A' + 10;
            }
            else
            {
                return c - 'a' + 10;
            }
        }

        // 3D物体在2D屏幕上的位置
        public static Vector3 GetUIPosBy3DGameObj(GameObject gobj3d,
                                       Camera camer3d, Camera camera2d, float z, float y)
        {
            Vector3 v1 = camer3d.WorldToViewportPoint(new Vector3(gobj3d.transform.position.x, y, gobj3d.transform.position.z));
            Vector3 v2 = camera2d.ViewportToWorldPoint(v1);
            v2.z = z;
            return v2;
        }

        //设置2d物体 到 3D物体在屏幕上的位置
        public static void SetUIPosBy3DGameObj(GameObject gobj2d, GameObject gobj3d,
                                       Camera camer3d, Camera camera2d, float z, Vector3 offset)
        {
            Vector3 v1 = camer3d.WorldToViewportPoint(gobj3d.transform.position);
            Vector3 v2 = camera2d.ViewportToWorldPoint(v1);
            v2.z = z;
            gobj2d.transform.position = v2 + offset;
        }

        // 写二进制文件
        public static void WriteBytes(string path, byte[] bytes)
        {
            FileStream fs = new FileStream(path, FileMode.Create);
            fs.Write(bytes, 0, bytes.Length);
            fs.Flush();
            fs.Close();
        }

        // 写txt文件
        public static void WriteTxt(string path, string text)
        {
            FileStream fs = new FileStream(path, FileMode.Create);
            byte[] data = System.Text.Encoding.UTF8.GetBytes(text.ToString());
            fs.Write(data, 0, data.Length);
            fs.Flush();
            fs.Close();
        }

        // 删除文件
        public static void RemoveTxt(string path)
        {
            File.Delete(path);
        }

        // 项目内部读文件String
        public static string LoadGameString(string path)
        {
            string txt = ((TextAsset)Resources.Load(path)).text;
            return txt;
        }

        // 项目内部读文件json
        public static JSONNode LoadGameJson(string path)
        {
            string txt = ((TextAsset)Resources.Load(path)).text;
            JSONNode json = JSONNode.Parse(txt);
            return json;
        }

        // 项目内读取二进制文件
        public static byte[] LoadBytes(string path)
        {
            FileStream file = new FileStream(path, FileMode.Open);
            int len = (int)file.Length;
            byte[] byData = new byte[len];
            file.Read(byData, 0, len);
            return byData;
        }

        // 外部读txt文件
        public static string LoadString(string path)
        {
            FileStream file = new FileStream(path, FileMode.Open);
            int len = (int)file.Length;
            byte[] byData = new byte[len];
            file.Read(byData, 0, len);
            string text = Encoding.UTF8.GetString(byData);
            file.Close();
            return text;
        }

        // 字符串是否有中文字
        public static bool IsChinese(string text)
        {
            for (int i = 0; i < text.Length; i++)
            {
                if (System.Text.RegularExpressions.Regex.IsMatch(text[i].ToString(), @"^[\u4e00-\u9fa5]+$"))
                {
                    return true;
                }
            }
            return false;
        }

        // 字符串是否有特殊符号
        public static bool IsSymbol(string text)
        {
            for (int i = 0; i < text.Length; i++)
            {
                if (!char.IsLetter(text[i]) && !char.IsNumber(text[i]))
                {
                    return true;
                }
            }
            return false;
        }

        // 字符串长度（中文字为2个字符）
        public static int GetStringLength(string text)
        {
            int num = 0;
            for (int i = 0; i < text.Length; i++)
            {
                if (System.Text.RegularExpressions.Regex.IsMatch(text[i].ToString(), @"^[\u4e00-\u9fa5]+$"))
                {
                    num++;
                }
            }

            return text.Length + num;
        }

        // 中英字是否超出长度
        public static bool IsStringLength(string text, int num)
        {
            if (text.Length > num) return true;

            int temp = 0;
            for (int i = 0; i < text.Length; i++)
            {
                if (System.Text.RegularExpressions.Regex.IsMatch(text[i].ToString(), @"^[\u4e00-\u9fa5]+$"))
                {
                    temp++;
                }
            }
            Debug.LogError(text + "==" + temp + "===" + text.Length + "  " + temp);
            if (text.Length + temp > num)
            {
                return true;
            }
            return false;
        }

        // 字符串是否纯数字
        public static bool IsNumber(string str)
        {
            for (int i = 0; i < str.Length; i++)
            {
                if (!Char.IsNumber(str, i))
                {
                    return false;
                }
            }
            return true;
        }

        // 解析时间戳
        public static string[] GetTimeStamp(string _time)
        {
            long timeStamp = long.Parse(_time);
            System.DateTime dtStart = TimeZoneInfo.ConvertTime(new System.DateTime(1970, 1, 1), TimeZoneInfo.Local);
            long lTime = timeStamp * 10000000;

            System.TimeSpan toNow = new System.TimeSpan(lTime);

            System.DateTime dtResult = dtStart.Add(toNow);
            string date = dtResult.ToShortDateString().ToString();
            string time = dtResult.ToString("HH:mm:ss");
            string[] date_arr = date.Split('/');
            string[] time_arr = time.Split(':');
            string secondarr = time_arr[2];
            char[] second = secondarr.ToCharArray();

            string[] result = new string[]{ date_arr[0] + "月" + date_arr[1] + "日",
                                        time_arr[0] + ":" +time_arr[1] + ":"
                                        + second[0] + second[1]};

            return result;
        }

        /// <summary>
        /// 延时执行
        /// </summary>
        public static IEnumerator DelayTo(Action action, float seconds)
        {
            yield return new WaitForSeconds(seconds);
            action();
        }

        /// <summary>
        /// 获取文件并执行相应处理
        /// </summary>
        public static void GetAllFile(FileSystemInfo info, Action<FileInfo> action)
        {
            if (!info.Exists)
                return;

            DirectoryInfo dirInfo = info as DirectoryInfo;
            if (dirInfo == null)
                return;

            FileSystemInfo[] fileSysInfos = dirInfo.GetFileSystemInfos();
            for (int i = 0; i < fileSysInfos.Length; i++)
            {
                FileInfo fileInfo = fileSysInfos[i] as FileInfo;
                if (fileInfo != null)
                {
                    action(fileInfo);
                }
            }
        }

        /// <summary>
        /// 判断是否是模拟器
        /// </summary>
        /// <returns></returns>
        public static bool IsSimulator()
        {
#if UNITY_ANDROID
        //判断是否存在光传感器来判断是否为模拟器
        AndroidJavaObject sensorManager  = currentActivity.Call<AndroidJavaObject>("getSystemService", "sensor");
        AndroidJavaObject sensor  = sensorManager.Call<AndroidJavaObject>("getDefaultSensor",5);
        return sensor;
#endif
            return false;
        }
    }
}

