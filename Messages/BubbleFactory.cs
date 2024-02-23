using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Line.Messaging.Messages
{
    public static class BubbleFactory
    {
        /// <summary>
        /// Create bubble with picture in hero
        /// </summary>
        /// <returns></returns>
        public static BubbleContainer CreateImageBubble(
            AspectRatio aspectRatio,
            string pictureUrl,
            string HeaderText,
            string buttonText = "",
            string buttonUrl = "",
            string[]? innerText = null,
            ComponentSize componentSize = ComponentSize.Full, AspectMode aspectMode = AspectMode.Cover)
            => CreateBubble(aspectRatio, pictureUrl, HeaderText, new Dictionary<string, string> { { buttonText, buttonUrl } }, innerText, componentSize, aspectMode);
        
        /// <summary>
        /// Create bubble with picture in hero
        /// </summary>
        /// <returns></returns>
        public static BubbleContainer CreateImageBubble(
           AspectRatio aspectRatio,
           string pictureUrl,
           string HeaderText,
           Dictionary<string, string> buttonTextAndUrl,
           string[]? innerText = null,
           ComponentSize componentSize = ComponentSize.Full, AspectMode aspectMode = AspectMode.Cover)
            => CreateBubble(aspectRatio, pictureUrl, HeaderText, buttonTextAndUrl, innerText, componentSize, aspectMode);


        /// <summary>
        /// Create bubble without hero(picture)
        /// </summary>
        /// <returns></returns>
        public static BubbleContainer CreateDefaultBubble(
            string HeaderText,
            string buttonText = "",
            string buttonUrl = "",
            string[]? innerText = null)
            =>CreateBubble(null, null, HeaderText, new Dictionary<string, string> { { buttonText, buttonUrl } }, innerText);

        /// <summary>
        /// Create bubble without hero(picture)
        /// </summary>
        /// <returns></returns>
        public static BubbleContainer CreateDefaultBubble(
            string HeaderText,
            Dictionary<string, string> buttonTextAndUrl,
            string[]? innerText = null)
            => CreateBubble(null, null, HeaderText, buttonTextAndUrl, innerText);

        /// <summary>
        /// Insert inner text to container,one string in one line.
        /// </summary>
        public static void AddTextInContainerBody(this BubbleContainer container, string[] innerText, string textColor = "#555555", bool htmlDecode = true)
        {
            foreach (var line in innerText)
            {
                string cleanLine = htmlDecode ? HttpUtility.HtmlDecode(line) : line;

                container.Body.Contents.Add(new BoxComponent
                {
                    Layout = BoxLayout.Vertical,
                    Margin = Spacing.Lg,
                    Spacing = Spacing.Sm,
                    Contents = new IFlexComponent[]
                    {
                        new BoxComponent
                        {
                            Layout = BoxLayout.Baseline,
                            Spacing = Spacing.Sm,
                            Contents = new IFlexComponent[]
                            {
                                new TextComponent
                                {
                                    Text = cleanLine,
                                    Color = textColor,
                                    Size = ComponentSize.Sm,
                                    Flex = 1
                                }
                            }
                        }
                    }
                });
            }
        }

        private static BubbleContainer CreateBubble(
            AspectRatio? aspectRatio,
            string? pictureUrl,
            string HeaderText,
            Dictionary<string, string>? buttonTextAndUrl,
            string[]? innerText = null,
            ComponentSize componentSize = ComponentSize.Full, AspectMode aspectMode = AspectMode.Cover)
        {

            BubbleContainer container;
            if (aspectRatio is not null && !string.IsNullOrEmpty(pictureUrl))
            {

                container = new()
                {
                    Hero = new ImageComponent
                    {
                        Url = pictureUrl,
                        Size = componentSize,
                        AspectRatio = aspectRatio,
                        AspectMode = aspectMode
                    },
                    Body = new BoxComponent
                    {
                        Layout = BoxLayout.Vertical,
                        Spacing = Spacing.Md,
                        Contents = new List<IFlexComponent>
                    {
                        new TextComponent
                        {
                            Text = HeaderText,
                            Weight = Weight.Bold,
                            Size = ComponentSize.Xl
                        },
                        new SeparatorComponent()
                    }
                    },
                    Footer = new BoxComponent
                    {
                        Layout = BoxLayout.Vertical,
                        Spacing = Spacing.Sm,
                        Contents = new List<IFlexComponent>()
                    }
                };
            }
            else
            {
                container = new()
                {
                    Body = new BoxComponent
                    {
                        Layout = BoxLayout.Vertical,
                        Spacing = Spacing.Md,
                        Contents = new List<IFlexComponent>
                    {
                        new TextComponent
                        {
                            Text = HeaderText,
                            Weight = Weight.Bold,
                            Size = ComponentSize.Xl
                        },
                        new SeparatorComponent()
                    }
                    },
                    Footer = new BoxComponent
                    {
                        Layout = BoxLayout.Vertical,
                        Spacing = Spacing.Sm,
                        Contents = new List<IFlexComponent>()
                    }
                };
            }

            if (buttonTextAndUrl is not null && buttonTextAndUrl.Count > 0)
            {
                foreach (var TextAndUrl in buttonTextAndUrl)
                {
                    if (!string.IsNullOrWhiteSpace(TextAndUrl.Key) && !string.IsNullOrWhiteSpace(TextAndUrl.Value))
                    {
                        container.AddFooterContents(
                            new ButtonComponent
                            {
                                Style = ButtonStyle.Secondary,
                                Height = ButtonHeight.Sm,
                                Action = new UriTemplateAction(TextAndUrl.Key, TextAndUrl.Value)
                            }
                        );
                    }
                }
                container.AddFooterContents(new BoxComponent { Layout = BoxLayout.Vertical });
            }

            if (innerText is not null && innerText.Length > 0)
            {
                container.AddTextInContainerBody(innerText);
            }

            return container;
        }
    }
}
