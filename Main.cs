using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SRML;
using SRML.SR;
using SRML.Utils.Enum;
using SimpleSRmodLibrary.Creation;
using UnityEngine;
using AssetsLib;
using LargoLibrary;
using SRML.SR.Templates.Identifiables;

namespace ElementSlime
{
    
    [EnumHolder]
    public static class Ids
    {
        public static readonly Identifiable.Id ICE_PLORT;
        public static readonly Identifiable.Id ICE_SLIME;
        public static readonly Identifiable.Id GRASS_PLORT;
        public static readonly Identifiable.Id GRASS_SLIME;
        public static readonly Identifiable.Id LIGHTNING_PLORT;
        public static readonly Identifiable.Id LIGHTNING_SLIME;
        public static readonly Identifiable.Id METAL_PLORT;
        public static readonly Identifiable.Id METAL_SLIME;
        public static readonly Identifiable.Id WOOD_PLORT;
        public static readonly Identifiable.Id WOOD_SLIME;
        public static readonly Identifiable.Id NONE_SLIME;
        public static readonly Identifiable.Id ELEMENT_FRUIT;
        public static readonly SpawnResource.Id ELEMENT_TREE;
        public static readonly SpawnResource.Id ELEMENT_TREE_DLX;
        public static readonly Identifiable.Id ELEMENT_DIAMON_CRAFT;
        public static readonly PediaDirector.Id PEDIA_ELEMENT_SLIME;
        public static readonly PediaDirector.Id PEDIA_ELEMENT_FRUIT;
        public static readonly PediaDirector.Id PEDIA_NONE_SLIME;
    }

