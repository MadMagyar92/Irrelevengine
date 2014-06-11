using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace Completely_Irrelevant
{
    public class TerrainParser
    {
        public List<AbstractTerrain> TerrainList;

        private Dictionary<string, object> typeMap;

        public TerrainParser()
        {
            TerrainList = new List<AbstractTerrain>();

            typeMap = new Dictionary<string, object>();
            typeMap.Add(GenericParser.sandstone, new Sandstone(new Rectangle(0, 0, 0, 0), null, CollisionType.Solid, ""));
        }

        public void Parse(IEnumerable<XElement> terrainList)
        {
            foreach (XElement element in terrainList)
            {
                Rectangle position = ParsePosition(element);
                CollisionType collisionType = ParseCollisionType(element);
                XAttribute attribute = element.Attribute(GenericParser.typeAttr);
                string message = ParseMessage(element);

                if (attribute != null && (typeMap[((string)attribute)] is Sandstone))
                {
                    TerrainList.Add(new Sandstone(position, SpriteFactory.GetSandstoneSprite(), collisionType, message));
                }
            }
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
            }
            else
            {
                returnValue = TerrainDefaultValues.Orientation;
            }

            return returnValue;
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
                returnValue = TerrainDefaultValues.Message;
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
                xValue = (int)TerrainDefaultValues.Position.X;
            }

            if (y != null)
            {
                yValue = (int)y;
            }
            else
            {
                yValue = (int)TerrainDefaultValues.Position.Y;
            }

            if (width != null)
            {
                widthValue = (int)width;
            }
            else
            {
                widthValue = (int)TerrainDefaultValues.Size.X;
            }

            if (height != null)
            {
                heightValue = (int)height;
            }
            else
            {
                heightValue = (int)TerrainDefaultValues.Size.Y;
            }

            return new Rectangle(xValue, yValue, widthValue, heightValue);
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
                returnValue = TerrainDefaultValues.CollisionType;
            }

            return returnValue;
        }
    }
}
