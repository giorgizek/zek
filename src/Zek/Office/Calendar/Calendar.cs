using System.Collections.Generic;

namespace Zek.Office
{
    public class Calendar
    {
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets can edit.
        /// True if the user can write to the calendar, false otherwise. This property is true for the user who created the calendar. This property is also true for a user who has been shared a calendar and granted write access.
        /// </summary>
        public bool? CanEdit { get; set; }


        /// <summary>
        /// Gets or sets allowed online meeting providers.
        /// Represent the online meeting service providers that can be used to create online meetings in this calendar. Possible values are: unknown, skypeForBusiness, skypeForConsumer, teamsForBusiness.
        /// </summary>
        public IEnumerable<OnlineMeetingProviderType> AllowedOnlineMeetingProviders { get; set; }

        /// <summary>
        /// Gets or sets can share.
        /// True if the user has the permission to share the calendar, false otherwise. Only the user who created the calendar can share it.
        /// </summary>
        public bool? CanShare { get; set; }

        /// <summary>
        /// Gets or sets can view private items.
        /// True if the user can read calendar items that have been marked private, false otherwise.
        /// </summary>
        public bool? CanViewPrivateItems { get; set; }

        /// <summary>
        /// Gets or sets change key.
        /// Identifies the version of the calendar object. Every time the calendar is changed, changeKey changes as well. This allows Exchange to apply changes to the correct version of the object. Read-only.
        /// </summary>
        public string ChangeKey { get; set; }

        /// <summary>
        /// Gets or sets color.
        /// Specifies the color theme to distinguish the calendar from other calendars in a UI. The property values are: LightBlue=0, LightGreen=1, LightOrange=2, LightGray=3, LightYellow=4, LightTeal=5, LightPink=6, LightBrown=7, LightRed=8, MaxColor=9, Auto=-1
        /// </summary>
        public int? Color { get; set; }

        /// <summary>
        /// Gets or sets default online meeting provider.
        /// The default online meeting provider for meetings sent from this calendar. Possible values are: unknown, skypeForBusiness, skypeForConsumer, teamsForBusiness.
        /// </summary>
        public OnlineMeetingProviderType? DefaultOnlineMeetingProvider { get; set; }

        /// <summary>Gets or sets hex color.</summary>
        public string HexColor { get; set; }

        /// <summary>Gets or sets is default calendar.</summary>
        public bool? IsDefaultCalendar { get; set; }

        /// <summary>
        /// Gets or sets is removable.
        /// Indicates whether this user calendar can be deleted from the user mailbox.
        /// </summary>
        public bool? IsRemovable { get; set; }

        /// <summary>
        /// Gets or sets is tallying responses.
        /// Indicates whether this user calendar supports tracking of meeting responses. Only meeting invites sent from users' primary calendars support tracking of meeting responses.
        /// </summary>
        public bool? IsTallyingResponses { get; set; }

        /// <summary>
        /// Gets or sets name.
        /// The calendar name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets owner.
        /// If set, this represents the user who created or added the calendar. For a calendar that the user created or added, the owner property is set to the user. For a calendar shared with the user, the owner property is set to the person who shared that calendar with the user.
        /// </summary>
        public EmailAddress Owner { get; set; }


        public IEnumerable<Event> Events { get; set; }

    }
}