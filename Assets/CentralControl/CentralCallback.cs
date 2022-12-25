using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PIERStory {
    public class CentralCallback
    {
        
        public delegate void CentralDelegate<T>(T data, CentralError error);
        
           
    }
}