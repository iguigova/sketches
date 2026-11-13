using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Scripting.Math;

namespace Euler
{
    class EulerConsole
    {
        static void Main(string[] args)
        {
            DateTime date = DateTime.Now;
#region Prev
            // Console.WriteLine(Sum1(1000000, new int[]{3, 5}, Op.Mod));
            // Console.WriteLine(Sum2(4000000, new int[]{2}, Op.Mod));
            // Console.WriteLine(Sum3(1000000001));
            // Console.WriteLine(GetLargestPrimeFactor(13195, new int[] { 1 }));
            // Console.WriteLine(GetLargestPrimeFactor(1000000, new int[] { 1 }));
            // Console.WriteLine(GetLargestPrimeFactor(600851475143, new int[] { 6, 4, 2, 4, 2, 4, 6, 2 }));
            // Console.WriteLine(GetLargestPalindromic(3));
            // Console.WriteLine(GetNthPrimeNumber(1000000));
            // Console.WriteLine(GetGreatestProductFromConsecutiveDigitsInNumber("7316717653133062491922511967442657474235534919493496983520312774506326239578318016984801869478851843858615607891129494954595017379583319528532088055111254069874715852386305071569329096329522744304355766896648950445244523161731856403098711121722383113622298934233803081353362766142828064444866452387493035890729629049156044077239071381051585930796086670172427121883998797908792274921901699720888093776657273330010533678812202354218097512545405947522435258490771167055601360483958644670632441572215539753697817977846174064955149290862569321978468622482839722413756570560574902614079729686524145351004748216637048440319989000889524345065854122758866688116427171479924442928230863465674813919123162824586178664583591245665294765456828489128831426076900422421902267105562632111110937054421750694165896040807198403850962455444362981230987879927244284909188845801561660979191338754992005240636899125607176060588611646710940507754100225698315520005593572972571636269561882670428252483600823257530420752963450", 5));
            // Console.WriteLine(GetProductFromPythagoreanTriple(1000));            
            // Console.WriteLine(GetPrimeNumberSum(2000000));
            // Console.WriteLine(GetSumFromDigitsInNumber(2, 1000));
            // Console.WriteLine(GetSumFromDigitsInNumber(GetFactorial(100).ToString()));
            // Console.WriteLine(GetSumFromDigitsInBigNumber("37107287533902102798797998220837590246510135740250463769376774900097126481248969700780504170182605387432498619952474105947423330951305812372661730962991942213363574161572522430563301811072406154908250230675882075393461711719803104210475137780632466768926167069662363382013637841838368417873436172675728112879812849979408065481931592621691275889832738442742289174325203219235894228767964876702721893184745144573600130643909116721685684458871160315327670386486105843025439939619828917593665686757934951621764571418565606295021572231965867550793241933316490635246274190492910143244581382266334794475817892575867718337217661963751590579239728245598838407582035653253593990084026335689488301894586282278288018119938482628201427819413994056758715117009439035398664372827112653829987240784473053190104293586865155060062958648615320752733719591914205172558297169388870771546649911559348760353292171497005693854370070576826684624621495650076471787294438377604532826541087568284431911906346940378552177792951453612327252500029607107508256381565671088525835072145876576172410976447339110607218265236877223636045174237069058518606604482076212098132878607339694128114266041808683061932846081119106155694051268969251934325451728388641918047049293215058642563049483624672216484350762017279180399446930047329563406911573244438690812579451408905770622942919710792820955037687525678773091862540744969844508330393682126183363848253301546861961243487676812975343759465158038628759287849020152168555482871720121925776695478182833757993103614740356856449095527097864797581167263201004368978425535399209318374414978068609844840309812907779179908821879532736447567559084803087086987551392711854517078544161852424320693150332599594068957565367821070749269665376763262354472106979395067965269474259770973916669376304263398708541052684708299085211399427365734116182760315001271653786073615010808570091499395125570281987460043753582903531743471732693212357815498262974255273730794953759765105305946966067683156574377167401875275889028025717332296191766687138199318110487701902712526768027607800301367868099252546340106163286652636270218540497705585629946580636237993140746255962240744869082311749777923654662572469233228109171419143028819710328859780666976089293863828502533340334413065578016127815921815005561868836468420090470230530811728164304876237919698424872550366387845831148769693215490281042402013833512446218144177347063783299490636259666498587618221225225512486764533677201869716985443124195724099139590089523100588229554825530026352078153229679624948164195386821877476085327132285723110424803456124867697064507995236377742425354112916842768655389262050249103265729672370191327572567528565324825826546309220705859652229798860272258331913126375147341994889534765745501184957014548792889848568277260777137214037988797153829820378303147352772158034814451349137322665138134829543829199918180278916522431027392251122869539409579530664052326325380441000596549391598795936352974615218550237130764225512118369380358038858490341698116222072977186158236678424689157993532961922624679571944012690438771072750481023908955235974572318970677254791506150550495392297953090112996751986188088225875314529584099251203829009407770775672113067397083047244838165338735023408456470580773088295917476714036319800818712901187549131054712658197623331044818386269515456334926366572897563400500428462801835170705278318394258821455212272512503275512160354698120058176216521282765275169129689778932238195734329339946437501907836945765883352399886755061649651847751807381688378610915273579297013376217784275219262340194239963916804498399317331273132924185707147349566916674687634660915035914677504995186714302352196288948901024233251169136196266227326746080059154747183079839286853520694694454072476841822524674417161514036427982273348055556214818971426179103425986472045168939894221798260880768528778364618279934631376775430780936333301898264209010848802521674670883215120185883543223812876952786713296124747824645386369930090493103636197638780396218407357239979422340623539380833965132740801111666627891981488087797941876876144230030984490851411606618262936828367647447792391803351109890697907148578694408955299065364044742557608365997664579509666024396409905389607120198219976047599490197230297649139826800329731560371200413779037855660850892521673093931987275027546890690370753941304265231501194809377245048795150954100921645863754710598436791786391670211874924319957006419179697775990283006991536871371193661495281130587638027841075444973307840789923115535562561142322423255033685442488917353448899115014406480203690680639606723221932041495354150312888033953605329934036800697771065056663195481234880673210146739058568557934581403627822703280826165707739483275922328459417065250945123252306082291880205877731971983945018088807242966198081119777158542502016545090413245809786882778948721859617721078384350691861554356628840622574736922845095162084960398013400172393067166682355524525280460972253503534226472524250874054075591789781264330331690"));
//            Console.WriteLine(Sum4(new string[]{
//"37107287533902102798797998220837590246510135740250",
//"46376937677490009712648124896970078050417018260538",
//"74324986199524741059474233309513058123726617309629",
//"91942213363574161572522430563301811072406154908250",
//"23067588207539346171171980310421047513778063246676",
//"89261670696623633820136378418383684178734361726757",
//"28112879812849979408065481931592621691275889832738",
//"44274228917432520321923589422876796487670272189318",
//"47451445736001306439091167216856844588711603153276",
//"70386486105843025439939619828917593665686757934951",
//"62176457141856560629502157223196586755079324193331",
//"64906352462741904929101432445813822663347944758178",
//"92575867718337217661963751590579239728245598838407",
//"58203565325359399008402633568948830189458628227828",
//"80181199384826282014278194139940567587151170094390",
//"35398664372827112653829987240784473053190104293586",
//"86515506006295864861532075273371959191420517255829",
//"71693888707715466499115593487603532921714970056938",
//"54370070576826684624621495650076471787294438377604",
//"53282654108756828443191190634694037855217779295145",
//"36123272525000296071075082563815656710885258350721",
//"45876576172410976447339110607218265236877223636045",
//"17423706905851860660448207621209813287860733969412",
//"81142660418086830619328460811191061556940512689692",
//"51934325451728388641918047049293215058642563049483",
//"62467221648435076201727918039944693004732956340691",
//"15732444386908125794514089057706229429197107928209",
//"55037687525678773091862540744969844508330393682126",
//"18336384825330154686196124348767681297534375946515",
//"80386287592878490201521685554828717201219257766954",
//"78182833757993103614740356856449095527097864797581",
//"16726320100436897842553539920931837441497806860984",
//"48403098129077791799088218795327364475675590848030",
//"87086987551392711854517078544161852424320693150332",
//"59959406895756536782107074926966537676326235447210",
//"69793950679652694742597709739166693763042633987085",
//"41052684708299085211399427365734116182760315001271",
//"65378607361501080857009149939512557028198746004375",
//"35829035317434717326932123578154982629742552737307",
//"94953759765105305946966067683156574377167401875275",
//"88902802571733229619176668713819931811048770190271",
//"25267680276078003013678680992525463401061632866526",
//"36270218540497705585629946580636237993140746255962",
//"24074486908231174977792365466257246923322810917141",
//"91430288197103288597806669760892938638285025333403",
//"34413065578016127815921815005561868836468420090470",
//"23053081172816430487623791969842487255036638784583",
//"11487696932154902810424020138335124462181441773470",
//"63783299490636259666498587618221225225512486764533",
//"67720186971698544312419572409913959008952310058822",
//"95548255300263520781532296796249481641953868218774",
//"76085327132285723110424803456124867697064507995236",
//"37774242535411291684276865538926205024910326572967",
//"23701913275725675285653248258265463092207058596522",
//"29798860272258331913126375147341994889534765745501",
//"18495701454879288984856827726077713721403798879715",
//"38298203783031473527721580348144513491373226651381",
//"34829543829199918180278916522431027392251122869539",
//"40957953066405232632538044100059654939159879593635",
//"29746152185502371307642255121183693803580388584903",
//"41698116222072977186158236678424689157993532961922",
//"62467957194401269043877107275048102390895523597457",
//"23189706772547915061505504953922979530901129967519",
//"86188088225875314529584099251203829009407770775672",
//"11306739708304724483816533873502340845647058077308",
//"82959174767140363198008187129011875491310547126581",
//"97623331044818386269515456334926366572897563400500",
//"42846280183517070527831839425882145521227251250327",
//"55121603546981200581762165212827652751691296897789",
//"32238195734329339946437501907836945765883352399886",
//"75506164965184775180738168837861091527357929701337",
//"62177842752192623401942399639168044983993173312731",
//"32924185707147349566916674687634660915035914677504",
//"99518671430235219628894890102423325116913619626622",
//"73267460800591547471830798392868535206946944540724",
//"76841822524674417161514036427982273348055556214818",
//"97142617910342598647204516893989422179826088076852",
//"87783646182799346313767754307809363333018982642090",
//"10848802521674670883215120185883543223812876952786",
//"71329612474782464538636993009049310363619763878039",
//"62184073572399794223406235393808339651327408011116",
//"66627891981488087797941876876144230030984490851411",
//"60661826293682836764744779239180335110989069790714",
//"85786944089552990653640447425576083659976645795096",
//"66024396409905389607120198219976047599490197230297",
//"64913982680032973156037120041377903785566085089252",
//"16730939319872750275468906903707539413042652315011",
//"94809377245048795150954100921645863754710598436791",
//"78639167021187492431995700641917969777599028300699",
//"15368713711936614952811305876380278410754449733078",
//"40789923115535562561142322423255033685442488917353",
//"44889911501440648020369068063960672322193204149535",
//"41503128880339536053299340368006977710650566631954",
//"81234880673210146739058568557934581403627822703280",
//"82616570773948327592232845941706525094512325230608",
//"22918802058777319719839450180888072429661980811197",
//"77158542502016545090413245809786882778948721859617",
//"72107838435069186155435662884062257473692284509516",
//"20849603980134001723930671666823555245252804609722",
//"53503534226472524250874054075591789781264330331690"}));
 
            // Console.WriteLine(GetLargestProductInMatrix(2, 1, 3, 2, "1 1 4 1 5 1"));
//            Console.WriteLine(GetLargestProductInMatrix(4, 2, 20, 20,
//"08 02 22 97 38 15 00 40 00 75 04 05 07 78 52 12 50 77 91 08 " +  
//"49 49 99 40 17 81 18 57 60 87 17 40 98 43 69 48 04 56 62 00 " + 
//"81 49 31 73 55 79 14 29 93 71 40 67 53 88 30 03 49 13 36 65 " + 
//"52 70 95 23 04 60 11 42 69 24 68 56 01 32 56 71 37 02 36 91 " + 
//"22 31 16 71 51 67 63 89 41 92 36 54 22 40 40 28 66 33 13 80 " + 
//"24 47 32 60 99 03 45 02 44 75 33 53 78 36 84 20 35 17 12 50 " + 
//"32 98 81 28 64 23 67 10 26 38 40 67 59 54 70 66 18 38 64 70 " + 
//"67 26 20 68 02 62 12 20 95 63 94 39 63 08 40 91 66 49 94 21 " + 
//"24 55 58 05 66 73 99 26 97 17 78 78 96 83 14 88 34 89 63 72 " + 
//"21 36 23 09 75 00 76 44 20 45 35 14 00 61 33 97 34 31 33 95 " + 
//"78 17 53 28 22 75 31 67 15 94 03 80 04 62 16 14 09 53 56 92 " + 
//"16 39 05 42 96 35 31 47 55 58 88 24 00 17 54 24 36 29 85 57 " + 
//"86 56 00 48 35 71 89 07 05 44 44 37 44 60 21 58 51 54 17 58 " + 
//"19 80 81 68 05 94 47 69 28 73 92 13 86 52 17 77 04 89 55 40 " + 
//"04 52 08 83 97 35 99 16 07 97 57 32 16 26 26 79 33 27 98 66 " + 
//"88 36 68 87 57 62 20 72 03 46 33 67 46 55 12 32 63 93 53 69 " + 
//"04 42 16 73 38 25 39 11 24 94 72 18 08 46 29 32 40 62 76 36 " + 
//"20 69 36 41 72 30 23 88 34 62 99 69 82 67 59 85 74 04 36 16 " + 
//"20 73 35 29 78 31 90 01 74 31 49 71 48 86 81 16 23 57 05 54 " + 
//"01 70 54 71 83 51 54 69 16 92 33 48 61 43 52 01 89 19 67 48 "));
            // Console.WriteLine(GetCollatzSequence(837799));
            // Console.WriteLine(GetLongestCollatzSequence(1000000));
#endregion
            // Console.WriteLine(GetFactorsCount(12));
            // Console.WriteLine(GetFirstTriangleNumberWithNFactors(500));
            // Console.WriteLine(GetFirstFibounachiTermWithNDigits(1000));
            Console.WriteLine(GetNumberOfRoutesInMatrix(20, 20));
            Console.WriteLine(new TimeSpan(DateTime.Now.Ticks - date.Ticks));
        }

