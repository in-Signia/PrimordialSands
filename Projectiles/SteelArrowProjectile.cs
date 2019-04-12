using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;

namespace PrimordialSands.Projectiles
{
    public class SteelArrowProjectile : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Steel Arrow");
        }

        public override void SetDefaults()
        {
            projectile.width = 10;
            projectile.height = 32;
            projectile.penetrate = -1;
            projectile.timeLeft = 200;
            projectile.aiStyle = 1;
            projectile.ranged = true;
            projectile.friendly = true;
            projectile.tileCollide = true;
            projectile.ignoreWater = false;
            aiType = ProjectileID.WoodenArrowFriendly;
        }

        public override void Kill(int timeLeft)
        {
            Main.PlaySound(0, (int)projectile.position.X, (int)projectile.position.Y);
            int num3;
            int num570 = 6;
            for (int num571 = 0; num571 < num570; num571 = num3 + 1)
            {
                int num572 = Dust.NewDust(projectile.Center, 0, 0, mod.DustType("SteelDust"), 0f, 0f, 100, default(Color), 1f);
                Dust dust = Main.dust[num572];
                dust.velocity *= 1.6f;
                Dust dust56 = Main.dust[num572];
                dust56.velocity.Y = dust56.velocity.Y - 1f;
                dust = Main.dust[num572];
                dust.velocity += -projectile.velocity * (Main.rand.NextFloat() * 2f - 1f) * 0.5f;
                Main.dust[num572].scale = 1f;
                Main.dust[num572].noGravity = true;
                num3 = num571;
            }
        }
    }
}