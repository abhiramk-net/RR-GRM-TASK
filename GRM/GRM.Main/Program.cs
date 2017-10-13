using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRM.Main
{
    public class Program
    {
        static void Main(string[] args)
        {
            Arguments();
            Console.Read();
        }

        private static void Arguments()
        {
            string usages;
            DateTime startDate;

            Console.WriteLine("Enter Parter Name:");
            while (true)
            {
                usages = Console.ReadLine();
                if (!string.IsNullOrEmpty(usages)) break;

                Console.WriteLine("**Partner Name required.\nTry Again: ");
                continue;
            }

            Console.WriteLine("Enter Effective Date: ");
            while (true)
            {
                if (DateTime.TryParse(Console.ReadLine(), out startDate)) break;
                Console.WriteLine("**Enter Valid Date (MM-DD-YYY): ");
                continue;
            }

            PrintMusicContracts(usages, startDate.ToShortDateString());
        }

        public static string PrintMusicContracts(string partner, string startDate)
        {
            var list = ListMusicContracts(partner, startDate, out string[] contracts);

            var c = contracts[0].Split('|');
            var sb = new StringBuilder(Environment.NewLine);
            sb.Append(string.Format("| {0,-15} | {1,-25} | {2,-25} | {3,-10} | {4,-10} |", c[0], c[1], c[2], c[3], c[4]));
            sb.Append(Environment.NewLine);
            sb.Append(string.Format("| {0,-15} | {1,-25} | {2,-25} | {3,-10} | {4,-10} |", new String('-', 15), new String('-', 25), new String('-', 25), new String('-', 10), new String('-', 10)));

            list.ForEach(x =>
            {
                sb.Append(Environment.NewLine);
                var endDate = string.Empty;
                if (x.EndDate.HasValue) endDate = x.EndDate.Value.ToString("MM-dd-yyyy");
                sb.Append(string.Format("| {0,-15} | {1,-25} | {2,-25} | {3,-10} | {4,-10} |", x.Artist, x.Title, x.Usages, x.StartDate.ToString("MM-dd-yyyy"), endDate));
            });

            Console.WriteLine(sb.ToString());

            return sb.ToString();
        }

        public static List<MusicContract> MusicContracts(string partner, string startDate)
        {
            return ListMusicContracts(partner, startDate, out string[] contracts);
        }

        private static List<MusicContract> ListMusicContracts(string partner, string startDate, out string[] contracts)
        {
            string usage;
            DateTime sd;

            var list = new List<MusicContract>();
            var path = Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.Parent.FullName;

            var partners = File.ReadAllLines(Path.Combine(path, "GRM.Main\\Data\\DistributionPartnerContract.txt"));
            var partnersList = new List<DistributionPartnerContract>();
            for (var line = 1; line < partners.Length; line++)
            {
                var data = partners[line].Split('|');
                partnersList.Add(new DistributionPartnerContract { Partner = data[0], Usage = data[1] });
            }

            usage = string.Empty;
            var singlePartner = partnersList.SingleOrDefault(x => x.Partner.ToLower() == partner.ToLower());
            if (singlePartner != null)
                usage = singlePartner.Usage;

            sd = DateTime.Parse(startDate);
            contracts = File.ReadAllLines(Path.Combine(path, "GRM.Main\\Data\\MusicContract.txt"));

            for (var line = 1; line < contracts.Length; line++)
            {
                var data = contracts[line].Split('|');
                if (data[2].Contains(usage) && DateTime.Parse(data[3]) < sd)
                {
                    var mc = new MusicContract { Artist = data[0], Title = data[1], Usages = usage, StartDate = DateTime.Parse(data[3]) };
                    if (!string.IsNullOrEmpty(data[4])) mc.EndDate = DateTime.Parse(data[4]);

                    list.Add(mc);
                }
            }

            return list;
        }
    }
}
