using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PIERStory {

    public static class CentralRequest
    {
        
        /// <summary>
        /// 패키지 초기화 시점에 사용.
        /// </summary>
        public class CentralConfiguration {
            public string packageID = string.Empty;
            public string clientVersion = string.Empty;
            public string targetStore = string.Empty;
            public string os = string.Empty;

            // 빌드해시 
            public string buildToken_meta = string.Empty;
            public string buildToken_64 = string.Empty;
            public string buildToken_7 = string.Empty;            
        }
        
    }
}