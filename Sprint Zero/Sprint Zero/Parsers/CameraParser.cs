using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace Completely_Irrelevant
{
    public class CameraParser
    {
        public List<Camera> CameraList;

        public CameraParser()
        {
            CameraList = new List<Camera>();
        }

        public void Parse(IEnumerable<XElement> cameras, List<AbstractCharacter> characterList)
        {
            foreach (XElement element in cameras)
            {
                int minX, maxX, minY, maxY, paddingX, paddingY;
                float scale;
                AbstractCharacter character;

                Rectangle position = ParsePosition(element);
                minX = GetInt(element, GenericParser.minXAttr);
                maxX = GetInt(element, GenericParser.maxXAttr);
                minY = GetInt(element, GenericParser.minYAttr);
                maxY = GetInt(element, GenericParser.maxYAttr);
                paddingX = GetInt(element, GenericParser.paddingXAttr);
                paddingY = GetInt(element, GenericParser.paddingYAttr);
                scale = GetFloat(element, GenericParser.scaleAttr);

                if (characterList.Count > CameraList.Count)
                {
                    character = characterList[CameraList.Count];
                }
                else
                {
                    character = null;
                }

                CameraList.Add(new Camera(position, minX, minY, maxX, maxY, paddingX, paddingY, scale, character));
            }
        }

        private int GetInt(XElement element, string attrName)
        {
            XAttribute attribute = element.Attribute(attrName);

            int returnValue = 0;

            if (attribute != null)
            {
                returnValue = (int)attribute;
            }

            else
            {
                if (attrName.Equals(GenericParser.minXAttr))
                {
                    returnValue = CameraDefaultValues.MinX;
                }
                else if (attrName.Equals(GenericParser.maxXAttr))
                {
                    returnValue = CameraDefaultValues.MaxX;
                }
                else if (attrName.Equals(GenericParser.minYAttr))
                {
                    returnValue = CameraDefaultValues.MinY;
                }
                else if (attrName.Equals(GenericParser.maxYAttr))
                {
                    returnValue = CameraDefaultValues.MaxY;
                }
                else if (attrName.Equals(GenericParser.paddingXAttr))
                {
                    returnValue = CameraDefaultValues.PaddingX;
                }
                else if (attrName.Equals(GenericParser.paddingYAttr))
                {
                    returnValue = CameraDefaultValues.PaddingY;
                }
            }

            return returnValue;
        }

        private float GetFloat(XElement element, string attrName)
        {
            XAttribute attribute = element.Attribute(attrName);

            float returnValue = 0;

            if (attribute != null)
            {
                returnValue = (float)attribute;
            }

            else
            {
                if (attrName.Equals(GenericParser.scaleAttr))
                {
                    returnValue = CameraDefaultValues.Scale;
                }
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
                xValue = (int)CameraDefaultValues.Position.X;
            }

            if (y != null)
            {
                yValue = (int)y;
            }
            else
            {
                yValue = (int)CameraDefaultValues.Position.Y;
            }

            if (width != null)
            {
                widthValue = (int)width;
            }
            else
            {
                widthValue = (int)CameraDefaultValues.Size.X;
            }

            if (height != null)
            {
                heightValue = (int)height;
            }
            else
            {
                heightValue = (int)CameraDefaultValues.Size.Y;
            }

            return new Rectangle(xValue, yValue, widthValue, heightValue);
        }
    }
}
