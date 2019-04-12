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
    public class IndenGrassTile : ModTile
    {
        public override void SetDefaults()
        {
            //AddToArray(ref TileID.Sets.Conversion.Grass);
            //TileID.Sets.Conversion.Grass[Type] = true;
            Main.tileSolid[Type] = true;
            Main.tileMergeDirt[Type] = false;
            TileID.Sets.Grass[Type] = true;
            AddMapEntry(new Color(73, 115, 119));
            Main.tileBrick[Type] = true;
            drop = mod.ItemType("Indendirt");
            TileID.Sets.ChecksForMerge[Type] = true;
            dustType = mod.DustType("IndendirtDust");
            minPick = 0;
            soundType = 6;
            soundStyle = 6;
            //Main.tileMoss[Type] = true;
            //Main.tileMerge[Type][TileID.Dirt] = true;

            TileID.Sets.NeedsGrassFraming[Type] = true;
            TileID.Sets.NeedsGrassFramingDirt[Type] = mod.TileType("IndenSoilTile");
            Main.tileMerge[Type][mod.TileType("IndenstoneTile")] = true;
            SetModTree(new IndenwoodTreeTile());
        }
        public override void RandomUpdate(int i, int j)
        {
            WorldGen.SpreadGrass(i, j, mod.TileType("IndenSoilTile"), mod.TileType("IndenGrassTile"), true, Main.tile[i, j].color());
        }
        #region Extras
        public override void ChangeWaterfallStyle(ref int style)
        {
            style = mod.GetWaterfallStyleSlot("FloodWaterfallStyle");
        }

        public override void NumDust(int i, int j, bool fail, ref int num)
        {
            num = fail ? 1 : 3;
        }
        public override int SaplingGrowthType(ref int style)
        {
            style = 0;
            return mod.TileType("IndenwoodSaplingTile");
        }
        #endregion
        public override void KillTile(int i, int j, ref bool fail, ref bool effectOnly, ref bool noItem)
        {
            if (!effectOnly)
            {
                fail = true;
                Main.tile[i, j].type = (ushort)mod.TileType("IndenSoilTile");
                WorldGen.SquareTileFrame(i, j, true);
                for (int i1 = 0; i1 < 3; i1++)
                {
                    Dust.NewDust(new Vector2(i * 16, j * 16), 16, 16, mod.DustType("IndendirtDust"), 0f, 0f, 0, default(Color), 1.0f);
                }
            }
        }
    }
}