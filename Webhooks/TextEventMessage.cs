namespace Line.Messaging.Webhooks
{
    /// <summary>
    /// Message object which contains the text sent from the source.
    /// </summary>
    public class TextEventMessage : EventMessage
    {
        public string Text { get; }

        public TextEventMessage(string id, string text) : base(EventMessageType.Text, id)
        {
            Text = text;
        }
    }
    /// <summary>
    /// Message object which contains the image sent from the source.
    /// </summary>
    public class ImageEventMessage : MediaEventMessage
    {
        public ImageEventMessage(
            EventMessageType type,
            string id,
            ContentProvider contentProvider = null,
            int? duration = null
            ) : base(type, id, contentProvider, duration)
        {
        }
    }
    /// <summary>
    /// Message object which contains the video sent from the source.
    /// </summary>
    public class VideoEventMessage : MediaEventMessage
    {
        public VideoEventMessage(
            EventMessageType type,
            string id,
            ContentProvider contentProvider = null,
            int? duration = null
            ) : base(type, id, contentProvider, duration)
        {
        }
    }
    /// <summary>
    /// Message object which contains the audio sent from the source.
    /// </summary>
    public class AudioEventMessage : MediaEventMessage
    {
        public AudioEventMessage(
            EventMessageType type,
            string id,
            ContentProvider contentProvider = null,
            int? duration = null
            ) : base(type, id, contentProvider, duration)
        {
        }
    }
}
