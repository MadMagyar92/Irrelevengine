using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;

namespace Completely_Irrelevant
{

    public static class SoundFactory
    {
        private static ContentManager content;
        private static int index;
        public readonly static int MaxAmountOfSounds = 15;
        private static SoundEffectInstance[] sei = new SoundEffectInstance[MaxAmountOfSounds];

        public static void SetContentManager(ContentManager manager)
        {
            content = manager;
            index = 0;
        }
        
        public static SoundEffectInstance GetDesertSong()
        {
            SoundEffectInstance s = content.Load<SoundEffect>("Sounds\\Music\\InTheDesert").CreateInstance();
            s.IsLooped = true;
            return s;
        }

        public static SoundEffectInstance GetHospitalSong()
        {
            SoundEffectInstance s = content.Load<SoundEffect>("Sounds\\Music\\InTheHospital").CreateInstance();
            s.IsLooped = true;
            return s;
        }

        public static SoundEffectInstance GetInvincibleSong()
        {
            SoundEffectInstance s = content.Load<SoundEffect>("Sounds\\Music\\Placeholder").CreateInstance();
            s.IsLooped = true;
            return s;
        }

        public static SoundEffectInstance GetDeathSong()
        {
            SoundEffectInstance s = content.Load<SoundEffect>("Sounds\\Music\\Placeholder").CreateInstance();
            s.IsLooped = true;
            return s;
        }

        //TEMPLATE SOUND METHOD
        /*public static void Play____Sound()
        {
            if(sei[index] != null)
            {
                sei[index].Dispose();
            }
            sei[index] = content.Load<SoundEffect>("Sounds\\SFX\\").CreateInstance();
            sei[index].Play();
            index = (index++ % MaxAmountOfSounds);
        }*/

        public static void PlayButtonPressedSound()
        {
            if (sei[index] != null)
            {
                sei[index].Dispose();
            }
            sei[index] = content.Load<SoundEffect>("Sounds\\SFX\\ButtonPressed").CreateInstance();
            sei[index].Play();
            index = (index++ % MaxAmountOfSounds);
        }

        public static void PlayCactusShootingSound()
        {
            if (sei[index] != null)
            {
                sei[index].Dispose();
            }
            sei[index] = content.Load<SoundEffect>("Sounds\\SFX\\CactusShooting").CreateInstance();
            sei[index].Play();
            index = (index++ % MaxAmountOfSounds);
        }

        public static void PlayCharacterHurtSound()
        {
            if (sei[index] != null)
            {
                sei[index].Dispose();
            }
            sei[index] = content.Load<SoundEffect>("Sounds\\SFX\\CharacterHurt").CreateInstance();
            sei[index].Play();
            index = (index++ % MaxAmountOfSounds);
        }

        public static void PlayCharacterJumpingSound()
        {
            if (sei[index] != null)
            {
                sei[index].Dispose();
            }
            sei[index] = content.Load<SoundEffect>("Sounds\\SFX\\CharacterJumping").CreateInstance();
            sei[index].Play();
            index = (index++ % MaxAmountOfSounds);
        }

        public static void PlayCollectibleCollectedSound()
        {
            if (sei[index] != null)
            {
                sei[index].Dispose();
            }
            sei[index] = content.Load<SoundEffect>("Sounds\\SFX\\CollectibleCollected").CreateInstance();
            sei[index].Play();
            index = (index++ % MaxAmountOfSounds);
        }

        public static void PlayCrateLandingSound()
        {
            if (sei[index] != null)
            {
                sei[index].Dispose();
            }
            sei[index] = content.Load<SoundEffect>("Sounds\\SFX\\CrateLanding").CreateInstance();
            sei[index].Play();
            index = (index++ % MaxAmountOfSounds);
        }

        public static void PlayCrateMovingSound()
        {
            if (sei[index] != null)
            {
                sei[index].Dispose();
            }
            sei[index] = content.Load<SoundEffect>("Sounds\\SFX\\CrateMoving").CreateInstance();
            sei[index].Play();
            index = (index++ % MaxAmountOfSounds);
        }

        public static void PlayDoorOpenSound()
        {
            if (sei[index] != null)
            {
                sei[index].Dispose();
            }
            sei[index] = content.Load<SoundEffect>("Sounds\\SFX\\DoorOpen").CreateInstance();
            sei[index].Play();
            index = (index++ % MaxAmountOfSounds);
        }

        public static void PlayEnemyDeathSound()
        {
            if (sei[index] != null)
            {
                sei[index].Dispose();
            }
            sei[index] = content.Load<SoundEffect>("Sounds\\SFX\\EnemyDeath").CreateInstance();
            sei[index].Play();
            index = (index++ % MaxAmountOfSounds);
        }

        public static void PlayFishbowlShatteredSound()
        {
            if (sei[index] != null)
            {
                sei[index].Dispose();
            }
            sei[index] = content.Load<SoundEffect>("Sounds\\SFX\\FishbowlShattered").CreateInstance();
            sei[index].Play();
            index = (index++ % MaxAmountOfSounds);
        }

        /*public static void PraiseTheSun()
        {
            //ALL HAIL LORD GWYN
        }*/
    }
}
