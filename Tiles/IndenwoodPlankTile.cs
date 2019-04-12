using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Terraria.ObjectData;

namespace PrimordialSands.Tiles
{
    public class IndenwoodPlankTile : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileMergeDirt[Type] = true;
            dustType = 7;
            mineResist = 0.5f;
            drop = mod.ItemType("Indenwood");
            ModTranslation name = CreateMapEntryName();
            name.SetDefault("Indenwood");
            AddMapEntry(new Color(119, 79, 98), name);
        }

        public override void NumDust(int i, int j, bool fail, ref int num)
        {
            num = fail ? 1 : 3;
        }
    }
}