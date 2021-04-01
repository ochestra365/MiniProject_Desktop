using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleNaverMoiveFinder
{
    class Program
    {
        static void Main(string[] args)
        {
            string clientID = "Mxe3E8mU7xfCj5OHPpeK";//나의 네이버 클라이언트 아이디
            string clientSecret = "yPekeefDmu";
            string search = "starwars";//변경 가능
            string openApiUrl = $"https://openapi.naver.com/v1/search/movie?query={search}";

            string responseJson = GetOpenApiResult(openApiUrl,clientID,clientSecret);
            JObject parsedJson = JObject.Parse(responseJson);

            int total =Convert.ToInt32(parsedJson["total"]);
            Console.WriteLine($"총 검색결과 : {total}");
            int display = Convert.ToInt32(parsedJson["display"]);
            Console.WriteLine($"화면출력 : {display}");

            JToken items = parsedJson["items"];
            JArray json_array = (JArray)items;//var로 해놓고 디버그 잡으면서 혹은 커서를 올려서 클래스나 데이터 타입을 알아내는 거시다!!!!! 개꿀팁이다!!!!!!!

            foreach (var item in json_array)
            {
                Console.WriteLine($"{item["title"]}/{item["image"]}/{item["subtitle"]}/{item["actor"]}/");//이 안의 파라미터들을 클래스로 만들고 속성으로 변환시켜서 그리드에 집어 넣으면 끝난다.
            }
        }
        private static string GetOpenApiResult(string openApiUrl, string clientID, string clientSecret)
        {
            string result = "";
            try//네트워크를 배워야 할 수 있는 코딩이다.
            {
                WebRequest request = WebRequest.Create(openApiUrl);
                request.Headers.Add("X-Naver-Client-ID",clientID);
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
    }
}
