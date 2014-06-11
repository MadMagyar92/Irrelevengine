using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace Completely_Irrelevant
{
    public class CharacterParser
    {
        public List<AbstractCharacter> CharacterList;

        public CharacterParser()
        {
            CharacterList = new List<AbstractCharacter>();
        }

        public void Parse(IEnumerable<XElement> characters, List<IController> controllers)
        {
            foreach (XElement element in characters)
            {
                int type, health, waterLevel, dehydrationDamage, dehydrationTime;
                bool dehydration;

                Rectangle position = ParsePosition(element);
                string message = ParseMessage(element);
                type = ParseInt(element, GenericParser.typeAttr);
                health = ParseInt(element, GenericParser.healthAttr);
                waterLevel = ParseInt(element, GenericParser.waterLevelAttr);
                dehydrationDamage = ParseInt(element, GenericParser.dehydrationDamageAttr);
                dehydrationTime = ParseInt(element, GenericParser.dehydrationTime);
                dehydration = ParseBool(element, GenericParser.dehydrationAttr);
                CollisionType collisionType = ParseCollisionType(element);

                if (type == 1)
                {
                    CharacterList.Add(new CharacterImpl(health, waterLevel, position, controllers, dehydration, dehydrationDamage, dehydrationTime, collisionType, message));
                }
                else
                {
                    CharacterList.Add(new CharacterImpl(health, waterLevel, position, controllers, dehydration, dehydrationDamage, dehydrationTime, collisionType, message));

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
                returnValue = CharacterDefaultValues.Message;
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
                if (attrName.Equals(GenericParser.healthAttr))
                {
                    returnValue = CharacterDefaultValues.HealthLevel;
                }
                else if (attrName.Equals(GenericParser.waterLevelAttr))
                {
                    returnValue = CharacterDefaultValues.MeterLevel;
                }
                else if (attrName.Equals(GenericParser.dehydrationDamageAttr))
                {
                    returnValue = CharacterDefaultValues.DehydrationDamage;
                }
                else if (attrName.Equals(GenericParser.dehydrationTime))
                {
                    returnValue = CharacterDefaultValues.DehydrationTime;
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
                if (attrName.Equals(GenericParser.dehydrationAttr))
                {
                    returnValue = CharacterDefaultValues.DehydrationEnabled;
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
                returnValue = CharacterDefaultValues.CollisionType;
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
                xValue = (int)CharacterDefaultValues.Position.X;
            }

            if (y != null)
            {
                yValue = (int)y;
            }
            else
            {
                yValue = (int)CharacterDefaultValues.Position.Y;
            }

            if (width != null)
            {
                widthValue = (int)width;
            }
            else
            {
                widthValue = (int)CharacterDefaultValues.Size.X;
            }

            if (height != null)
            {
                heightValue = (int)height;
            }
            else
            {
                heightValue = (int)CharacterDefaultValues.Size.Y;
            }

            return new Rectangle(xValue, yValue, widthValue, heightValue);
        }
    }
}
