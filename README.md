# CentralControlSystem
게임서비스 중앙관제시스템 Unity Client SDK 


NHN 게임베이스, Naver 게임팟을 사용하지 않기 위해 
사내 게임들에서 공용적으로 사용할 관제 서비스를 작성 중입니다.

현재 아래의 기능이 구현되어 사용중입니다.

### 클라리언트 관리
os별로 실행한 클라이언트의 version을 등록해서 관리합니다. 
상태에 따라 접속되는 서버 주소를 다르게 설정하여 테스트/심사/라이브 과정에 편의성을 부여합니다.

udpate 상태, 점검 상태로 변경하여 유저의 게임 진입을 제어할 수 있습니다.


### 적용된 게임 
- 상태이상 : 오토메 러브 판타지(https://play.google.com/store/apps/details?id=carpe.story.stat&hl=ko&gl=KR)
