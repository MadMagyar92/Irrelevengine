using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace Completely_Irrelevant
{
    public class ItemSetParser
    {
        public List<ItemSet> ItemSetList;

        private Dictionary<string, object> typeMap;

        public ItemSetParser()
        {
            ItemSetList = new List<ItemSet>();
            typeMap = new Dictionary<string, object>();
            typeMap.Add(GenericParser.basicItemSetString, new BasicItemSet(InvokingMode.Single));
        }

        public void Parse(IEnumerable<XElement> itemSets, List<IController> controllers)
        {
            foreach (XElement element in itemSets)
            {
                string key = (string)element.Attribute("name");
                string invokingMode = (string)element.Attribute(GenericParser.invokingModeString);
                object obj = typeMap[key];

                if (obj is BasicItemSet)
                {
                    ParseBasicItemSet(element, invokingMode, controllers);
                }
            }
        }

        private void ParseBasicItemSet(XElement element, string invokingMode, List<IController> controllers)
        {
            IEnumerable<XElement> sender = from el in element.Elements(GenericParser.sendersString) select el;
            IEnumerable<XElement> receiver = from el in element.Elements(GenericParser.receiversString) select el;

            List<ISender> senders = new List<ISender>();
            List<IReceiver> receivers = new List<IReceiver>();

            ParseIReceivers(receivers, receiver, controllers);
            ParseISenders(senders, sender, controllers);

            if (receivers.Count > 0 && senders.Count > 0)
            {
                InvokingMode mode = InvokingMode.Single;

                if (invokingMode.Equals(GenericParser.invokingModeSingle))
                {
                    mode = InvokingMode.Single;
                }
                else if (invokingMode.Equals(GenericParser.invokingModeMultiple))
                {
                    mode = InvokingMode.Multiple;
                }

                BasicItemSet itemSet = new BasicItemSet(mode, senders, receivers);
                ItemSetList.Add(itemSet);
            }
        }

        private void ParseIReceivers(List<IReceiver> receivers, IEnumerable<XElement> receiver, List<IController> controllers)
        {
            IEnumerable<XElement> charactersSenderList = from el in receiver.Elements() where IsCharacter(el) select el;
            IEnumerable<XElement> terrainsSenderList = from el in receiver.Elements() where IsTerrain(el) select el;
            IEnumerable<XElement> collectablesSenderList = from el in receiver.Elements() where IsCollectable(el) select el;
            IEnumerable<XElement> itemsSenderList = from el in receiver.Elements() where IsItem(el) select el;
            IEnumerable<XElement> enemiesSenderList = from el in receiver.Elements() where IsEnemy(el) select el;

            List<AbstractCharacter> characterSenderList = new List<AbstractCharacter>();
            List<AbstractTerrain> terrainSenderList = new List<AbstractTerrain>();
            List<ICollectable> collectableSenderList = new List<ICollectable>();
            List<IItem> itemSenderList = new List<IItem>();
            List<IEnemy> enemySenderList = new List<IEnemy>();

            CharacterParser characterParser = new CharacterParser();
            characterParser.Parse(charactersSenderList, controllers);
            characterSenderList = characterParser.CharacterList;

            TerrainParser terrainParser = new TerrainParser();
            terrainParser.Parse(terrainsSenderList);
            terrainSenderList = terrainParser.TerrainList;

            ICollectableParser collectableParser = new ICollectableParser();
            collectableParser.Parse(collectablesSenderList);
            collectableSenderList = collectableParser.CollectablesList;

            IItemParser itemParser = new IItemParser();
            itemParser.Parse(itemsSenderList);
            itemSenderList = itemParser.ItemList;

            IEnemyParser enemyParser = new IEnemyParser();
            enemyParser.Parse(enemiesSenderList);
            enemySenderList = enemyParser.EnemiesList;

            foreach (AbstractCharacter c in characterSenderList)
            {
                receivers.Add(c);
            }

            foreach (AbstractTerrain t in terrainSenderList)
            {
                receivers.Add(t);
            }

            foreach (ICollectable i in collectableSenderList)
            {
                receivers.Add(i);
            }

            foreach (IItem i in itemSenderList)
            {
                receivers.Add(i);
            }

            foreach (IEnemy i in enemySenderList)
            {
                receivers.Add(i);
            }
        }

        private void ParseISenders(List<ISender> senders, IEnumerable<XElement> sender, List<IController> controllers)
        {
            IEnumerable<XElement> charactersSenderList = from el in sender.Elements() where IsCharacter(el) select el;
            IEnumerable<XElement> terrainsSenderList = from el in sender.Elements() where IsTerrain(el) select el;
            IEnumerable<XElement> collectablesSenderList = from el in sender.Elements() where IsCollectable(el) select el;
            IEnumerable<XElement> itemsSenderList = from el in sender.Elements() where IsItem(el) select el;
            IEnumerable<XElement> enemiesSenderList = from el in sender.Elements() where IsEnemy(el) select el;

            List<AbstractCharacter> characterSenderList = new List<AbstractCharacter>();
            List<AbstractTerrain> terrainSenderList = new List<AbstractTerrain>();
            List<ICollectable> collectableSenderList = new List<ICollectable>();
            List<IItem> itemSenderList = new List<IItem>();
            List<IEnemy> enemySenderList = new List<IEnemy>();

            CharacterParser characterParser = new CharacterParser();
            characterParser.Parse(charactersSenderList, controllers);
            characterSenderList = characterParser.CharacterList;

            TerrainParser terrainParser = new TerrainParser();
            terrainParser.Parse(terrainsSenderList);
            terrainSenderList = terrainParser.TerrainList;

            ICollectableParser collectableParser = new ICollectableParser();
            collectableParser.Parse(collectablesSenderList);
            collectableSenderList = collectableParser.CollectablesList;

            IItemParser itemParser = new IItemParser();
            itemParser.Parse(itemsSenderList);
            itemSenderList = itemParser.ItemList;

            IEnemyParser enemyParser = new IEnemyParser();
            enemyParser.Parse(enemiesSenderList);
            enemySenderList = enemyParser.EnemiesList;

            foreach (AbstractCharacter c in characterSenderList)
            {
                senders.Add(c);
            }

            foreach (AbstractTerrain t in terrainSenderList)
            {
                senders.Add(t);
            }

            foreach (ICollectable i in collectableSenderList)
            {
                senders.Add(i);
            }

            foreach (IItem i in itemSenderList)
            {
                senders.Add(i);
            }

            foreach (IEnemy i in enemySenderList)
            {
                senders.Add(i);
            }
        }

        private bool IsItem(XElement element)
        {
            string name = element.Name.ToString();
            return name.Equals(GenericParser.button) || name.Equals(GenericParser.continuousbutton) || name.Equals(GenericParser.crate) || name.Equals(GenericParser.door) || name.Equals(GenericParser.fishbowl) || name.Equals(GenericParser.teleporter) || name.Equals(GenericParser.falsebrick) || name.Equals(GenericParser.conveyor);
        }

        private bool IsCharacter(XElement element)
        {
            string name = element.Name.ToString();
            return name.Equals(GenericParser.characterString);
        }

        private bool IsTerrain(XElement element)
        {
            string name = element.Name.ToString();
            return name.Equals(GenericParser.terrainString);
        }

        private bool IsCollectable(XElement element)
        {
            string name = element.Name.ToString();
            return name.Equals(GenericParser.bottle) || name.Equals(GenericParser.bubble) || name.Equals(GenericParser.star) || name.Equals(GenericParser.sunscreen);
        }

        private bool IsEnemy(XElement element)
        {
            string name = element.Name.ToString();
            return name.Equals(GenericParser.spider) || name.Equals(GenericParser.hortoise) || name.Equals(GenericParser.cactus);
        }

    }
}
