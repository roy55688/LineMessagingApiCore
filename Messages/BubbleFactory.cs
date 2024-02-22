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
        {
            BubbleContainer container = new()
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

            if (!string.IsNullOrEmpty(buttonText) && !string.IsNullOrEmpty(buttonUrl))
            {
                container.AddFooterContents(
                        new ButtonComponent
                        {
                            Style = ButtonStyle.Secondary,
                            Height = ButtonHeight.Sm,
                            Action = new UriTemplateAction(buttonText, buttonUrl)
                        }
                    );

                container.AddFooterContents(new BoxComponent{Layout = BoxLayout.Vertical});
                            
            }

            if (innerText is not null && innerText.Length > 0)
            {
                container.AddMutiTextToContainerBody(innerText);
            }

            return container;
        }

        /// <summary>
        /// Create bubble with picture in hero
        /// </summary>
        /// <param name="buttonTextAndUrl">Key is text,value is url</param>
        /// <returns></returns>
        public static BubbleContainer CreateImageBubble(
           AspectRatio aspectRatio,
           string pictureUrl,
           string HeaderText,
           Dictionary<string, string> buttonTextAndUrl,
           string[]? innerText = null,
           ComponentSize componentSize = ComponentSize.Full, AspectMode aspectMode = AspectMode.Cover)
        {
            BubbleContainer container = new()
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
                }
            };

            foreach (var TextAndUrl in buttonTextAndUrl)
            {
                container.AddFooterContents(
                    new BoxComponent
                    {
                        Layout = BoxLayout.Vertical,
                        Spacing = Spacing.Sm,
                        Contents = new List<IFlexComponent>
                        {
                            new ButtonComponent
                            {
                                Style = ButtonStyle.Secondary,
                                Height = ButtonHeight.Sm,
                                Action = new UriTemplateAction(TextAndUrl.Key, TextAndUrl.Value)
                            },
                            new BoxComponent
                            {
                                Layout = BoxLayout.Vertical
                            }
                        }
                    });
            }

            if (innerText is not null && innerText.Length > 0)
            {
                container.AddMutiTextToContainerBody(innerText);
            }

            return container;
        }

        /// <summary>
        /// Create bubble without picture in hero
        /// </summary>
        /// <returns></returns>
        public static BubbleContainer CreateDefaultBubble(
            string HeaderText,
            string buttonText = "",
            string buttonUrl = "",
            string[]? innerText = null)
        {
            BubbleContainer container = new()
            {
                Hero = new ImageComponent
                { },
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
                }
            };

            if (!string.IsNullOrEmpty(buttonText) && !string.IsNullOrEmpty(buttonUrl))
            {
                container.AddFooterContents(
                    new BoxComponent
                    {
                        Layout = BoxLayout.Vertical,
                        Spacing = Spacing.Sm,
                        Contents = new List<IFlexComponent>
                        {
                            new ButtonComponent
                            {
                                Style = ButtonStyle.Secondary,
                                Height = ButtonHeight.Sm,
                                Action = new UriTemplateAction(buttonText, buttonUrl)
                            },
                            new BoxComponent
                            {
                                Layout = BoxLayout.Vertical
                            }
                        }
                    });
            }

            if (innerText is not null && innerText.Length > 0)
            {
                container.AddMutiTextToContainerBody(innerText);
            }

            return container;
        }

        /// <summary>
        /// Create bubble without picture in hero
        /// </summary>
        /// <param name="buttonTextAndUrl">Key is text,value is url</param>
        /// <returns></returns>
        public static BubbleContainer CreateDefaultBubble(
            string HeaderText,
           Dictionary<string, string> buttonTextAndUrl,
            string[]? innerText = null)
        {
            BubbleContainer container = new()
            {
                Hero = new ImageComponent
                { },
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
                }
            };

            foreach (var TextAndUrl in buttonTextAndUrl)
            {
                container.AddFooterContents(
                    new BoxComponent
                    {
                        Layout = BoxLayout.Vertical,
                        Spacing = Spacing.Sm,
                        Contents = new List<IFlexComponent>
                        {
                            new ButtonComponent
                            {
                                Style = ButtonStyle.Secondary,
                                Height = ButtonHeight.Sm,
                                Action = new UriTemplateAction(TextAndUrl.Key, TextAndUrl.Value)
                            },
                            new BoxComponent
                            {
                                Layout = BoxLayout.Vertical
                            }
                        }
                    });
            }

            if (innerText is not null && innerText.Length > 0)
            {
                container.AddMutiTextToContainerBody(innerText);
            }

            return container;
        }

        public static void AddMutiTextToContainerBody(this BubbleContainer container, string[] innerText, string textColor = "#555555", bool htmlDecode = true)
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
    }
}
