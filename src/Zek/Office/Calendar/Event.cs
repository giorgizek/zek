using System.Collections.Generic;

namespace Zek.Office
{
    public class Event : Event<int, DateTimeTimeZone, EventType?, int>
    {

    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TId"></typeparam>
    /// <typeparam name="TDate">DateTime? or DateTimeTimeZone</typeparam>
    /// <typeparam name="TEventType">EventType? or other nullable enum</typeparam>
    /// <typeparam name="TStatus"></typeparam>
    public class Event<TId, TDate, TEventType, TStatus> : EventBase<TId, TDate, TEventType, TStatus>
    {
        /// <summary>
        /// Gets or sets calendar.
        /// The calendar that contains the event. Navigation property. Read-only.
        /// </summary>
        public Calendar Calendar { get; set; }
    }


    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TId">int, string, Guid</typeparam>
    /// <typeparam name="TDate">DateTime? or DateTimeTimeZone</typeparam>
    /// <typeparam name="TEventType">EventType? or other nullable enum</typeparam>
    /// <typeparam name="TStatus"></typeparam>
    public class EventBase<TId, TDate, TEventType, TStatus>
    {
         public TId Id { get; set; }

        /// <summary>
        /// Gets or sets type.
        /// The event type. The possible values are: singleInstance, occurrence, exception, seriesMaster. Read-only.
        /// </summary>
        public TEventType Type { get; set; }

        /// <summary>
        /// Gets or sets start.
        /// The date, time, and time zone that the event starts. By default, the start time is in UTC.
        /// </summary>
        public TDate Start { get; set; }

        /// <summary>
        /// Gets or sets end.
        /// The date, time, and time zone that the event ends. By default, the end time is in UTC.
        /// </summary>
        public TDate End { get; set; }

        /// <summary>
        /// Gets or sets is all day.
        /// Set to true if the event lasts all day.
        /// </summary>
        public bool? AllDay { get; set; }

        /// <summary>
        /// Gets or sets subject.
        /// The text of the event's subject line.
        /// </summary>
        public string Subject { get; set; }

        /// <summary>
        /// Gets or sets location.
        /// The location of the event.
        /// </summary>
        public Location Location { get; set; }

        /// <summary>
        /// Gets or sets locations.
        /// The locations where the event is held or attended from. The location and locations properties always correspond with each other. If you update the location property, any prior locations in the locations collection would be removed and replaced by the new location value.
        /// </summary>
        public IEnumerable<Location> Locations { get; set; }

        /// <summary>
        /// Gets or sets body preview.
        /// The preview of the message associated with the event. It is in text format.
        /// </summary>
        public string BodyPreview { get; set; }

        /// <summary>
        /// Gets or sets body.
        /// The body of the message associated with the event. It can be in HTML or text format.
        /// </summary>
        public ItemBody Body { get; set; }

        /// <summary>
        /// Gets or sets importance.
        /// The importance of the event. The possible values are: low, normal, high.
        /// </summary>
        public Importance? Importance { get; set; }

        /// <summary>
        /// Gets or sets sensitivity.
        /// The possible values are: normal, personal, private, confidential.
        /// </summary>
        public Sensitivity? Sensitivity { get; set; }

        /// <summary>
        /// Gets or sets show as.
        /// The status to show. The possible values are: free, tentative, busy, oof, workingElsewhere, unknown.
        /// </summary>
        public FreeBusyStatus? ShowAs { get; set; }

        /// <summary>
        /// Event status
        /// </summary>
        public TStatus Status { get; set; }

        /// <summary>
        /// Event color
        /// </summary>
        public string Color { get; set; }


        /// <summary>
        /// Gets or sets is cancelled.
        /// Set to true if the event has been canceled.
        /// </summary>
        public bool? IsCancelled { get; set; }

        /// <summary>
        /// Gets or sets is draft.
        /// </summary>
        public bool? IsDraft { get; set; }


