using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

public static class RoleManager
{
    private static readonly Dictionary<string, (string RoleText, string ImagePath, string ColorHex)> RoleBadges = new Dictionary<string, (string, string, string)>
    {
        { "OWNER", ("Owner", "pack://application:,,,/src/images/RoleBadges/Owner.png", "#FFD700") },
        { "COOWNER", ("CoOwner", "pack://application:,,,/src/images/RoleBadges/CoOwner.png", "#1E90FF") },
        { "CONTENT CREATOR", ("Content Creator", "pack://application:,,,/src/images/RoleBadges/ContentCreator.png", "#32CD32") },
        { "FOUNDER", ("Founder", "pack://application:,,,/src/images/RoleBadges/Founder.png", "#FF4500") },
        { "DELUXE", ("Deluxe", "pack://application:,,,/src/images/RoleBadges/Deluxe.png", "#DAA520") },
        { "STARTER", ("Starter", "pack://application:,,,/src/images/RoleBadges/Starter.png", "#D3D3D3") }
    };

    public static (string RoleText, string ImagePath, string ColorHex) GetBadgeImagePath(string role)
    {
        if (RoleBadges.ContainsKey(role))
        {
            return RoleBadges[role];
        }
        else
        {
            return (null, null, null);
        }
    }
}



