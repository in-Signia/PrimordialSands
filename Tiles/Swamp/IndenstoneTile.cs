using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Terraria.ObjectData;

namespace PrimordialSands.Tiles.Swamp
{
    public class IndenstoneTile : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileSpelunker[Type] = true;
            TileID.Sets.ChecksForMerge[Type] = true;
            Main.tileMerge[Type][mod.TileType("IndenSoilTile")] = true;
            Main.tileMerge[Type][mod.TileType("IndenGrassTile")] = true;
            dustType = 54;
            mineResist = 0.65f;
            minPick = 55;
            soundType = 21;
            soundStyle = 2;
            drop = mod.ItemType("Indenstone");
            ModTranslation name = CreateMapEntryName();
            name.SetDefault("Indenstone");
            AddMapEntry(new Color(56, 87, 94), name);
        }

        public override void NumDust(int i, int j, bool fail, ref int num)
        {
            num = fail ? 1 : 3;
        }
    }
}