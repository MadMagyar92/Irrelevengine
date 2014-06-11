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
    public class LevelParser
    {
        private string fileName;
        public List<AbstractTerrain> TerrainList;
        public List<ICollectable> CollectablesList;
        public List<IItem> ItemsList;
        public List<IEnemy> EnemiesList;
        public List<AbstractCharacter> CharactersList;
        public List<Camera> CamerasList;
        public List<ItemSet> ItemSetList;
        public AbstractAnimatedSprite Background;
        public SoundEffectInstance Music;

        private List<IController> controllers;
        private Dictionary<string, object> typeMap;

        public LevelParser(string fileName, List<IController> controllers)
        {
            this.fileName = fileName;
            this.TerrainList = new List<AbstractTerrain>();
            this.CollectablesList = new List<ICollectable>();
            this.ItemsList = new List<IItem>();
            this.EnemiesList = new List<IEnemy>();
            this.CharactersList = new List<AbstractCharacter>();
            this.CamerasList = new List<Camera>();
            this.ItemSetList = new List<ItemSet>();
            this.controllers = controllers;
        }

        public void Parse()
        {
            XElement root = XElement.Load(fileName);

            IEnumerable<XElement> terrain = from el in root.Elements(GenericParser.terrainString) select el;
            IEnumerable<XElement> collectable = from el in root.Elements(GenericParser.collectableString) select el;
            IEnumerable<XElement> item = from el in root.Elements(GenericParser.itemString) select el;
            IEnumerable<XElement> enemy = from el in root.Elements(GenericParser.enemyString) select el;
            IEnumerable<XElement> character = from el in root.Elements(GenericParser.characterString) select el;
            IEnumerable<XElement> camera = from el in root.Elements(GenericParser.cameraString) select el;
            IEnumerable<XElement> properties = from el in root.Elements(GenericParser.propertiesString) select el;
            IEnumerable<XElement> itemSets = from el in root.Elements(GenericParser.itemSetString) select el;
            
            IEnumerable<XElement> terrains = terrain.Elements<XElement>();
            IEnumerable<XElement> collectables = collectable.Elements<XElement>();
            IEnumerable<XElement> items = item.Elements<XElement>();
            IEnumerable<XElement> enemies = enemy.Elements<XElement>();
            IEnumerable<XElement> characters = character.Elements<XElement>();
            IEnumerable<XElement> cameras = camera.Elements<XElement>();
            IEnumerable<XElement> propertyElements = properties.Elements<XElement>();
            IEnumerable<XElement> itemSetsElements = itemSets.Elements<XElement>();

            ICollectableParser collectableParser = new ICollectableParser();
            collectableParser.Parse(collectables);
            CollectablesList = collectableParser.CollectablesList;

            IItemParser itemParser = new IItemParser();
            itemParser.Parse(items);
            ItemsList = itemParser.ItemList;

            IEnemyParser enemyParser = new IEnemyParser();
            enemyParser.Parse(enemies);
            EnemiesList = enemyParser.EnemiesList;

            CharacterParser characterParser = new CharacterParser();
            characterParser.Parse(characters, controllers);
            CharactersList = characterParser.CharacterList;

            CameraParser cameraParser = new CameraParser();
            cameraParser.Parse(cameras, CharactersList);
            CamerasList = cameraParser.CameraList;

            ItemSetParser itemSetParser = new ItemSetParser();
            itemSetParser.Parse(itemSetsElements, controllers);
            ItemSetList = itemSetParser.ItemSetList;

            TerrainParser terrainParser = new TerrainParser();
            terrainParser.Parse(terrains);
            TerrainList = terrainParser.TerrainList;

            PropertiesParser propertiesParser = new PropertiesParser();
            propertiesParser.Parse(propertyElements);
            Music = propertiesParser.Music;
            Background = propertiesParser.Background;
        }
    }
}