    public class Main : ModEntryPoint
    {
        public override void PreLoad()
        {
            PlortCreation.PlortPreLoad(Ids.ICE_PLORT, "雪花结晶", false);
            PlortCreation.PlortPreLoad(Ids.GRASS_PLORT, "青草结晶", false);
            PlortCreation.PlortPreLoad(Ids.LIGHTNING_PLORT, "闪电结晶", false);
            PlortCreation.PlortPreLoad(Ids.METAL_PLORT, "金属结晶", false);
            PlortCreation.PlortPreLoad(Ids.WOOD_PLORT, "枯木结晶", false);
            SpawnCreation.CreateEveryZoneSpawner(Ids.ICE_SLIME, 0.4f);
            SpawnCreation.CreateSingleZoneSpawner(Ids.GRASS_SLIME, ZoneDirector.Zone.MOSS, 0.09f);
            SpawnCreation.CreateEveryZoneSpawner(Ids.LIGHTNING_SLIME, 0.07f);
            SpawnCreation.CreateSingleZoneSpawner(Ids.METAL_SLIME, ZoneDirector.Zone.QUARRY, 0.04f);
            SpawnCreation.CreateDualZoneSpawner(Ids.WOOD_SLIME, ZoneDirector.Zone.MOSS, ZoneDirector.Zone.DESERT, 0.3f);
            SpawnCreation.CreateEveryZoneSpawner(Ids.NONE_SLIME, 0.5f);
            SpawnCreation.CreateEveryZoneSpawner(Identifiable.Id.QUANTUM_SLIME, 9.9f);


            HarmonyInstance.PatchAll();
        }
        public override void Load()
        {
            SlimeAppearenceSSRML.SpeckleInstructions speckleInstructions = default(SlimeAppearenceSSRML.SpeckleInstructions);
            speckleInstructions.active = false;
            speckleInstructions.addB = 0f;
            speckleInstructions.addR = 0f;
            speckleInstructions.addG = 0f;
            speckleInstructions.applyTo = "_None";
            SlimeAppearenceSSRML.SpeckleInstructions speckleInstructions2 = speckleInstructions;
            SlimeAppearenceSSRML.GlowInstructions glowInstructions = default(SlimeAppearenceSSRML.GlowInstructions);
            glowInstructions.color = Color.clear;
            glowInstructions.max = new float[1];
            glowInstructions.min = new float[1];
            SlimeAppearenceSSRML.GlowInstructions glowInstructions2 = glowInstructions;
            var ElementFruit = CropCreation.CreateCropNoFModel(
                Ids.ELEMENT_FRUIT, "元素果", 
                Identifiable.Id.MANGO_FRUIT,
                CropCreation.CreateCropMaterial(
                    Identifiable.Id.MANGO_FRUIT, 
                    TextureUtils.LoadImage("FuitHeadRamp.png"),
                    TextureUtils.LoadImage("FuitLeafRamp.png"), 
                    TextureUtils.LoadImage("FruitColor.png"),
                    TextureUtils.LoadImage("FruitColor.png")), new Vector3(0.3f, 0.3f, 0.3f),
                new Vector3(0.3f, 0.3f, 0.3f),false);
            ElementFruit.GetComponentInChildren<ResourceCycle>().rottenMat = CropCreation.CreateCropMaterial(
                Identifiable.Id.MANGO_FRUIT, 
                TextureUtils.LoadImage("FuitHeadRamp.png"),
                TextureUtils.LoadImage("FuitHeadRamp.png"),
                TextureUtils.LoadImage("FruitColor.png"), 
                TextureUtils.LoadImage("FruitColor.png"));
            CropCreation.LoadCrop(Ids.ELEMENT_FRUIT, ElementFruit, false, false, true, false);
            SlimePediaCreation.LoadSlimePediaIcon(Ids.PEDIA_ELEMENT_FRUIT,
                TextureUtils.LoadImage("ElementFruit.png").CreateSprite());
            SlimePediaCreation.PreLoadSlimePediaConnection(
                Ids.PEDIA_ELEMENT_FRUIT,
                Ids.ELEMENT_FRUIT,PediaRegistry.PediaCategory.RESOURCES);
            SlimePediaCreation.CreateSlimePediaForItemWithName(
                Ids.PEDIA_ELEMENT_FRUIT,
                Ids.ELEMENT_FRUIT, 
                "元素果", 
                "吸收天地元素而形成的果子", 
                "水果", 
                "元素史莱姆",
                "对人类来说，它的味道特别难吃，但对于不同归属元素的史莱姆来说尝起来味道也不一样呢!",
                "可以在园地里种植");
            var ElementTree = CropCreation.CropFruitFarmSetup(
                SpawnResource.Id.MANGO_TREE, new Vector3(0f, 0f), 
                "ElementTree", 
                Ids.ELEMENT_TREE, 
                Ids.ELEMENT_FRUIT,
                Ids.ELEMENT_FRUIT, 
                30f, 25f, 10f, 5f, 0f, 0,
                Ids.ELEMENT_FRUIT, 
                Ids.ELEMENT_FRUIT);
            var ElementTreeDLX = CropCreation.CropFruitFarmSetup(
                SpawnResource.Id.MANGO_TREE_DLX, new Vector3(0f, 0f), 
                "ElementTreeDLX", 
                Ids.ELEMENT_TREE_DLX,
                Ids.ELEMENT_FRUIT,
                Ids.ELEMENT_FRUIT, 
                40f, 35f, 8f, 4f, 0f, 0,
                Ids.ELEMENT_FRUIT,
                Ids.ELEMENT_FRUIT);
            CropCreation.LoadFarmSetup(Ids.ELEMENT_FRUIT, ElementTree, ElementTreeDLX);
            TranslationPatcher.AddPediaTranslation("t.pedia_element_fruit", "元素果");
            VacItemCreation.NewVacItem(
                Vacuumable.Size.NORMAL,
                ElementFruit,
                Ids.ELEMENT_FRUIT, 
                "元素果", 
                TextureUtils.LoadImage("ElementFruit.png").CreateSprite(),
                Color.cyan);

            var IceTopColor = new Color32(193, 213, 255, 255);
            var SlimeFaceColor = new Color32(17, 29, 55, 255);
            var IcePlort = PlortCreation.CreatePlort(
                "雪花结晶", Ids.ICE_PLORT,
                Vacuumable.Size.NORMAL,
                new Color32(181, 225, 255, 255),
                new Color32(193, 213, 255, 255),
                new Color32(181, 225, 255, 255));
            var GrassPlort = PlortCreation.CreatePlort(
                "青草结晶", Ids.GRASS_PLORT,
                Vacuumable.Size.NORMAL,
                new Color32(0, 158, 0, 255),
                new Color32(7, 243, 127, 255),
                new Color32(0, 151, 76, 255)         
                );
            var LightningPlort = PlortCreation.CreatePlort(
                "闪电结晶", Ids.LIGHTNING_PLORT,
                Vacuumable.Size.NORMAL,
                new Color32(255, 209, 0, 255),
                new Color32(255, 209, 0, 255),
                new Color32(255, 149, 42, 255),
                Identifiable.Id.BOOM_PLORT
                );
            var MetalPlort = PlortCreation.CreatePlort(
                "金属结晶", Ids.METAL_PLORT,
                Vacuumable.Size.NORMAL,
                new Color32(220, 220, 220, 255),
                new Color32(180, 180, 180, 255),
                new Color32(115, 115, 115, 255)
                );
            var WoodPlort = PlortCreation.CreatePlort(
                "枯木结晶", Ids.WOOD_PLORT,
                Vacuumable.Size.NORMAL,
                new Color32(200, 73, 0, 255),
                new Color32(206, 129, 0, 255),
                new Color32(152, 63, 0, 255)
                );
            PlortCreation.PlortLoad(
                Ids.ICE_PLORT,
                60f,15f,
                IcePlort,
                TextureUtils.LoadImage("IcePlort.png").CreateSprite(),
                new Color32(181, 225, 255, 255),
                true,true,false
                );
            PlortCreation.PlortLoad(
                Ids.GRASS_PLORT,
                100f, 20f,
                GrassPlort,
                TextureUtils.LoadImage("GrassPlort.png").CreateSprite(),
                new Color32(0, 158, 0, 255),
                true, true, false
                );
            PlortCreation.PlortLoad(
                Ids.LIGHTNING_PLORT,
                90f, 30f,
                LightningPlort,
                TextureUtils.LoadImage("LightningPlort.png").CreateSprite(),
                new Color32(255, 209, 0, 255),
                true, true, false
                );
            PlortCreation.PlortLoad(
                Ids.METAL_PLORT,
                300f, 80f,
                MetalPlort,
                TextureUtils.LoadImage("MetalPlort.png").CreateSprite(),
                new Color32(220, 220, 220, 255),
                true, true, false
                );
            PlortCreation.PlortLoad(
                Ids.WOOD_PLORT,
                70f, 10f,
                WoodPlort,
                TextureUtils.LoadImage("WoodPlort.png").CreateSprite(),
                new Color32(206, 129, 0, 255),
                true, true, false
                );
            var IceSlime = SlimeCreation.SlimeBaseCreate(
                Ids.ICE_SLIME,
                "ice_slime",
                "雪花史莱姆",
                "slimeIce",
                "雪花史莱姆",
                Identifiable.Id.PINK_SLIME,
                Identifiable.Id.PINK_SLIME,
                Identifiable.Id.PINK_SLIME,
                Identifiable.Id.PINK_SLIME,
                SlimeEat.FoodGroup.FRUIT,
                Ids.ELEMENT_FRUIT,
                Identifiable.Id.DEEP_BRINE_CRAFT,
                Identifiable.Id.RUBBER_DUCKY_TOY,
                Ids.ICE_PLORT,
                false,
                TextureUtils.LoadImage("IceSlime.png").CreateSprite(),
                Vacuumable.Size.NORMAL,
                true,0.1f,0.1f,
                new Color32(193, 213, 255, 255), new Color32(181, 225, 255, 255),
                new Color32(181, 225, 255, 255), new Color32(181, 225, 255, 255),
                SlimeFaceColor, SlimeFaceColor,
                SlimeFaceColor, SlimeFaceColor,
                SlimeFaceColor, SlimeFaceColor, 
                new Color32(181, 225, 255, 255), new Color32(181, 225, 255, 255),
                new Color32(181, 225, 255, 255), new Color32(181, 225, 255, 255)

                );

            var GrassSlime = SlimeCreation.SlimeBaseCreate(
                Ids.GRASS_SLIME,
                "grass_slime",
                "青草史莱姆",
                "slimeGrass",
                "青草史莱姆",
                Identifiable.Id.TANGLE_SLIME,
                Identifiable.Id.TANGLE_SLIME,
                Identifiable.Id.TANGLE_SLIME,
                Identifiable.Id.TANGLE_SLIME,
                SlimeEat.FoodGroup.FRUIT,
                Ids.ELEMENT_FRUIT,
                Identifiable.Id.DEEP_BRINE_CRAFT,
                Identifiable.Id.SOL_MATE_TOY,
                Ids.GRASS_PLORT,
                false,
                TextureUtils.LoadImage("GrassSlime.png").CreateSprite(),
                Vacuumable.Size.NORMAL,
                true, 0.1f, 0.1f,
                new Color32(0, 103, 0, 255), new Color32(0,252, 97, 255),
                new Color32(0, 159, 0, 255), new Color32(0, 103, 0, 255),
                SlimeFaceColor, SlimeFaceColor,
                SlimeFaceColor, SlimeFaceColor,
                SlimeFaceColor, SlimeFaceColor,
                new Color32(0, 103, 0, 255), new Color32(0, 103, 0, 255),
                new Color32(0, 103, 0, 255), new Color32(0, 103, 0, 255)

                );
            var LightningSlime = SlimeCreation.SlimeBaseCreate(
                Ids.LIGHTNING_SLIME,
                "lightning_slime",
                "闪电史莱姆",
                "slimeLightning",
                "闪电史莱姆",
                Identifiable.Id.PINK_SLIME,
                Identifiable.Id.PINK_SLIME,
                Identifiable.Id.BOOM_SLIME,
                Identifiable.Id.BOOM_SLIME,
                SlimeEat.FoodGroup.FRUIT,
                Ids.ELEMENT_FRUIT,
                Identifiable.Id.DEEP_BRINE_CRAFT,
                Identifiable.Id.BOMB_BALL_TOY,
                Ids.LIGHTNING_PLORT,
                false,
                TextureUtils.LoadImage("LightningSlime.png").CreateSprite(),
                Vacuumable.Size.NORMAL,
                true, 0.1f, 0.1f,
                new Color32(255, 112, 0, 255), new Color32(255, 165, 0, 255),
                new Color32(238, 255, 0, 255), new Color32(238, 255, 0, 255),
                SlimeFaceColor, SlimeFaceColor,
                SlimeFaceColor, SlimeFaceColor,
                SlimeFaceColor, SlimeFaceColor,
                new Color32(255, 165, 0, 255), new Color32(255, 165, 0, 255),
                new Color32(255, 165, 0, 255), new Color32(255, 165, 0, 255)

                );
            var MetalSlime = SlimeCreation.SlimeBaseCreate(
                Ids.METAL_SLIME,
                "metal_slime",
                "金属史莱姆",
                "slimeMetal",
                "金属史莱姆",
                Identifiable.Id.PINK_SLIME,
                Identifiable.Id.PINK_SLIME,
                Identifiable.Id.GOLD_SLIME,
                Identifiable.Id.PINK_SLIME,
                SlimeEat.FoodGroup.FRUIT,
                Ids.ELEMENT_FRUIT,
                Identifiable.Id.DEEP_BRINE_CRAFT,
                Identifiable.Id.CRYSTAL_BALL_TOY,
                Ids.METAL_PLORT,
                false,
                TextureUtils.LoadImage("MetalSlime.png").CreateSprite(),
                Vacuumable.Size.NORMAL,
                true, 0.1f, 0.1f,
                new Color32(194, 194, 194, 255), new Color32(194, 194, 194, 255),
                new Color32(125, 125, 125, 255), new Color32(125, 125, 125, 255),
                Color.white, Color.white,
                Color.white, Color.white,
                Color.white, Color.white,
                new Color32(125, 125, 125, 255), new Color32(125, 125, 125, 255),
                new Color32(125, 125, 125, 255), new Color32(125, 125, 125, 255)
                );
            var WoodSlime = SlimeCreation.SlimeBaseCreate(
                Ids.WOOD_SLIME,
                "wood_slime",
                "枯木史莱姆",
                "slimeWood",
                "枯木史莱姆",
                Identifiable.Id.PINK_SLIME,
                Identifiable.Id.PINK_SLIME,
                Identifiable.Id.PINK_SLIME,
                Identifiable.Id.PINK_SLIME,
                SlimeEat.FoodGroup.FRUIT,
                Ids.ELEMENT_FRUIT,
                Identifiable.Id.DEEP_BRINE_CRAFT,
                Identifiable.Id.BUZZY_BEE_TOY,
                Ids.WOOD_PLORT,
                false,
                TextureUtils.LoadImage("WoodSlime.png").CreateSprite(),
                Vacuumable.Size.NORMAL,
                true, 0.1f, 0.1f,
                new Color32(201, 111, 0, 255), new Color32(201, 111, 0, 255),
                new Color32(201, 111, 0, 255), new Color32(201, 111, 0, 255),
                Color.white, Color.white,
                Color.white, Color.white,
                Color.white, Color.white,
                new Color32(201, 111, 0, 255), new Color32(201, 111, 0, 255),
                new Color32(201, 111, 0, 255), new Color32(201, 111, 0, 255)
                );
            var NoneSlime = SlimeCreation.SlimeBaseCreate(
                Ids.NONE_SLIME,
                "none_slime",
                "虚无史莱姆",
                "slimeNone",
                "虚无史莱姆",
                Identifiable.Id.PINK_SLIME,
                Identifiable.Id.PINK_SLIME,
                Identifiable.Id.PINK_SLIME,
                Identifiable.Id.PINK_SLIME,
                SlimeEat.FoodGroup.PLORTS,
                Identifiable.Id.NONE,
                Identifiable.Id.NONE,
                Identifiable.Id.NONE,
                Identifiable.Id.NONE,
                false,
                TextureUtils.LoadImage("NoneSlime.png").CreateSprite(),
                Vacuumable.Size.NORMAL,
                true,0.1f, 0.1f,
                new Color32(222, 111, 0, 0), new Color32(201, 111, 0, 0),
                new Color32(201, 111, 0, 0), new Color32(201, 111, 0, 0),
                SlimeFaceColor, SlimeFaceColor,
                SlimeFaceColor, SlimeFaceColor,
                SlimeFaceColor, SlimeFaceColor,
                new Color32(201, 111, 0, 0), new Color32(201, 111, 0, 0),
                new Color32(201, 111, 0, 0),
                new Color32(0, 0, 0, 0)

                );
            SlimePediaCreation.LoadSlimePediaIcon(
                Ids.PEDIA_ELEMENT_SLIME, 
                TextureUtils.CreateSprite(TextureUtils.LoadImage("PediaIcon.png", FilterMode.Bilinear, TextureWrapMode.Repeat)));
              SlimePediaCreation.LoadSlimePediaIcon(
                Ids.PEDIA_NONE_SLIME,
                TextureUtils.CreateSprite(TextureUtils.LoadImage("NoneSlime.png", FilterMode.Bilinear, TextureWrapMode.Repeat)));
            SlimePediaCreation.PreLoadSlimePediaConnection(Ids.PEDIA_ELEMENT_SLIME, Ids.ICE_SLIME, PediaRegistry.PediaCategory.SLIMES);
            SlimePediaCreation.PreLoadSlimePediaConnection(Ids.PEDIA_ELEMENT_SLIME, Ids.GRASS_SLIME, PediaRegistry.PediaCategory.SLIMES);
            SlimePediaCreation.PreLoadSlimePediaConnection(Ids.PEDIA_ELEMENT_SLIME, Ids.LIGHTNING_SLIME, PediaRegistry.PediaCategory.SLIMES);
            SlimePediaCreation.PreLoadSlimePediaConnection(Ids.PEDIA_ELEMENT_SLIME, Ids.WOOD_SLIME, PediaRegistry.PediaCategory.SLIMES);
            SlimePediaCreation.PreLoadSlimePediaConnection(Ids.PEDIA_NONE_SLIME, Ids.NONE_SLIME, PediaRegistry.PediaCategory.SLIMES);
            SlimePediaCreation.CreateSlimePediaForSlime(
                Ids.PEDIA_ELEMENT_SLIME, 
                "吸收天地元素形成的史莱姆们",
                "水果",
                "元素果",
                "他们与天地有着奇妙的关系，所以不要小看他们的能力！因为是元素产物，所以无法进行混种!",
                "它们人畜无害（应该吧），对牧场没有任何威胁",
                "各种元素结晶有超强的元素能力，在科学界有很高的研究价值（也就是他们很值钱？）");
            SlimePediaCreation.CreateSlimePediaForSlime(
                Ids.PEDIA_NONE_SLIME,
                "似实似虚，虚实双生",
                "结晶",
                "无",
                "将元素史莱姆的结晶给他们，他们就会变成各元素的史莱姆",
                "尚无记载",
                "他们不会产生任何结晶");
            TranslationPatcher.AddPediaTranslation("t.pedia_element_slime", "元素史莱姆");
            TranslationPatcher.AddPediaTranslation("t.pedia_none_slime", "虚无史莱姆");
            SlimeCreation.LoadSlime(IceSlime);
            SlimeCreation.LoadSlime(GrassSlime);
            SlimeCreation.LoadSlime(LightningSlime);
            SlimeCreation.LoadSlime(MetalSlime);
            SlimeCreation.LoadSlime(WoodSlime);
            SlimeCreation.LoadSlime(NoneSlime);

        }
        public override void PostLoad()
        {
            SlimeDefinition SpawnElementFruit = SRSingleton<GameContext>.Instance.SlimeDefinitions.GetSlimeByIdentifiableId(Ids.ICE_SLIME);
            SpawnElementFruit.Diet.EatMap.Add(new SlimeDiet.EatMapEntry
            {
                eats = Identifiable.Id.MANGO_FRUIT,
                producesId = Ids.ELEMENT_FRUIT,
                becomesId = Identifiable.Id.NONE,
                driver = SlimeEmotions.Emotion.NONE
            });
            SlimeDefinition NoneSlimeToOthers = SRSingleton<GameContext>.Instance.SlimeDefinitions.GetSlimeByIdentifiableId(Ids.NONE_SLIME);
            NoneSlimeToOthers.Diet.EatMap.Add(new SlimeDiet.EatMapEntry
            {
                eats = Ids.ICE_PLORT,
                producesId = Identifiable.Id.NONE,
                becomesId = Ids.ICE_SLIME,
                driver = SlimeEmotions.Emotion.NONE
            });
            NoneSlimeToOthers.Diet.EatMap.Add(new SlimeDiet.EatMapEntry
            {
                eats = Ids.GRASS_PLORT,
                producesId = Identifiable.Id.NONE,
                becomesId = Ids.GRASS_SLIME,
                driver = SlimeEmotions.Emotion.NONE
            });
            NoneSlimeToOthers.Diet.EatMap.Add(new SlimeDiet.EatMapEntry
            {
                eats = Ids.LIGHTNING_PLORT,
                producesId = Identifiable.Id.NONE,
                becomesId = Ids.LIGHTNING_SLIME,
                driver = SlimeEmotions.Emotion.NONE
            });
            NoneSlimeToOthers.Diet.EatMap.Add(new SlimeDiet.EatMapEntry
            {
                eats = Ids.METAL_PLORT,
                producesId = Identifiable.Id.NONE,
                becomesId = Ids.METAL_SLIME,
                driver = SlimeEmotions.Emotion.NONE
            });
            NoneSlimeToOthers.Diet.EatMap.Add(new SlimeDiet.EatMapEntry
            {
                eats = Ids.WOOD_PLORT,
                producesId = Identifiable.Id.NONE,
                becomesId = Ids.WOOD_SLIME,
                driver = SlimeEmotions.Emotion.NONE
            });
            List<Identifiable.Id> MetalClone = new List<Identifiable.Id>() { Identifiable.Id.PINK_PLORT, Identifiable.Id.ROCK_PLORT, Identifiable.Id.TABBY_PLORT, Identifiable.Id.PHOSPHOR_PLORT, Identifiable.Id.HONEY_PLORT, Identifiable.Id.BOOM_PLORT, Identifiable.Id.CRYSTAL_PLORT };

        }
    }
}