        /*
        /// <summary>
        /// Gets or sets has attachments.
        /// Set to true if the event has attachments.
        /// </summary>
        public bool? HasAttachments { get; set; }

        */


        #region Reminder


        /// <summary>
        /// Gets or sets is reminder on.
        /// Set to true if an alert is set to remind the user of the event.
        /// </summary>
        public bool? IsReminderOn { get; set; }

        /// <summary>
        /// Gets or sets reminder minutes before start.
        /// The number of minutes before the event start time that the reminder alert occurs.
        /// </summary>
        public int? ReminderMinutesBeforeStart { get; set; }

        #endregion


        /// <summary>
        /// Gets or sets is organizer.
        /// Set to true if the calendar owner (specified by the owner property of the calendar) is the organizer of the event (specified by the organizer property of the event). This also applies if a delegate organized the event on behalf of the owner.
        /// </summary>
        public bool? IsOrganizer { get; set; }

        /// <summary>
        /// Gets or sets organizer.
        /// The organizer of the event.
        /// </summary>
        public Recipient Organizer { get; set; }



        #region Attendees

        /// <summary>
        /// Gets or sets response requested.
        /// Default is true, which represents the organizer would like an invitee to send a response to the event.
        /// </summary>
        public bool? ResponseRequested { get; set; }

        /// <summary>
        /// Gets or sets response status.
        /// Indicates the type of response sent in response to an event message.
        /// </summary>
        public ResponseStatus ResponseStatus { get; set; }


        /// <summary>
        /// Gets or sets attendees.
        /// The collection of attendees for the event.
        /// </summary>
        public virtual IEnumerable<Attendee> Attendees { get; set; }

        ///<summary>Gets or sets hide attendees.</summary>
        public bool? HideAttendees { get; set; }
        public bool? AllowNewTimeProposals { get; set; }

        #endregion


        /// <summary>
        /// Gets or sets web link.
        /// The URL to open the event in Outlook on the web.Outlook on the web opens the event in the browser if you are signed in to your mailbox. Otherwise, Outlook on the web prompts you to sign in.This URL cannot be accessed from within an iFrame.
        /// </summary>
        public string WebLink { get; set; }

        /// <summary>
        /// Gets or sets i cal uid.
        /// A unique identifier for an event across calendars. This ID is different for each occurrence in a recurring series. Read-only.
        /// </summary>
        // ReSharper disable once InconsistentNaming
        public string ICalUId { get; set; }


        #region Online Meeting


        /// <summary>
        /// Gets or sets is online meeting.
        /// True if this event has online meeting information, false otherwise. Default is false. Optional.
        /// </summary>
        public bool? IsOnlineMeeting { get; set; }

        /// <summary>
        /// Gets or sets online meeting.
        /// Details for an attendee to join the meeting online. Read-only.
        /// </summary>
        public OnlineMeetingInfo OnlineMeeting { get; set; }

        /// <summary>
        /// Gets or sets online meeting provider.
        /// Represents the online meeting service provider. The possible values are teamsForBusiness, skypeForBusiness, and skypeForConsumer. Optional.
        /// </summary>
        public OnlineMeetingProviderType? OnlineMeetingProvider { get; set; }

        /// <summary>
        /// Gets or sets online meeting url.
        /// A URL for an online meeting. The property is set only when an organizer specifies an event as an online meeting such as a Skype meeting. Read-only.
        /// </summary>
        public string OnlineMeetingUrl { get; set; }


        #endregion

        /// <summary>
        /// Gets or sets transaction id.
        /// A custom identifier specified by a client app for the server to avoid redundant POST operations in case of client retries to create the same event. This is useful when low network connectivity causes the client to time out before receiving a response from the server for the client's prior create-event request. After you set transactionId when creating an event, you cannot change transactionId in a subsequent update. This property is only returned in a response payload if an app has set it. Optional.
        /// </summary>
        public string TransactionId { get; set; }
    }
}
