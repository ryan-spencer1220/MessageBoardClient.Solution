using System.Collections.Generic;
using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MessageBoardClient.Models
{
  public class Message
  {
    public int MessageId { get; set; }
    public string Date { get; set; }
    public string Title { get; set; }
    public string Body { get; set; }
    public string Author { get; set; }
    public string Group { get; set; }

    public static List<Message> GetMessages()
    {
      var apiCallTask = ApiHelper.GetAll();
      var result = apiCallTask.Result;

      // Console.WriteLine("result = " + result[5]);
      // var arr = JArray.Parse(result);
      // Console.WriteLine("obj = " + arr);
      // var newResult = obj.data;

      var resultParse = JObject.Parse(result);
      JArray newResult = null; 

      foreach (KeyValuePair<string, JToken> kvp in resultParse)
      {
        if ( kvp.Key == "data" )
        {
          // Console.WriteLine("Key = {0}, Value = {1}",
          // kvp.Key, kvp.Value);

          newResult = (JArray)kvp.Value;
          Console.WriteLine(newResult);
        }
      }

      // JArray jsonResponse = JsonConvert.DeserializeObject<JArray>(newesult);
      // Console.WriteLine("jsonResponse = " + jsonResponse);

      List<Message> messageList = JsonConvert.DeserializeObject<List<Message>>(newResult.ToString());

      return messageList;
    }

    public static Message GetDetails(int id)
    {
      var apiCallTask = ApiHelper.Get(id);
      var result = apiCallTask.Result;

      JObject jsonResponse = JsonConvert.DeserializeObject<JObject>(result);
      Message message = JsonConvert.DeserializeObject<Message>(jsonResponse.ToString());

      return message;
    }

    public static void Post(Message message)
    {
      string jsonMessage = JsonConvert.SerializeObject(message);
      var apiCallTask = ApiHelper.Post(jsonMessage);
    }

    public static void Put(Message message)
    {
      string jsonMessage = JsonConvert.SerializeObject(message);
      var apiCallTask = ApiHelper.Put(message.MessageId, jsonMessage);
    }

    public static void Delete(int id)
    {
      var apiCallTask = ApiHelper.Delete(id);
    }
  }
}