namespace Schedules.WebClients.Requests
{
    /// <summary>
    ///The Schedule Details Request 
    /// <author>Jeremiah Liscum</author>
    /// </summary>
    public class ScheduleNotesRequest
    {
        public int SchedulePKey { get; set; }
        public int NoteOwnerType { get; set; }
        public int OwnerPKey { get; set; }
        public string ScheduleNotes { get; set; }
        public string ClientNotes { get; set; }
    }
}
