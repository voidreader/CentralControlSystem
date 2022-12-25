using System;
using System.Collections;
using System.Collections.Generic;
using LitJson;
using UnityEngine;


namespace PIERStory {

    public static class CentralUtility 
    {
        
        public static class JsonHelper {

            /// <summary>
            /// JSON 특정 노드의 노드 값을 알려주쇼
            /// </summary>
            /// <param name="__node"></param>
            /// <param name="__col"></param>
            /// <returns></returns>
            public static JsonData GetJsonNode(JsonData __node, string __col) {

                if (__node == null)
                    return null;

                if (!__node.ContainsKey(__col))
                    return null;
                    
                if (__node[__col] == null)
                    return null;
                    
                return __node[__col];
            }
            
            /// <summary>
            /// 특정 노드의 int 값을 알려주세요. 
            /// </summary>
            /// <param name="__node"></param>
            /// <param name="__col"></param>
            /// <returns></returns>
            public static int GetJsonNodeInt(JsonData __node, string __col) {
                if (__node == null || !__node.ContainsKey(__col))
                    return 0;


                if (__node[__col] == null)
                    return 0;
                
                try {    
                    return int.Parse(GetJsonNodeString(__node, __col));
                }
                catch (Exception e) {
                    Debug.LogError(e.StackTrace);
                    return 0;
                }

                
            }
            
            public static long GetJsonNodeLong(JsonData __node, string __col) {
                if (__node == null || !__node.ContainsKey(__col))
                    return 0;


                if (__node[__col] == null)
                    return 0;
                
                try {    
                    return long.Parse(GetJsonNodeString(__node, __col));
                }
                catch (Exception e) {
                    return 0;
                }

                
            }
            
            
            /// <summary>
            /// 
            /// </summary>
            /// <param name="__node"></param>
            /// <param name="__col"></param>
            /// <returns></returns>
            public static float GetJsonNodeFloat(JsonData __node, string __col) {
                if (__node == null || !__node.ContainsKey(__col))
                    return 0;


                if (__node[__col] == null)
                    return 0;
                
                try {    
                    return float.Parse(GetJsonNodeString(__node, __col));
                }
                catch {
                    return 0;
                }
            }
            

            /// <summary>
            /// JSON 특정 노드의 string 값을 알려주세요
            /// </summary>
            /// <param name="__node"></param>
            /// <param name="__col"></param>
            /// <returns></returns>
            public static string GetJsonNodeString(JsonData __node, string __col)
            {
            
                if (__node == null || !__node.ContainsKey(__col))
                    return string.Empty;


                if (__node[__col] == null)
                    return string.Empty;

                return __node[__col].ToString();
                
            }
            
            /// <summary>
            /// 특정 노드의 bool 값을 알려주세요.
            /// </summary>
            /// <param name="__node"></param>
            /// <param name="__col"></param>
            /// <returns></returns>
            public static bool GetJsonNodeBool(JsonData __node, string __col) {
                
                if (__node == null || !__node.ContainsKey(__col)) {
                    // Debug.Log(string.Format("Error : [{0}] is not a node", __col));
                    return false;
                }
                    
                if (__node[__col] == null) {
                    Debug.Log(string.Format("Error : [{0}] is null", __col));
                    return false;
                }
                
                if(string.IsNullOrEmpty(__node[__col].ToString()) ||  __node[__col].ToString() == "0")
                    return false;
                
                return true;
            }
            
        } // ? END OF JsonHelper
        
    }
}