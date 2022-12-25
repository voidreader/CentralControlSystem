using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PIERStory {
    public class CentralError 
    {
        
        
        public string sender = string.Empty;
        public string textID = string.Empty;
        public string message = string.Empty; 
        public string resultCode = string.Empty;

        
        public CentralError(string __sender, string __resultCode, string __textID) {
            
            textID = __textID;
            // message = SystemManager.GetLocalizedText(__textID);
            
            resultCode = __resultCode;
        }
    }
}