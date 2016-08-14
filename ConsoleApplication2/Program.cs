using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading;
using RiotApi.Net.RestClient.Configuration;
using RiotApi.Net.RestClient;
using RiotApi.Net.RestClient.Dto.League;
using RiotApi.Net.RestClient.Helpers;

namespace ConsoleApplication2
{
    class Program
    {
       static public RiotApi.Net.RestClient.Dto.Summoner.SummonerDto username;
        static public List<String> GameMembers = new List<String>();
        static public String BlueTeam;
        static public String RedTeam;
        static public String Red1;
        static public String Red2;
        static public String Red3;
        static public String Red4;
        static public int BlueTeamELO;
            static public int RedteamELO;
        static public IRiotClient riotClient = new RiotClient("RGAPI-CEE9BFC4-F5B2-45F6-BBBC-0510BA3E0A71");
        static public String Red5;
        static public String Blue1;
        static public String Blue2;
        static public String Blue3;
        static public String Blue4;
        static public String Blue5;
        static public long userteam;

        static void Main(string[] args)
    
        {
            Console.Title = "League Stats";
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            Console.WriteLine("Welcome to ldrpc's League Stats");
            Console.WriteLine("Settings Located at: " + config.FilePath);
            Thread.Sleep(500);
            if (config.AppSettings.Settings["Username"] == null)
            {
                Color("It appears like you haven't yet entered your League of Legends username!", ConsoleColor.Red);
                Thread.Sleep(500);
                String username = Ask("Enter League username: ");
                config.AppSettings.Settings.Add("Username", username);
                config.Save(ConfigurationSaveMode.Modified);
            }
            try
            {
                var testing = riotClient.Summoner.GetSummonersByName(RiotApiConfig.Regions.NA, config.AppSettings.Settings["Username"].Value.ToLower().Replace(" ", string.Empty));
                username = testing[config.AppSettings.Settings["Username"].Value.ToLower().Replace(" ", string.Empty)];

                Color("Successfully connected to user " + username.Name + " with summoner level " + username.SummonerLevel, ConsoleColor.Green);

            } catch
            {
                Color("Username " + config.AppSettings.Settings["Username"].Value + " seems to be incorrect", ConsoleColor.Red);
                String strl = Console.ReadLine();
          
            }
            Thread.Sleep(200);
            Color("Loading Game Information...", ConsoleColor.White);

            StartGame();

            }
        public static void StartGame()
        {
            try
            {

                var current = riotClient.CurrentGame.GetCurrentGameInformationForSummonerId(RiotApiConfig.Platforms.NA1, username.Id);

                var ts = TimeSpan.FromSeconds(current.GameLength);


                GameMembers.Clear();
                BlueTeam = "";
                RedTeam = "";
                Red1 = "";
                Red2 = "";
                Red3 = "";
                Red4 = "";
                Red5 = "";
                Blue1 = "";
                userteam = 0;
                Blue2 = "";
                Blue3 = "";
                Blue4 = "";
                Blue5 = "";
                BlueTeamELO = 0;
                RedteamELO = 0;
                String strq = string.Format("{0}:{1}:{2}", ts.Hours, ts.Minutes, ts.Seconds);
                foreach (var item in current.Participants)
                {
                    if (item.SummonerId == username.Id)
                        userteam = item.TeamId;
                }
                foreach (var item in current.Participants)
                {
                    if (item.TeamId == 200)
                    {
                        if (Red1 == "" && Red2 == "" && Red3 == "" && Red4 == "" && Red5 == "")
                        {
                            String rank = Rank(item.SummonerId, item.TeamId);
                            if (item.Spell1Id == 12 || item.Spell2Id == 12)
                            {
                                Red1 = item.SummonerName + " - Top - " + rank;
                            }
                            else if (item.Spell1Id == 3 || item.Spell2Id == 3)
                            {
                                Red1 = item.SummonerName + " - Support - " + rank;
                            }
                            else if (item.Spell1Id == 7 || item.Spell2Id == 7)
                            {
                                Red1 = item.SummonerName + " - ADC - " + rank;
                            }
                            else if (item.Spell1Id == 14 || item.Spell2Id == 14 || item.Spell1Id == 21 || item.Spell2Id == 21 || item.Spell1Id == 13 || item.Spell2Id == 13)
                            {
                                Red1 = item.SummonerName + " - Mid/Top - " + rank;
                            }
                            else if (item.Spell1Id == 11 || item.Spell2Id == 11)
                            {
                                Red1 = item.SummonerName + " - Jungle - " + rank;
                            }
                            else
                            {
                                Red1 = item.SummonerName + " - Unknown - " + rank;
                            }

                        }
                        else if (Red1 != "" && Red2 == "" && Red3 == "" && Red4 == "" && Red5 == "")
                        {
                            String rank = Rank(item.SummonerId, item.TeamId);
                            if (item.Spell1Id == 12 || item.Spell2Id == 12)
                            {
                                Red2 = item.SummonerName + " - Top - " + rank;
                            }
                            else if (item.Spell1Id == 3 || item.Spell2Id == 3)
                            {
                                Red2 = item.SummonerName + " - Support - " + rank;
                            }
                            else if (item.Spell1Id == 7 || item.Spell2Id == 7)
                            {
                                Red2 = item.SummonerName + " - ADC - " + rank;
                            }
                            else if (item.Spell1Id == 14 || item.Spell2Id == 14 || item.Spell1Id == 21 || item.Spell2Id == 21 || item.Spell1Id == 13 || item.Spell2Id == 13)
                            {
                                Red2 = item.SummonerName + " - Mid/Top - " + rank;
                            }
                            else if (item.Spell1Id == 11 || item.Spell2Id == 11)
                            {
                                Red2 = item.SummonerName + " - Jungle - " + rank;
                            }
                            else
                            {
                                Red2 = item.SummonerName + " - Unknown - " + rank;
                            }
                        }
                        else if (Red1 != "" && Red2 != "" && Red3 == "" && Red4 == "" && Red5 == "")
                        {
                            String rank = Rank(item.SummonerId, item.TeamId);
                            if (item.Spell1Id == 12 || item.Spell2Id == 12)
                            {
                                Red3 = item.SummonerName + " - Top - " + rank;
                            }
                            else if (item.Spell1Id == 3 || item.Spell2Id == 3)
                            {
                                Red3 = item.SummonerName + " - Support - " + rank;
                            }
                            else if (item.Spell1Id == 7 || item.Spell2Id == 7)
                            {
                                Red3 = item.SummonerName + " - ADC - " + rank;
                            }
                            else if (item.Spell1Id == 14 || item.Spell2Id == 14 || item.Spell1Id == 21 || item.Spell2Id == 21 || item.Spell1Id == 13 || item.Spell2Id == 13)
                            {
                                Red3 = item.SummonerName + " - Mid/Top - " + rank;
                            }
                            else if (item.Spell1Id == 11 || item.Spell2Id == 11)
                            {
                                Red3 = item.SummonerName + " - Jungle - " + rank;
                            }
                            else
                            {
                                Red3 = item.SummonerName + " - Unknown - " + rank;
                            }
                        }
                        else if (Red1 != "" && Red2 != "" && Red3 != "" && Red4 == "" && Red5 == "")
                        {
                            String rank = Rank(item.SummonerId, item.TeamId);
                            if (item.Spell1Id == 12 || item.Spell2Id == 12)
                            {
                                Red4 = item.SummonerName + " - Top - " + rank;
                            }
                            else if (item.Spell1Id == 3 || item.Spell2Id == 3)
                            {
                                Red4 = item.SummonerName + " - Support - " + rank;
                            }
                            else if (item.Spell1Id == 7 || item.Spell2Id == 7)
                            {
                                Red4 = item.SummonerName + " - ADC - " + rank;
                            }
                            else if (item.Spell1Id == 14 || item.Spell2Id == 14 || item.Spell1Id == 21 || item.Spell2Id == 21 || item.Spell1Id == 13 || item.Spell2Id == 13)
                            {
                                Red4 = item.SummonerName + " - Mid/Top - " + rank;
                            }
                            else if (item.Spell1Id == 11 || item.Spell2Id == 11)
                            {
                                Red4 = item.SummonerName + " - Jungle - " + rank;
                            }
                            else
                            {
                                Red4 = item.SummonerName + " - Unknown - " + rank;
                            }
                        }
                        else if (Red1 != "" && Red2 != "" && Red3 != "" && Red4 != "" && Red5 == "")
                        {
                            String rank = Rank(item.SummonerId, item.TeamId);
                            if (item.Spell1Id == 12 || item.Spell2Id == 12)
                            {
                                Red5 = item.SummonerName + " - Top - " + rank;
                            }
                            else if (item.Spell1Id == 3 || item.Spell2Id == 3)
                            {
                                Red5 = item.SummonerName + " - Support - " + rank;
                            }
                            else if (item.Spell1Id == 7 || item.Spell2Id == 7)
                            {
                                Red5 = item.SummonerName + " - ADC - " + rank;
                            }
                            else if (item.Spell1Id == 14 || item.Spell2Id == 14 || item.Spell1Id == 21 || item.Spell2Id == 21 || item.Spell1Id == 13 || item.Spell2Id == 13)
                            {
                                Red5 = item.SummonerName + " - Mid/Top - " + rank;
                            }
                            else if (item.Spell1Id == 11 || item.Spell2Id == 11)
                            {
                                Red5 = item.SummonerName + " - Jungle - " + rank;
                            }
                            else
                            {
                                Red5 = item.SummonerName + " - Unknown - " + rank;
                            }
                        }

                    }

                    if (item.TeamId == 100)
                    {
                        if (Blue1 == "" && Blue2 == "" && Blue3 == "" && Blue4 == "" && Blue5 == "")
                        {
                            String rank = Rank(item.SummonerId, item.TeamId);
                            if (item.Spell1Id == 12 || item.Spell2Id == 12)
                            {
                                Blue1 = item.SummonerName + " - Top - " + rank;
                            }
                            else if (item.Spell1Id == 3 || item.Spell2Id == 3)
                            {
                                Blue1 = item.SummonerName + " - Support - " + rank;
                            }
                            else if (item.Spell1Id == 7 || item.Spell2Id == 7)
                            {
                                Blue1 = item.SummonerName + " - ADC - " + rank;
                            }
                            else if (item.Spell1Id == 14 || item.Spell2Id == 14 || item.Spell1Id == 21 || item.Spell2Id == 21 || item.Spell1Id == 13 || item.Spell2Id == 13)
                            {
                                Blue1 = item.SummonerName + " - Mid/Top - " + rank;
                            }
                            else if (item.Spell1Id == 11 || item.Spell2Id == 11)
                            {
                                Blue1 = item.SummonerName + " - Jungle - " + rank;
                            }
                            else
                            {
                                Blue1 = item.SummonerName + " - Unknown - " + rank;
                            }

                        }
                        else if (Blue1 != "" && Blue2 == "" && Blue3 == "" && Blue4 == "" && Blue5 == "")
                        {
                            String rank = Rank(item.SummonerId, item.TeamId);
                            if (item.Spell1Id == 12 || item.Spell2Id == 12)
                            {
                                Blue2 = item.SummonerName + " - Top - " + rank;
                            }
                            else if (item.Spell1Id == 3 || item.Spell2Id == 3)
                            {
                                Blue2 = item.SummonerName + " - Support - " + rank;
                            }
                            else if (item.Spell1Id == 7 || item.Spell2Id == 7)
                            {
                                Blue2 = item.SummonerName + " - ADC - " + rank;
                            }
                            else if (item.Spell1Id == 14 || item.Spell2Id == 14 || item.Spell1Id == 21 || item.Spell2Id == 21 || item.Spell1Id == 13 || item.Spell2Id == 13)
                            {
                                Blue2 = item.SummonerName + " - Mid/Top - " + rank;
                            }
                            else if (item.Spell1Id == 11 || item.Spell2Id == 11)
                            {
                                Blue2 = item.SummonerName + " - Jungle - " + rank;
                            }
                            else
                            {
                                Blue2 = item.SummonerName + " - Unknown - " + rank;
                            }
                        }
                        else if (Blue1 != "" && Blue2 != "" && Blue3 == "" && Blue4 == "" && Blue5 == "")
                        {

                            String rank = Rank(item.SummonerId, item.TeamId);
                            if (item.Spell1Id == 12 || item.Spell2Id == 12)
                            {
                                Blue3 = item.SummonerName + " - Top - " + rank;
                            }
                            else if (item.Spell1Id == 3 || item.Spell2Id == 3)
                            {
                                Blue3 = item.SummonerName + " - Support - " + rank;
                            }
                            else if (item.Spell1Id == 7 || item.Spell2Id == 7)
                            {
                                Blue3 = item.SummonerName + " - ADC - " + rank;
                            }
                            else if (item.Spell1Id == 14 || item.Spell2Id == 14 || item.Spell1Id == 21 || item.Spell2Id == 21 || item.Spell1Id == 13 || item.Spell2Id == 13)
                            {
                                Blue3 = item.SummonerName + " - Mid/Top - " + rank;
                            }
                            else if (item.Spell1Id == 11 || item.Spell2Id == 11)
                            {
                                Blue3 = item.SummonerName + " - Jungle - " + rank;
                            }
                            else
                            {
                                Blue3 = item.SummonerName + " - Unknown - " + rank;
                            }
                        }
                        else if (Blue1 != "" && Blue2 != "" && Blue3 != "" && Blue4 == "" && Blue5 == "")
                        {
                            String rank = Rank(item.SummonerId, item.TeamId);
                            if (item.Spell1Id == 12 || item.Spell2Id == 12)
                            {
                                Blue4 = item.SummonerName + " - Top - " + rank;
                            }
                            else if (item.Spell1Id == 3 || item.Spell2Id == 3)
                            {
                                Blue4 = item.SummonerName + " - Support - " + rank;
                            }
                            else if (item.Spell1Id == 7 || item.Spell2Id == 7)
                            {
                                Blue4 = item.SummonerName + " - ADC - " + rank;
                            }
                            else if (item.Spell1Id == 14 || item.Spell2Id == 14 || item.Spell1Id == 21 || item.Spell2Id == 21 || item.Spell1Id == 13 || item.Spell2Id == 13)
                            {
                                Blue4 = item.SummonerName + " - Mid/Top - " + rank;
                            }
                            else if (item.Spell1Id == 11 || item.Spell2Id == 11)
                            {
                                Blue4 = item.SummonerName + " - Jungle - " + rank;
                            }
                            else
                            {
                                Blue4 = item.SummonerName + " - Unknown - " + rank;
                            }
                        }
                        else if (Blue1 != "" && Blue2 != "" && Blue3 != "" && Blue4 != "" && Blue5 == "")
                        {

                            String rank = Rank(item.SummonerId, item.TeamId);
                            if (item.Spell1Id == 12 || item.Spell2Id == 12)
                            {
                                Blue5 = item.SummonerName + " - Top - " + rank;
                            }
                            else if (item.Spell1Id == 3 || item.Spell2Id == 3)
                            {
                                Blue5 = item.SummonerName + " - Support - " + rank;
                            }
                            else if (item.Spell1Id == 7 || item.Spell2Id == 7)
                            {
                                Blue5 = item.SummonerName + " - ADC - " + rank;
                            }
                            else if (item.Spell1Id == 14 || item.Spell2Id == 14 || item.Spell1Id == 21 || item.Spell2Id == 21 || item.Spell1Id == 13 || item.Spell2Id == 13)
                            {
                                Blue5 = item.SummonerName + " - Mid/Top - " + rank;
                            }
                            else if (item.Spell1Id == 11 || item.Spell2Id == 11)
                            {
                                Blue5 = item.SummonerName + " - Jungle - " + rank;
                            }
                            else
                            {
                                Blue5 = item.SummonerName + " - Unknown - " + rank;
                            }


                        }
                    }
                }


                Color("----------------------------- [ Game Info ] -----------------------------", ConsoleColor.Magenta);
                Color("Game ID: " + current.GameId, ConsoleColor.Magenta);
                Color("Game Mode: " + current.GameMode, ConsoleColor.Magenta);
                Color("Game Type: " + current.GameType, ConsoleColor.Magenta);
                Color("Current Match Time: " + strq, ConsoleColor.Magenta);
                Color("         ", ConsoleColor.Red);
                Color("Match's Participants: ", ConsoleColor.Magenta);
                if (userteam == 100)
                    Color("Teams:   [BLUE] - Combined ELO: " + BlueTeamELO + " (Your Team!)", ConsoleColor.Cyan);
                if (userteam != 100)
                    Color("Teams:   [BLUE] - Combined ELO: " + BlueTeamELO, ConsoleColor.Cyan);
                Color("         " + Blue1, ConsoleColor.Cyan);
                Color("         " + Blue2, ConsoleColor.Cyan);
                Color("         " + Blue3, ConsoleColor.Cyan);
                Color("         " + Blue4, ConsoleColor.Cyan);
                Color("         " + Blue5, ConsoleColor.Cyan);
                Color("         ", ConsoleColor.Red);
                if (userteam == 200)
                    Color("         [RED] - Combined ELO: " + RedteamELO + " (Your Team!)", ConsoleColor.Red);
                if (userteam != 200)
                    Color("         [RED] - Combined ELO: " + RedteamELO, ConsoleColor.Red);
                Color("         " + Red1, ConsoleColor.Red);
                Color("         " + Red2, ConsoleColor.Red);
                Color("         " + Red3, ConsoleColor.Red);
                Color("         " + Red4, ConsoleColor.Red);
                Color("         " + Red5, ConsoleColor.Red);
                Color("         ", ConsoleColor.Red);
                int sum = BlueTeamELO + RedteamELO;
                decimal divRed = Decimal.Divide(RedteamELO, sum);
                decimal reddec = divRed * 100;
                decimal red = (int)Math.Round(reddec);
                decimal divBlue = Decimal.Divide(BlueTeamELO, sum);
                decimal bluedec = divBlue * 100;
                decimal blue = Math.Round(bluedec);
                Color("Chances of Winning:", ConsoleColor.Magenta);
                if (BlueTeamELO > RedteamELO)
                {
                    Color("Blue Team: " + blue.ToString() + "%" + " (Expected Winner!)", ConsoleColor.Cyan);
                    Color("Red Team:  " + red.ToString() + "%", ConsoleColor.Red);
                }
                else if (BlueTeamELO < RedteamELO)
                {
                    Color("Red Team:  " + red.ToString() + "%" + " (Expected Winner!)", ConsoleColor.Red);
                    Color("Blue Team: " + blue.ToString() + "%", ConsoleColor.Blue);
                }
                else if (BlueTeamELO == RedteamELO)
                {
                    Color("Red Team:  " + red.ToString() + "%" + " (Equal Chances!)", ConsoleColor.Red);
                    Color("Blue Team: " + blue.ToString() + "%" + " (Equal Chances!)", ConsoleColor.Blue);
                }
                Color("-------------------------------------------------------------------------", ConsoleColor.Magenta);
                String str = Console.ReadLine();
            }
            catch
            {
                Color("Player " + username.Name + " is currently not in a match.", ConsoleColor.Red);
                String str = Console.ReadLine();
                Color("Checking again in 15 seconds!", ConsoleColor.White);
                Thread.Sleep(15000);
                StartGame();
            }

        }
        static public String Rank(long id, long Team)
        {
            try { 
            Dictionary<string, IEnumerable<LeagueDto>> league_response = riotClient.League.GetSummonerLeagueEntriesByIds(RiotApiConfig.Regions.NA, id);

            // While there is only one entry in the response we still need to extract it to use it.
            // This is done with summoner id. Because the response from riot api is coming back as JSON we need to
            // convert our summoner id to a string.
            IEnumerable<LeagueDto> my_league = league_response[id.ToString()];

            // This is converting the object to a list for easy access, since we know there is only one entry.
            List<LeagueDto> league = my_league.ToList();
            LeagueDto ranked = league[0];

            // This is converting to a list again since entries has ranked as its only entry in it.
            List<LeagueDto.LeagueEntryDto> entries = ranked.Entries.ToList();
            LeagueDto.LeagueEntryDto my_entry = entries[0];

            // Now we can finally extract the information your after.
            Enums.Tier tier = ranked.Tier;
            string division = my_entry.Division;

            int playerElo = CalculateElo(tier, division);
            
                if(Team == 200)
                {
                    RedteamELO = RedteamELO + playerElo;
                } else if (Team == 100)
                {
                    BlueTeamELO = BlueTeamELO + playerElo;
                }
            
            return tier + " (" + division + ")";
            }
            catch
            {
                if (Team == 200)
                {
                    RedteamELO = RedteamELO + 3;
                }
                else if (Team == 100)
                {
                    BlueTeamELO = BlueTeamELO + 3;
                }
                return "No Rank";

            }
        }
        static public int CalculateElo(Enums.Tier tier, String division)
        {
            if (tier == Enums.Tier.BRONZE)
            {
                if (division == "I")
                {
                    return 5;
                }
                else if (division == "II")
                {
                    return 4;
                }
                else if (division == "III")
                {
                    return 3;
                }
                else if (division == "IV")
                {
                    return 2;
                }
                else if (division == "V")
                {
                    return 1;
                }
            }
            if (tier == Enums.Tier.SILVER)
            {
                if (division == "I")
                {
                    return 12;
                }
                else if (division == "II")
                {
                    return 11;
                }
                else if (division == "III")
                {
                    return 10;
                }
                else if (division == "IV")
                {
                    return 9;
                }
                else if (division == "V")
                {
                    return 8;
                }
                }
            if (tier == Enums.Tier.GOLD)
            {
                if (division == "I")
                {
                    return 19;
                }
                else if (division == "II")
                {
                    return 18;
                }
                else if (division == "III")
                {
                    return 17;
                }
                else if (division == "IV")
                {
                    return 16;
                }
                else if (division == "V")
                {
                    return 15;
                }
            }
            if (tier == Enums.Tier.PLATINUM)
            {
                if (division == "I")
                {
                    return 26;
                }
                else if (division == "II")
                {
                    return 25;
                }
                else if (division == "III")
                {
                    return 24;
                }
                else if (division == "IV")
                {
                    return 23;
                }
                else if (division == "V")
                {
                    return 22;
                }
            }
            if (tier == Enums.Tier.DIAMOND)
            {
                if (division == "I")
                {
                    return 33;
                }
                else if (division == "II")
                {
                    return 32;
                }
                else if (division == "III")
                {
                    return 31;
                }
                else if (division == "IV")
                {
                    return 30;
                }
                else if (division == "V")
                {
                    return 29;
                }

            }
            if (tier == Enums.Tier.MASTER)
            {
                if (division == "I")
                {
                    return 40;
                }
                else if (division == "II")
                {
                    return 39;
                }
                else if (division == "III")
                {
                    return 38;
                }
                else if (division == "IV")
                {
                    return 37;
                }
                else if (division == "V")
                {
                    return 36;
                }
            }
            if (tier == Enums.Tier.CHALLENGER)
            {
                if (division == "I")
                {
                    return 47;
                }
                else if (division == "II")
                {
                    return 46;
                }
                else if (division == "III")
                {
                    return 45;
                }
                else if (division == "IV")
                {
                    return 44;
                }
                else if (division == "V")
                {
                    return 43;
                }
            }
            return 0;
        }
        static public void Color(String text, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(text);
            Console.ForegroundColor = ConsoleColor.White;
        }
        static public String Ask(String k)
        {

            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(k);
            String str = Console.ReadLine();
            return str;
        }
    }
}
