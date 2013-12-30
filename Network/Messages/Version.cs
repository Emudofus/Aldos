using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Aldos.Network.Messages
{
    class Version : Utils.Objects.Packet
    {
        public int Major { get; set; }
        public int Minor { get; set; }
        public int Release { get; set; }
        public int Revision { get; set; }
        public int BuildType { get; set; }
        public int Patch { get; set; }

        public Version() : base(PacketID.Version)
        {
        }
        public Version(int major, int minor, int release, int revision, int buildType, int patch)
            : base(PacketID.Version)
        {
            Major       = major;
            Minor       = minor;
            Release     = release;
            Revision    = revision;
            BuildType   = buildType;
            Patch       = patch;
        }

        public override Utils.Objects.ByteBuffer Pack()
        {
            serialize(this);
            return base.Pack();
        }
        public static string ToString()
        {
            return GlobalConfig.Version.Major + "." +
                                                GlobalConfig.Version.Minor + "." +
                                                GlobalConfig.Version.Release + "." +
                                                GlobalConfig.Version.Revision + "." +
                                                GlobalConfig.Version.BuildType + "." +
                                                GlobalConfig.Version.Patch;
        }
        public bool Compare(Version match)
        {
            if (Major       == match.Major      &&
                Minor       == match.Minor      &&
                Release     == match.Release    &&
                Revision    == match.Revision   &&
                BuildType   == match.BuildType  &&
                Patch       == match.Patch) return true;
            return false;
        }

        public static void serialize(Utils.Objects.Packet sender)
        {
            sender.WriteByte((byte)GlobalConfig.Version.Major);
            sender.WriteByte((byte)GlobalConfig.Version.Minor);
            sender.WriteByte((byte)GlobalConfig.Version.Release);
            sender.WriteUShort((ushort)GlobalConfig.Version.Revision);
            sender.WriteByte((byte)GlobalConfig.Version.BuildType);
            sender.WriteByte((byte)GlobalConfig.Version.Patch);
        }
        public static Version deserialize(Utils.Objects.Packet sender)
        {
            return new Version(sender.ReadByte(), sender.ReadByte(), sender.ReadByte(), sender.ReadUShort(),
                               sender.ReadByte(), sender.ReadByte());
        }

        public static Version Default
        {
            get
            {
                return new Version(GlobalConfig.Version.Major, GlobalConfig.Version.Minor, GlobalConfig.Version.Release,
                                   GlobalConfig.Version.Revision, GlobalConfig.Version.BuildType, GlobalConfig.Version.Patch);
            }
        }
    }
}