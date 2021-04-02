# MiniProject_Desktop(WPF)
미니프로젝트 데스크탑앱(ERP 재고관리)--누겟패키지를 통한 DB연동 및 소스 코드 효율화를 통해 사용자 친화적인 UI를 신속하게 개발 가능
-------------
<img src="https://github.com/ochestra365/StudyDesktopApp/blob/main/WPFApp/WpfAdvBank/FineDustMonApp/Git_hub_Image/%EC%88%98%EB%8B%AC.png" width="40%" height="30%" >
내가 주로 하는 실수는 초기 파일을 재사용할 때, 경로 설정 및 네이밍을 잘 못 주는 실수를 하였다. 그래서 파라미터를 잘 못 넣어서 오류가 발생해도 인지하지 못하고 있었다....<br>

<br><br>
ERP 시스템에 대하여 개발해 볼 것이다!!
-------------
##목차
1. ![Helper](https://github.com/ochestra365/MiniProject_Desktop/blob/main/WpfSMSApp/Helper/Commons.cs)
2. ![Logic.DataAccess](https://github.com/ochestra365/MiniProject_Desktop/blob/main/WpfSMSApp/Logic/DataAccess.cs)
3. ![View](https://github.com/ochestra365/MiniProject_Desktop/tree/main/WpfSMSApp/View)
4. ![Mainxaml](https://github.com/ochestra365/MiniProject_Desktop/blob/main/WpfSMSApp/MainWindow.xaml)
5. ![Mainxaml.cs](https://github.com/ochestra365/MiniProject_Desktop/blob/main/WpfSMSApp/MainWindow.xaml.cs)
-------------
##설명
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
MD5Hash로 인해 DB에서 모르는 사용자만 아는 비밀번호로 접속할 수 있었다.<br><br>
<img src="https://github.com/ochestra365/MiniProject_Desktop/blob/main/WpfSMSApp/WPF%EA%B9%83%ED%97%88%EB%B8%8C%EC%97%90%20%EC%98%AC%EB%A6%B4%20%EC%9D%B4%EB%AF%B8%EC%A7%80%20%EC%82%AC%EC%A7%84%EB%93%A4/%EC%A0%91%EC%86%8D.png" width="40%" height="30%" ><br><br>

<img src="https://github.com/ochestra365/MiniProject_Desktop/blob/main/WpfSMSApp/WPF%EA%B9%83%ED%97%88%EB%B8%8C%EC%97%90%20%EC%98%AC%EB%A6%B4%20%EC%9D%B4%EB%AF%B8%EC%A7%80%20%EC%82%AC%EC%A7%84%EB%93%A4/DB%20%ED%99%94%EB%A9%B4.png" width="40%" height="30%" ><br><br>
<img src="https://github.com/ochestra365/MiniProject_Desktop/blob/main/WpfSMSApp/WPF%EA%B9%83%ED%97%88%EB%B8%8C%EC%97%90%20%EC%98%AC%EB%A6%B4%20%EC%9D%B4%EB%AF%B8%EC%A7%80%20%EC%82%AC%EC%A7%84%EB%93%A4/PDF%20EXPORT.png" width="40%" height="30%" ><br><br>
<img src="https://github.com/ochestra365/MiniProject_Desktop/blob/main/WpfSMSApp/WPF%EA%B9%83%ED%97%88%EB%B8%8C%EC%97%90%20%EC%98%AC%EB%A6%B4%20%EC%9D%B4%EB%AF%B8%EC%A7%80%20%EC%82%AC%EC%A7%84%EB%93%A4/%EA%B3%84%EC%A0%95%EC%A0%95%EB%B3%B4%EC%88%98%EC%A0%95.png" width="40%" height="30%" ><br><br>
<img src="https://github.com/ochestra365/MiniProject_Desktop/blob/main/WpfSMSApp/WPF%EA%B9%83%ED%97%88%EB%B8%8C%EC%97%90%20%EC%98%AC%EB%A6%B4%20%EC%9D%B4%EB%AF%B8%EC%A7%80%20%EC%82%AC%EC%A7%84%EB%93%A4/%EC%82%AC%EC%9A%A9%EC%9E%90%EB%A6%AC%EC%8A%A4%ED%8A%B8%20.png" width="40%" height="30%" ><br><br>
<img src="https://github.com/ochestra365/MiniProject_Desktop/blob/main/WpfSMSApp/WPF%EA%B9%83%ED%97%88%EB%B8%8C%EC%97%90%20%EC%98%AC%EB%A6%B4%20%EC%9D%B4%EB%AF%B8%EC%A7%80%20%EC%82%AC%EC%A7%84%EB%93%A4/%EC%82%AC%EC%9A%A9%EC%9E%90%EB%A6%AC%EC%8A%A4%ED%8A%B8%20%EC%B6%94%EA%B0%80.png" width="40%" height="30%" ><br><br>

<img src="https://github.com/ochestra365/MiniProject_Desktop/blob/main/WpfSMSApp/WPF%EA%B9%83%ED%97%88%EB%B8%8C%EC%97%90%20%EC%98%AC%EB%A6%B4%20%EC%9D%B4%EB%AF%B8%EC%A7%80%20%EC%82%AC%EC%A7%84%EB%93%A4/%EC%A2%85%EB%A3%8C%EC%82%AC%EC%A7%84.png" width="40%" height="30%" ><br><br>
<img src="https://github.com/ochestra365/MiniProject_Desktop/blob/main/WpfSMSApp/WPF%EA%B9%83%ED%97%88%EB%B8%8C%EC%97%90%20%EC%98%AC%EB%A6%B4%20%EC%9D%B4%EB%AF%B8%EC%A7%80%20%EC%82%AC%EC%A7%84%EB%93%A4/%ED%98%84%EC%9E%AC%20%EC%A0%91%EC%86%8D%EC%A4%91%EC%9D%B8%20%EA%B3%84%EC%A0%95%EC%A0%95%EB%B3%B4.png" width="40%" height="30%" ><br><br>
<img src="https://github.com/ochestra365/MiniProject_Desktop/blob/main/NaverMovieFinderApp/WpfMiniProject/%EA%B9%83%ED%97%88%EB%B8%8C%EC%97%90%20%EC%98%AC%EB%A6%B4%20%EC%82%AC%EC%A7%84%EB%93%A4/%EA%B2%80%EC%83%89%20%EA%B2%B0%EA%B3%BC.png" width="40%" height="30%" ><br><br>
