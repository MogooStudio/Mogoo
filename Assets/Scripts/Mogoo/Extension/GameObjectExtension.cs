using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mogoo.Extension
{
    public class GameObjectExtension
    {
        //读取创建预制
        public static GameObject createGameObject(string path)
        {
            if (path == null || path == "") return null;
            GameObject obj = null;
            try
            {
                obj = GameObject.Instantiate(Resources.Load(path)) as GameObject;
                NameReset(obj);
            }
            catch (System.Exception ex)
            {
                Debug.LogError("!!!! path = " + path);
            }
            return obj;
        }

        //读取创建预制 并设置父类
        public static GameObject createGameObjectTr(string path, GameObject go)
        {
            if (path == null || path == "") return null;
            GameObject obj = null;
            try
            {
                obj = GameObject.Instantiate(Resources.Load(path)) as GameObject;
                NameReset(obj);
            }
            catch (System.Exception ex)
            {
                Debug.LogError("!!!! path = " + path);
            }
            try
            {
                obj.transform.parent = go.transform;
                obj.transform.localPosition = Vector3.zero;
            }
            catch (System.Exception ex)
            {
                Debug.LogError("!!!! GO  Tr = Null");
            }
            return obj;
        }

        //重置 预制件名字
        public static void NameReset(GameObject go)
        {
            int fpos = go.name.IndexOf("(");
            if (fpos >= 0)
            {
                go.name = go.name.Substring(0, fpos);
            }
        }

        //返回物体内名字为 “” 的gameobject
        static GameObject findGo = null;
        public static GameObject GetNameFindGameObject(GameObject go, string name)
        {
            findGo = null;
            GetFindGameObjectName(go, name);

            if (findGo != null)
            {
                return findGo;
            }
            return findGo;
        }

        static void GetFindGameObjectName(GameObject go, string name)
        {
            bool find = false;
            for (int i = 0; i < go.transform.childCount; i++)
            {
                if (go.transform.GetChild(i).name == name)
                {
                    find = true;
                    findGo = go.transform.GetChild(i).gameObject;
                    return;
                }
            }
            if (!find)
            {
                for (int i = 0; i < go.transform.childCount; i++)
                {
                    if (go.transform.GetChild(i).childCount > 0)
                    {
                        GetFindGameObjectName(go.transform.GetChild(i).gameObject, name);
                    }
                }
            }
        }
    }

}

