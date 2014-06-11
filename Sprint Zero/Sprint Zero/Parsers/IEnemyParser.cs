using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace Completely_Irrelevant
{
    public class IEnemyParser
    {
        public List<IEnemy> EnemiesList;

        public IEnemyParser()
        {
            EnemiesList = new List<IEnemy>();
        }

        public void Parse(IEnumerable<XElement> enemies)
        {
            foreach (XElement element in enemies)
            {
                int power, speed, freq;
                SpriteOrientation orient;

                Rectangle position = ParsePosition(element);
                string message = ParseMessage(element);
                power = ParseInt(element, GenericParser.powerAttr);
                speed = ParseInt(element, GenericParser.speedAttr);
                freq = ParseInt(element, GenericParser.freqAttr);
                orient = ParseSpriteOrientation(element, GenericParser.orientionAttr);
                CollisionType collisionType = ParseCollisionType(element);

                if (element.Name.ToString().Equals(GenericParser.hortoise))
                {
                    HortoiseEnemy h = new HortoiseEnemy(position, power, speed, orient, collisionType, message);
                    EnemiesList.Add(h);
                }

                else if (element.Name.ToString().Equals(GenericParser.spider))
                {
                    SpiderEnemy s = new SpiderEnemy(position, power, speed, freq, orient, collisionType, message);
                    EnemiesList.Add(s);
                }

                else if (element.Name.ToString().Equals(GenericParser.cactus))
                {
                    CactusEnemy c = new CactusEnemy(position, power, speed, freq, collisionType, message);
                    EnemiesList.Add(c);
                }

                else if (element.Name.ToString().Equals(GenericParser.golem))
                {
                    GolemEnemy g = new GolemEnemy(position, new Vector2(0, 0), SpriteFactory.GetGolemEnemySprite(), power, collisionType, message);
                    EnemiesList.Add(g);
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
                returnValue = IEnemyDefaultValues.Message;
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
                returnValue = IEnemyDefaultValues.CollisionType;
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
                if (attrName.Equals(GenericParser.powerAttr))
                {
                    returnValue = IEnemyDefaultValues.Power;
                }
                else if (attrName.Equals(GenericParser.speedAttr))
                {
                    returnValue = IEnemyDefaultValues.Speed;
                }
                else if (attrName.Equals(GenericParser.freqAttr))
                {
                    returnValue = IEnemyDefaultValues.Frequency;
                }
            }

            return returnValue;
        }

        private static SpriteOrientation ParseSpriteOrientation(XElement element, string attrName)
        {
            XAttribute attribute = element.Attribute(attrName);
            SpriteOrientation returnValue = SpriteOrientation.Up;

            if (attribute != null)
            {
                string s = (string)attribute;


                if(s.Equals(GenericParser.upsidedown))
                {
                    returnValue = SpriteOrientation.Down;
                }
                else if(s.Equals(GenericParser.rightside))
                {
                    returnValue = SpriteOrientation.Up;
                }
                else if(s.Equals(GenericParser.left))
                {
                    returnValue = SpriteOrientation.Left;
                }
                else if(s.Equals(GenericParser.right))
                {
                    returnValue = SpriteOrientation.Right;
                }
            }
            else
            {
                returnValue = IEnemyDefaultValues.Orientation;
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
                xValue = (int)IEnemyDefaultValues.Position.X;
            }

            if (y != null)
            {
                yValue = (int)y;
            }
            else
            {
                yValue = (int)IEnemyDefaultValues.Position.Y;
            }

            if (width != null)
            {
                widthValue = (int)width;
            }
            else
            {
                widthValue = (int)IEnemyDefaultValues.Size.X;
            }

            if (height != null)
            {
                heightValue = (int)height;
            }
            else
            {
                heightValue = (int)IEnemyDefaultValues.Size.Y;
            }

            return new Rectangle(xValue, yValue, widthValue, heightValue);
        }
    }
}
