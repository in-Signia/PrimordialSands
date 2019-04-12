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
    public class CarbonTile : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileMergeDirt[Type] = true;
            Main.tileSpelunker[Type] = true;
            dustType = 54;
            mineResist = 0.65f;
            minPick = 50;
            soundType = 21;
            soundStyle = 2;
            drop = mod.ItemType("Carbon");
            ModTranslation name = CreateMapEntryName();
            name.SetDefault("Carbon");
            AddMapEntry(new Color(120, 120, 120), name);
        }

        public override void NumDust(int i, int j, bool fail, ref int num)
        {
            num = fail ? 1 : 3;
        }
    }
}