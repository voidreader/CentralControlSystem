using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace PIERStory {
    public static class CentralResponse
    {
        
        /// <summary>
        /// 앱 정보 
        /// </summary>
        public class CentralApplication {
            public string clientStatus = string.Empty;
            public string serverURL = string.Empty;
            public string projectID = string.Empty;
            public string installURL = string.Empty;
            
            
            public CentralApplication(string __clientStatus, string __serverURL, string __projectID, string __installURL) {
                clientStatus = __clientStatus;
                serverURL = __serverURL;
                projectID = __projectID;
                installURL = __installURL;
            }
        }
    }
}