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
    public class IndenSoilTile : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileMergeDirt[Type] = true;
            Main.tileMerge[Type][mod.TileType("IndenGrassTile")] = true;
            Main.tileMerge[Type][mod.TileType("IndenstoneTile")] = true;
            dustType = mod.DustType("IndendirtDust");
            drop = mod.ItemType("Indendirt");
            mineResist = 1.25f;
            AddMapEntry(new Color(73, 57, 63));
        }
        public override void RandomUpdate(int i, int j)
        {
            if (WorldGen.genRand.Next(30) == 0)
            {
                WorldGen.SpreadGrass(i, j, mod.TileType("IndenSoilTile"), mod.TileType("IndenGrassTile"), true, Main.tile[i, j].color());
            }
        }
        public override void NumDust(int i, int j, bool fail, ref int num)
        {
            num = fail ? 1 : 3;
        }
    }
}