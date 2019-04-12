using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;

namespace PrimordialSands.Projectiles.Minions
{
    public class TreeEntMinion : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            Main.projFrames[projectile.type] = 1;
            Main.projPet[projectile.type] = true;
            ProjectileID.Sets.MinionSacrificable[projectile.type] = true;
            ProjectileID.Sets.Homing[projectile.type] = true;
            DisplayName.SetDefault("Tree Ent");
        }

        public override void SetDefaults()
        {
            aiType = 266;
            projectile.width = 24;
            projectile.height = 36;
            projectile.aiStyle = 26;
            projectile.penetrate = -1;
            projectile.timeLeft = projectile.timeLeft * 5;
            projectile.minionSlots = 1f;
            projectile.minion = true;
            projectile.friendly = true;
            projectile.netImportant = true;
            projectile.tileCollide = false;
        }
        public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough)
        {
            fallThrough = false;
            return true;
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            if (projectile.penetrate == 0)
            {
                projectile.Kill();
            }
            return false;
        }
        public override bool MinionContactDamage()
        {
            return true;
        }
        public override void AI()
        {
            Player player = Main.player[projectile.owner];
            bool minionCheck = projectile.type == mod.ProjectileType("TreeEntMinion");
            player.AddBuff(mod.BuffType("TreeEntMinionBuff"), 3600);
            PrimordialSandsPlayer modPlayer = player.GetModPlayer<PrimordialSandsPlayer>(mod);
            if (minionCheck)
            {
                if (player.dead)
                {
                    modPlayer.treeEnt = false;
                }
                if (modPlayer.treeEnt)
                {
                    projectile.timeLeft = 2;
                }
            }
        }
    }
}