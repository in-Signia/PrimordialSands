using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ModLoader;
using Terraria.UI;
using Terraria.DataStructures;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.GameContent.UI;

using System;
using System.IO;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PrimordialSands;

namespace PrimordialSands.NPCs
{
    public class _GlobalNPC : GlobalNPC
    {
        public override bool InstancePerEntity
        {
            get
            {
                return true;
            }
        }

        public bool flood = false;
        public bool splintered = false;

        public override void ResetEffects(NPC npc)
        {
            flood = false;
            splintered = false;
        }

        public override void UpdateLifeRegen(NPC npc, ref int damage)
        {
            if (flood)
            {
                if (npc.lifeRegen > 0)
                {
                    npc.lifeRegen = 0;
                }
                npc.lifeRegen -= 5;
                if (damage < 3)
                {
                    damage = 3;
                }
            }
            if (splintered)
            {
                npc.defense /= 4;
            }
        }

        public override void DrawEffects(NPC npc, ref Color drawColor)
        {
            if (flood)
            {
                if (Main.rand.Next(4) < 3)
                {
                    int dust = Dust.NewDust(npc.position - new Vector2(2f, 2f), npc.width + 4, npc.height + 4, 89, npc.velocity.X * 0.4f, npc.velocity.Y * 0.4f, 100, default(Color), 1f);
                    Main.dust[dust].scale = 1f;
                    Main.dust[dust].noGravity = true;
                }
                Lighting.AddLight(npc.position, 0f, 0.25f, 0f);
            }
            if (splintered)
            {
                if (Main.rand.Next(4) < 3)
                {
                    int dust = Dust.NewDust(npc.position - new Vector2(2f, 2f), npc.width + 4, npc.height + 4, 7, npc.velocity.X * 0.4f, npc.velocity.Y * 0.4f, 100, default(Color), 1f);
                    Main.dust[dust].scale = 1f;
                    Main.dust[dust].noGravity = true;
                }
                Lighting.AddLight(npc.position, 0f, 0f, 0f);
            }
        }
    }
}