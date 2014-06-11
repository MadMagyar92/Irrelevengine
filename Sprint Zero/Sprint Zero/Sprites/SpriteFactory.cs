using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Completely_Irrelevant
{
    // To add a new sprite:
    // 1) Make method "public static AbstractAnimatedSprite GetMyNewSprite"
    // 2) Declare a sprite to return in the new method, and set properties as desired

    // Currently, sprites are implemented as individual classes and each set most of their own defaults.
    // Making a generic sprite class and adding methods that take textures is possible, but breaks convention.
    public enum SpriteOrientation { Left, Right, Up, Down, Clockwise, CounterClockwise, None };

    public static class SpriteFactory
    {
        private static ContentManager content; 

        public static void SetContentManager(ContentManager manager)
        {
            content = manager;
        }
        
        public static AbstractAnimatedSprite GetCharacterDeadSprite(int type)
        {
            AbstractAnimatedSprite sprite = new CharacterDeadSprite();
            sprite.Texture = content.Load<Texture2D>(ReturnCorrectResourceName(type, false, "Dead"));

            return sprite;
        }

        public static AbstractAnimatedSprite GetCharacterIdleSprite(int type, SpriteOrientation orientation, bool isCharacterHoldingAnItem)
        {
            AbstractAnimatedSprite sprite = new CharacterIdleSprite(orientation);
            sprite.Texture = content.Load<Texture2D>(ReturnCorrectResourceName(type, isCharacterHoldingAnItem, "Idle"));

            return sprite;
        }

        public static AbstractAnimatedSprite GetCharacterWalkingSprite(int type, SpriteOrientation orientation, bool isCharacterHoldingAnItem)
        {
            AbstractAnimatedSprite sprite = new CharacterWalkingSprite(orientation);
            sprite.Texture = content.Load<Texture2D>(ReturnCorrectResourceName(type, isCharacterHoldingAnItem, "Run"));

            return sprite;
        }

        public static AbstractAnimatedSprite GetCharacterPushingSprite(int type, SpriteOrientation orientation)
        {
            AbstractAnimatedSprite sprite = new CharacterPushingSprite(orientation);
            sprite.Texture = content.Load<Texture2D>(ReturnCorrectResourceName(type, false, "Push"));

            return sprite;
        }

        public static AbstractAnimatedSprite GetCharacterJumpingSprite(int type, SpriteOrientation orientation, bool isCharacterHoldingAnItem)
        {
            AbstractAnimatedSprite sprite = new CharacterJumpingSprite(orientation);
            sprite.Texture = content.Load<Texture2D>(ReturnCorrectResourceName(type, isCharacterHoldingAnItem, "Jump"));

            return sprite;
        }

        public static AbstractAnimatedSprite GetInvincibilityStarSprite()
        {
            AbstractAnimatedSprite sprite = new InvincibilityStarSprite();
            sprite.Texture = content.Load<Texture2D>("Sprites\\CollectableSprites\\StarSprite");

            return sprite;
        }

        public static AbstractAnimatedSprite GetBubbleShieldSprite()
        {
            AbstractAnimatedSprite sprite = new BubbleShieldSprite();
            sprite.Texture = content.Load<Texture2D>("Sprites\\CollectableSprites\\BubbleshieldSprite");

            return sprite;
        }

        public static AbstractAnimatedSprite GetSunscreenSprite()
        {
            AbstractAnimatedSprite sprite = new SunscreenSprite();
            sprite.Texture = content.Load<Texture2D>("Sprites\\CollectableSprites\\SunscreenSprite2");

            return sprite;
        }

        public static AbstractAnimatedSprite GetCoinSprite()
        {
            AbstractAnimatedSprite sprite = new CoinSprite();
            sprite.Texture = content.Load<Texture2D>("Sprites\\CollectableSprites\\CoinSprite");

            return sprite;
        }

        public static AbstractAnimatedSprite GetCrateSprite()
        {
            AbstractAnimatedSprite sprite = new CrateSprite();
            sprite.Texture = content.Load<Texture2D>("Sprites\\InteractableSprites\\InteractableDesert\\CrateSprite");

            return sprite;
        }

        public static AbstractAnimatedSprite GetBoulderSprite()
        {
            AbstractAnimatedSprite sprite = new BoulderSprite();
            sprite.Texture = content.Load<Texture2D>("Sprites\\InteractableSprites\\InteractableDS\\BoulderSprite");

            return sprite;
        }

        public static AbstractAnimatedSprite GetFishbowlSprite()
        {
            AbstractAnimatedSprite sprite = new FishbowlSprite();
            sprite.Texture = content.Load<Texture2D>("Sprites\\InteractableSprites\\InteractableDesert\\FishbowlSprite");

            return sprite;
        }

        public static AbstractAnimatedSprite GetJoeBotaSprite()
        {
            AbstractAnimatedSprite sprite = new FishbowlSprite();
            sprite.Texture = content.Load<Texture2D>("Sprites\\EnvironmentSprites\\EnvironmentDS\\JoeBota");

            return sprite;
        }

        public static AbstractAnimatedSprite GetButtonSprite()
        {
            AbstractAnimatedSprite sprite = new ButtonSprite();
            sprite.Texture = content.Load<Texture2D>("Sprites\\InteractableSprites\\InteractableDesert\\ButtonSprite");

            return sprite;
        }

        public static AbstractAnimatedSprite GetHortoiseSprite(SpriteOrientation spriteOrientation)
        {
            AbstractAnimatedSprite sprite = new HortoiseSprite(spriteOrientation);
            sprite.Texture = content.Load<Texture2D>("Sprites\\EnemySprites\\EnemyDesert\\HortoiseSprite");

            return sprite;
        }

        public static AbstractAnimatedSprite GetSpiderSprite(SpriteOrientation spriteOrientation)
        {
            AbstractAnimatedSprite sprite = new SpiderSprite(spriteOrientation);
            sprite.Texture = content.Load<Texture2D>("Sprites\\EnemySprites\\EnemyDesert\\SpiderSprite");

            return sprite;
        }

        public static AbstractAnimatedSprite GetCactusSprite()
        {
            AbstractAnimatedSprite sprite = new CactusSprite();
            sprite.Texture = content.Load<Texture2D>("Sprites\\EnemySprites\\EnemyDesert\\CactusSprite");

            return sprite;
        }

        public static AbstractAnimatedSprite GetNeedleSprite(SpriteOrientation spriteOrientation)
        {
            AbstractAnimatedSprite sprite = new NeedleSprite(spriteOrientation);
            sprite.Texture = content.Load<Texture2D>("Sprites\\EnemySprites\\EnemyDesert\\NeedleSprite");

            return sprite;
        }

        public static AbstractAnimatedSprite GetSandstoneSprite()
        {
            AbstractAnimatedSprite sprite = new TerrainSprite();
            sprite.Texture = content.Load<Texture2D>("Sprites\\EnvironmentSprites\\EnvironmentDesert\\SandstoneSprite2");

            return sprite;
        }

        public static AbstractAnimatedSprite GetHospitalSprite()
        {
            AbstractAnimatedSprite sprite = new TerrainSprite();
            sprite.Texture = content.Load<Texture2D>("Sprites\\EnvironmentSprites\\EnvironmentHospital\\HospitalSprite");

            return sprite;
        }

        public static AbstractAnimatedSprite GetBrickSprite()
        {
            AbstractAnimatedSprite sprite = new TerrainSprite();
            sprite.Texture = content.Load<Texture2D>("Sprites\\EnvironmentSprites\\EnvironmentDS\\BrickSprite");

            return sprite;
        }

        public static AbstractAnimatedSprite GetWaterBottleSprite()
        {
            AbstractAnimatedSprite sprite = new WaterBottleSprite();
            sprite.Texture = content.Load<Texture2D>("Sprites\\CollectableSprites\\WaterbottleSprite");

            return sprite;
        }

        public static AbstractAnimatedSprite GetDesertBackgroundSprite()
        {
            AbstractAnimatedSprite sprite = new DesertBackground();
            sprite.Texture = content.Load<Texture2D>("Sprites\\EnvironmentSprites\\EnvironmentDesert\\DesertBackgroundSprite2");

            return sprite;
        }

        public static AbstractAnimatedSprite GetHospitalBackgroundSprite()
        {
            AbstractAnimatedSprite sprite = new HospitalBackground();
            sprite.Texture = content.Load<Texture2D>("Sprites\\EnvironmentSprites\\EnvironmentHospital\\HospitalBackgroundSprite");

            return sprite;
        }

        public static AbstractAnimatedSprite GetDSBackgroundSprite()
        {
            AbstractAnimatedSprite sprite = new DSBackground();
            sprite.Texture = content.Load<Texture2D>("Sprites\\EnvironmentSprites\\EnvironmentDS\\DSBackgroundSprite");

            return sprite;
        }

        public static AbstractAnimatedSprite GetDoorSprite()
        {
            AbstractAnimatedSprite sprite = new DoorSprite();
            sprite.Texture = content.Load<Texture2D>("Sprites\\InteractableSprites\\InteractableDesert\\DoorClosedSprite");

            return sprite;
        }

        public static AbstractAnimatedSprite GetSecretDoorSprite()
        {
            AbstractAnimatedSprite sprite = new SecretLevelDoorSprite();
            sprite.Texture = content.Load<Texture2D>("Sprites\\InteractableSprites\\SecretDoorClosedSprite");

            return sprite;
        }

        public static AbstractAnimatedSprite GetHeartEmptySprite()
        {
            AbstractAnimatedSprite sprite = new HeartEmptySprite();
            sprite.Texture = content.Load<Texture2D>("Sprites\\HUD\\HUDDesert\\HeartEmptySprite");

            return sprite;
        }

        public static AbstractAnimatedSprite GetHeartFullSprite()
        {
            AbstractAnimatedSprite sprite = new HeartFullSprite();
            sprite.Texture = content.Load<Texture2D>("Sprites\\HUD\\HUDDesert\\HeartFullSprite");

            return sprite;
        }

        public static AbstractAnimatedSprite GetWaterMeter100pSprite()
        {
            AbstractAnimatedSprite sprite = new WaterMeter100pSprite();
            sprite.Texture = content.Load<Texture2D>("Sprites\\HUD\\HUDDesert\\HUDBowl100p");

            return sprite;
        }

        public static AbstractAnimatedSprite GetWaterMeter75pSprite()
        {
            AbstractAnimatedSprite sprite = new WaterMeter75pSprite();
            sprite.Texture = content.Load<Texture2D>("Sprites\\HUD\\HUDDesert\\HUDBowl75p");

            return sprite;
        }

        public static AbstractAnimatedSprite GetWaterMeter50pSprite()
        {
            AbstractAnimatedSprite sprite = new WaterMeter50pSprite();
            sprite.Texture = content.Load<Texture2D>("Sprites\\HUD\\HUDDesert\\HUDBowl50p");

            return sprite;
        }

        public static AbstractAnimatedSprite GetWaterMeter25pSprite()
        {
            AbstractAnimatedSprite sprite = new WaterMeter25pSprite();
            sprite.Texture = content.Load<Texture2D>("Sprites\\HUD\\HUDDesert\\HUDBowl25p");

            return sprite;
        }

        public static AbstractAnimatedSprite GetWaterMeter0pSprite()
        {
            AbstractAnimatedSprite sprite = new WaterMeter0pSprite();
            sprite.Texture = content.Load<Texture2D>("Sprites\\HUD\\HUDDesert\\HUDBowl0p");

            return sprite;
        }

        public static AbstractAnimatedSprite GetEstusMeter100pSprite()
        {
            AbstractAnimatedSprite sprite = new WaterMeter100pSprite();
            sprite.Texture = content.Load<Texture2D>("Sprites\\HUD\\HUDDS\\HUDFlask100p");

            return sprite;
        }

        public static AbstractAnimatedSprite GetEstusMeter75pSprite()
        {
            AbstractAnimatedSprite sprite = new WaterMeter75pSprite();
            sprite.Texture = content.Load<Texture2D>("Sprites\\HUD\\HUDDS\\HUDFlask75p");

            return sprite;
        }

        public static AbstractAnimatedSprite GetEstusMeter50pSprite()
        {
            AbstractAnimatedSprite sprite = new WaterMeter50pSprite();
            sprite.Texture = content.Load<Texture2D>("Sprites\\HUD\\HUDDS\\HUDFlask50p");

            return sprite;
        }

        public static AbstractAnimatedSprite GetEstusMeter25pSprite()
        {
            AbstractAnimatedSprite sprite = new WaterMeter25pSprite();
            sprite.Texture = content.Load<Texture2D>("Sprites\\HUD\\HUDDS\\HUDFlask25p");

            return sprite;
        }

        public static AbstractAnimatedSprite GetEstusMeter0pSprite()
        {
            AbstractAnimatedSprite sprite = new WaterMeter0pSprite();
            sprite.Texture = content.Load<Texture2D>("Sprites\\HUD\\HUDDS\\HUDFlask0p");

            return sprite;
        }

        public static AbstractAnimatedSprite GetPowerUpViewerSprite()
        {
            AbstractAnimatedSprite sprite = new PowerUpViewerSprite();
            sprite.Texture = content.Load<Texture2D>("Sprites\\HUD\\HUDDesert\\HUDPowerUpContainer");

            return sprite;
        }

        public static AbstractAnimatedSprite GetStartMenuSprite()
        {
            AbstractAnimatedSprite sprite = new StartMenuSprite();
            sprite.Texture = content.Load<Texture2D>("Sprites\\Start Menu\\StartMenu");

            return sprite;
        }

        public static AbstractAnimatedSprite GetLevelMenuSprite()
        {
            AbstractAnimatedSprite sprite = new LevelMenuSprite();
            sprite.Texture = content.Load<Texture2D>("Sprites\\Start Menu\\LevelMenu");

            return sprite;
        }

        public static AbstractAnimatedSprite GetGolemEnemySprite()
        {
            AbstractAnimatedSprite sprite = new GolemEnemySprite();
            sprite.Texture = content.Load<Texture2D>("Sprites\\EnemySprites\\GolemEnemyIdle");

            return sprite;
        }

        public static AbstractAnimatedSprite GetLavaSprite()
        {
            AbstractAnimatedSprite sprite = new LavaSprite();
            sprite.Texture = content.Load<Texture2D>("Sprites\\EnvironmentSprites\\EnvironmentDesert\\LavaSprite");

            return sprite;
        }

        public static ConveyorSprite GetConveyorSprite(int blockWidth, SpriteOrientation orientation, float rotationSpeed)
        {
            ConveyorSprite sprite = new ConveyorSprite(blockWidth, orientation, rotationSpeed);
            sprite.SetLeftTexture(content.Load<Texture2D>("Sprites\\InteractableSprites\\InteractableDesert\\ConveyorLeft"));
            sprite.SetMiddleTexture(content.Load<Texture2D>("Sprites\\InteractableSprites\\InteractableDesert\\ConveyorMiddle"));
            sprite.SetRightTexture(content.Load<Texture2D>("Sprites\\InteractableSprites\\InteractableDesert\\ConveyorRight"));
            sprite.PrepareConveyor();

            return sprite;
        }

        public static AbstractAnimatedSprite GetTeleporterSprite()
        {
            AbstractAnimatedSprite sprite = new TeleporterSprite();
            sprite.Texture = content.Load<Texture2D>("Sprites\\InteractableSprites\\InteractableDesert\\Teleporter");

            return sprite;
        }

        public static AbstractAnimatedSprite GetTimeStopSprite()
        {
            AbstractAnimatedSprite sprite = new TimeStopSprite();
            sprite.Texture = content.Load<Texture2D>("Sprites\\CollectableSprites\\TimestopSprite");

            return sprite;
        }

        public static AbstractAnimatedSprite GetHealthBottleSprite()
        {
            AbstractAnimatedSprite sprite = new HealthBottleSprite();
            sprite.Texture = content.Load<Texture2D>("Sprites\\CollectableSprites\\HealthBottleSprite");

            return sprite;
        }

        public static AbstractAnimatedSprite GetSpikedTerrain(SpriteOrientation orientation)
        {
            AbstractAnimatedSprite sprite = new SpikedTerrainSprite(orientation);
            sprite.Texture = content.Load<Texture2D>("Sprites\\EnvironmentSprites\\EnvironmentDesert\\SpikedTerrain");

            return sprite;
        }

        public static AbstractAnimatedSprite GetBonfireSprite()
        {
            AbstractAnimatedSprite sprite = new BonfireSprite();
            sprite.Texture = content.Load<Texture2D>("Sprites\\CollectableSprites\\BonfireSprite");

            return sprite;
        }

        public static SpriteFont GetScoreSpriteFont()
        {
            SpriteFont sprite;
            sprite = content.Load<SpriteFont>("Sprites\\HUD\\HUDDesert\\ScoreFont");

            return sprite;
        }

        public static SpriteFont GetDeathSpriteFont()
        {
            SpriteFont sprite;
            sprite = content.Load<SpriteFont>("Sprites\\HUD\\HUDDesert\\DeathFont");

            return sprite;
        }

        public static SpriteFont GetLightSpriteFont()
        {
            SpriteFont sprite;
            sprite = content.Load<SpriteFont>("Sprites\\HUD\\HUDDS\\LightFont");

            return sprite;
        }

        public static SpriteFont GetSoulsSpriteFont()
        {
            SpriteFont sprite;
            sprite = content.Load<SpriteFont>("Sprites\\HUD\\HUDDS\\SoulsFont");

            return sprite;
        }

        private static string ReturnCorrectResourceName(int type, bool isCharacterHoldingAnItem, string action)
        {
            string initialString = "Sprites\\CharacterSprites\\";

            string folderName = "";
            string suffix = "";

            switch (type)
            {
                case 1: folderName = "CharacterImpl\\"; suffix = ""; break;
                case 2: folderName = "CHARACTER_TYPE_2\\"; suffix = "DS";  break;
                case 3: folderName = "CharacterSMB\\"; suffix = "SMB"; break;
                default: folderName = "CharacterImpl\\"; suffix = ""; break;
            }

            string isCarrying = isCharacterHoldingAnItem ? "CharacterSpriteCarry" : "CharacterSprite";

            initialString += folderName + isCarrying + action + suffix;

            return initialString;
        }
    }
}
