using Microsoft.Xna.Framework.Audio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace Completely_Irrelevant
{
    public class PropertiesParser
    {
        public SoundEffectInstance Music;
        public AbstractAnimatedSprite Background;

        public PropertiesParser()
        {
            Music = null;
            Background = null;
        }

        public void Parse(IEnumerable<XElement> elements)
        {
            foreach (XElement element in elements)
            {
                String b, m;
                b = (string)element.Attribute(GenericParser.backgroundAttr);
                m = (string)element.Attribute(GenericParser.musicAttr);

                if (b.Equals(GenericParser.desert))
                {
                    Background = SpriteFactory.GetDesertBackgroundSprite();
                }

                else if (b.Equals(GenericParser.hospital))
                {
                    Background = SpriteFactory.GetHospitalBackgroundSprite();
                }

                else
                {
                    Background = SpriteFactory.GetDSBackgroundSprite();
                }

                if (m.Equals(GenericParser.desert))
                {
                    Music = SoundFactory.GetDesertSong();
                }

                else if (m.Equals(GenericParser.hospital))
                {
                    Music = SoundFactory.GetHospitalSong();
                }

                else
                {
                    Music = SoundFactory.GetInvincibleSong();
                }
            }
        }
    }
}
