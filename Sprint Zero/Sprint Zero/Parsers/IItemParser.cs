using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace Completely_Irrelevant
{
    public class IItemParser
    {
        public List<IItem> ItemList;

        public IItemParser()
        {
            ItemList = new List<IItem>();
        }

        public void Parse(IEnumerable<XElement> items)
        {
            foreach (XElement element in items)
            {
                Rectangle position = ParsePosition(element);
                CollisionType collisionType = ParseCollisionType(element);
                string message = ParseMessage(element);

                if (element.Name.ToString().Equals(GenericParser.crate))
                {
                    CrateItem c = new CrateItem(position, SpriteFactory.GetCrateSprite(), collisionType, message);
                    ItemList.Add(c);
                }

                else if (element.Name.ToString().Equals(GenericParser.boulder))
                {
                    BoulderItem b = new BoulderItem(position, SpriteFactory.GetBoulderSprite(), collisionType, message);
                    ItemList.Add(b);
                }

                else if (element.Name.ToString().Equals(GenericParser.falsebrick))
                {
                    FalseBrick f = new FalseBrick(SpriteFactory.GetBrickSprite(), ParsePosition(element), ParseCollisionType(element), ParseBool(element, GenericParser.isWaitingForActivationAttr), message);
                    ItemList.Add(f);
                }

                else if (element.Name.ToString().Equals(GenericParser.button))
                {
                    ButtonItem b = new ButtonItem(position, SpriteFactory.GetButtonSprite(), collisionType, message);
                    ItemList.Add(b);
                }

                else if (element.Name.ToString().Equals(GenericParser.fishbowl))
                {
                    FishbowlItem f = new FishbowlItem(position, SpriteFactory.GetFishbowlSprite(), collisionType, message);
                    ItemList.Add(f);
                }

                else if (element.Name.ToString().Equals(GenericParser.joebota))
                {
                    FishbowlItem jb = new FishbowlItem(position, SpriteFactory.GetJoeBotaSprite(), collisionType, message);
                    ItemList.Add(jb);
                }

                else if (element.Name.ToString().Equals(GenericParser.door))
                {
                    Door d = new Door(ParseInt(element, GenericParser.nextLevelAttr), ParseBool(element, GenericParser.isWaitingForActivationAttr), SpriteFactory.GetDoorSprite(), position, collisionType, message);
                    ItemList.Add(d);
                }

                else if (element.Name.ToString().Equals(GenericParser.continuousbutton))
                {
                    ContinuousButton b = new ContinuousButton(position, SpriteFactory.GetButtonSprite(), collisionType, message);
                    ItemList.Add(b);
                }

                else if (element.Name.ToString().Equals(GenericParser.conveyor))
                {
                    int blockWidth = ParseInt(element, GenericParser.blockWidthAttr);
                    SpriteOrientation orientation = ParseSpriteOrientation(element,GenericParser.orientionAttr);
                    float speed = ParseFloat(element,GenericParser.speedAttr);
                    Conveyor c = new Conveyor(
                        new Vector2(position.X, position.Y), 
                        blockWidth, 
                        speed, 
                        orientation,
                        ParseFloat(element, GenericParser.activatedSpeedAttr), 
                        ParseSpriteOrientation(element, GenericParser.activatedOrientationAttr), 
                        SpriteFactory.GetConveyorSprite(blockWidth, orientation, speed));
                    ItemList.Add(c);
                }

                else if (element.Name.ToString().Equals(GenericParser.teleporter))
                {
                    Teleporter teleporter = new Teleporter(SpriteFactory.GetTeleporterSprite(), ParsePosition(element), ParseCollisionType(element), new Vector2(ParseInt(element, GenericParser.newXAttr), ParseInt(element, GenericParser.newYAttr)), ParseBool(element, GenericParser.isWaitingForActivationAttr), message);
                    ItemList.Add(teleporter);
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
                returnValue = IItemDefaultValues.Message;
            }

            return returnValue;
        }
        

        private static int ParseInt(XElement element, string attrName)
        {
            XAttribute attribute = element.Attribute(attrName);

            int returnValue = 0;

            if (attribute != null)
            {
                returnValue = (int)attribute;
            }

            else
            {
                if (attrName.Equals(GenericParser.nextLevelAttr))
                {
                    returnValue = IItemDefaultValues.NextLevel;
                }
                else if (attrName.Equals(GenericParser.newXAttr))
                {
                    returnValue = IItemDefaultValues.NewX;
                }
                else if (attrName.Equals(GenericParser.newYAttr))
                {
                    returnValue = IItemDefaultValues.NewY;
                }

            }

            return returnValue;
        }

        private static float ParseFloat(XElement element, string attrName)
        {
            XAttribute attribute = element.Attribute(attrName);

            float returnValue = 0;

            if (attribute != null)
            {
                returnValue = (float)attribute;
            }

            else
            {
                if (attrName.Equals(GenericParser.nextLevelAttr))
                {
                    returnValue = IItemDefaultValues.NextLevel;
                }
            }

            return returnValue;
        }

        private static bool ParseBool(XElement element, string attrName)
        {
            XAttribute attribute = element.Attribute(attrName);

            bool returnValue = false;

            if (attribute != null)
            {
                returnValue = (bool)attribute;
            }

            else
            {
                if (attrName.Equals(GenericParser.isWaitingForActivationAttr))
                {
                    returnValue = IItemDefaultValues.WaitingForActivation;
                }
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
                returnValue = IItemDefaultValues.CollisionType;
            }

            return returnValue;
        }

        private Rectangle ParsePosition(XElement element)
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
                xValue = (int)IItemDefaultValues.Position.X;
            }

            if (y != null)
            {
                yValue = (int)y;
            }
            else
            {
                yValue = (int)IItemDefaultValues.Position.Y;
            }

            if (width != null)
            {
                widthValue = (int)width;
            }
            else
            {
                widthValue = (int)IItemDefaultValues.Size.X;
            }

            if (height != null)
            {
                heightValue = (int)height;
            }
            else
            {
                heightValue = (int)IItemDefaultValues.Size.Y;
            }

            return new Rectangle(xValue, yValue, widthValue, heightValue);
        }

        private static SpriteOrientation ParseSpriteOrientation(XElement element, string attrName)
        {
            XAttribute attribute = element.Attribute(attrName);
            SpriteOrientation returnValue = SpriteOrientation.Up;

            if (attribute != null)
            {
                string s = (string)attribute;


                if (s.Equals(GenericParser.upsidedown))
                {
                    returnValue = SpriteOrientation.Down;
                }
                else if (s.Equals(GenericParser.rightside))
                {
                    returnValue = SpriteOrientation.Up;
                }
                else if (s.Equals(GenericParser.left))
                {
                    returnValue = SpriteOrientation.Left;
                }
                else if (s.Equals(GenericParser.right))
                {
                    returnValue = SpriteOrientation.Right;
                }
                else if (s.Equals(GenericParser.clockwise))
                {
                    returnValue = SpriteOrientation.Clockwise;
                }
                else if (s.Equals(GenericParser.counterclockwise))
                {
                    returnValue = SpriteOrientation.CounterClockwise;
                }
            }
            else
            {
                returnValue = IEnemyDefaultValues.Orientation;
            }

            return returnValue;
        }
    }
}
