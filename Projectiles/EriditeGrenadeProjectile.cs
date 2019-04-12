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
    public class EriditeGrenadeProjectile : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Eridite Grenade");
        }

        public override void SetDefaults()
        {
            projectile.width = 16;
            projectile.height = 16;
            projectile.penetrate = 2;
            projectile.timeLeft = 260;
            projectile.aiStyle = 16;
            projectile.ranged = true;
            projectile.friendly = true;
            projectile.tileCollide = true;
            projectile.ignoreWater = false;
        }
        public override void Kill(int timeLeft)
        {
            Main.PlaySound(SoundID.Item14, projectile.position);
            projectile.position.X = projectile.position.X + (float)(projectile.width / 2);
            projectile.position.Y = projectile.position.Y + (float)(projectile.height / 2);
            projectile.width = 22;
            projectile.height = 22;
            projectile.position.X = projectile.position.X - (float)(projectile.width / 2);
            projectile.position.Y = projectile.position.Y - (float)(projectile.height / 2);
            int num3;
            for (int num729 = 0; num729 < 30; num729 = num3 + 1)
            {
                int num730 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 31, 0f, 0f, 100, default(Color), 1.5f);
                Dust dust = Main.dust[num730];
                dust.velocity *= 1.4f;
                num3 = num729;
            }
            for (int num731 = 0; num731 < 20; num731 = num3 + 1)
            {
                int num732 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 6, 0f, 0f, 100, default(Color), 3.5f);
                Main.dust[num732].noGravity = true;
                Dust dust = Main.dust[num732];
                dust.velocity *= 7f;
                num732 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 6, 0f, 0f, 100, default(Color), 1.5f);
                dust = Main.dust[num732];
                dust.velocity *= 3f;
                num3 = num731;
            }
        }
    }
}