        private enum Op { LessThan, LessThanOrEqual, Mod };

        private static bool Filter(int input, int control, Op filter)
        {
            switch (filter)
            {
                case Op.LessThanOrEqual: return (input <= control);
                case Op.Mod: return ((input % control) == 0);
            }
            return true;
        }

        private static int Sum1(int threshold, int[] controls, Op filter)
        {
            int sum = 0;
            for (int i = 0; i < threshold; i++)
            {
                bool isSummable = false;

                for (int j = 0; j < controls.Length; j++)
                {
                    isSummable = isSummable || Filter(i, controls[j], filter);

                    if (isSummable)
                    {
                        sum = sum + i;
                        break;
                    }
                }
            }
            return sum;
        }

        private static int Sum2(int threshold, int[] controls, Op filter)
        {
            int fswap;
            int f1 = 1;
            int f2 = 2;

            int sum = 0;// Check whether f1 meets the filter before adding it to the sum;

            while (f2 < threshold)
            {
                bool isSummable = false;

                for (int j = 0; j < controls.Length; j++)
                {
                    isSummable = isSummable || Filter(f2, controls[j], filter);

                    if (isSummable)
                    {
                        sum = sum + f2;
                        break;
                    }
                }

                fswap = f1;
                f1 = f2;
                f2 = fswap + f2;
            }

            return sum;
        }

