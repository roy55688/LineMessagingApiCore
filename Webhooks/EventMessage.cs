using System;

namespace Line.Messaging.Webhooks
{
    /// <summary>
    /// Contents of the message
    /// </summary>
    public class EventMessage
    {
        /// <summary>
        /// Message ID
        /// </summary>
        public string Id { get; }

        /// <summary>
        /// EventMessageType
        /// </summary>
        public EventMessageType Type { get; }

        public EventMessage(EventMessageType type, string id)
        {
            Type = type;
            Id = id;
        }

        internal static EventMessage CreateFrom(dynamic dynamicObject)
        {
            var message = dynamicObject?.message;
            if (message == null) { return null; }
            if (!Enum.TryParse((string)message.type, true, out EventMessageType messageType))
            {
                return null;
            }
            switch (messageType)
            {
                case EventMessageType.Text:
                    return new TextEventMessage((string)message.id, (string)message.text);
                case EventMessageType.Image:
                    ContentProvider contentProviderImage = null;
                    if (Enum.TryParse((string)message.contentProvider?.type, true, out ContentProviderType providerTypeImage))
                    {
                        contentProviderImage = new ContentProvider(providerTypeImage,
                                (string)message.contentProvider?.originalContentUrl,
                                (string)message.contentProvider?.previewContentUrl);
                    }
                    return new ImageEventMessage(messageType,(string)message.id,contentProviderImage, (int?)message.duration);
                case EventMessageType.Audio:
                    ContentProvider contentProviderAudio = null;
                    if (Enum.TryParse((string)message.contentProvider?.type, true, out ContentProviderType providerTypeAudio))
                    {
                        contentProviderAudio = new ContentProvider(providerTypeAudio,
                                (string)message.contentProvider?.originalContentUrl,
                                (string)message.contentProvider?.previewContentUrl);
                    }
                    return new AudioEventMessage(messageType, (string)message.id, contentProviderAudio, (int?)message.duration);
                case EventMessageType.Video:
                    ContentProvider contentProviderVideo = null;
                    if (Enum.TryParse((string)message.contentProvider?.type,true, out ContentProviderType providerTypeVideo))
                    {
                        contentProviderVideo = new ContentProvider(providerTypeVideo,
                                (string)message.contentProvider?.originalContentUrl,
                                (string)message.contentProvider?.previewContentUrl);
                    }
                    return new VideoEventMessage(messageType, (string)message.id, contentProviderVideo, (int?)message.duration);
                case EventMessageType.Location:
                    return new LocationEventMessage((string)message.id, (string)message.title, (string)message.address,
                        (decimal)message.latitude, (decimal)message.longitude);
                case EventMessageType.Sticker:
                    return new StickerEventMessage((string)message.id, (string)message.packageId, (string)message.stickerId);
                case EventMessageType.File:
                    return new FileEventMessage((string)message.id, (string)message.fileName, (long)message.fileSize);
                default:
                    return null;
            }
        }
    }
}
