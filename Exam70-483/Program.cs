using static System.Console;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Runtime.InteropServices;
using Exam70_483.Bloomberg;
using System.Diagnostics;
using System.Threading;
using System.Globalization;
using System.Numerics;
using System.Runtime;

namespace Exam70_483
{
    struct MyStruct
    {
        public int foo;
    }
    public class Student
    {      
        public Student(string first, string last, char initial)
        {
            firstName = first;
            lastname = last;
            this.initial = initial;
        }

        public string Name => $"{firstName} {initial} {lastname}";
        public string firstName;
        public string lastname;        
        public char initial;
        public double[] scores => Enumerable.Range(1, 5).Select(x => (double)x).ToArray();        
        public double TestAverage => scores.Average();
        public override string ToString()
        {   
            return $"Student: {Name} has test scores [{ string.Join(",", scores) }]and average score {TestAverage}";
        }
    }

    public class DumbStudent : Student        
    {
        public DumbStudent() : base("John",  "Doe", 'W')
        {

        }
        public bool IsPassing { get; } = false;
    }

    public class SmartStudent : Student
    {
        public SmartStudent() : base("John", "Doe", 'W')
        {

        }
        public bool IsPassing { get; } = true;        
    }

    public class Bomb<T> where T : Student
    {
        public void Bombardeer<U>(T who, U partners) where U : List<Student>
        {
            who.firstName = "Bianca";
            WriteLine( $"The bomber is: { who } and her partner is: {partners.FirstOrDefault()}");
        }
    }

    public class Base
    {
        public Base() : this("Talk to me...")
        {
            WriteLine("Base() constructor performed.");
        }

        public Base(string Hello)
        {
            WriteLine("Base(string Hello) constructor performed.");
        }
    }

    public class Derived : Base
    {
        public Derived()
        {
            WriteLine("Derived() constructor performed.");
        }
    }

    class Program
    {
        static string FirstName { get; set; } = "John";
        static string LastName { get; set; } = "Doe";
        static string Name => $"{FirstName} {LastName}";
        static int[] MyRange => Enumerable.Range( 1, 49).ToArray();
        static string DownAndDirtyName(string f, string l, string nick)
        {
            return $"{f} {l} aka {nick}";
        }

        [DllImport("kernel32.dll", CharSet = CharSet.Auto,
            SetLastError = true)]
        static extern uint GetShortPathName(string lpszLongPath,
            char[] lpszShortPath, int cchBuffer);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        static extern uint GetShortPathName(
         [MarshalAs(UnmanagedType.LPTStr)] string lpszLongPath,
         [MarshalAs(UnmanagedType.LPTStr)] StringBuilder lpszShortPath,
         uint cchBuffer);

        public static async Task GetStockQuoteAsync(string symbol)
        {
            StockQuote result = null;
            var sw = new Stopwatch();
            WriteLine($"Retrieving stock quote for {nameof(symbol)} {symbol.Trim()}.");
            sw.Start();            
            var quote = new StockQuoteServiceClient();
            quote.InnerChannel.OperationTimeout = TimeSpan.FromMinutes(20);
            try
            {
                quote.Open();
                result = await quote.GetStockQuoteAsync(symbol.Trim());
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }   
            finally
            {
                sw.Stop();
                quote.Close();
            }

            var last = 0.0;
            var qresult = double.TryParse(result?.Last, out last);
            var qlast = qresult ? last.ToString("C") : "N/A";
            WriteLine($"{result?.Name ?? "Market Quote ", -40} | Symbol [{result?.Symbol ?? symbol, -6}] | Last [{qlast, -10}] | Elapsed time: [{sw.ElapsedMilliseconds}ms]");            
        }

        public static void ThrowException()
        {
            try
            {
                ExceptionMethod();
            }
            catch (Exception )
            {

                throw;
            }
        }

        public static void ExceptionMethod()
        {
            throw new ArgumentNullException();
        }

        public static void HandleException()
        {
            try
            {
                ThrowException();
            }
            catch (Exception ex)
            {
                WriteLine(ex.ToString());
            }

            try
            {
                ThrowExceptionEx();
            }
            catch (Exception ex)
            {
                WriteLine(ex.ToString());
            }
        }