        private static int Sum3(int threshold)
        {
            int sum = 0;
            int squares = 0;
            for (int i = 0; i < threshold; i++)
            {
                sum = sum + i;
                squares = squares + i * i;
            }
            return sum * sum - squares;
        }

        private static string Sum4(string[] numbers)
        {
            #region BigIntegers
            //BigInteger sum = 0;

            //for (int i = 0; i < numbers.Length; i++)
            //{
            //    byte[] digits = new byte[numbers[i].Length];
            //    for (int j = 0; j < digits.Length; j++)
            //    {
            //        digits[j] = (byte)numbers[i][j];
            //    }
            //    sum = sum + BigInteger.Create(digits);
            //}
            //return sum.ToString();
            #endregion

            #region Arrays
            if (numbers.Length > 0)
            {
                Stack<string> sumStack = new Stack<string>();

                int loopIdx = 0;
                int loopSum = 0;
                int loopCarry = 0;
                string loopStack = "";

                int numIdx = 0;
                string num = "";

                bool done = false;

                while (!done)
                {
                    loopIdx++;
                    loopSum = loopCarry;
                    for (int i = 0; i < numbers.Length; i++)
                    {
                        numIdx = numbers[i].Length - loopIdx;
                        num = numbers[i].Substring(numIdx, 1);
                        loopSum = loopSum + Int32.Parse(num);
                        done = done || (numIdx - 1 < 0);
                    }
                    loopCarry = (int)(loopSum / 10);
                    loopStack = loopSum.ToString();
                    sumStack.Push(loopStack.Substring(loopStack.Length - 1, 1));
                }
                sumStack.Push(loopStack.Substring(0, loopStack.Length - 1));

                string sumStr = "";
                while (sumStack.Count > 0)
                {
                    sumStr = sumStr + sumStack.Pop();
                }
                return sumStr;
            }
            return "";
            #endregion
        }

