using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using NLog;
using System.Threading.Tasks;
using System.Windows;
using System.Net;
using System.IO;
using System;
using System.Text.RegularExpressions;

namespace WpfMiniProject.Helper
{
    public class Commons
    {
        //NLOG 정적객체, 프레임워크의 기반이 되거나 Dll이 될 수도 있다.
        public static readonly Logger LOGGER = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Metro MessageBox 공통메서드
        /// </summary>
        /// <param name="title"></param>
        /// <param name="message"></param>
        /// <param name="style"></param>
        /// <returns></returns>
        public static async Task<MessageDialogResult> ShowMessageAsync(string title, string message, MessageDialogStyle style = MessageDialogStyle.Affirmative)//비동기로 일처리된 내용이 메세지처럼 돌아온 다는 것을 의미한다.
        {
            //this랑 같은 것이다.
            return await ((MetroWindow)Application.Current.MainWindow).ShowMessageAsync(title, message, style, null);//매개변수만 설정해주려고 데이터타입을 string 준 것이다. 해당 메서드를 사용하면 이 메서드의 값을 불러 내겠다. 단 윈도우와 동기화를 풀어서 보여주겠다. 현재 윈도우화면의 상태를 의미한다.
        }

        public static string GetOpenApiResult(string openApiUrl, string clientID, string clientSecret)
        {
            string result = "";
            try//네트워크를 배워야 할 수 있는 코딩이다.
            {
                WebRequest request = WebRequest.Create(openApiUrl);
                request.Headers.Add("X-Naver-Client-ID", clientID);
                request.Headers.Add("X-Naver-Client-Secret", clientSecret);

                WebResponse response = request.GetResponse();
                Stream stream = response.GetResponseStream();
                StreamReader reader = new StreamReader(stream);

                result = reader.ReadToEnd();

                reader.Close();
                stream.Close();
                response.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"예외발생 : {ex}");
            }
            return result;//연산만 하면 안됨 반드시 반환값이 튀어나와야 프론트엔드에서 쓸 수 있는 값이 나온다.
        }
        /// <summary>
        /// HTML 태그 삭제 메서드
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string StripHtmlTag(string text)
        {
            return Regex.Replace(text, @"<(.|\n)*?>", "");//HTML 태그 삭제하는 정규표현식 @@참조를 공백으로 바꾼다.@@
        }
        public static string StripPipe(string text)
        {
            if (string.IsNullOrEmpty(text))
            return "";
            string result = text.Replace("|", ", ");
            return result.Substring(0, result.LastIndexOf(","));
        }
    }
}
