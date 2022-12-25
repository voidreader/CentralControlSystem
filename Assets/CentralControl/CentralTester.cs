using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PIERStory {

    public class CentralTester : MonoBehaviour
    {
        public string currentAppStatus = string.Empty;
        public string currentServerURL = string.Empty;
        public string currentInstallURL = string.Empty;
        
        // Start is called before the first frame update
        void Start()
        {
            
        }

        
        public void OnClickInit() {
            CentralRequest.CentralConfiguration config = new CentralRequest.CentralConfiguration();
            config.packageID = CentralControl.main.packageID;
            config.targetStore = "google";
            config.clientVersion = Application.version;
            
            
            /*
            if(isHashSupported) {
                config.buildToken_meta = SystemManager.main.tokenMeta.ToString();
                config.buildToken_64 = SystemManager.main.token64.ToString();
                config.buildToken_7 = SystemManager.main.token7.ToString();
            } 
            */           
            
            CentralControl.main.Initialize(config, OnCentralInitialize);
            
        }
        
        /// <summary>
        /// 잘못된 초기화 테스트 
        /// </summary>
        public void OnClickInvalidClientInit() {
            CentralRequest.CentralConfiguration config = new CentralRequest.CentralConfiguration();
            config.packageID = "pier.xxx.xxxx";
            config.targetStore = "google";
            config.clientVersion = "0.3.3";
            
            CentralControl.main.Initialize(config, OnCentralInitialize);
        }
        
        public void OnClickInvalidVersionInit() {
            CentralRequest.CentralConfiguration config = new CentralRequest.CentralConfiguration();
            config.packageID = CentralControl.main.packageID;
            config.targetStore = "google";
            config.clientVersion = "0.3.3";
            
            CentralControl.main.Initialize(config, OnCentralInitialize);
        }
        
        
        
        
        void OnCentralInitialize(CentralResponse.CentralApplication app, CentralError error) {
            Debug.Log("OnCentralInitialize Start");
            
            if(!CentralControl.IsSuccess(error)) {
                Debug.Log(error.resultCode + "/" + error.textID);
                
                // TODO 시스템 팝업 처리    
                return;
            }
            
            
            // 여기서부터 초기화 이후 처리
            Debug.Log($"App server url : [{app.serverURL}]");
            Debug.Log($"App status : [{app.clientStatus}]");
            Debug.Log($"App install url : [{app.installURL}]");
            Debug.Log($"App project id : [{app.projectID}]");
            
            currentAppStatus = app.clientStatus;
            currentServerURL = app.serverURL;
            currentInstallURL = app.installURL;
            
            switch(currentAppStatus) {
                case CentralClientStatus.IN_TEST:
                break;
                
                case CentralClientStatus.IN_REVIEW:
                break;
                
                case CentralClientStatus.REQUIRE_UPDATE:
                // TODO 업데이트로 빠지게 처리 
                Debug.Log("UPDATE REQUIRED!!!!");
                break;
                
                case CentralClientStatus.IN_SERVICE:
                break;
                
                case CentralClientStatus.CLOSED:
                break;
            }
            
            
            
        }
    }
}