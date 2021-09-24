using System.Reflection.Metadata.Ecma335;
using System.Net.Mime;
using System;
using System.Collections.Generic;
using System.IO;
using System.Numerics;
using System.Xml.Serialization;
using Raylib_cs;
using Takeover.Components;
using Takeover.Entities;
using Takeover.Enums;

namespace Takeover.Systems
{
    public class LevelGeneratorSystem : Systems.System
    {
        private Texture2D TowerTexture { get; set; }
        private Texture2D PlayerTexture { get; set; }
        public LevelGeneratorSystem()
        {
            TowerTexture = Raylib.LoadTexture("Assets/tower.png");
            PlayerTexture = Raylib.LoadTexture("Assets/player.png");
        }
        public override void UpdateAll(List<Entity> entities, GameEngine engine)
        {
            var singleton = entities.Find(x => x.GetComponentByType<Singleton>() != null);
            var data = singleton.GetComponentByType<Singleton>();
            if (data.State == Enums.GameStates.InProgress)
            {
                var toAdd = new List<Entity>();

                if (data.WorldGenerated)
                {
                    return;
                }

                if (!string.IsNullOrEmpty(data.CampaignLevel))
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(Campaign));
                    var xml = File.ReadAllText("Data/Campaign.xml");

                    using (StringReader reader = new StringReader(xml))
                    {
                        var campaign = (Campaign)serializer.Deserialize(reader);
                        var level = campaign.Level.Find(x => x.Index == int.Parse(data.CampaignLevel));
                        foreach (var node in level.Nodes.Node)
                        {
                            var faction = Enums.Factions.Neutral;
                            switch (node.Owner)
                            {
                                case "Player":
                                    faction = Factions.Player;
                                    break;
                                case "AI":
                                    faction = Factions.AI;
                                    break;
                            }
                            toAdd.AddRange(GenerateNodes(faction, new Vector2(node.X, node.Y)));
                        }
                    }
                }
                else
                {
                    toAdd.AddRange(GenerateRandomNodes(Factions.AI, 2));
                    toAdd.AddRange(GenerateRandomNodes(Factions.Neutral, 10));
                    //toAdd.AddRange(GenerateRandomNodes(Factions.Player, 2));
                    var player = new Entity();

                    player.Components.Add(new Render(50, 50) { Texture = PlayerTexture });
                    player.Components.Add(new Controllable());
                    toAdd.Add(player);
                }
                engine.Entities.AddRange(toAdd);
                data.WorldGenerated = true;
            }
        }

        private IEnumerable<Entity> GenerateRandomNodes(Factions faction, int count = 1)
        {
            var toAdd = new List<Entity>();
            var width = Raylib.GetScreenWidth();
            var height = Raylib.GetScreenHeight();
            var random = new Random();

            for (var i = 0; i < count; i++)
            {
                var node = new Entity();

                var render = new Render(random.Next(60, width - 60), random.Next(60, height - 60));
                render.Texture = TowerTexture;
                node.Components.Add(render);
                node.Components.Add(new Selectable());
                node.Components.Add(new Target());
                node.Components.Add(new Health());
                node.Components.Add(new Allegiance(faction));

                toAdd.Add(node);
            }
            return toAdd;
        }

        private IEnumerable<Entity> GenerateNodes(Factions faction, Vector2 position)
        {
            var toAdd = new List<Entity>();

            var render = new Render(position);
            render.Texture = TowerTexture;

            var node = new Entity();
            node.Components.Add(render);
            node.Components.Add(new Selectable());
            node.Components.Add(new Target());
            node.Components.Add(new Health());
            node.Components.Add(new Allegiance(faction));

            toAdd.Add(node);

            return toAdd;
        }
    }

    // using System.Xml.Serialization;
    // XmlSerializer serializer = new XmlSerializer(typeof(Campaign));
    // using (StringReader reader = new StringReader(xml))
    // {
    //    var test = (Campaign)serializer.Deserialize(reader);
    // }

    [XmlRoot(ElementName = "Node")]
    public class Node
    {

        [XmlAttribute(AttributeName = "x")]
        public int X { get; set; }

        [XmlAttribute(AttributeName = "y")]
        public int Y { get; set; }

        [XmlAttribute(AttributeName = "owner")]
        public string Owner { get; set; }
    }

    [XmlRoot(ElementName = "Nodes")]
    public class Nodes
    {

        [XmlElement(ElementName = "Node")]
        public List<Node> Node { get; set; }
    }

    [XmlRoot(ElementName = "Level")]
    public class Level
    {

        [XmlElement(ElementName = "Nodes")]
        public Nodes Nodes { get; set; }

        [XmlAttribute(AttributeName = "index")]
        public int Index { get; set; }
    }

    [XmlRoot(ElementName = "Campaign")]
    public class Campaign
    {

        [XmlElement(ElementName = "Level")]
        public List<Level> Level { get; set; }

        [XmlAttribute(AttributeName = "version")]
        public string Version { get; set; }
    }




}