        private static long GetNumberOfRoutesInMatrix(int rows, int cols)
        {
            long[,] mxn = new long[rows, cols];

            /*
             * It can indeed be solved in one equation. The solution lies in the N choose R formula. For any number of rows R, you take (2R!)/(R! ^ 2). This comes from the equation n choose r = n!/r!(n-r)!, and you replace n with 2r, for the number of rows problem. :) 
             * Additionally,for a generalized solution of gridsize a*b routes=[(a+b)!]/[a!* b!]
             */

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    if (i == 0)
                    {
                        mxn[i, j] = (j == 0) ? 2 : mxn[i, j - 1] + 1; 
                    }
                    else 
                    {
                        mxn[i, j] = (j == 0) ? mxn[i - 1, j] + 1 : mxn[i - 1, j] + mxn[i, j - 1];
                    }
                }
            }
            return mxn[rows - 1, cols - 1];
        }

        private static bool IsPrimeNumber(long number)
        {
            if (number > 0)
            {
                // Speed it for numbers divisible by the first few primes 
                int[] primes = new int[] { 2, 3, 5, 7, 11, 13, 17, 19, 23 };
                for (int i = 0; i < primes.Length; i++)
                {
                    if (number == primes[i]) return true;
                    if (number % primes[i] == 0) return false;
                }

                // Find a divisor
                long divisor = (long)Math.Sqrt(number); // Any number, n, can have only one prime factor greater than sqrt(n)
                while ((divisor > 1) && ((number % divisor) > 0))
                {
                    divisor--; //TODO: All primes greater than 3 can be written in the form 6k +/- 1
                }
                return (divisor == 1);
            }
            return false;
        }

        private static long GetNextPrimeNumber(long lastPrimeNumber)
        {
            if (lastPrimeNumber == 2) return 3;

            long nextPrimeNumber = lastPrimeNumber + 2;

            while (!IsPrimeNumber(nextPrimeNumber))
            {
                nextPrimeNumber = nextPrimeNumber + 2; // TODO: speed it up
            }

            return nextPrimeNumber;
        }

        private static long GetLargestPrimeNumber(int threshold)
        {
            long nthPrimeNumber = 2;
            long nthMinusOnePrimeNumber = 2; 

            while (nthPrimeNumber < threshold)
            {
                nthMinusOnePrimeNumber = nthPrimeNumber;
                nthPrimeNumber = GetNextPrimeNumber(nthMinusOnePrimeNumber);
            }

            return nthMinusOnePrimeNumber;
        }

        private static long GetNthPrimeNumber(int n)
        {
            long nthPrimeNumber = 2;

            for (int i = 1; i < n; i++)
            {
                nthPrimeNumber = GetNextPrimeNumber(nthPrimeNumber);
                // if (i % 10 == 0)
                // if (( i >= 180) && (i <= 190))
                //    Console.WriteLine("{0:G}, {1:G}", i, nthPrimeNumber);
            }

            return nthPrimeNumber;
        }

        private static long GetPrimeFactorQuotient(long dividend, long divisor)
        {
            if ((dividend >= divisor) && (divisor != 0) && ((dividend % divisor) == 0) && IsPrimeNumber(divisor))
            {
                return (long)(dividend / divisor);
            }
            return 0; 
        }

        private static long GetLargestPrimeFactor(long number, int[] controls)
        {
            long[] factors = GetPrimeFactors(number, controls);
            if (factors == null) return -1;
            return factors[factors.Length - 1];
        }

        private static long[] GetPrimeFactors(long number, int[] controls)
        {
            if (number > 0)
            {
                /*
                 * a = number
                 * b = smallest prime factor of a   <<<< find a better way to find b
                 * repeat with a = a/b, b' >= b
                 * until a = 1
                 */

                int idx = 0;
                int idxLength = controls.Length;

                long dividend = number;
                long divisor = 1;
                long quotient = 0;

                List<long> factors = new List<long>();

                while ((dividend > 1) && (dividend > divisor))
                {
                    divisor = divisor + controls[idx++ % idxLength];
                    // Console.WriteLine(divisor);

                    quotient = GetPrimeFactorQuotient(dividend, divisor);
                    while (quotient > 0)
                    {
                        dividend = quotient;
                        factors.Add(divisor);
                        quotient = GetPrimeFactorQuotient(dividend, divisor);
                    }
                }
                return factors.ToArray();
            }
            else
            {
                return null;
            }
        }

        private static long GetPrimeNumberSum(int threshold)
        {     
            long prime = 2;
            long sum = 0;
            while (prime < threshold)
            {
                sum = sum + prime;
                prime = GetNextPrimeNumber(prime);
            }
            return sum;
        }


        private static int GetNextPalindromic(ref int[] palindromicDigits)
        {
            int palindromic = 0;
            try
            {
                int order = palindromicDigits.Length;

                for (int i = 0; i < order; i++)
                {
                    int digit = palindromicDigits[order - i - 1];
                    if (digit == 0)
                    {
                        palindromicDigits[order - i - 1] = 9;
                    }
                    else
                    {
                        palindromicDigits[order - i - 1] = digit - 1;
                        break;
                    }
                }

                if (palindromicDigits[0] > 0)
                {
                    for (int i = 0; i < order; i++)
                    {
                        palindromic = palindromic + palindromicDigits[i] * (int)Math.Pow(10, i) + palindromicDigits[i] * (int)Math.Pow(10, (2 * order) - i - 1);
                    }
                }
            }
            catch { } // TODO: Handle the exception, what if the array is not initialized 
            return palindromic;
        }

        private static int ValidatePalindromic(int palindromic, int multiplier, int order)
        {
            while (multiplier > (Math.Pow(10, order - 1)))
            {
                if (palindromic % multiplier == 0)
                {
                    int factor = palindromic / multiplier;
                    if ((factor >= Math.Pow(10, order - 1)) && (factor < Math.Pow(10, order)))
                    {
                        return palindromic;
                    }
                    else
                    {
                        break;
                    }
                }
                multiplier--;
            }
            return 0;
        }

        private static int GetLargestPalindromic(int order)
        {
            int[] multiplierDigits = new int[order];
            int[] palindromicDigits = new int[order];

            int multiplier = 0;
            int palindromic = 0;

            for (int i = 0; i < order; i++)
            {
                multiplierDigits[i] = 9;
                palindromicDigits[i] = 9;

                multiplier = multiplier + multiplierDigits[i] * (int)Math.Pow(10, i);
                palindromic = palindromic + palindromicDigits[i] * (int)Math.Pow(10, i) + palindromicDigits[i] * (int)Math.Pow(10, (2 * order) - i - 1);
            }

            while (ValidatePalindromic(palindromic, multiplier, order) == 0)
            {
                palindromic = GetNextPalindromic(ref palindromicDigits);
            }

            return palindromic; 
        }


        private static bool ValidateProduct(int product, int[] factorset, ref int[] factors)
        {
            int quotient = product;

            for (int i = 0; i < factors.Length; i++)
            {
                for (int j = 0; j < factorset.Length; j++)
                {
                    if ((factorset[j] > 0) && (quotient % factorset[j] == 0))
                    {
                        quotient = quotient / factorset[j];
                        factors[i] = factorset[j];
                        break;
                    }
                }
            }
            return (quotient == 1); 
        }

        private static bool FindConsecutiveDigitsInNumber(string number, int lead, int[] digits)
        {
            bool found = false;

            for (int i = 0; i < digits.Length; i++)
            {
                int subnumber = lead * 10 + digits[i];
                int[] subdigits = new int[digits.Length - 1];

                for (int j = 0; j < digits.Length; j++)
                {
                    if (j < i)
                    {
                        subdigits[j] = digits[j];
                    }
                    else if (j > i)
                    {
                        subdigits[j - 1] = digits[j];
                    }
                }

                if (digits.Length == 1)
                {
                    found = (number.IndexOf(subnumber.ToString()) > 0);
                }
                else
                {
                    found = FindConsecutiveDigitsInNumber(number, subnumber, subdigits);
                }

                if (found) break;
            }
            return found;
        }

        private static int GetGreatestProductFromConsecutiveDigitsInNumber(string number, int size)
        {
            int product = (int)Math.Pow(9, size);
            int[] factorset = new int[]{9, 8, 7, 6, 5, 4, 3, 2, 1};
            int[] factors = new int[size];

            while ((product > 1) && (!(ValidateProduct(product, factorset, ref factors) && FindConsecutiveDigitsInNumber(number, 0, factors))))
            {
                    product--;
            }
            return product;
        }

        private static long GetSumFromDigitsInNumber(string number)
        {
            long sum = 0;
            for (int i = 0; i < number.Length; i++)
            {
                int digit;

                if (!int.TryParse(number[i].ToString(), out digit))
                    return 0;

                sum = sum + digit;
            }
            return sum;
        }

        private static long GetSumFromDigitsInBigNumber(string number)
        {
            long sum = 0; 
            string substring = number.Replace("0", "");
            string subsubstring = "";
            int factor = 0;

            for (int i = 1; i < 10; i++)
            {
                subsubstring = substring.Replace(i.ToString(), "");
                factor = substring.Length - subsubstring.Length;
                sum = sum + i * factor;
                substring = subsubstring;
            }
            return sum;
        }

        private static long GetSumFromDigitsInNumber(long basenumber, long exponent)
        {
            /* If exponent == 0 return 1 
             * If exponent == 1 return GetSumFromDigitsInNumber(basenumber)
             * 
             * factors = GetPrimeFactors(exponent, new int[]{1})
             * if factors == null return 0;
             * 
             * number = basenumber;
             * For 0 to factors do
             *    number = number^factors[i]
             * return GetSumFromDigitsInNumber(number)
             */

            if (exponent == 0) return 1;
            if (exponent == 1) return GetSumFromDigitsInNumber(basenumber.ToString());

            long[] factors = GetPrimeFactors(exponent, new int[] { 1 });
            if (factors == null) return 0;

            BigInteger number = basenumber;

            for (int i = factors.Length - 1; i > -1; i--) // the largest factors are at the back of the array
            {
                //number = (BigInteger)Math.Pow(number, factors[i]); // this approach is slower
                BigInteger multiplier = number;
                for (int j = 1; j < factors[i]; j++)
                {
                    number = number * multiplier;
                }
            }
            return GetSumFromDigitsInNumber(number.ToString());
        }


        private static bool IsCoprime(int n1, int n2)
        {
            if (n1 == n2) return false;
            if ((n1 == 1) || (n2 == 1)) return false;

            long factor = 2;
            bool factorized = false;
            bool done = false; 

            while(!(done || factorized))
            {
                done = ((n1 < factor) && (n2 < factor));
                factorized = ((n1 % factor == 0) && (n2 % factor == 0));

                factor = GetNextPrimeNumber(factor);
            }

            return !factorized; 
        }

        private static int GetProductFromPythagoreanTriple(int sum)
        {       
            // Given that a + b + c = sum;
            // http://en.wikipedia.org/wiki/Pythagorean_triple
            int a = 0;
            int b = 0;
            int c = 0;
            int n;
            int msquared;
            int nsquared;
            for (int k = 1; k < sum; k++)
            {
                #region // http://www.larryhammer.com/pythagomath.html

                // {j(2k+1), 2jk(k+1), j(2k(k+1)+1)}, j odd
                a = (2 * k + 1);
                b = 2 * k * (k + 1);
                c = 2 * k * (k + 1) + 1;
                if (a + b + c == sum) return a * b * c;

                // {(j/2)2k, (j/2)(k^2-1), (j/2)(k^2+1)}, j even
                a = (2 * k);
                b = (int)Math.Pow(k, 2) - 1;
                c = (int)Math.Pow(k, 2) + 1;
                if (a + b + c == sum) return a * b * c;
                #endregion 

                if (sum % (2 * k) == 0)
                { 
                    for (int m = (int)Math.Round(Math.Sqrt((sum / 4 * k))); m < Math.Sqrt(sum / (2 * k)); m++)
                    {
                        msquared = (int)Math.Pow(m, 2);
                        n = ((sum / (2 * k)) - msquared) / m;
                        nsquared = (int)Math.Pow(n, 2);

                        a = k * 2 * m * n;
                        b = k * (msquared - nsquared);
                        c = k * (msquared + nsquared);

                        if (a + b + c == 1000)
                            return a * b * c;
                    }
                }
            }
            return 0;
        }


        private static BigInteger GetFactorial(int number)
        {
            if (number > 0)
            {
                BigInteger factorial = 1;

                for (int i = 1; i < number; i++)
                {
                    factorial = factorial * (i + 1);
                }
                return factorial;
            }
            return 0;
        }


        private enum Direction { Horizontal, Vertical, DiagonalDown, DiagonalUp };

        private static int[,] LoadMatrix(int token, int rows, int cols, string matrix)
        {
            int[,] mxn = new int[rows, cols];

            int row = 0;
            int col = 0;
            int idx = 0;

            while (idx < matrix.Length)
            {
                // TODO: Exception Handling
                mxn[row, col] = Int32.Parse(matrix.Substring(idx, token));

                col = (col + 1) % cols;
                if (col == 0)
                    row = (row + 1) % rows;
                idx = idx + token + 1;
            }
            return mxn;
        }

        private static int GetLargerProductInDirection(int output, int size, Direction dir, int row, int col, int[,] matrix)
        {
            int product = matrix[row, col];
            for (int i = 1; i < size; i++)
            {
                switch (dir)
                {
                    case Direction.Horizontal:
                        product = product * matrix[row, col + i];
                        break;
                    case Direction.Vertical:
                        product = product * matrix[row + i, col];
                        break;
                    case Direction.DiagonalDown:
                        product = product * matrix[row + i, col + i];
                        break;
                    case Direction.DiagonalUp:
                        product = product * matrix[row - i, col + i];
                        break;
                }                
            }
            return (product > output) ? product : output;
        }

        private static int GetLargestProductInMatrix(int size, int token, int rows, int cols, string matrix)
        {
            int[,] mxn = LoadMatrix(token, rows, cols, matrix);

            int product = 0;

            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < cols; col++)
                {
                    // calculate product along the horizontal direction
                    // 0 <= row < rows; 0 <= col <= cols - size
                    if (col <= cols - size)
                    {
                        product = GetLargerProductInDirection(product, size, Direction.Horizontal, row, col, mxn);
                    }

                    // calculate product along the vertical direction
                    // 0 <= row <= rows - size; 0 <= col < cols
                    if (row <= rows - size)
                    {
                        product = GetLargerProductInDirection(product, size, Direction.Vertical, row, col, mxn);
                    }

                    // calculate the product along the diagonal downwards direction
                    // 0 <= row <= rows - size; 0 <= col <= cols - size
                    if ((col < cols - size) && (row < rows - size))
                    {
                        product = GetLargerProductInDirection(product, size, Direction.DiagonalDown, row, col, mxn);
                    }

                    // calculate product along the diagonal upwards direction
                    // size <= row < rows; 0 <= col <= cols - size
                    if ((col < cols - size) && (size <= row))
                    {
                        product = GetLargerProductInDirection(product, size, Direction.DiagonalUp, row, col, mxn);
                    }
                }
            }
            return product;
        }


        private static int GetCollatzSequence(long number)
        {
            int count = 1;

            while (number > 1)
            {
                number = (number % 2 == 0) ? (number / 2) : (3 * number + 1);
                count++;
            }
            return count;
        }

        private static long GetLongestCollatzSequence(int threshold)
        {
            int count = 0;
            int output = 0;
            long number = 0; 
            long index = 1;

            while (index < threshold)
            {
                count = GetCollatzSequence(index);
                number = (count > output) ? index : number;
                output = (count > output) ? count : output;
                index = index + 2;
            }
            return number; //output;
        }


        private static int GetFactorsCount(int number)
        {
            // TODO: See solution to Project Euler 12 - use prime factorization, etc.
            int count = 0;
            int threshold = (int)Math.Ceiling(Math.Sqrt(number));
            int factor = 1;
            int offset = (number % 2 == 0) ? 1 : 2;

            while (factor < threshold)
            {
                count = (number % factor == 0) ? count + 2 : count;
                factor = factor + offset;
            }
            return (Math.Pow(threshold, 2) == number) ? count + 1 : count;
        }

        private static int GetFirstTriangleNumberWithNFactors(int threshold)
        {
            int number = 0;

            if (threshold > 0)
            {
                int count = 0;
                int index = 1;

                while (count <= threshold - 1)
                {
                    number = number + index++;
                    if (threshold < Math.Sqrt(number))
                        count = GetFactorsCount(number);
                }
            }
            return number; 
        }


        private static int GetFirstFibounachiTermWithNDigits(int threshold)
        {
            int term = 1; 
            #region BruteForce
            //if (threshold > 0)
            //{
            //    BigInteger number = 1;
            //    BigInteger index = 1;
            //    BigInteger offset = 10;
            //    int count = 1;
            //    term++;

            //    while (count <= threshold - 1)
            //    {
            //        term++; 
            //        number = number + index;
            //        index = number - index;
            //        // count = (Math.Floor(number / Math.Pow(10, count)) > 0) ? count + 1 : count;

            //        if (BigInteger.Divide(number, offset) >= 1)
            //        {
            //            offset = offset * 10;
            //            count = count + 1;
            //        }
            //    }
            //}
            #endregion

            /*
             * Fibonacci terms converge to (n)*Phi=(n+1), where Phi is the Golden Ratio (1+sqrt5)/2. 
             * http://en.wikipedia.org/wiki/Fibonacci_number
             * (n) = (Phi^n - (1 - Phi)^n)/sqrt5 
             * (n) = floor(Phi^n / sqrt5 + 1/2)
             * we need (n) > 10^(threshold - 1)
             * =CEILING( (1000 - 1 + LOG10(SQRT(5))) / LOG10((1+SQRT(5))/2),1)
             */
            term = (int)Math.Ceiling(((threshold - 1) + Math.Log10(Math.Sqrt(5)))/Math.Log10((1 + Math.Sqrt(5))/2));

            return term; 
        }
    }
}
