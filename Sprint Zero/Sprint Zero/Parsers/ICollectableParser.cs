using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;

namespace Completely_Irrelevant
{
    public class ICollectableParser
    {
        public List<ICollectable> CollectablesList;

        public ICollectableParser()
        {
            this.CollectablesList = new List<ICollectable>();
        }

        public void Parse(IEnumerable<XElement> collectables)
        {
            foreach (XElement element in collectables)
            {
                int dur = ParseDuration(element);
                Rectangle position = ParsePosition(element);
                CollisionType collisionType = ParseCollisionType(element);
                string message = ParseMessage(element);

                if (element.Name.ToString().Equals(GenericParser.star))
                {
                    InvincibilityStar s = new InvincibilityStar(SpriteFactory.GetInvincibilityStarSprite(), dur, position, collisionType, message);
                    CollectablesList.Add(s);
                }

                else if (element.Name.ToString().Equals(GenericParser.bottle))
                {
                    WaterBottle b = new WaterBottle(SpriteFactory.GetWaterBottleSprite(), dur, position, collisionType, message);
                    CollectablesList.Add(b);
                }

                else if (element.Name.ToString().Equals(GenericParser.healthAttr))
                {
                    HealthBottle health = new HealthBottle(position, SpriteFactory.GetHealthBottleSprite(), collisionType, dur, message);
                    CollectablesList.Add(health);
                }

                else if (element.Name.ToString().Equals(GenericParser.coin))
                {
                    Coin coin = new Coin(position, SpriteFactory.GetCoinSprite(), collisionType, dur, message);
                    CollectablesList.Add(coin);
                }
            }
        }

        private string ParseMessage(XElement element)
        {
            XAttribute attribute = element.Attribute(GenericParser.messageAttr);

            string returnValue = "";

            if (attribute != null)
            {
                returnValue = (string)attribute;
            }
            else
            {
                returnValue = ICollectableDefaultValues.Message;
            }

            return returnValue;
        }

        private CollisionType ParseCollisionType(XElement element)
        {
            XAttribute attribute = element.Attribute(GenericParser.collisionTypeAttr);

            CollisionType returnValue = CollisionType.Solid;

            if (attribute != null)
            {
                string type = (string)element.Attribute(GenericParser.collisionTypeAttr);

                if (type.Equals(GenericParser.solidAttr))
                    returnValue = CollisionType.Solid;
                else if (type.Equals(GenericParser.liquidAttr))
                    returnValue = CollisionType.Liquid;
                else if (type.Equals(GenericParser.gasAttr))
                    returnValue = CollisionType.Gas;
            }

            else
            {
                returnValue = ICollectableDefaultValues.CollisionType;
            }

            return returnValue;
        }

        private static int ParseDuration(XElement element)
        {
            XAttribute durationAttribute = element.Attribute(GenericParser.durationAttr);
            int returnValue = 0;

            if (durationAttribute != null)
            {
                returnValue = (int)durationAttribute;
            }
            else
            {
                returnValue = ICollectableDefaultValues.Duration;
            }

            return returnValue;
        }

        private static Rectangle ParsePosition(XElement element)
        {
            XAttribute x = element.Attribute(GenericParser.xAttr);
            XAttribute y = element.Attribute(GenericParser.yAttr);
            XAttribute width = element.Attribute(GenericParser.widthAttr);
            XAttribute height = element.Attribute(GenericParser.heightAttr);

            int xValue, yValue, widthValue, heightValue;

            if (x != null)
            {
                xValue = (int)x;
            }
            else
            {
                xValue = (int)ICollectableDefaultValues.Position.X;
            }

            if (y != null)
            {
                yValue = (int)y;
            }
            else
            {
                yValue = (int)ICollectableDefaultValues.Position.Y;
            }

            if (width != null)
            {
                widthValue = (int)width;
            }
            else
            {
                widthValue = (int)ICollectableDefaultValues.Size.X;
            }

            if (height != null)
            {
                heightValue = (int)height;
            }
            else
            {
                heightValue = (int)ICollectableDefaultValues.Size.Y;
            }

            return new Rectangle(xValue, yValue, widthValue, heightValue);
        }
    }
}
