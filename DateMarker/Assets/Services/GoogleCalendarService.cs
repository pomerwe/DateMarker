using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3;
using GoogleData = Google.Apis.Calendar.v3.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using UnityEngine.XR.WSA.Input;
using System.IO;
using System.Threading;
using UnityEditor.PackageManager.Requests;
using Google.Apis.Calendar.v3.Data;


public class GoogleCalendarService : MonoBehaviour
{
  static string[] Scopes = { CalendarService.Scope.Calendar };
  static string ApplicationName = "Date Marker";
  static string CALENDARID = "primary";

  private GoogleCalendarService instance;
  private GoogleCalendarService Instance 
  { 
    get 
    {
      if(instance == null)
      {
        instance = FindObjectOfType<GoogleCalendarService>();
      }
      return this.instance;
    }
  }


  private CalendarService googleService; 

  public void Start()
  {
    UserCredential credential;

    using (var stream =
        new FileStream("credentials.json", FileMode.Open, FileAccess.Read))
    {
      // The file token.json stores the user's access and refresh tokens, and is created
      // automatically when the authorization flow completes for the first time.
      string credPath = "token.json";
      credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
          GoogleClientSecrets.Load(stream).Secrets,
          Scopes,
          "user",
          CancellationToken.None,
          new FileDataStore(credPath, true)).Result;
      Console.WriteLine("Credential file saved to: " + credPath);
    }

    // Create Google Calendar API service.
    googleService = new CalendarService(new BaseClientService.Initializer()
    {
      HttpClientInitializer = credential,
      ApplicationName = ApplicationName,
    });
  }

  public GoogleData.Event CreateEvent(DateMarkerEvent dateMarkerEvent)
  {
    var googleEvent = new GoogleData.Event()
    {
      Start = new EventDateTime() { DateTime = dateMarkerEvent.Start },
      End = new EventDateTime() { DateTime = dateMarkerEvent.End },
      Description = dateMarkerEvent.Description,
      Summary = dateMarkerEvent.Title
    };

    return googleEvent;
  }

  public void InsertEvent(DateMarkerEvent dateMarkerEvent)
  {
    var googleEvent = CreateEvent(dateMarkerEvent);

    EventsResource.InsertRequest insertRequest = googleService.Events.Insert(googleEvent, CALENDARID);
    insertRequest.Execute();
  }

  public Events GetEvents()
  {
    EventsResource.ListRequest getRequest = googleService.Events.List(CALENDARID);
    getRequest.TimeMin = DateTime.Now;
    getRequest.ShowDeleted = false;
    getRequest.SingleEvents = true;
    getRequest.MaxResults = 10;
    getRequest.OrderBy = EventsResource.ListRequest.OrderByEnum.StartTime;
    return getRequest.Execute();
  }

  public void DeleteEvent(DateMarkerEvent dateMarkerEvent)
  {
    var deleteRequest = googleService.Events.Delete(CALENDARID, dateMarkerEvent.GoogleEvent.Id);
    deleteRequest.Execute();
  }
}
