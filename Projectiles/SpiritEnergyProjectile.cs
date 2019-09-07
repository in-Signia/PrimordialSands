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
    public class SpiritEnergyProjectile : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Spirit Energy");
        }

        public override void SetDefaults()
        {
            projectile.width = 6;
            projectile.height = 6;
            projectile.penetrate = 1;
            projectile.aiStyle = -1;
            projectile.timeLeft = 240;
            projectile.ranged = true;
            projectile.friendly = true;
            projectile.tileCollide = true;
        }
        public override void AI()
        {
            projectile.velocity.X *= 0.99f;
            projectile.velocity.Y *= 0.99f;
            projectile.rotation = projectile.velocity.ToRotation() + 1.57079637f;
            Player owner = Main.player[projectile.owner];
            {
                if (projectile.alpha <= 240)
                {
                    projectile.alpha += 2;
                }
                if (projectile.alpha >= 240)
                {
                    projectile.Kill();
                }
            }
        }
        public override void Kill(int timeLeft)
        {
            Main.PlaySound(0, projectile.position);
            Dust.NewDust(projectile.Center, 0, 0, 89, 0f, 0f, 100, default(Color), 1f);
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            if (Main.rand.Next(3) == (0))
            {
                target.AddBuff(mod.BuffType("Flood"), 120);
            }
        }
    }
}