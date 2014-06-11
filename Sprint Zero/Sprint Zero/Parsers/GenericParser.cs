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
    public static class GenericParser
    {
        public static AbstractAnimatedSprite background;
        public static SoundEffectInstance music;

        public static string xAttr = "x";
        public static string newXAttr = "newx";
        public static string yAttr = "y";
        public static string newYAttr = "newy";
        public static string orientionAttr = "orientation";
        public static string activatedOrientationAttr = "activatedorientation";
        public static string durationAttr = "duration";
        public static string widthAttr = "width";
        public static string blockWidthAttr = "blockwidth";
        public static string heightAttr = "height";
        public static string healthAttr = "health";
        public static string waterLevelAttr = "waterlevel";
        public static string powerAttr = "power";
        public static string speedAttr = "speed";
        public static string activatedSpeedAttr = "activatedspeed";
        public static string freqAttr = "freq";
        public static string nextLevelAttr = "nextlevel";
        public static string isWaitingForActivationAttr = "iswaitingforactivation";
        public static string typeAttr = "type";
        public static string damageAttr = "damage";
        public static string messageAttr = "message";

        public static string minXAttr = "minx";
        public static string maxXAttr = "maxx";
        public static string minYAttr = "miny";
        public static string maxYAttr = "maxy";
        public static string paddingXAttr = "paddingx";
        public static string paddingYAttr = "paddingy";
        public static string scaleAttr = "scale";

        public static string hortoise = "hortoise";
        public static string spider = "spider";
        public static string cactus = "cactus";
        public static string golem = "golem";

        public static string bonfire = "bonfire";
        public static string sunscreen = "sunscreen";
        public static string bubble = "bubbleshield";
        public static string star = "invincibilitystar";
        public static string bottle = "waterbottle";
        public static string timestop = "timestop";
        public static string coin = "coin";

        public static string crate = "crate";
        public static string boulder = "boulder";
        public static string falsebrick = "falsebrick";
        public static string button = "button";
        public static string fishbowl = "fishbowl";
        public static string joebota = "joebota";
        public static string door = "door";
        public static string teleporter = "teleporter";
        public static string conveyor = "conveyor";
        public static string continuousbutton = "continuousbutton";

        public static string left = "left";
        public static string right = "right";
        public static string rightside = "up";
        public static string upsidedown = "down";
        public static string clockwise = "cw";
        public static string counterclockwise = "ccw";

        public static string backgroundAttr = "background";
        public static string musicAttr = "music";

        public static string dehydrationAttr = "dehydration";
        public static string dehydrationDamageAttr = "dehydrationdamage";
        public static string dehydrationTime = "dehydrationtime";

        public static string desert = "desert";
        public static string hospital = "hospital";

        public static string terrainString = "terrain";
        public static string collectableString = "collectable";
        public static string itemString = "item";
        public static string enemyString = "enemy";
        public static string characterString = "character";
        public static string cameraString = "camera";
        public static string propertiesString = "properties";
        public static string itemSetString = "itemset";

        public static string sendersString = "senders";
        public static string receiversString = "receivers";
        public static string basicItemSetString = "basicitemset";
        public static string invokingModeString = "invokingmode";
        public static string invokingModeSingle = "single";
        public static string invokingModeMultiple = "multiple";

        public static string collisionTypeAttr = "collisiontype";
        public static string solidAttr = "solid";
        public static string liquidAttr = "liquid";
        public static string gasAttr = "gas";

        public static string lava = "lava";
        public static string sandstone = "sandstone";
        public static string brick = "brick";
        public static string spiked = "spiked";


        public static void ParseProperties(IEnumerable<XElement> elements)
        {

            foreach (XElement element in elements)
            {
                String b, m;
                b = (string)element.Attribute(backgroundAttr);
                m = (string)element.Attribute(musicAttr);

                if (b.Equals(desert))
                {
                    background = SpriteFactory.GetDesertBackgroundSprite();
                }

                else if (b.Equals(hospital))
                {
                    background = SpriteFactory.GetDesertBackgroundSprite();
                }

                else
                {
                    //background = SpriteFactory.GetDesertBackgroundSprite();
                }

                if (m.Equals(desert))
                {
                    music = SoundFactory.GetDesertSong();
                }

                else if (m.Equals(hospital))
                {
                    music = SoundFactory.GetHospitalSong();
                }

                else
                {
                    //music = SoundFactory.GetDesertSongSound();
                }
            }
        }

        

        public static CollisionType ParseCollisionType(XElement element)
        {
            string type = (string)element.Attribute(collisionTypeAttr);

            if (type.Equals(solidAttr))
                return CollisionType.Solid;
            else if (type.Equals(liquidAttr))
                return CollisionType.Liquid;
            else if (type.Equals(gasAttr))
                return CollisionType.Gas;
            else
                return CollisionType.Solid;
        }
    }
}
