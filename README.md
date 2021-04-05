# MiniProject_Desktop(WPF)
미니프로젝트 데스크탑앱(ERP 재고관리)--누겟패키지를 통한 DB연동 및 소스 코드 효율화를 통해 사용자 친화적인 UI를 신속하게 개발 가능
-------------
<img src="https://github.com/ochestra365/StudyDesktopApp/blob/main/WPFApp/WpfAdvBank/FineDustMonApp/Git_hub_Image/%EC%88%98%EB%8B%AC.png" width="40%" height="30%" >
내가 주로 하는 실수는 초기 파일을 재사용할 때, 경로 설정 및 네이밍을 잘 못 주는 실수를 하는 것이다.. 그래서 파라미터를 잘 못 넣어서 오류가 발생해도 인지하지 못하고 있었다....<br>
WPF Binding의 생성자의 대소문자 구분을 잘못 하고 있었다.<br>
네임스페이스의 경로를 잘못 주고 있었다.<br>
누겟패키지 관리에서 패키지를 잘못 설치했다.<br>
<br><br>
ERP 시스템에 대하여 개발해 볼 것이다!!
-------------
##목차<br><br>
* ![Helper](https://github.com/ochestra365/MiniProject_Desktop/blob/main/WpfSMSApp/Helper/Commons.cs)<br>
* ![Logic.DataAccess](https://github.com/ochestra365/MiniProject_Desktop/blob/main/WpfSMSApp/Logic/DataAccess.cs)<br>
* ![View](https://github.com/ochestra365/MiniProject_Desktop/tree/main/WpfSMSApp/View)<br>
* ![Mainxaml](https://github.com/ochestra365/MiniProject_Desktop/blob/main/WpfSMSApp/MainWindow.xaml)<br>
* ![Mainxaml.cs](https://github.com/ochestra365/MiniProject_Desktop/blob/main/WpfSMSApp/MainWindow.xaml.cs)<br>
* ![Database.sql](https://github.com/ochestra365/MiniProject_Desktop/blob/main/NaverMovieFinderApp/WpfMiniProject/%EB%84%A4%EC%9D%B4%EB%B2%84%20%EB%8D%B0%EC%9D%B4%ED%84%B0%EB%B2%A0%EC%9D%B4%EC%8A%A4.sql)<br>
* ![MahApps_GetStarted](https://mahapps.com/docs/guides/quick-start)<br><br>

##설명
-------------

<br><br><br>
해당 프로젝트는 누겟패키지를 최대한 활용하여 만들었다. 설치한 누겟패키지는 현재 다음과 같다.<br><br>
<img src="https://github.com/ochestra365/MiniProject_Desktop/blob/main/WpfSMSApp/WPF%EA%B9%83%ED%97%88%EB%B8%8C%EC%97%90%20%EC%98%AC%EB%A6%B4%20%EC%9D%B4%EB%AF%B8%EC%A7%80%20%EC%82%AC%EC%A7%84%EB%93%A4/%EB%88%84%EA%B2%9F%ED%8C%A8%ED%82%A4%EC%A7%80.png" width="40%" height="30%" ><br><br>
Helper 폴더에는 모든 클래스에 유용하게 사용되는 코드들이 있다. Getmd5Hash로 모든 암호화하는 것, 이메일 정규화 식, Metro MessageBox의 공통메서드가 있다.<br><br>
Logic.DataAccess 폴더에는 DB에 있는 데이터 값을 불러오고 수정하는 코드가 작성되어 있다. <br><br>
~~~
using System.Data.Entity.Migrations;
~~~
데이터베이스 엔티티를 설치해서 해당 자원을 쓸 수 있었다. <br><br>
<img src="https://github.com/ochestra365/MiniProject_Desktop/blob/main/WpfSMSApp/WPF%EA%B9%83%ED%97%88%EB%B8%8C%EC%97%90%20%EC%98%AC%EB%A6%B4%20%EC%9D%B4%EB%AF%B8%EC%A7%80%20%EC%82%AC%EC%A7%84%EB%93%A4/%EC%9D%B4%EB%AF%B8%EC%A7%80%2020210401001.png" width="40%" height="30%" ><br><br>
확인할 수 있는 바와 같이 SSMS의 다이어그램이 비쥬얼 스튜디오 상에 나타나며 Database Entity의 다양한 솔루션들을 활용할 수 있다.<br><br>
다음으로 View 폴더는 내가 만든 WPF 페이지들이 만들어져 있다. 초기 화면을 만들고 나서<br><br>
복사해서 재사용한다. 이때, 클래스명이 바뀌게 되는 데, xaml에서는 Title 명을 클래스와 동일시 하고<br><br>
로컬과 클래스의 경로를 재설정해준다.<br><br> xaml.cs에서는 namespace경로를 잡아주고, partial 클래스와 InitializeComponent의 public명을 클래스 별로 맞춰준다.

-------------

##구동화면
<br><br><br>
현재 내가 만든 화면의 구동화면은 다음과 같다.
<br><br>
<img src="https://github.com/ochestra365/MiniProject_Desktop/blob/main/WpfSMSApp/WPF%EA%B9%83%ED%97%88%EB%B8%8C%EC%97%90%20%EC%98%AC%EB%A6%B4%20%EC%9D%B4%EB%AF%B8%EC%A7%80%20%EC%82%AC%EC%A7%84%EB%93%A4/%ED%99%9C%EC%84%B1%ED%99%94%EB%A9%B4.png" width="40%" height="30%" ><br><br>
처음 활성창화면이다.<br><br>
<img src="https://github.com/ochestra365/MiniProject_Desktop/blob/main/WpfSMSApp/WPF%EA%B9%83%ED%97%88%EB%B8%8C%EC%97%90%20%EC%98%AC%EB%A6%B4%20%EC%9D%B4%EB%AF%B8%EC%A7%80%20%EC%82%AC%EC%A7%84%EB%93%A4/%EB%A1%9C%EA%B7%B8%EC%9D%B8%20%EC%8B%A4%ED%8C%A8.png" width="40%" height="30%" ><br><br>
잘못된 ID로 로그인을 하려고 하니 실패하였다.<br><br>
그래서 DB에있는 제대로 된 권한을 가진 사람의 ID로 접속을 해야 성공할 수 있다.<br><br>


<img src="https://github.com/ochestra365/MiniProject_Desktop/blob/main/WpfSMSApp/WPF%EA%B9%83%ED%97%88%EB%B8%8C%EC%97%90%20%EC%98%AC%EB%A6%B4%20%EC%9D%B4%EB%AF%B8%EC%A7%80%20%EC%82%AC%EC%A7%84%EB%93%A4/DB%20%ED%99%94%EB%A9%B4.png" width="40%" height="30%" ><br><br>
해당 이미지는 DB상에 실존하는 Admin의 주소이다. <br><br>
MD5Hash로 인해 DB에서 관리자는 비밀번호를 모른다. 사용자만 아는 비밀번호로 접속할 수 있었다.<br><br>
amdin 관리자에게만 접속권한을 줌으로써 보안이 강화되었다.<br><br>
<img src="https://github.com/ochestra365/MiniProject_Desktop/blob/main/WpfSMSApp/WPF%EA%B9%83%ED%97%88%EB%B8%8C%EC%97%90%20%EC%98%AC%EB%A6%B4%20%EC%9D%B4%EB%AF%B8%EC%A7%80%20%EC%82%AC%EC%A7%84%EB%93%A4/%EC%A0%91%EC%86%8D.png" width="40%" height="30%" ><br>
해당 이미지는 권한이 부여된 관리자가 접속하는 것을 보여준다.<br><br>
<img src="https://github.com/ochestra365/MiniProject_Desktop/blob/main/WpfSMSApp/WPF%EA%B9%83%ED%97%88%EB%B8%8C%EC%97%90%20%EC%98%AC%EB%A6%B4%20%EC%9D%B4%EB%AF%B8%EC%A7%80%20%EC%82%AC%EC%A7%84%EB%93%A4/PDF%20EXPORT.png" width="40%" height="30%" ><br><br>
CRUD를 통해 만들어진 최신 자료를 PDF로 형변환 할 수도 있다.<br><br>

사용자(관리자) UI
-------------
<img src="https://github.com/ochestra365/MiniProject_Desktop/blob/main/WpfSMSApp/WPF%EA%B9%83%ED%97%88%EB%B8%8C%EC%97%90%20%EC%98%AC%EB%A6%B4%20%EC%9D%B4%EB%AF%B8%EC%A7%80%20%EC%82%AC%EC%A7%84%EB%93%A4/%EA%B3%84%EC%A0%95%EC%A0%95%EB%B3%B4%EC%88%98%EC%A0%95.png" width="40%" height="30%" ><br><br>
<img src="https://github.com/ochestra365/MiniProject_Desktop/blob/main/WpfSMSApp/WPF%EA%B9%83%ED%97%88%EB%B8%8C%EC%97%90%20%EC%98%AC%EB%A6%B4%20%EC%9D%B4%EB%AF%B8%EC%A7%80%20%EC%82%AC%EC%A7%84%EB%93%A4/%EC%82%AC%EC%9A%A9%EC%9E%90%EB%A6%AC%EC%8A%A4%ED%8A%B8%20.png" width="40%" height="30%" ><br><br>
<img src="https://github.com/ochestra365/MiniProject_Desktop/blob/main/WpfSMSApp/WPF%EA%B9%83%ED%97%88%EB%B8%8C%EC%97%90%20%EC%98%AC%EB%A6%B4%20%EC%9D%B4%EB%AF%B8%EC%A7%80%20%EC%82%AC%EC%A7%84%EB%93%A4/%EC%82%AC%EC%9A%A9%EC%9E%90%EB%A6%AC%EC%8A%A4%ED%8A%B8%20%EC%B6%94%EA%B0%80.png" width="40%" height="30%" ><br><br>

<img src="https://github.com/ochestra365/MiniProject_Desktop/blob/main/WpfSMSApp/WPF%EA%B9%83%ED%97%88%EB%B8%8C%EC%97%90%20%EC%98%AC%EB%A6%B4%20%EC%9D%B4%EB%AF%B8%EC%A7%80%20%EC%82%AC%EC%A7%84%EB%93%A4/%EC%A2%85%EB%A3%8C%EC%82%AC%EC%A7%84.png" width="40%" height="30%" ><br><br>
<img src="https://github.com/ochestra365/MiniProject_Desktop/blob/main/WpfSMSApp/WPF%EA%B9%83%ED%97%88%EB%B8%8C%EC%97%90%20%EC%98%AC%EB%A6%B4%20%EC%9D%B4%EB%AF%B8%EC%A7%80%20%EC%82%AC%EC%A7%84%EB%93%A4/%ED%98%84%EC%9E%AC%20%EC%A0%91%EC%86%8D%EC%A4%91%EC%9D%B8%20%EA%B3%84%EC%A0%95%EC%A0%95%EB%B3%B4.png" width="40%" height="30%" ><br><br><br><br>

네이버 영화앱 검색기!
-------------

<img src="https://github.com/ochestra365/MiniProject_Desktop/blob/main/NaverMovieFinderApp/WpfMiniProject/%EA%B9%83%ED%97%88%EB%B8%8C%EC%97%90%20%EC%98%AC%EB%A6%B4%20%EC%82%AC%EC%A7%84%EB%93%A4/%EA%B2%80%EC%83%89%20%EA%B2%B0%EA%B3%BC.png" width="40%" height="30%" ><br><br>
네이버의 OpenApi를 이용해서 검색을 할 수 있는 앱을 만들어 보았다.<br><br>
역시 MahApp과 MahIconpack을 이용하니 UI가 좋아져서 기분이 상쾌했다.<br><br>
여기서는 OpenApi를 이용하기 때문에 Json이라는 새로운 누겟패키지를 사용했다!!!!!! 사용된 누겟패키지는 다음과 같다.<br><br>
<img src="https://github.com/ochestra365/MiniProject_Desktop/blob/main/NaverMovieFinderApp/WpfMiniProject/%EA%B9%83%ED%97%88%EB%B8%8C%EC%97%90%20%EC%98%AC%EB%A6%B4%20%EC%82%AC%EC%A7%84%EB%93%A4/%EC%84%A4%EC%B9%98%EB%90%9C%20%EB%88%84%EA%B2%9F%ED%94%BC%ED%82%A4%EC%A7%80.png" width="40%" height="30%" ><br><br>
해당 프로그램을 하면서 느낀점은 다음과 같다.<br><br>
* MahApp 설치를 통해서 UI를 사용자 친화적으로 만들었다.
* NLOG설치를 통해 에러가 발생할 시 기록되도록 만들었다.
* NewtonSoft.Json을 통해 OpenApi를 활용해보았다.<br><br>
<img src="https://github.com/ochestra365/MiniProject_Desktop/blob/main/NaverMovieFinderApp/WpfMiniProject/%EA%B9%83%ED%97%88%EB%B8%8C%EC%97%90%20%EC%98%AC%EB%A6%B4%20%EC%82%AC%EC%A7%84%EB%93%A4/%EA%B2%80%EC%83%89%20%EA%B2%B0%EA%B3%BC.png" width="40%" height="30%" ><br><br>
데이터 바인딩을 줄 때, xaml 생성자 대소문자를 잘 못줘서 오류가 발생한 모습이다. 바로 잡으면 화면이 다음과 같이 정상적으로 바뀐다.<br><br>
<img src="https://github.com/ochestra365/MiniProject_Desktop/blob/main/NaverMovieFinderApp/WpfMiniProject/%EA%B9%83%ED%97%88%EB%B8%8C%EC%97%90%20%EC%98%AC%EB%A6%B4%20%EC%82%AC%EC%A7%84%EB%93%A4/%EA%B2%80%EC%83%89%EC%82%AC%EC%A7%84.png" width="40%" height="30%" ><br><br>
<img src="https://github.com/ochestra365/MiniProject_Desktop/blob/main/NaverMovieFinderApp/WpfMiniProject/%EA%B9%83%ED%97%88%EB%B8%8C%EC%97%90%20%EC%98%AC%EB%A6%B4%20%EC%82%AC%EC%A7%84%EB%93%A4/%EC%82%AD%EC%A0%9C%EB%82%B4%EC%9A%A9.png" width="40%" height="30%" ><br><br>
즐겨찾기를 DB상에 저장하고 기록할 수 있다. (@@@@@@해당 내용 캡쳐해서 올릴 것.@@@@@@@@)<br><br>

<img src="https://github.com/ochestra365/MiniProject_Desktop/blob/main/NaverMovieFinderApp/WpfMiniProject/%EA%B9%83%ED%97%88%EB%B8%8C%EC%97%90%20%EC%98%AC%EB%A6%B4%20%EC%82%AC%EC%A7%84%EB%93%A4/%EC%98%A4%EB%A5%98%EB%B0%9C%EC%83%9D.png" width="40%" height="30%" ><br><br>
@@현재 OpenApi파트에서 오류가 발생중이며 디버그중인 과정에 있다. 해당 인스턴스에 값이 들어가야 하는데 Null로 나온다.<br><br>
OpenApi 주소가 잘 못되었던지 아니면 생성자를 잘못 줬던 지이다. 이게 다 네이밍을 신경쓰지 않은 나의 버릇 때문이다.<br><br>
다음은 이 오류를 잡은 과정을 적을 것이고 이것을 통해 깨닳은 점을 커밋할 것이다.<br>
해당 프로그램을 하면서 느낀점은 다음과 같다.<br>
~~~
1. 오픈 API에서 response 날릴 때 주요한 것은 시그니처의 네이밍이다. 이것을 알기 위해서는 signiture에 대해 잘알아야 할 필요가 있다. 
2. 또한 위치도 중요하다 
3. 오픈API를 쓰기 위해서는 백엔드의 검색 형식에 맞춰서 response를 날려줘야 한다. 
4. 회사 내부의 DB가 없으면 종속 관계를 유지할 수 밖에 없다. 
5. xaml에서 컨트롤의 네이밍을 잘 못 줘서 일이 비효율적으로 했다. 그래서 네이밍을 잘 주는 게 얼마나 중요한 지 알게 되었다.
6. mah양식을 따를 경우, xaml에서 고쳐야 할 부분과 xaml.cs에서 고쳐야 할 부분을 아는 것이 중요했다.
~~~
<br><br>
주요개념습득<br>
~~~
데이터 바인딩은 앱 UI와 해당 UI가 표시하는 데이터를 연결하는 프로세스입니다. 
바인딩 설정이 올바르고 데이터가 적절한 알림을 제공하는 경우 데이터 값이 변경될 때 데이터에 바인딩된 요소에 변경 사항이 자동으로 반영됩니다. 
또한 요소에서 데이터의 외부 표현이 변경되면 내부 데이터가 자동으로 업데이트되어 변경 내용이 반영될 수 있습니다. 
예를 들어 사용자가 TextBox 요소의 값을 편집하면 내부 데이터 값이 자동으로 업데이트되어 해당 변경 내용이 반영됩니다.
~~~
