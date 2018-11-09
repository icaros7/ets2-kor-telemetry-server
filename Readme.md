## ETS2 Telemetry 웹 서버 3.2.6 + 모바일 대시보드 한글 버전

이것은 [원본 ETS2 Telemetry 웹 서버](https://github.com/Funbit/ets2-telemetry-server) (@Funbit)을 한글화 및 일부 개선한 포크 입니다.

웹소켓과 REST API에 기반한 C#으로 작성된 [Euro Truck Simulator 2](http://www.eurotrucksimulator2.com/) 와 [American Truck Simulator](http://www.americantrucksimulator.com/)를 위한 무료 Telemetry 웹 서버 입니다. 클라이언트쪽에선 최신 데스크탑과 모바일 브라우저에서 스킨을 씌울 수 있는 HTML5 모바일 대시보드 앱으로 구성됩니다.

## 주요 기능

- 오픈 소스 및 무료 사용가능
- 자동 설치
- Telemetry 데이터를 위한 REST API
- 웹소켓에 기반한 실시간 telemetry 데이터 스트리밍을 위한 HTML5 대시보드 앱
- 커스텀 대시보드 스킨을 위한 강력한 지원 (스킨 튜토리얼 내장)
- Telemetry 데이터 브로드캐스팅을 위해 HTTP 프로토콜을 이용한 URL을 부여

### Telemetry REST API
  
    GET http://localhost:25555/api/ets2/telemetry

Telemetry REST API 관련 내용은 [원본 제작자 Funbit github (영문)](https://github.com/Funbit/ets2-telemetry-server)에서 보실 수 있습니다.

### HTML5 모바일 대시보드 어플리케이션
    http://localhost:25555/

이 HTML5 대시보드 어플리케이션은 모바일과 데스크탑을 위해 디자인 되었습니다. 이 어플리케이션은 모바일 사파리(iOS 8 이상), 안드로이드 4.0 이상 브라우저(기본 브라우저나 크롬 등)이나 아무 데스크탑 브라우저에서 URL에 들어가는 것만으로 이용 할 수있습니다.

모바일 대시보드는 브라우저에서 아래와 같이 뜹니다.

![](https://raw.githubusercontent.com/icaros7/ets2-kor-telemetry-server/master/server/Html/skins/default/dashboard.jpg)


패키지엔 다른 현실적인 스킨도 포함되어있습니다. 예시로 MAN-TGX 스킨 사진:

![](https://raw.githubusercontent.com/Funbit/ets2-telemetry-server/master/server/Html/skins/man-tgx/dashboard.jpg)

그리고 대시보드 디자인은 완전히 커스텀 할 수 있습니다. HTML과 CSS에 대한 아주 기초적인 지식만 있다면 직접 자신만의 스킨을 만들수도 있습니다. 더 자세한 내용은 대시보드 스킨 튜토리얼을 따르세요.

## 설정

### 지원하는 운영체제

- **Windows Vista, Windows 7, 8 혹은 10 (32-bit 혹은 64-bit)**. Windows XP는 지원되지 않습니다.
- **.NET Framework 4.5** (Windows 8 이상은 기본 설치). 만약 설치되있지 않다면 아마 서버 실행 전에 알림이 뜰껍니다.

### 지원하는 게임

- 스팀 혹은 단일 버전의 1.15 버전 이상 Euro Truck Simulator 2 (32-bit 혹은 64-Bit) 멀티 플레이 버전도 잘 지원됩니다.
- 스팀 혹은 단일 버전의 American Truck Simulator

### 확인된 브라우저

- iOS 8 이상의 모바일 사파리 (가장 최고의 사용자 경험을 위해 강력 추천함)
- 최신의 파이어폭스, 크롬 혹은 인터넷 익스플로러 11 (파이어폭스와 크롬 권장)
- 안드로이드 4.0 이상 (안드로이드 4.4 이상 권장)의 기본 브라우저 혹은 크롬 브라우저 (성능 관련 문제가 있다면 FAQ 참고)

### 설치

1. 이 페이지의 **Download ZIP** 버튼을 클릭하여 번들 파일을 받습니다.
2. *아무곳이나* 다운로로드 받은 파일의 **압축을 풉니다.**
3. **server\Ets2Telemetry.exe** 를 실행합니다.
4. 설치를 하기 위해 **설치** 버튼을 누릅니다.
5. 설치가 끝난 경우 "**확인**" 버튼을 누르고, 네트워크 인터페이스를 고른 다음 "**HTML5 앱 URL**" 링크를 눌러 대시보드를 엽니다.
6. **완료!** (이제 사용법 항목으로 가서 서버 사용법에 대해 알아봅니다)

***보안 관련*** : 설치 과정은 꼭 관리자 권한으로 실행하셔야만 합니다. 서버가 시스템에 어떤 영향을 주는지를 알고 싶다면 아래를 참고 해주세요.

1. 게임을 설치된 경로를 찾고, 거기에 ets2-telemetry-server.dll 플러그인을 복사합니다.
2. 25555번 포트에 대한 로컬 서브넷 허용 규칙을 "ETS2 TELEMETRY SERVER (PORT 25555)" 이름으로 엽니다. (참고로 외부 인터넷에선 이걸 따로 볼수 없습니다. 그래서 안전합니다.)
3. Microsoft OWIN의 httpListener ([자세한 내용](http://msdn.microsoft.com/en-us/library/ms733768%28v=vs.110%29.aspx))가 25555번 포트를 통해 사용을 위해 새로운 ACL 규칙을 만듭니다.
4. 어플리케이션 설정 파일을 만듭니다. "\Users\USERNAME\AppData\Local\Ets2 Telemetry Server".

서버는 모든 것을 로그 파일 (Ets2Temetry.log)에 기록합니다. 로그에 잘 나와있습니다. (이 역시 한글)

또한 제작자의 미리 컴파일된 ets2-telemetry-server.dll이 미심적다면, [플러그인 소스코드](https://github.com/icaros7/ets2-sdk-plugin)를 비주얼 스튜디오 2013 이상의 버전에서 직접 컴파일 해 사용 가능합니다.

### 스킨 설치

만약 제3자 스킨 (다른곳에서 받은 경우 dashboard.html, css, js 와 이미지 파일이 있습니다. 하지만 **절대 EXE 파일은 없습니다!!**)을 설치 하고싶은 경우 그냥 **server/Html/skins** 폴더에 복사하세요. 새로고침 하시면 스킨 선택 메뉴에 바로 뜰껍니다.

### 업그레이드

만약 이전 버전이 설치된 경우, 해당 버전은 놔둔채로 **다른 폴더에 새 버전을 압축을 풉니다.** 이 방법은 여러분들의 설정 파일이나 로그 등을 절대로 잃을 일이 없습니다.

만약에 이전 버전으로 돌아가려 하려면, 먼저 새 버전을 제거하는걸 잊지마세요!

### 제거

만약 서버가 기대에 못미치로 제거하기로 마음 먹으셨다면...

1. Euro Truck Simulator / American Truck Simulator를 종료해 주세요.
2. 데스크탑 서버 프로그램의 메뉴 중 "서버 -> 설치제거"를 누릅니다.
3. 팝업 윈도우의 "**제거**" 버튼을 누릅니다.
4. **제거 완료**

시스템이 제거 완료 시점에서는 설치 하기전과 완벽하게 같아집니다.

## 사용법

1. **server/Ets2Telemetry.exe**  를 실행합니다.
2. Euro Truck Simulator 2 / American Truck Simulator 를 실행합니다. (**순서가 바뀌면 안됨**)
3. **데스크탑 사용자** : Wi-Fi 혹은 랜을 통해 같은 네트워크(공유기)에 연결하세요. 다음 HTML5 앱 URL로 크롬, 파이어폭스, IE 등을 이용하여 이동하시면 사용 가능합니다.
3. **iOS 사용자** : Wi-Fi 혹은 랜을 통해 같은 네트워크(공유기)에 연결하세요. 다음 HTML5 앱 URL로 사파리, 크롬 등을 이용하여 이동하시면 사용 가능합니다.
4. **Android 웹 사용자** : Wi-Fi 혹은 랜을 통해 같은 네트워크(공유기)에 연결하세요. 다음 HTML5 앱 URL로 크롬, 파이어폭스, IE 등을 이용하여 이동하시면 사용 가능합니다.
5. **Enjoy** 모바일 대시보드를 좋아하는 시뮬레이터를 하는 동안 즐기세요!

## FAQ

> 서버를 실행하고 HTML5 앱을 열었는데, "페이지를 찾을 수 없음"이 떠요. 뭘 해야하죠?

우선, 셀룰러 (데이터, 3G, 4G 등) 대신 Wi-Fi를 꼭 사용해야합니다. 그런 다음 알맞은 "네트워크 인터페이스"를 서버 화면에서 선택하세요. Wi-Fi 네트워크에 직접 연결된 네트워크를 선택해야 합니다. *주로* "Wi-Fi"나 "이더넷" 혹은 "LAN"이라고 되있습니다. 또한 공유기에서 "AP 격리"가 꺼져 있어야합니다. [자세한 내용보기 (영문)](http://www.howtogeek.com/179089/lock-down-your-wi-fi-network-with-your-routers-wireless-isolation-option/).  
만약 여전히 연결 할 수 없다면... 잠깐 방화벽이나 백신을 꺼보세요. (특히 제3자 프로그램의!) 만약 문제가 계속된다면 시스템 관리자에게 연락 해보세요.

> 제공된 안드로이드 어플리케이션을 깔았습니다.  하지만 계속 "Could not connect to the server"나 "Disconnected" 상태 입니다. 뭘 해야하죠?

앱 말고 브라우저에서 한번 연결을 시도해봐주세요(위 답변을 참고). 만약에 브라우저에서는 사용 가능하다면 어플리케이션이 뭔가 잘못된겁니다. 다시시작이나 재설치를 해보세요..

> 게임을 제대로 시작했지만 대시보드에는 "연결됬습니다. 주행을 기다리는 중..."이 표시됩니다. 뭐가 잘못됬죠?

데스크탑의 서버 프로그램 창에서 "시뮬레이터에 연결됨" 상태 메시지가 정확히 뜨게 만들어주세요. 만약 "시뮬레이터가 실행중"이 대신 표시된다면 아마 telemetry 플러그인 (ets2-telemetry-server.dll) 설치가 잘못된 것 입니다. 만탹 "시뮬레이터가 실행중이 아님" 이라는 메시지가 뜨지만 실제로는 실행 중인 경우 아마 호환되지 않는 ETS2/ATS 버전을 실행 중일껍니다.

> 대시보드 UI 애니메이션(속도계)가 종종 끊깁니다. 제 기기 사양이 충분하지 않은건가요? 무엇을 해야하죠?

HTML5 대시보드는 최근 브라우저와 빠른 인터넷 환경 (유선 연결 및 내부 Wi-Fi 통신. 같은 공유기 내 통신)에 최적화 되어있습니다. 아마 구형 기기나 구버전 웹브라우저에서는 문제를 겪을 수 있습니다.   

몇몇 알려진 성능 예시 :   

삼성 갤럭시 탭S (4.4.2)에서 매우 동작합니다만, 갤럭시 노트1이나 킨들 파이어HD는 부족한 GPU가 탑제됬거나 (한국용 LTE 지원 갤럭시 노트1) GPU 그래픽 가속이 꺼져있는 경우 제대로 동작하지 않습니다. 안드로이드 기기에서 최고의 성능을 내기 위해서는 기본 탑제된 앱 대신 Chrome 브라우저를 사용해 보세요 (하지만 대시보드를 사용하는 경우 설정에서 화면 꺼짐 시간 설정을 꺼야합니다).   

iOS 8.0 이상 기기(아이폰6이나 아이패드 에어 이후 기기)에서는 부드럽게 잘 동작합니다. 하지만 iOS6.X 기기 (아이폰3GS, 아이팟 터치 4세대 이하)에서는 제대로 동작하지 않습니다. 만약 새 기기에서도 느린 현상이 나타난다면 앱을 모두 종료하고 다시시도 해주세요 (특히 사파리. 전원버튼을 꾹 눌러 전원 메뉴 상태에서 홈버튼을 5초간 꾹 누르면 모든 앱이 종료됩니다). 아마 이 방법이 크게 도와줄껍니다.   

최신 파이어폭스, 크롬이나 IE11이 깔린 아무 PC나 노트북에선 완벽하게 잘 될껍니다.   
또한 백그라운드 다운로드를 잠시 꺼두는 것도 잊지마세요. 특히 토렌트 클라이언트가 기기 간의 인터넷 속도를 현저하게 감소 시킵니다!!

> 서버를 사용하는 것이 안정적이긴 합니까? 게임을 꺼지게 한다던가 할 수 있나요? 아니면 FPS(초당 프레임 수)나 CPU 사용률 등 게임 성능에도 영향을 주나요?

서버는 매우 조심스럽게 프러그래밍 되어있어 게임을 꺼지게 한다던가 그러지 않습니다. 왜냐하면 사용되는 telemetry 플러그인은 공인된 개발자들이 만들었거든요. 그리고 CPU를 갈구지도 않고 (사용률 1프로 미만) 메모리도 매우 적게 먹습니다 (20MB 근방). 그러므로 FPS에선 아무 차이를 못느끼실껍니다.   

만약 미리 제작자가 컴파일한 exe나 dll파일을 신뢰하지 못한다면 아무떼나 [VirusTotal](https://www.virustotal.com/)에 가셔서 50개가 넘는 다른 백신 프로그램을 돌려보세요.

> 모바일 대시보드를 안드로이드 2.x 기기에서 사용 할 수 있나요?

아니오. 뭐 동작 될수는 있지만 지원되는 기기는 아닙니다.

## 대시보드 스킨 튜토리얼

튜토리얼은 ZIP 안에 포함되어 있습니다. ("Dashboard Skin Tutorial.pdf" 영문 파일). 아니면 [여기]를 눌러 해당 파일만 보실 수 있습니다.(https://raw.githubusercontent.com/Funbit/ets2-telemetry-server/master/Dashboard%20Skin%20Tutorial.pdf).

## 지원

이제 ETS2 Telemetry 웹 서버는 만드는데 엄청난 시간을 요구하는 꽤 복잡한 오픈 소스 프로젝트로 진화했습니다. 관심이 있다면 아래 버튼을 클릭하여 약간의 실질적인 지원을 원본 제작자(@Funbit)에게 줄수있습니다.

[![](https://raw.githubusercontent.com/Funbit/ets2-telemetry-server/master/server/Html/images/donate-link.png)](http://funbit.info/ets2/donate.htm)

감사합니다!

## 버전 체인지로그
### 3.2.6
- SCS SDK 1.9 업데이트 (플러그인 DLL 업데이트)
- 일반적 대시보드 스킨 외 스킨 업데이트 (RenaultDash-Info 제외)

### 3.2.5.3
- 목적지 까지 남은 시간 추가 (일반적 대시보드 스킨)
- 다음 휴식 시간 추가 (일반적 대시보드 스킨)
- 실시간 속도 제한 표시기 추가 (일반적 대시보드 스킨)
- 자가 트레일러 구분 (일반적 대시보드 스킨)
- 리타터 계수기 추가 (일반적 대시보드 스킨)
- 파킹 브레이크 등 추가 (일반적 대시보드 스킨)
- 클릭하여 뒤로가기 제거 (일반적 대시보드 스킨)
- 기어 표시기의 Driving 마크 제거 (일반적 대시보드 스킨)

### 3.2.5.2
- 트럭 손상도 표기 방식 변경 (변경 전 : 모든 부품 손상도 합계, 변경 후 : ETS2 표기와 동일하게 최대 값) (기본 스킨)

### 3.2.5.1
- 데스크탑 어플리케이션 레이아웃 개선
- 한글 작업 (추후 다른 언어를 위해 지역화 예정)
- Visual Studio 2017 마이그레이션

### 3.2.5

- Another improvement for job information reset code (plugin DLL update).

이전 버전 기록은 [원본 제작자 Funbit github (영문)](https://github.com/Funbit/ets2-telemetry-server)에서 보실 수 있습니다.

## 한글화

꽤 오래전에 사용하던 녀석인데 갑자기 유로트럭 하고 싶어 하다가 생각나서 다시 사용 중 이었습니다. 그러던중 그냥 한글화 한번 하고싶어서 해봤습니다.   
다만 업데이트가 안된지 한참 되서 안되는 일부 화물이나 표기가 안나옵니다...일부...

## 외부 링크

원본 제작자

[Funbit github (영문)](https://github.com/Funbit/ets2-telemetry-server)

포럼

- [공식 SCS 토론 포럼 (영문)](http://forum.scssoft.com/viewtopic.php?f=41&t=171000)

비디오

- [Dashboard overview from SimplySimulators (영어)](https://www.youtube.com/watch?v=mM13wfTYfM8)
- [Dashboard usage with OBS from A JonC (영어)](https://www.youtube.com/watch?v=yLFu4DPixCM)
- [Dashboard overview from MrSoundwaves Cubes (영어)](https://www.youtube.com/watch?v=2OCs9RwA0AI)
- [Dashboard usage from z5teve](https://www.youtube.com/watch?v=gdwpTwhzZIg)
- [Dashboard overview from Driver Geo (루마니아어)](https://www.youtube.com/watch?v=zcrmyD5wq10)
- [Dashboard overview from MaRKiToX12 (스페인어)](https://www.youtube.com/watch?v=J_SpwY8RIX4)
- [Dashboard overview from Branislav Rác (슬로바키아어)](https://www.youtube.com/watch?v=LpKyuNWxJTU)
- [Dashboard overview from 1Tera Games (포르투칼어)](https://www.youtube.com/watch?v=hfUMWmuLToQ)
- [Обзор от Саши Плотникова (러시아어)](https://www.youtube.com/watch?v=mmNm27eTTBs)
  
## 라이센스

[GNU General Public License v3 (GPL-3)](https://tldrlegal.com/license/gnu-general-public-license-v3-%28gpl-3%29).