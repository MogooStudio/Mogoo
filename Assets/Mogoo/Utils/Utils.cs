using System;
using System.IO;
using System.Collections;
using System.Text;

using UnityEngine;
using UnityEngine.UI;

using SimpleJSON;
using static System.Char;

namespace Mogoo.Utils
{
    public class Utils
    {
        // 读取对象Object
        public static UnityEngine.Object LoadResources(string path)
        {
            var obj = Resources.Load(path);
            if (obj == null) return null;
            var go = UnityEngine.Object.Instantiate(obj);
            return path != null ? go : null;
        }

        // 读取音效
        public static AudioClip LoadAudio(string path)
        {
            AudioClip audio = null;
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
        public static Color ColorFromString(string color)
        {

            var r = VFromChar(color[0]) * 16 + VFromChar(color[1]);
            var g = VFromChar(color[2]) * 16 + VFromChar(color[3]);
            var b = VFromChar(color[4]) * 16 + VFromChar(color[5]);
            var a = VFromChar(color[6]) * 16 + VFromChar(color[7]);
            return new UnityEngine.Color(r * 1f / 255, g * 1f / 255, b * 1f / 255, a * 1f / 255);
        }

        public static int VFromChar(int c)
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
            var fs = new FileStream(path, FileMode.Create);
            fs.Write(bytes, 0, bytes.Length);
            fs.Flush();
            fs.Close();
        }

        // 写txt文件
        public static void WriteTxt(string path, string text)
        {
            var fs = new FileStream(path, FileMode.Create);
            var data = System.Text.Encoding.UTF8.GetBytes(text.ToString());
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
            var txt = ((TextAsset)Resources.Load(path)).text;
            return txt;
        }

        // 项目内部读文件json
        public static JSONNode LoadGameJson(string path)
        {
            var txt = ((TextAsset)Resources.Load(path)).text;
            var json = JSONNode.Parse(txt);
            return json;
        }

        // 项目内读取二进制文件
        public static byte[] LoadBytes(string path)
        {
            var file = new FileStream(path, FileMode.Open);
            var len = (int)file.Length;
            var byData = new byte[len];
            file.Read(byData, 0, len);
            return byData;
        }

        // 外部读txt文件
        public static string LoadString(string path)
        {
            var file = new FileStream(path, FileMode.Open);
            var len = (int)file.Length;
            var byData = new byte[len];
            file.Read(byData, 0, len);
            var text = Encoding.UTF8.GetString(byData);
            file.Close();
            return text;
        }

        // 字符串是否有中文字
        public static bool IsChinese(string text)
        {
            for (var i = 0; i < text.Length; i++)
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
            for (var i = 0; i < text.Length; i++)
            {
                if (!IsLetter(text[i]) && !Char.IsNumber(text[i]))
                {
                    return true;
                }
            }
            return false;
        }

        // 字符串长度（中文字为2个字符）
        public static int GetStringLength(string text)
        {
            var num = 0;
            for (var i = 0; i < text.Length; i++)
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

            var temp = 0;
            for (var i = 0; i < text.Length; i++)
            {
                if (System.Text.RegularExpressions.Regex.IsMatch(text[i].ToString(), @"^[\u4e00-\u9fa5]+$"))
                {
                    temp++;
                }
            }
            Debug.LogError(text + "==" + temp + "===" + text.Length + "  " + temp);
            return text.Length + temp > num;
        }

        // 字符串是否纯数字
        public static bool IsNumber(string str)
        {
            for (var i = 0; i < str.Length; i++)
            {
                if (!char.IsNumber(str, i))
                {
                    return false;
                }
            }
            return true;
        }

        // 解析时间戳
        public static string[] GetTimeStamp(string time)
        {
            var timeStamp = long.Parse(time);
            var dtStart = TimeZoneInfo.ConvertTime(new System.DateTime(1970, 1, 1), TimeZoneInfo.Local);
            var lTime = timeStamp * 10000000;

            var toNow = new System.TimeSpan(lTime);

            var dtResult = dtStart.Add(toNow);
            var dateStr = dtResult.ToShortDateString().ToString();
            var timeStr = dtResult.ToString("HH:mm:ss");
            var dateArr = dateStr.Split('/');
            var timeArr = timeStr.Split(':');
            var secondArr = timeArr[2];
            var second = secondArr.ToCharArray();

            var result = new string[]{ dateArr[0] + "月" + dateArr[1] + "日",
                                        timeArr[0] + ":" +timeArr[1] + ":"
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

            if (!(info is DirectoryInfo dirInfo))
                return;

            var fileSysInfos = dirInfo.GetFileSystemInfos();
            for (var i = 0; i < fileSysInfos.Length; i++)
            {
                if (fileSysInfos[i] is FileInfo fileInfo)
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

