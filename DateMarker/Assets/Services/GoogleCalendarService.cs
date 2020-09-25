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

  private static GoogleCalendarService instance;
  public static GoogleCalendarService Instance
  {
    get
    {
      if (instance == null)
      {
        instance = FindObjectOfType<GoogleCalendarService>();
      }
      return instance;
    }
  }

  private CalendarService googleService;

  public void Awake()
  {
    CreateGoogleService();
  }
  public void CreateGoogleService()
  {
    UserCredential credential;

    using (var stream = new FileStream("credentials.json", FileMode.Open, FileAccess.Read))
    {
      string credPath = "token.json";
      credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
          GoogleClientSecrets.Load(stream).Secrets,
          Scopes,
          "user",
          CancellationToken.None,
          new FileDataStore(credPath, true)).Result;
    }

    googleService = new CalendarService(new BaseClientService.Initializer()
    {
      HttpClientInitializer = credential,
      ApplicationName = ApplicationName,
    });
  }

  public DateMarkerEvent InsertEvent(DateMarkerEvent dateMarkerEvent)
  {
    EventsResource.InsertRequest insertRequest = googleService.Events.Insert(dateMarkerEvent.GoogleEvent, CALENDARID);

    return insertRequest.Execute().ToDateMarkerEvent();
  }

  public List<DateMarkerEvent> GetMonthEvents(Month month)
  {
    EventsResource.ListRequest getRequest = googleService.Events.List(CALENDARID);
    getRequest.TimeMin = new DateTime(month.Year, month.MonthNumber, month.Days.First().DayNumber, 00, 00, 00);
    getRequest.TimeMax = new DateTime(month.Year, month.MonthNumber, month.Days.Last().DayNumber, 23, 59, 59);
    getRequest.ShowDeleted = false;
    getRequest.SingleEvents = true;
    getRequest.MaxResults = 999;
    getRequest.OrderBy = EventsResource.ListRequest.OrderByEnum.StartTime;
    return getRequest.Execute().ToDateMarkerEvents();
  }

  public List<DateMarkerEvent> GetYearEvents(Calendar calendar)
  {
    EventsResource.ListRequest getRequest = googleService.Events.List(CALENDARID);

    var startMonth = calendar.Months.First().MonthNumber;
    var startDay = calendar.Months.First().Days.First().DayNumber;
    getRequest.TimeMin = new DateTime(calendar.Year, startMonth, startDay, 00, 00 ,00);

    var endMonth = calendar.Months.Last().MonthNumber;
    var endDay = calendar.Months.Last().Days.Last().DayNumber;
    getRequest.TimeMax = new DateTime(calendar.Year, endMonth, endDay, 23, 59 ,59);

    getRequest.ShowDeleted = false;
    getRequest.SingleEvents = true;
    getRequest.MaxResults = 999;
    getRequest.OrderBy = EventsResource.ListRequest.OrderByEnum.StartTime;
    return getRequest.Execute().ToDateMarkerEvents();
  }

  public void DeleteEvent(DateMarkerEvent dateMarkerEvent)
  {
    var deleteRequest = googleService.Events.Delete(CALENDARID, dateMarkerEvent.GoogleEvent.Id);
    deleteRequest.Execute();
  }

  public DateMarkerEvent UpdateEvent(DateMarkerEvent dateMarkerEvent)
  {
    var updateRequest = googleService.Events.Update(dateMarkerEvent.GoogleEvent, CALENDARID, dateMarkerEvent.GoogleEvent.Id);

    return updateRequest.Execute().ToDateMarkerEvent();
  }
}