        public static void ThrowExceptionEx()
        {
            try
            {
                ExceptionMethod();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        static void Main(string[] args)
        {
            HackerRank_FindStrings();
        }

        static void Main1(string[] args)
        {
            if (args != null)
                WriteLine($"{nameof(args)} is NOT null.");

            WriteLine(Name);
            foreach (var i in MyRange)
                WriteLine(i);

            var s = new Student(FirstName, LastName, 'V');
            WriteLine(s);

            var b = new Bomb<Student>();
            b.Bombardeer(s, new List<Student> { s });
            WriteLine($"Current Student {s}");

            //var ds = (DumbStudent)s;
            //WriteLine($"{ds} is passing {ds.IsPassing}");

            //var ss = (SmartStudent)s;
            //WriteLine($"{ss} is passing {ss.IsPassing}");
            
            WriteLine($"Convert.ToInt16(9.5) = {Convert.ToInt16(9.5)} and Convert.ToInt16(10.5) = {Convert.ToInt16(10.5)}");

            int value = 0;
            float fl = 0;
            float.IsInfinity(fl);
            double db = fl;

            switch(value)
            {
                case 1:
                    WriteLine($"{nameof(value)} is {value}.");
                    break;
                default:
                    Write($"{nameof(value)} is NOT 1 but ");
                    break;
            }

            do
            {
                WriteLine($"{ value}.");
            } while( value > 10);

            MyStruct? f = null;
            int? foo = null;

            WriteLine($"{nameof(f)} is {f?.foo ?? 0}");
            WriteLine($"{nameof(foo)} is {foo ?? 0}");

            WriteLine(myfoo.Bar);
            WriteLine(DownAndDirtyName(nick: "Kiss My Ass", l: "Ass", f: "Kiss"));

            WriteLine($"Short Path Name: { InternalGetShortPathName() }");

            WriteLine($"(int)'A' = {'A'}");

            DisplayAlphabetSequentially();
            double d = 1234.54;
            WriteLine(d.ToString("c"));
            WriteLine($"{d:C}");

            WriteLine(string.Format("{2}, {0}, {3}", "vidi", "Venti", "Veni", "vici"));

            var _b = new Base();
            var _d = new Derived();

            Action<string> action = async (string symbol) =>
            {
                await GetStockQuoteAsync(symbol);                
            };

            // Test throw vs throw ex (throw ex cause a loss of the call stack prior to the throw ex.
            //HandleException();
            //BarrierExample();

            var ex = new MoreExamples();
            foreach (var e in ex.SomeExamples)
                e.DoIt();
            ex.DoIt();
            WriteLine($"Sum of Matrix {{3x3 1 2 3}} = {ex.SumMatrixDiag()}");

            var n = 6;
            var ss = "#";
            var fmt = $"{{0,{n}}}";
            for (var i = 0; i < n; i++)
                WriteLine(fmt, ss.PadLeft(i + 1, '#'));

            HackerRank_FindStrings();
            //HackRank_Library();
            //var nums = "0 1 12 14 9";
            //WriteLine("{0}", nums.Split(' ').Select(int.Parse).AsQueryable().Sum());

            try
            {
                StartQuoteBox(action);
            }
            finally
            {
                ReadLine();
            }                                               
        }

        private static void HackRank_Library()
        {
            var eDt = DateTime.Now;
            var aDt = DateTime.Now;

            var r = DateTime.TryParseExact("9 6 2015", "d M yyyy", null,
                                       DateTimeStyles.None, out aDt);
            r = DateTime.TryParseExact("6 6 2015", "d M yyyy", null,
                                       DateTimeStyles.None, out eDt);
            if (aDt <= eDt)
            {
                Console.WriteLine("0");
                return;
            }

            var dt = aDt - eDt;
            WriteLine(dt.ToString());
            var v = DateTime.Today.AddYears(1) - DateTime.Today;
            
            var result = 10;            
            var bi = new BigInteger(result);

            for(var i = 1; i <= result - 2; i++)
                bi *= new BigInteger(result - i);
            
            WriteLine($"n! result of {result} = {bi}");

            //144.98906270 25.07352606 164.87604770 25.10565434 

            //var res = Math.Sqrt(Math.Pow(Math.Sqrt(Math.Pow((144.98906270 - 25.07352606), 2D)) - 
            //          Math.Sqrt(Math.Pow(164.87604770 - 25.10565434, 2D)), 2D));

            var res = Math.Sqrt(Math.Pow(Math.Sqrt(Math.Pow((144.9891 - 25.0735), 2D)) -
                      Math.Sqrt(Math.Pow(164.8760 - 25.1057, 2D)), 2D));

            //WriteLine($"{Calc(9, 1, 7)}");
            //WriteLine($"{CalcFib(7)}");

            //WriteLine($"{Calc(741695401, 245789563, 88713108)}");  // 741695401 245789563 88713108  = 276306136

        }

        public static BigInteger Calc(long a, long b, int n)
        {            
            var n2 = new BigInteger(b);
            var fib = new BigInteger(b + a);
            var n1 = fib;

            for (int i = 3; i <= n; i++)
            {
                n1 = fib;
                fib += n2;
                n2 = n1;
            }

            return n == 1 ? b : n == 0 ? a : BigInteger.ModPow(10, 9, fib) + 7;
        }

        public static double CalcFib(int n)
        {
            Double part1 = (1 / Math.Sqrt(5));
            Double part2 = Math.Pow(((1 + Math.Sqrt(5)) / 2), n);
            Double part3 = Math.Pow(((1 - Math.Sqrt(5)) / 2), n);
            return (part1 * part2) - (part1 * part3);

        }

        private static void StartQuoteBox(Action<string> action)
        {
            var someValue = "-q";
            do
            {
                WriteLine($"Enter one or more comma seperated stock symbol(s) for quote(s)\nor -nasdaq | -djia to use a default symbol set\nor type -q to exit.");
                someValue = ReadLine();
                
                if (someValue != "-q" && !string.IsNullOrEmpty(someValue))
                {
                    var symbols = someValue.Split(',');
                    
                    if (someValue.StartsWith("-nasdaq"))
                        symbols = SymbolLibrary.StockSymbols;
                    else if (someValue.StartsWith("-djia"))
                        symbols = SymbolLibrary.DJIA30;

                    Parallel.ForEach(symbols, x => action(x));                    
                }

            } while (someValue != "-q");

            WriteLine("Exiting QuoteBox...");
        }

        public static void BarrierExample()
        {
            var participants = 5;

            // We create a CancellationTokenSource to be able to initiate the cancellation
            var tokenSource = new CancellationTokenSource();
            // We create a barrier object to use it for the rendez-vous points
            var barrier = new Barrier(participants,
                b => {
                    WriteLine("{0} paricipants are at rendez-vous point {1}.",
                                    b.ParticipantCount,
                                    b.CurrentPhaseNumber);
                });

            for (int i = 0; i < participants; i++)
            {
                var localCopy = i;
                Task.Run(() => {
                    WriteLine("Task {0} left point A!", localCopy);
                    Thread.Sleep(1000 * localCopy + 1); // Do some "work"
                    if (localCopy % 2 == 0)
                    {
                        WriteLine("Task {0} arrived at point B!", localCopy);
                        barrier.SignalAndWait(tokenSource.Token);
                    }
                    else
                    {
                        WriteLine("Task {0} changed its mind and went back!",
                                        localCopy);
                        barrier.RemoveParticipant();
                        return;
                    }
                    Thread.Sleep(1000 * localCopy + 1);
                    WriteLine("Task {0} arrived at point C!", localCopy);
                    barrier.SignalAndWait(tokenSource.Token);
                });
            }

            WriteLine("Main thread is waiting for {0} tasks!",
                                barrier.ParticipantsRemaining - 1);
            WriteLine("Press enter to cancel!");
            ReadLine();
            if (barrier.CurrentPhaseNumber < 2)
            {
                tokenSource.Cancel();
                WriteLine("We canceled the operation!");
            }
            else
            {
                WriteLine("Too late to cancel!");
            }
            WriteLine("Main thread is done!");
        }

        private static string InternalGetShortPathName()
        {
            // Get the long file name.
            string longName = @"C:\Program Files (x86)\Microsoft Analysis Services\AS OLEDB";

            // Allocate a buffer to hold the result.
            char[] buffer = new char[1024];
            int capacity = 1024;
            var sb = new StringBuilder(capacity);
            long length = GetShortPathName(longName, buffer, buffer.Length);

            // Get the short name.     
            return new string(buffer);
        }

        public partial class Foo
        {
            public int Bar { get; } = 99;
        }

        static Foo myfoo { get; } = new Foo();

        public static void  DisplayAlphabetSequentially()
        {
            var letters = new StringBuilder("ABCDEFGHIJKLMNOPQRSTUVWXYZ");
            var result = new StringBuilder();
            for(var i = 0; i < 26; i++)
                result.AppendLine($"{letters.ToString(0, i + 1)}");            

            WriteLine(result.ToString());
        }

        public static void HackerRank_FindStrings()
        {            
            var s1 = new string[] { "wwylgbplrqsaurkdrznqudinktphqmqcihjvgenampyjrivcudlyceobsoaqzxbzqcjfkrrapgvvvzurcssmnflbnwuyfejlstqbrrytwcaqfouutoqefjrelnswaszlwjnajdolsxxxrerxocspfvhekawnuaaorowslfirqbyhljtexrkxpgdxltaoqgvwnobezjitdfllhkbldghrfrhmvknoycjostzpsxczrgvgccdnpzncmayqzmkhxraafyidkmqlwvuczybufvduhtkibzwmfithwlaxjlprsjuctpwobyebwjbsvjlhpwpjsalblnzhsigproikectugqldhfjzsfzdbbhfuxxzmpopeanwvgqmkxljbxckasbmdmbqkvurlplbbayudbppbkjkdgumvivyxikdvebmvfqxbgyywjxjuysrlvruyxnmcxxlnbwovrfrcktqzpnnqcpnipyihcqjclkwjjzcbe",
                                    "yglnaawhbwzewrfnbbqhzexnjjetgprxnihdayyeegbknueszwtsqhxovicgzriogancljkmfzxavwxdglgeiuqnwhkvpesmtgdlggtvacxvmwjbklzqejhmkduxrttvhakxvwwbedybqqmodhsfpinztxbitznrfthezvfggzrvzyrllmkvoeaxuohdgyhelhfvkynlxaiozjgcazyelemgjmcotqazkdymjhiwmndiycceyywmbshtkxgdcxvlkxvknzqulxmpbkmgyckwovwiwmkbaaljlgamnmfnyhhsjsoijfbaqxxhpbghcxkvgpwnwokpaxtxosxpbrsydoqdzmfyiwswiojmdbfzdsmoecbsqjkoonvgpxlqpeksnvhubrhaoklakjbzkremkiwicbbmxcgmzbie",
                                    "hupzcdzuxkqfkhembdpayujrhneyvenkbohjojrowxaejkuzgqddpnqkhreetwjbmjlewdrlwvvhvmrwtbgddzdlakcjwumfcsqkrpjjpdvfleqqyxevoufnfdqjmxkmtsbqugebihxrsovggmisbdikviapcfiphmjjudpztvgvouprfrvmtvjcglilgihupfcovavzqyypjiuaktmeetnnlipxsmafyabpobssrpnjgogfhrdvqydczakrpcmrwbyxghojttyajjatcelnavqxidyewnrhxpquewjlntudilxxifdyzgdwctoinjqjmejlsirjyrrofprerificdi",
                                    "gdnltjqkmkfikunodybcvppmxigyeoultyjyvooqkmshwqqqjvwpvcfuqqklovubtwuzfomuzzukksrgpwvlpjcksbuqopmzaxwkaoicbzhoddxpgnncibqnymubflvbyyluwvborzlbntlgkdwsyxcjhvxjkayhscvuxyotilzlesbzrsngfzyhipfimnemgvokhgnuxtgjdjeigskewguhqneqbaxxxiooahhofrqxmmgqzncnxdoetooqmwgyqaaleqwxvcaefhakhddjzhqwnsdlamkgypqsjsfpduqyyrizszepxezvevynjjtnmoeftcjjfranqtnrwulcujchaybfowsvusmyjondlfgoxw",
                                    "cnyqnxolhmwtirppugboskcpjasnglrwzvtflzgddlawpeeyrbrfyhqgguhzfboymtunykutcptzhelqljqaejcomsxkgakguvgqvvfndawlihsawqjpezumwvygzvfeplxeinperrbiewkhfpjysvlybagygdjraetzdpzrmqllvyfjetpismcfhapodbdivxcdyqskecadwakteqkklbxiyiqjippbrxbmeugpvyrhfkjwnazkaxlcyrzkqogbxdbjxpyqysprcusfcbdiczebualkorwlmplwnrbihbovgwwenyamwqjhbxhofenpwqavqggkzeoazftfscpuurhitkyfetiynlzwqmtzqzbhsvasabwzegepfzedrkjnyziuakkxercrdrokrtjcyekgfhwmtjjmmkpvewewqysjbplypkfztzgisdbkvlptrfcfylzwaeqbxhqixfagapuhuqqmzjcbipzpiffwoybuq",
                                    "vfkffmzueolmkbqrhvjdxjvhhfpkduepmdcsjaaecihehkofkyelzotcmgcjaxppxkjjzwyeohxuahdmtvchdpdseuxxsrnbvobmqxdzqxqwvfmitokldbjgfcmxkizyldzobdfwtqxemezhvtysbuhyajhnblbljkcczmzeqioziucolhcmokkmvrkumtbrzzrxilpmcknamuaewitncmoukzzyojkhjnbpscotyjurseinjewngvltctuwvelgarknbuidc",
                                    "eupaiuagbcumdcnowludlhewkhrqlwktuppmrsqjgzjhgtwtlpzbdrjfyhchlfsxplbhkulxjxuovonnxiwznzcfhihymjjatlrdqotsnxpytkudinbksiikcmkesawuheyncuuayostpmrrxvdtfbkxvpvwpbkgazllefohimmagccaajgwjzjnoqhatnpglruxhvmfvxbsbfzgiacaehjcwlhmevqeixxhnavfzrigmssxcjvrxqdhpgrjcwoarhbqiemzfeeicmyjufybkfqsranqfczngtzijhnfojnffrigyurkxurggzrcfhiqjqncpwsbdrlmcethqmtjtmjcbgslrnhh",
                                    "euhmbcmbkkdznxtkeaxfriukcehjadcrswcwekznpaeyfmqmmeyafhmpugbxfjmhxywffmphxnheonemtbwwufvbdpghuqrjzaqfzsleodywxslrlfnpdejjqirylpfeydufluxiezggyydhfoiknjorisiwaeaktzcngfczrufqahrtkgmjhfzoqzpnzblywiqblzskpynftqrjznglcbzwjhliqjuqerpcgyoqrecrwspujcotgpsiinhzj",
                                    "chkmwaumrfgwlsxmhmnbmdasyedkzmediyycdhiflzzhnjdxvcrnxxowvtosgxtcsfytqxvxovvqzcvfuktoumwbqyfztgtgteltcnqtuszabsddmwreibgqxzdbscovlffzxnplvlyafozxxqueeyjifdvbnuitungksrstqmzplgqoniaahusicquanhghcjyiekqtoantrnhfxogkmrnxavlvmwqffkhlhstlztugdcjssrjybtalckavlhnesecwiuivupsbwblomznxndozxpqwmsyxchvvbydmetkxkgawafwwuobfhvqaobkhkucvkbjfxkxntxgwapoqsakcqqvmvwwtowsucirntdqrniqimsodtxpoakpydvelzliugwftdfxueke",
                                    "vsrvnwhdbtmfmdmjptbdbmcdmnxiktwlxivzohpzunqzdnflrbulhicxwpnyftlnwfjyalpmdeitcnnfhebhegjpvvtjdwpscugyzwjbehiffskblsbcireiyersaqjpygkwvbcappbjreoggwznoikvcxybuqrkjtwggpkejsrogxcjkfwbmrtzacbcdsxorlbwzbrnpoeygqxxtcwulbnvexcjxulkycksufldpjnzvcjgfctnlxvchtgcpdraivkqazbqeedpaxzsmcfeuiykbcivpqflllanlfibyppisoevmaxevvpqhmoipxtniddibtnqajlkuludveoqnntupfnaokuyskntsfxwgejrxwkhsawlgzkkiirokegrxulcrauiqibgt",
                                    "lafmllwsdrzcvqsfknxvnippnyrjigpwpgmymyvomkmxoulquqciwsezdbokzybbwkxkxhsegeuxunspblheztbussfkksiblcorhuovjhuafukwitzvxodqwuzbeambzfcjknooctfhagkqkeavvooorcppkbzgkyhmrutbclcrksphmdvkypvrumuzzkbtioankndpodsytgemehfdymbaytklkoowzoqltjxvctgtkqttargucrzyguyijrpmgwckhnngekadfdcmvrwnryqgmkktcoqvxovtfskstcmhgfffvppqwmcyyapaagbufiuwdhypdqmvuviinkwmypinqgxdzvzwvfkhcxkbdonycodnzcmpekxblvcuififqknyfltssuiymogezfmmjlonnxhrovpjmzsesfewsrhqmskdmclockxbkrvekbkaufflmzurnirybvjziilorvuftozgcyqfhhizacqthssvacl",
                                    "qtbhluhllshmzioplmzfffmduzvkzwofrmzlwzrotgfsabkawqkvdlttzqqxkxgduepjxwrmopunpjnuuebqxqammbiydopfquvfqaopiwvbqujhwufqltjztrdbnsxzoimkjtbjydvbrevrcgkhvluvevrzglryjtqnhorrnroaleevoteclsknlzmapvbvzifhgyjrvblywhzyjtcgxsxmsdhdaugrunalkuhfpdgakcyjfdmxyaothobixbajwzmmqabizykleaitergstbxbmjyotxotthkgukmikfv",
                                    "yifibrlwjbvbdvyavequctlharybpjajdvmfcubvspnesulcunktduxiovijgfriojefrllssbggerwceenvryvdndtimjtxddcprtzheuztmtqaiyjlfvwohsgzpprjwrfartbhporgwfeokxthcolrxyvenhpkylwademwvvejnsmbiadlqqmauocotpjfrcofzamagpqrfnfsybwcsxoeegtnevnmgktozaqyjcfpdfsaadifiqgojwwdljpbzltzcqwtnaocnchbcjmpeaaogdampmpbarwpuqxbcidmwiygaqmeptawycoascpayvewohtxlfwiqocbuytwyhdluyifpjgyiigutheuddboqkavnjadzyhqrdzgfqfjefshm",
                                    "lqagwxqsukealzifrzhkzghlkjsxuoxdssqnrfqfwvppeokmvglcwleqbyvystqumegaabzykwsflnnvgljrzdnznwwpgcgybpxobaxvuskikiuhjqnympqlkdoxwqnxfmavdttlygojzgtoutjdapadjanyqfydbgcvwzyqqhaadohqkexyycekhyiyjegiyntnykplrjvsllmajlkgnfrzmtatdceiulhuucksmovhlmapkurvbjdnqyrzslhnlkvwnphshqdgrtwxxixaak",
                                    "nmoguiqbqqqiyewbacvdcnynzvlcpjxacreqpgpuwptalnkvoyqyszgmqhltsehyexgvrhxrfbuevrvgrixieiuhduzijveivpuygiiydscshmcalgxyujbcqvddvxjsygrmppjstwvqjylzkdasbksftomyvggzfeettvtbpeykmvjyqjiqlsjenigaifrcdgjwuocfvejpooefzbpxhlyjrngypuzikgihxllhoyysuhkqrivcmitzuzkfkzauwqgdgxxuhpsvvqnhdgpuizbifandazttkvgketgegzjeiilzviixkpamuigqqhsdcqoozaujtnyfryumssearxgzbrlsszcsejoqkiumfxugystyjadzltetimzrvbneoytrbwywkocmyosvtektmgzlgxcwofieqqqjcihnzjwwir",
                                    "pvkxgwfbaksttgsoknandjcbglzgdtvzumgczuekjotevdjfkxmtxotdgizrrsuyaosxvtxztebvzyasolqegpeztjeyhucwgeuzqfemhfjdxttjgczrtawcdsrxndrjxukhpjxoamlglaqifzsvgxtctgekqdvpptzpwqdogeetbxmylypogscxwzywvpvlwspneoqmryymwjzsxtbzyeisvlitldhgjzgmythasnxuobwwmmcwlzgojrbfvlhirhrwuhkjctnwoyfjbtruzirekvxvwcddktfqsmsetcxipfyxuvqyerjfxrumxsrufpmtjrmbxqyhnwhkyjuxkhuhyegljyfxpzlumyqfujkznmndxfrmzuymdpcvrqxasomriutlpokjkxpzn",
                                    "btiorbvzeuwzaadiqkqgkgxcahzdzbmxskcgpkacmghsuvdqtqswdllqvozmdalygmdydgtglfnbkmtnwsveqjmrxefmxshrrsorvhgxzrapitnklswppnjmzcqkedoldfzogushkmqpybpucawgdwoukasjyrduiuwdugidlkivcygxuuogvlsfiworsoiwnjekkoizpkkyqzzbokjvhgpsswqhzcztijxaytimncffcpttbnjslegdmwdicrsssvtnurpztesckmtawixfqsetzzsvpejnaooctyhnunxrlpkvnrlguucxoisaloxgidkiixoghornmfegskhupzruqsohthghbrzikbswsipvlfjmzgovzlrqimgrmyaamdivkoviteclksblrcpmjbzonvxccmmaakofhmfyuddljhtfguwsrdhfjalfxmdyfbhzjweftwcydje",
                                    "pfhnydjkdprqhnqmdghtlstkjgdpriwvxypdykhwcbuyutzndtzzdznpmgtxjaqtfxziihbsxoysvuscnsakqgswtkjnxrbvyvmjnaeqkbemqufvagaabmsgtthgqtepymrrtfpddikxrxvcbfztexdpmgckoiciyzzxelrvkifphlytnbgtsofidxvafyzpznzvbgwzgvghpywurgzvjkwwolsuimsjsjjktqikkqnlijozeewnzrokidecjbksxziussrmtczdhkjaprhpriovuqofybnefpmafqylrrwzlqrjwtbmovxqjkfrkknbxhjqsnkhyudqjravuhxmdywdglwfuoefxbbewyktnaombgb",
                                    "xslnmjmqepijaeepgpbgvpaypynekikgosbttdhkfpvccgxlzwgyewcmvabqbfdemwxevcknxgdjwwnbpamgsjcyguohehkruhbcquhrkeonvcpsqwkbacchtesrupvygdhorodpvxmmivzfcfyvtpqgytxlkzuemshdwpsbiyyncnpotjpscectvxflnndkskauqsmhufxkxxpughrnftdcnnkfjhjqsaubtgdozhkelfzltvevlevxrdviolosucxhdukmyedctoxyfoallikbdqyobtkcmerfpoyvbklcceqg",
                                    "xjhzgjdlzpqlkvouwuhsyjbghcrsyswmhqcmvlekpaxplwofoopbuazubapoptypouwdzifyhmhzdlebcbtqjjmmbuccsbsadeuswpuxioizkdmxliepghmtxktqtnkicfxjoomxjqrecjwhinbgqxdictufugeqtlfwzfoodpdhjpmjmtfwcakwtjxniwmvdnequgckrrojkwqaxqzuzbhslkatipycnquhlwiqiptgyvsifkwbuozpacwanshqhplepdkdllprfwfwdgeztfwwxwfqtroocwgokgcnxdsjvknaaexhiaadgjbinccnhjgniasgtkzwzifikxcwyryghosliqrvfjfxmxvlkxmqurehonsnpknauuhrjidvmweshexhyjkcnmibluewvfdnmjntagzkbxzlbtetqawumnjafeqmyhcvzkyumneqejyhuljtnsnajyckuh",
            };
            var q = new int[] { 8257,   21071,  8340,   13244,  25744,  19772,  751,    493,    1711,   15042,  10114,  10698,  28448,  14868,  23084,  31189,  22973,  10075,  29229,  21492,  3627,   13689,  8417,   20634,  29104,  24852,  5760,   7128,   5992,   29030,  46315305,   30726,  31773,  29464,  3755,   55430551,   30830,  29413,  5789414,    30569,  50380212,   21839,  23097,  31356,  30266,  16224,  31383,  6527,   4330,   57647773,   14147,  10988,  5445,   48655706,   7462,   15957,  18415,  621,    18090,  20551,  5866,   15774,  43112651,   21789,  17807,  11571,  278,    36337806,   20570894,   19673,  8073,   28289,  15224,  18783,  23565,  17017,  11826,  32125,  10285,  14165,  15632,  18433,  24266264,   23131,  12201,  23118,  18546,  28386,  1028,   20695,  16804,  22970,  18053,  7158,   11161,  7765,   28778,  15972,  27299,  17377 };

            //Find String Implemenation
            Soluction.DoIt(new string[] { "aab", "aac" }, new int[] { 3, 8, 23 });


            var ul = new List<string>();
            IEnumerable<string> query = new List<string>();
            // Add last char
            var sb = new StringBuilder();

            foreach (var s2 in s1)
            {

                var len = s2.Length;
                var result = new string[len];
                for (int i = 0; i < len; i++)
                {
                    result[i] = (s2.Substring(i, len - i));
                }
                Array.Sort(result, string.Compare);
                ul.AddRange(result);
                
                //query = query.Concat(
                //                (from j in Enumerable.Range(0, s2.Length - j)
                //                let s = s2.Substring(j, s2.Length - j)
                //                orderby s
                //                select s).Distinct()
                //          );

                //query = from i in Enumerable.Range(0, s2.Length)
                //             from j in Enumerable.Range(0, s2.Length - i + 1)
                //             where j >= 1
                //             select new string[] { s2.Substring(i, j) };
                //ul.AddRange(query1);
                //var sets = $"s2.Length: {s2.Length} sets: {query1.Count()} unique sets: {query1.Distinct().Count()}";
                //query = query.Concat(query1);
                // query = query.Concat(query1);               
            }
            


            // Union distict sets
            // Perform lex query for results e.g. sort, then select by length                   
            //ul.Sort();
            //ul = ul.Distinct().ToList();
            //var count = ul.Count();
            //query = query.OrderBy(s => s);            
            var count = query.Distinct().Count();            
            
            foreach (var v in q)
            {
                if (v - 1 < count)
                    WriteLine($"{query.ElementAt(v - 1)}");
                //    WriteLine($"{ul[v - 1]}");
                else
                    WriteLine("INVALID");                
            }
        }

        //public static List<T[]> CreateSubsets<T>(T[] originalArray)
        //{
        //    List<T[]> subsets = new List<T[]>();

        //    foreach (var s2 in s1)
        //    {
        //        var query1 = from i in Enumerable.Range(0, s2.Length)
        //                     from j in Enumerable.Range(0, s2.Length - i + 1)
        //                     where j >= 1
        //                     select s2.Substring(i, j);
        //    }

        //        for (int i = 0; i < originalArray.Length; i++)
        //    {
        //        int subsetCount = subsets.Count;
        //        subsets.Add(new T[] { originalArray[i] });

        //        for (int j = 0; j < subsetCount; j++)
        //        {
        //            T[] newSubset = new T[subsets[j].Length + 1];
        //            subsets[j].CopyTo(newSubset, 0);
        //            newSubset[newSubset.Length - 1] = originalArray[i];
        //            subsets.Add(newSubset);
        //        }
        //    }

        //    return subsets;
        //}

        public static int FindDistinctEx(StringBuilder sb, string input)
        {            
            sb.AppendLine(input);
            return 0;
        }

        public static List<string> FindDistinct(string input)
        {
            var letters = new StringBuilder(input);
            var results = new List<string>();
            for (var i = 0; i < input.Length; i++)
                results.Add(letters.ToString(0, i + 1));            
            return results;
        }

        //Chapter 2 Answers: d, d, a, b, a, a, b, c, c 100% correct... The book has blatent errors.
        //Chapter 3 Answers: noa, true, b, true, a, b, a, b, b, a c, b, a, b, c, b     100% correct... The book has blatent errors.
        //Chapter 4 Answers: 1) c
        //                   2) d  incorrect
        //                   3) b  
        //                   4) c incorrect 
        //                   5) c incorrect
        //                   6) a 
        //                   7) d 
        //                   8) c 
        //                   9) a 
        //                  10) c 
        //                  11) d 
        //                  12) a
        //                  13) c
        //Chapter 5 Answers 
        // 1) C
        // 2) B
        // 3) A
        // 4) B
        // 5) D
        // 6) B
        // 7) C
        // 8) C
        // 9) B A Incorrect
        //10) D
        //11) C
        //12) D
        //13) B
        //14) A
    }
}
