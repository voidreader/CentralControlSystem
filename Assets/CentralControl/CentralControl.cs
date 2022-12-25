using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BestHTTP;
using LitJson;

using Unity.Services.Core;
using Unity.Services.Authentication;

namespace PIERStory {

    public class CentralControl : MonoBehaviour
    {
        public static CentralControl main = null;
        public const string originURL = "https://www.pier-live.com:443/client/";
        // public const string originURL = "https://pierstory.info:7606/client/";
        
        public bool isInitCentralControl = false; // 초기화 여부 
        public bool isInService = false; // 서비스 가능 여부 
        public string packageID = string.Empty;
        
        
        public CentralResponse.CentralApplication app = null;
        CentralCallback.CentralDelegate<CentralResponse.CentralApplication> callbackInitialize;
        
        
        void Awake() {
            
            if(main == null) {
                main = this;
                DontDestroyOnLoad(this);
            }
        }
        
        /// <summary>
        /// 중앙관제시스템 초기화 
        /// </summary>
        /// <param name="configuration"></param>        
        public void Initialize(CentralRequest.CentralConfiguration configuration, CentralCallback.CentralDelegate<CentralResponse.CentralApplication> callback) {
            
            isInitCentralControl = false;
            isInService = false;
            
            callbackInitialize = callback;
            
            #if UNITY_IOS
            configuration.os = "ios";
            #else
            configuration.os = "android";
            #endif
            
            // callback(new CentralResponse.centralApplication(), null);
            JsonData sendingData = new JsonData();
            sendingData["func"] = "InitializeClient";
            sendingData["package_id"] = configuration.packageID;
            sendingData["os_type"] = configuration.os;
            sendingData["app_store"] = configuration.targetStore;
            sendingData["client_version"] = configuration.clientVersion;
            
            // 해시
            sendingData["clientTokenMeta"] = configuration.buildToken_meta;
            sendingData["clientToken64"] = configuration.buildToken_64;
            sendingData["clientToken7"] = configuration.buildToken_7;
            
            if(Application.isEditor) 
                sendingData["editor"] = 1;
            else
                sendingData["editor"] = 0; 
            
            if(UnityServices.State == ServicesInitializationState.Initialized && AuthenticationService.Instance.IsSignedIn) {
                sendingData["ugsid"] = AuthenticationService.Instance.PlayerId;
            }
            
            
            
            // 통신 
            SendPost(OnFinishedInitialize, sendingData);
            
        } //? END Initialize
        
        bool CheckResponseValidation(HTTPRequest request, HTTPResponse response) {
            if(request.State == HTTPRequestStates.Finished && response.IsSuccess)
                return true;
                
                
            return false;
        }
        
        
        void OnFinishedInitialize(HTTPRequest request, HTTPResponse response) {
            
            isInitCentralControl = true; // 초기화 완료 
            
            // 연결실패 체크
            if(!CheckResponseValidation(request, response)) {
                
                Debug.Log("Connecting Error OnFinishedInitialize");
                
                // 처리 
                CentralError error = new CentralError("Initialize", "0", "6173");
                callbackInitialize(null, error);
                return;
            }
            
            Debug.Log("OnFinishedInitialize : " + response.DataAsText);
            
            JsonData result = JsonMapper.ToObject(response.DataAsText);
            
            // 실패 (등록되지 않는 클라이언트)
            if(!IsSuccess(result)) {
                // 처리 
                CentralError error = new CentralError("Initialize", CentralUtility.JsonHelper.GetJsonNodeString(result, "error"), CentralUtility.JsonHelper.GetJsonNodeString(result, "messageID"));
                callbackInitialize(null, error);
                return;
            }
            
            
            // 앱정보 생성 
            app = new CentralResponse.CentralApplication(
                CentralUtility.JsonHelper.GetJsonNodeString(result, "client_status"),
                CentralUtility.JsonHelper.GetJsonNodeString(result, "url"),
                CentralUtility.JsonHelper.GetJsonNodeString(result, "project_id"),
                CentralUtility.JsonHelper.GetJsonNodeString(result, "install_url")
            );
            
            // 업데이트 필수의 경우는 true로 변경하지 않음 
            if(app.clientStatus != CentralClientStatus.REQUIRE_UPDATE)
                isInService = true; // 서비스 가능 처리 
                
            callbackInitialize(app, null);
  
        }
        
        void SendPost(OnRequestFinishedDelegate __cb, JsonData __sendingData) {
            
            if(__sendingData == null)
                __sendingData = new JsonData();
                
            Debug.Log("SendPost : " + JsonMapper.ToJson(__sendingData));
            
            HTTPRequest request = new HTTPRequest(new System.Uri(originURL), HTTPMethods.Post, __cb);
            request.SetHeader("Content-Type", "application/json; charset=UTF-8");
            request.RawData = Encoding.UTF8.GetBytes(JsonMapper.ToJson(__sendingData));

            request.ConnectTimeout = System.TimeSpan.FromSeconds(15);
            request.Timeout = System.TimeSpan.FromSeconds(30);
            request.Send();
        }
        
        
        
        
        
        
        public static bool IsSuccess(CentralError error) {
            return (error == null || error.resultCode == "1");
        }
        
        public static bool IsSuccess(JsonData data) {
            if(data != null && CentralUtility.JsonHelper.GetJsonNodeString(data, "result") == "1")
                return true;
                
            return false;
        }
        
        
        /// <summary>
        /// 인스톨 URL 오픈 
        /// </summary>
        public void OpenInstallURL() {
            
            Debug.Log($"### OpenInstallURL : [{app.installURL}]");
            
            if(!string.IsNullOrEmpty(app.installURL))
                return;
            
            Application.OpenURL(app.installURL);
        }
        
    }
}