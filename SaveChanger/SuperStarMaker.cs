﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace CM0102Patcher
{
    public class SuperStarMaker
    {
        public int MakeSuperStar(List<TStaff> staff, List<TPlayer> players, byte[] staffData, byte[] playerData, DateTime? gameDate = null, int age = -1)
        {
            // Pick our player to make into a superstar at random
            var lesserKnown = players.Where(x => x.CurrentReputation < 1500).ToList();
            var ourPlayer = lesserKnown[rand.Next(lesserKnown.Count)];
            var ourStaff = staff.First(x => x.Player == ourPlayer.ID);

            ourStaff = MakeStaff(ourStaff, staffData);
            ourPlayer = MakePlayer(ourPlayer, playerData);

            if (gameDate != null && age != -1)
            {
                var birthday = TCMDate.ToDateTime(ourStaff.DateOfBirth);
                if (birthday.Month < gameDate.Value.Month || (birthday.Month == gameDate.Value.Month && birthday.Day <= gameDate.Value.Day)) // Force the age to be correct
                    age++;
                birthday = new DateTime(gameDate.Value.AddYears(-age).Year, birthday.Month, birthday.Day);
                ourStaff.DateOfBirth = TCMDate.FromDateTime(birthday);
                ourStaff.YearOfBirth = (ushort)age; // Looks like an age rather than a year of birth
            }

            staff[ourStaff.ID] = ourStaff;
            players[ourPlayer.ID] = ourPlayer;

            return ourStaff.ID;
        }

        public int MakeTHERichardMolloy(List<TStaff> staff, List<TPlayer> players, byte[] staffData, byte[] playerData, DateTime? gameDate, int age, List<string> firstNames, List<string> secondNames)
        {
            int molly = 0;
            var richardIdx = firstNames.IndexOf("Richard");
            var molloyIdx = secondNames.IndexOf("Molloy");
            if (richardIdx != -1 && molloyIdx != -1)
            {
                molly = MakeSuperStar(staff, players, staffData, playerData, gameDate, age);
                var mollyStaff = staff[molly];
                var mollyPlayer = players[mollyStaff.Player];
                mollyStaff.Adaptability = mollyStaff.Ambition = mollyStaff.Determination = mollyStaff.Pressure = mollyStaff.Professionalism = mollyStaff.Professionalism = 20;
                mollyStaff.FirstName = richardIdx;
                mollyStaff.SecondName = molloyIdx;
                mollyStaff.DateOfBirth = TCMDate.FromDateTime(new DateTime(mollyStaff.DateOfBirth.Year, 7, 25));
                var birthday = TCMDate.ToDateTime(mollyStaff.DateOfBirth);
                if (birthday.Month < gameDate.Value.Month || (birthday.Month == gameDate.Value.Month && birthday.Day <= gameDate.Value.Day)) // Force the age to be correct
                    age++;
                birthday = new DateTime(gameDate.Value.AddYears(-age).Year, birthday.Month, birthday.Day);
                mollyStaff.DateOfBirth = TCMDate.FromDateTime(birthday);
                var bday = TCMDate.ToDateTime(mollyStaff.DateOfBirth);
                mollyStaff.YearOfBirth = (ushort)age; // Looks like an age rather than a year of birth
                mollyPlayer.PotentialAbility = 125;
                mollyPlayer.CurrentAbility = 20;
                mollyPlayer.Consistency = mollyPlayer.LeftFoot = mollyPlayer.Leadership = mollyPlayer.ImportantMatches = mollyPlayer.NaturalFitness = mollyPlayer.Versatility = 20;
                mollyPlayer.PlayerPace = mollyPlayer.Acceleration = 19;
                mollyPlayer.WorkRate = 18;
                mollyPlayer.Strength += 3;
                mollyPlayer.Stamina += 3;
                mollyPlayer.Technique += 3;
                mollyPlayer.Balance += 4;
                mollyPlayer.Bravery += 4;
                mollyPlayer.Flair += 2;
                mollyPlayer.InjuryProneness = 1;
                staff[molly] = mollyStaff;
                players[mollyStaff.Player] = mollyPlayer;
            }
            return molly;
        }

        public void OutputPlayer(TStaff thePlayer, List<TPlayer> players, List<string> firstNames, List<string> secondNames)
        {
            var playerData = players.First(x => x.ID == thePlayer.Player);
            var staffBytes = StructToBytes(thePlayer);
            var playerBytes = StructToBytes(playerData);

            string firstName = firstNames[thePlayer.FirstName];
            string secondName = secondNames[thePlayer.SecondName];

            Console.WriteLine("// " + firstName + " " + secondName);
            WriteOutByteArray(firstName + secondName + "Staff", staffBytes, 86, 8);
            WriteOutByteArray(firstName + secondName + "Player", playerBytes);
        }

        public string GetPlayerName(int staffId, List<TStaff> staff, List<string> firstNames, List<string> secondNames)
        {
            return firstNames[staff[staffId].FirstName] + " " + secondNames[staff[staffId].SecondName];
        }

        TStaff MakeStaff(TStaff staff, byte[] staffData)
        {
            var staffBytes = StructToBytes(staff);
            Array.Copy(staffData, 0, staffBytes, 86, 8);
            return BytesToStruct<TStaff>(staffBytes);
        }

        TPlayer MakePlayer(TPlayer player, byte[] playerData)
        {
            var playerBytes = StructToBytes(player);
            Array.Copy(playerData, 5, playerBytes, 5, playerData.Length - 5);
            return BytesToStruct<TPlayer>(playerBytes);
        }

        byte[] StructToBytes<T>(T obj)
        {
            int objSize = Marshal.SizeOf(typeof(T));
            byte[] arr = new byte[objSize];
            IntPtr ptr = Marshal.AllocHGlobal(objSize);
            Marshal.StructureToPtr(obj, ptr, true);
            Marshal.Copy(ptr, arr, 0, objSize);
            Marshal.FreeHGlobal(ptr);
            return arr;
        }

        T BytesToStruct<T>(byte[] bytes)
        {
            int objSize = Marshal.SizeOf(typeof(T));
            var ptrObj = Marshal.AllocHGlobal(objSize);
            Marshal.Copy(bytes, 0, ptrObj, objSize);
            var obj = (T)Marshal.PtrToStructure(ptrObj, typeof(T));
            Marshal.FreeHGlobal(ptrObj);
            return obj;
        }

        void WriteOutByteArray(string name, byte[] arr, int start = 0, int length = -1)
        {
            string s = string.Format("public byte[] {0} = new byte[] {{ ", name);
            for (int i = start; i < ((length == -1) ? arr.Length : start + length); i++)
            {
                s += "0x" + arr[i].ToString("X2");
                s += ", ";
            }
            s = s.Substring(0, s.Length - 2);
            s += " };";
            Console.WriteLine(s);
        }

        Random rand = new Random();

        // https://champman0102.co.uk/showthread.php?t=631
        // Maxim Tsigalko
        public byte[] MaximTsigalkoStaff = new byte[] { 0x0E, 0x09, 0x12, 0x10, 0x0F, 0x08, 0x09, 0x07 };
        public byte[] MaximTsigalkoPlayer = new byte[] { 0x93, 0x37, 0x00, 0x00, 0x00, 0x50, 0x00, 0x6F, 0x00, 0x82, 0x14, 0x82, 0x14, 0xD6, 0x06, 0x00, 0x00, 0x00, 0x00, 0x0A, 0x14, 0x14, 0x00, 0x0A, 0x00, 0x14, 0x0A, 0x11, 0x11, 0x13, 0xD1, 0x0E, 0x0F, 0x0F, 0x08, 0xBE, 0xD6, 0x08, 0xB4, 0x33, 0x10, 0x06, 0x88, 0xD1, 0x12, 0x02, 0x12, 0x05, 0x10, 0xB9, 0xCA, 0x2E, 0x12, 0x04, 0x0F, 0xCC, 0xE0, 0xBE, 0xD1, 0x14, 0x0E, 0x0E, 0xCA, 0x0C, 0x0D, 0x09, 0x0A, 0xFE, 0x0D, 0x0A };

        // Cherno Samba
        public byte[] ChernoSambaStaff = new byte[] { 0x08, 0x14, 0x11, 0x0A, 0x0D, 0x13, 0x0D, 0x0C };
        public byte[] ChernoSambaPlayer = new byte[] { 0x03, 0x16, 0x00, 0x00, 0x00, 0x32, 0x00, 0xB5, 0x00, 0xA6, 0x0E, 0xA6, 0x0E, 0xC4, 0x09, 0x01, 0x01, 0x01, 0x01, 0x01, 0x01, 0x14, 0x01, 0x01, 0x01, 0x14, 0x01, 0x10, 0x0C, 0x0D, 0xCA, 0x0F, 0x0B, 0x09, 0x09, 0xB5, 0xBB, 0x03, 0xF2, 0x00, 0x12, 0x09, 0x88, 0xFB, 0x0B, 0x05, 0x10, 0x06, 0x14, 0xF2, 0xB5, 0xED, 0x0F, 0x33, 0x10, 0xE3, 0x4B, 0xCA, 0xD9, 0x14, 0x0C, 0x0E, 0xBA, 0x11, 0x0B, 0xFC, 0x09, 0xC0, 0x0C, 0x0A };

        // Anastasios Skalidis
        public byte[] AnastasiosSkalidisStaff = new byte[] { 0x13, 0x12, 0x14, 0x13, 0x14, 0x13, 0x14, 0x10 };
        public byte[] AnastasiosSkalidisPlayer = new byte[] { 0xC8, 0x59, 0x00, 0x00, 0x09, 0x32, 0x00, 0x8A, 0x00, 0xC4, 0x09, 0xC4, 0x09, 0x32, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x14, 0x00, 0x00, 0x00, 0x14, 0x00, 0x11, 0x0F, 0x0C, 0x03, 0x14, 0x14, 0x14, 0x02, 0xB8, 0x03, 0x10, 0x08, 0x50, 0x14, 0x13, 0x88, 0x11, 0x14, 0x13, 0x14, 0x12, 0x14, 0x11, 0xAE, 0x08, 0x12, 0x4B, 0x11, 0xAE, 0x11, 0xAE, 0xAE, 0x13, 0x12, 0x14, 0xAE, 0x01, 0x11, 0xF1, 0x01, 0xAE, 0x10, 0x0A };

        // Alexandros Papadopoulos
        public byte[] AlexandrosPapadopoulosStaff = new byte[] { 0x13, 0x12, 0x14, 0x13, 0x12, 0x14, 0x09, 0x0E };
        public byte[] AlexandrosPapadopoulosPlayer = new byte[] { 0x7F, 0x3B, 0x00, 0x00, 0x10, 0x50, 0x00, 0x8B, 0x00, 0xA0, 0x0F, 0xA0, 0x0F, 0x32, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x14, 0x00, 0x00, 0x00, 0x14, 0x00, 0x11, 0x12, 0x0C, 0xF1, 0x0E, 0x13, 0x14, 0x02, 0xB6, 0x0A, 0x13, 0xFB, 0x28, 0x12, 0x01, 0x88, 0xD8, 0x12, 0x01, 0x0A, 0x11, 0x14, 0xB6, 0xB6, 0x0F, 0x14, 0x3E, 0x10, 0xFB, 0x0F, 0xB6, 0xB6, 0x13, 0x13, 0x0D, 0x9E, 0x13, 0x0F, 0xE9, 0x01, 0xFB, 0x12, 0x0A };

        // Fernando José Torres
        public byte[] FernandoTorresStaff = new byte[] { 0x11, 0x14, 0x14, 0x12, 0x0C, 0x10, 0x11, 0x10 };
        public byte[] FernandoTorresPlayer = new byte[] { 0x6E, 0x24, 0x00, 0x00, 0x09, 0x8A, 0x00, 0x97, 0x00, 0x5E, 0x1A, 0x5E, 0x1A, 0x64, 0x19, 0x01, 0x01, 0x01, 0x01, 0x05, 0x0A, 0x14, 0x01, 0x0A, 0x0A, 0x14, 0x09, 0x0F, 0x0A, 0x10, 0xD6, 0x09, 0x0D, 0x10, 0x09, 0xDB, 0xFE, 0x0A, 0xF9, 0xFE, 0x14, 0x08, 0x88, 0xE5, 0x11, 0x06, 0x09, 0x0D, 0x08, 0xDB, 0xD4, 0x08, 0x07, 0x0F, 0x10, 0xE0, 0xF4, 0xD4, 0xCA, 0x14, 0x0E, 0x08, 0xD1, 0x0A, 0x0F, 0xCF, 0x0A, 0xE5, 0x0D, 0x0A };

        // Mike Duff
        public byte[] MikeDuffStaff = new byte[] { 0x0C, 0x12, 0x14, 0x0F, 0x0F, 0x12, 0x10, 0x0F };
        public byte[] MikeDuffPlayer = new byte[] { 0xA9, 0x2E, 0x00, 0x00, 0x06, 0x73, 0x00, 0x73, 0x00, 0xB8, 0x0B, 0xB8, 0x0B, 0xEE, 0x02, 0x01, 0x01, 0x14, 0x0A, 0x0A, 0x01, 0x01, 0x0F, 0x12, 0x01, 0x14, 0x01, 0x12, 0x04, 0x12, 0xC6, 0x11, 0x10, 0x0F, 0x0E, 0xDC, 0xDC, 0x03, 0xCB, 0xB7, 0x10, 0x0B, 0x88, 0xDC, 0x11, 0x04, 0x12, 0x0B, 0x11, 0xD6, 0xE1, 0xE1, 0x14, 0x05, 0x12, 0xD7, 0xD6, 0xDC, 0xCB, 0x14, 0x14, 0x0C, 0x23, 0x12, 0x10, 0x31, 0x10, 0xCB, 0x12, 0x0A };

        // Mark Kerr
        public byte[] MarkKerrStaff = new byte[] { 0x0C, 0x14, 0x14, 0x0A, 0x0D, 0x14, 0x0A, 0x14 };
        public byte[] MarkKerrPlayer = new byte[] { 0x00, 0x30, 0x00, 0x00, 0x00, 0x7B, 0x00, 0x7B, 0x00, 0x8E, 0x12, 0x8E, 0x12, 0xC4, 0x09, 0x01, 0x01, 0x01, 0x01, 0x14, 0x01, 0x01, 0x00, 0x0A, 0x01, 0x14, 0x00, 0x11, 0x0D, 0x11, 0xE0, 0x12, 0x10, 0x13, 0x0E, 0xE5, 0xD6, 0x06, 0xE0, 0xC4, 0x0D, 0x0D, 0x88, 0xC8, 0x10, 0x0A, 0x0E, 0x0B, 0x14, 0xD6, 0xC8, 0x26, 0x11, 0x03, 0x0C, 0xEA, 0xE0, 0xC4, 0xC3, 0x14, 0x12, 0x0E, 0xE5, 0x14, 0x12, 0x03, 0x0C, 0xDB, 0x13, 0x0A };

        // Hugo Pinheiro
        public byte[] HugoPinheiroStaff = new byte[] { 0x0C, 0x10, 0x0E, 0x0C, 0x09, 0x0C, 0x0E, 0x0C };
        public byte[] HugoPinheiroPlayer = new byte[] { 0xB1, 0x68, 0x00, 0x00, 0x00, 0x5A, 0x00, 0x66, 0x00, 0xE8, 0x03, 0xE8, 0x03, 0x32, 0x00, 0x14, 0x01, 0x01, 0x01, 0x01, 0x01, 0x01, 0x01, 0x01, 0x01, 0x01, 0x01, 0x09, 0x0E, 0x0E, 0xF2, 0x0E, 0x13, 0x0E, 0x01, 0xCD, 0x01, 0x01, 0xC9, 0xCE, 0x01, 0x09, 0x17, 0xD7, 0x0C, 0x01, 0x10, 0x0C, 0x07, 0xE6, 0xCD, 0xCD, 0x0E, 0x0F, 0x0F, 0xD3, 0xD7, 0x0B, 0xEA, 0x14, 0x0E, 0x0F, 0xD3, 0x0C, 0x0E, 0xD8, 0x03, 0xDC, 0x0F, 0x0A };

        // Franco Costanzo
        public byte[] FrancoCostanzoStaff = new byte[] { 0x0F, 0x0F, 0x14, 0x0D, 0x10, 0x07, 0x04, 0x0F };
        public byte[] FrancoCostanzoPlayer = new byte[] { 0xF0, 0x5C, 0x00, 0x00, 0x00, 0x82, 0x00, 0x82, 0x00, 0x70, 0x17, 0x70, 0x17, 0xB8, 0x0B, 0x14, 0x01, 0x01, 0x01, 0x01, 0x01, 0x01, 0x01, 0x01, 0x01, 0x01, 0x01, 0x14, 0x0E, 0x14, 0x06, 0x10, 0x0C, 0x14, 0x03, 0xC4, 0xE8, 0x04, 0xBC, 0xC8, 0x04, 0x02, 0x00, 0xBC, 0x10, 0x12, 0x0F, 0x08, 0x07, 0xC8, 0xB2, 0xC8, 0x10, 0x26, 0x0F, 0xCF, 0xC8, 0x1A, 0x00, 0x14, 0x12, 0x13, 0xDE, 0x0F, 0x07, 0xFE, 0x01, 0xC5, 0x11, 0x0A };

        // Dionisis Chiotis
        public byte[] DionisisChiotisStaff = new byte[] { 0x14, 0x12, 0x13, 0x13, 0x11, 0x0F, 0x07, 0x0A };
        public byte[] DionisisChiotisPlayer = new byte[] { 0x23, 0x59, 0x00, 0x00, 0x16, 0x7D, 0x00, 0x82, 0x00, 0x6A, 0x18, 0x6A, 0x18, 0x4E, 0x0C, 0x14, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x0F, 0x05, 0x13, 0x1D, 0x0C, 0x14, 0x12, 0x0A, 0xCE, 0x09, 0x08, 0xCE, 0xCE, 0x05, 0x05, 0x00, 0xFB, 0x0B, 0x03, 0x13, 0x08, 0x05, 0xCE, 0xCE, 0xCE, 0x11, 0x0D, 0x12, 0xFB, 0xE2, 0x09, 0x00, 0x14, 0x11, 0x12, 0xE2, 0x0C, 0x02, 0xD6, 0x01, 0xCE, 0x14, 0x0A };

        // Taribo West
        public byte[] TariboWestStaff = new byte[] { 0x0D, 0x0E, 0x13, 0x0A, 0x12, 0x0A, 0x0E, 0x11 };
        public byte[] TariboWestPlayer = new byte[] { 0x01, 0x0F, 0x00, 0x00, 0x00, 0x86, 0x00, 0xB4, 0x00, 0x76, 0x16, 0x76, 0x16, 0x76, 0x16, 0x01, 0x0D, 0x14, 0x08, 0x01, 0x01, 0x01, 0x05, 0x0D, 0x11, 0x14, 0x01, 0x0F, 0x14, 0x09, 0xD4, 0x12, 0x14, 0x0C, 0x08, 0xDE, 0xDE, 0x14, 0xED, 0xDD, 0x0B, 0x04, 0x88, 0xF7, 0x10, 0x0C, 0x11, 0x0E, 0x0C, 0xDE, 0x06, 0xE3, 0x0E, 0xD7, 0x10, 0xD4, 0xC3, 0xF2, 0xC7, 0x14, 0x13, 0x14, 0x01, 0x0F, 0x07, 0x27, 0x0E, 0xC3, 0x12, 0x0A };

        // Kim Källström
        public byte[] KimKallstromStaff = new byte[] { 0x0C, 0x13, 0x12, 0x0D, 0x12, 0x0E, 0x0D, 0x0B };
        public byte[] KimKallstromPlayer = new byte[] { 0x49, 0x6F, 0x00, 0x00, 0x0C, 0x8E, 0x00, 0x8E, 0x00, 0x46, 0x1E, 0x46, 0x1E, 0xA0, 0x0F, 0x01, 0x01, 0x01, 0x0F, 0x14, 0x14, 0x07, 0x01, 0x01, 0x08, 0x14, 0x0F, 0x07, 0x0C, 0x08, 0xE7, 0x0E, 0x10, 0x10, 0x0D, 0xE7, 0xEC, 0x07, 0xDD, 0xE2, 0x0D, 0x12, 0x88, 0xD4, 0x0C, 0x05, 0x0A, 0x0B, 0x14, 0x00, 0xDC, 0xDD, 0x11, 0xDA, 0x0A, 0xFB, 0xE7, 0xDC, 0xDC, 0x03, 0x0F, 0x08, 0xD6, 0x0D, 0x0F, 0x0C, 0x08, 0xFB, 0x11, 0x0A };

        // Eldar Hadzimehmedovic
        public byte[] EldarHadzimehmedovicStaff = new byte[] { 0x04, 0x05, 0x0A, 0x11, 0x0A, 0x09, 0x01, 0x08 };
        public byte[] EldarHadzimehmedovicPlayer = new byte[] { 0x83, 0x65, 0x00, 0x00, 0x0F, 0x66, 0x00, 0x9C, 0x00, 0x88, 0x13, 0x88, 0x13, 0x96, 0x00, 0x01, 0x01, 0x01, 0x01, 0x0A, 0x14, 0x14, 0x00, 0x11, 0x00, 0x14, 0x00, 0x0C, 0x08, 0x0C, 0xD8, 0x0D, 0x0E, 0x0B, 0x0D, 0xD8, 0xF1, 0x0A, 0xE7, 0x16, 0x0E, 0x0A, 0x88, 0xD5, 0x09, 0x09, 0x09, 0x09, 0x01, 0xE2, 0xD0, 0xEC, 0x0A, 0x03, 0x0D, 0xF6, 0xD8, 0xE2, 0xD7, 0x14, 0x0E, 0x0A, 0xCF, 0x0A, 0x0F, 0xF4, 0x06, 0xE7, 0x0C, 0x0A };

        // David Prutton
        public byte[] DavidPruttonStaff = new byte[] { 0x10, 0x12, 0x12, 0x14, 0x0F, 0x0E, 0x0C, 0x0E };
        public byte[] DavidPruttonPlayer = new byte[] { 0x56, 0x4D, 0x00, 0x00, 0x07, 0x87, 0x00, 0x93, 0x00, 0x7C, 0x15, 0x7C, 0x15, 0xDC, 0x05, 0x01, 0x01, 0x14, 0x14, 0x14, 0x01, 0x01, 0x0F, 0x14, 0x00, 0x14, 0x01, 0x0E, 0x0F, 0x0F, 0xF1, 0x0E, 0x0F, 0x10, 0x0C, 0xE3, 0xE8, 0x0C, 0xE3, 0xE3, 0x09, 0x0A, 0x88, 0xD4, 0x0E, 0x0C, 0x0C, 0x0F, 0x07, 0xE8, 0xEC, 0xF6, 0x10, 0xF2, 0x0F, 0xF1, 0xE3, 0xEC, 0xCA, 0x14, 0x0E, 0x0E, 0xEC, 0x12, 0x0F, 0x01, 0x12, 0xE8, 0x12, 0x0A };

        // Nhlanhla Shabalala
        public byte[] NhlanhlaShabalalaStaff = new byte[] { 0x12, 0x12, 0x10, 0x11, 0x0A, 0x08, 0x0F, 0x0C };
        public byte[] NhlanhlaShabalalaPlayer = new byte[] { 0x4A, 0x14, 0x00, 0x00, 0x00, 0x28, 0x00, 0x98, 0x00, 0xE8, 0x03, 0xE8, 0x03, 0xF4, 0x01, 0x01, 0x01, 0x01, 0x0A, 0x14, 0x0F, 0x01, 0x01, 0x01, 0x01, 0x14, 0x0F, 0x0D, 0x0C, 0x11, 0xDE, 0x0F, 0x0C, 0x10, 0x07, 0xC1, 0x22, 0x08, 0x50, 0xDE, 0x12, 0x0A, 0x88, 0xCA, 0x0C, 0x0F, 0x03, 0x0D, 0x0A, 0xBB, 0xBB, 0xDE, 0x12, 0x28, 0x10, 0xD9, 0xCA, 0xDE, 0xD0, 0x14, 0x12, 0x0E, 0xBB, 0x10, 0x0D, 0xF6, 0x0C, 0x13, 0x10, 0x0A };

        // Ibrahim Said
        public byte[] IbrahimSaidStaff = new byte[] { 0x0F, 0x14, 0x14, 0x05, 0x11, 0x0D, 0x0E, 0x0A };
        public byte[] IbrahimSaidPlayer = new byte[] { 0xAB, 0x07, 0x00, 0x00, 0x00, 0x7D, 0x00, 0x7D, 0x00, 0x22, 0x24, 0x22, 0x24, 0xA0, 0x0F, 0x00, 0x14, 0x14, 0x10, 0x00, 0x00, 0x0A, 0x00, 0x00, 0x00, 0x14, 0x00, 0x0F, 0x10, 0x11, 0xE0, 0x0F, 0x12, 0x11, 0x08, 0xBC, 0xE5, 0x0E, 0xDB, 0xC1, 0x11, 0x0E, 0x88, 0xDB, 0x12, 0x0A, 0x11, 0x0E, 0x14, 0xBA, 0xE5, 0xD6, 0x12, 0x02, 0x0F, 0xD6, 0x1D, 0xE5, 0xD5, 0x14, 0x11, 0x0F, 0x1D, 0x10, 0x12, 0xF3, 0x10, 0xC3, 0x10, 0x0A };
    }
}
