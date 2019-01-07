using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using StillWorkingThatList.Models;

namespace StillWorkingThatList.Controllers
{
    public class APIController : ApiController
    {
        const string userAgent = "Mozilla/5.0 (Windows NT 6.1; Win64; x64; rv:47.0) Gecko/20100101 Firefox/47.0";



            public Character GetCharacter(string characterName)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            HttpWebRequest request = WebRequest.CreateHttp($"https://www.anapioficeandfire.com/api/characters?name={characterName}");
            request.UserAgent = userAgent;
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            if (response.StatusCode == HttpStatusCode.OK)
            {              

                using (StreamReader data = new StreamReader(response.GetResponseStream()))
                {
                    string JsonData = data.ReadToEnd();
                    

                    //return character.ToObject<Character>();

                    JsonData = JsonData.TrimStart(new char[] { '[' }).TrimEnd(new char[] { ']' });

                    JObject characterObject = JObject.Parse(JsonData);

                    var convert = JsonConvert.DeserializeObject<Character>(JsonData);

                    return convert;

                    

                    



                }


            }


            return null;

        }
        //


    }
}
