using System.Text.RegularExpressions;
using CuisineCompanion.Extensions;
using CuisineCompanion.Helper;
using CuisineCompanion.Helpers;
using Newtonsoft.Json;
using RestSharp;

public class Program
{
    private static void Main(string[] args)
    {
        test6();
    }

    private static void test1()
    {
        var list = new List<string>
        {
            "qwq12345678",
            "12345qqqghrewqvbfdseawebsdseafw6",
            "qqjsjvekavodibnfocdjjdevjgwfqoevidfujJJVfsaegFFBUGHWFQWDKADSVDUHFJACDSijdfngjfwekvdhnvfajwkdsvj",
            @"qqjsjvekavodibnfocdjjdevjgw98174243521452qqqqqqqqqqqq6748590-`1234567890-=[]\;',./~!@#$%^&*()_+{}|:<>?fqoevaAFEAWAFAFsgfesafgrhaAWFidfujJJVFFBUGHWFQWDKADSVqwdfwDUHFJACDSijdfngjfwekvdhnvfajwkdsvj",
            @"qqjsjvekavodibnfocdjjdevjgw98174243521452qfhqdnwfevnwheqfvbhdfvnj12351423teggfwsfqqqqqqqqqqqq6748590-`1234567890-=[]\;',./~!@#$%^&*()_+{}|:<>?fqoevaAFEAWAFAFsgfesafgrhaAWFidfujJJVFFBUGHWFQWDKADSVqwdfwDUHFJACDSijdfngjfwekvdhnvfajwkdsvj",
            @"qqjsjvekavodibnfocdjjdevjgw98174243521452qqqqqqqafffffffaeagaqqqqq6748590-`1234567890-=[]\;',./~!@#$%^&*()_+{}|:<>?fqoevaAFEAWAFAFsgfesafgrhaAWFidfujJJVFFBUGHWFQWDKADSVqwdfwDUHFJACDSijdfngjfwekvdhnvfajwkdsvj",
            @"qqjsjvekavodibnfocdjjdevjgw98174243521452qqqqqqasgsgegwgarrrqqqqqq6748590-`1234567890-=[]\;',./~!@#$%^&*()_+{}|:<>?fqoevaAFEAWAFAFsgfesafgrhaAWFidfujJJVFFBUGHWFQWDKADSVqwdfwDUHFJACDSijdfngjfwekvdhnvfajwkdsvj",
            @"qqjsjvekavodibnfocdjjdevjgw98174243521452qqqqqqqqqqqq6748590-`1234567890-=[]\;',./~!@#$%^&*()_+{}|:<>?fqoevaAFEAWAFAFsgfesafgrhaAWFidfujJJVFFBUGHWFQWDKADSVqwdfwDUHFJACDSijdfngjfwekvdhnvfajwkdsvj"
        };
        foreach (var s in list)
        {
            FrontendEncryptionHelper.HasPassword(s, out var salt, out var hashpswd);
            Console.WriteLine(salt.Length + "  " + hashpswd.Length + "  " + hashpswd);
        }
    }

    private static void test2()
    {
        var client = new RestClient("http://localhost:5281/api/Account/Register");

        var request = new RestRequest(Method.POST);
        request.AddHeader("Content-Type", "application/json");
        var json = JsonConvert.SerializeObject(new
        {
            Name = "stri222232221222222222ng",
            Password = "str3333333333333ing",
            Salt = "stri2212222222222222ng",
            Email = "us12er@exa3mple.com",
            Phone = "str11333ing"
        });
        request.AddParameter("application/json", json, ParameterType.RequestBody);

        var response = client.Execute(request);
        Console.WriteLine(response.Content);
    }

    private static void test3()
    {
        var list =
            "[\n  {\n    \"蛋白质\":\"19.0g\",\"脂肪\":\"11.5g\",\"碳水化合物\":\"5.5g\",\"热量(千卡)\":\"202kcal\",\"热量(千焦)\":\"845kJ\",\"饱和脂肪(酸)\":\"5.6g\",\"不饱和脂肪酸\":\"5.2g\",\"单不饱和脂肪酸\":\"5.1g\",\"多不饱和脂肪酸\":\"0.1g\",\"n-3脂肪酸\":\"0.2g\",\"胆固醇\":\"81mg\",\"维生素A\":\"28μg\",\"维生素B2(核黄素)\":\"0.05mg\",\"维生素B6(吡哆醇)\":\"0.53mg\",\"维生素B12(钴胺素)\":\"0.38μg\",\"维生素E\":\"0.44mg\",\"烟酸(烟酰胺/尼克酸)\":\"4.36mg\",\"胆碱\":\"12.6mg\",\"生物素\":\"4.7μg\",\"钠\":\"51mg\",\"钾\":\"205mg\",\"钙\":\"8mg\",\"磷\":\"94mg\",\"镁\":\"17mg\",\"铁\":\"0.9mg\",\"锌\":\"0.42mg\",\"锰\":\"0.01mg\",\"硒\":\"8.7μg\",\"嘌呤\":\"138mg\"\n  },\n  {\n    \"热量(千卡)\": \"254kcal\",\n    \"热量(千焦)\": \"1063kJ\",\n    \"碳水化合物(克)\": \"2.7g\",\n    \"蛋白质(克)\": \"23.9g\",\n    \"脂肪(克)\": \"16.4g\",\n    \"饱和脂肪(酸)(克)\": \"3.8g\",\n    \"不饱和脂肪酸(克)\": \"11.8g\",\n    \"单不饱和脂肪酸(克)\": \"8.4g\",\n    \"多不饱和脂肪酸(克)\": \"3.4g\",\n    \"胆固醇(毫克)\": \"103mg\",\n    \"叶酸(微克)\": \"86μg\",\n    \"维生素A(微克)\": \"37μg\",\n    \"维生素B1(硫胺素)(毫克)\": \"0.01mg\",\n    \"维生素B2(核黄素)(毫克)\": \"0.13mg\",\n    \"维生素B12(钴胺素)(微克)\": \"0.47μg\",\n    \"维生素D(微克)\": \"0.2μg\",\n    \"维生素E(毫克)\": \"0.32mg\",\n    \"烟酸(烟酰胺/尼克酸)(毫克)\": \"2.4mg\",\n    \"钠(毫克)\": \"169mg\",\n    \"钾(毫克)\": \"108mg\",\n    \"钙(毫克)\": \"36mg\",\n    \"磷(毫克)\": \"76mg\",\n    \"镁(毫克)\": \"7mg\",\n    \"铁(毫克)\": \"1.4mg\",\n    \"锌 (毫克)\": \"0.9mg\",\n    \"铜(毫克)\": \"0.05mg\",\n    \"锰(毫克)\": \"0.03mg\",\n    \"硒(微克)\": \"10.0μg\"\n  },\n  {\n    \"热量(千卡)\": \"81kcal\",\n    \"热量(千焦)\": \"339kJ\",\n    \"碳水化合物(克)\": \"17.8g\",\n    \"膳食纤维(克)\": \"1.1g\",\n    \"果糖(克)\": null,\n    \"蛋白质(克)\": \"2.6g\",\n    \"脂肪(克)\": \"0.2g\",\n    \"饱和脂肪(酸)(克)\": null,\n    \"不饱和脂肪酸(克)\": null,\n    \"反式脂肪(酸)(克)\": null,\n    \"单不饱和脂肪酸(克)\": null,\n    \"多不饱和脂肪酸(克)\": null,\n    \"n-3脂肪酸(克)\": null,\n    \"胆固醇(毫克)\": null,\n    \"叶酸(微克)\": \"16μg\",\n    \"维生素A(微克)\": \"1μg\",\n    \"维生素B1(硫胺素)(毫克)\": \"0.1mg\",\n    \"维生素B2(核黄素)(毫克)\": \"0.02mg\",\n    \"维生素B6(吡哆醇)(毫克)\": null,\n    \"维生素B12(钴胺素)(微克)\": null,\n    \"维生素C(抗坏血酸)(毫克)\": \"14.0mg\",\n    \"胡萝卜素(微克)\": \"6\",\n    \"维生素D(微克)\": null,\n    \"维生素E(毫克)\": \"0.34mg\",\n    \"维生素K(微克)\": null,\n    \"烟酸(烟酰胺/尼克酸)(毫克)\": \"1.1mg\",\n    \"泛酸(毫克)\": \"0.3mg\",\n    \"胆碱(毫克)\": null,\n    \"生物素(微克)\": \"4.2μg\",\n    \"钠(毫克)\": \"6mg\",\n    \"钾(毫克)\": \"347mg\",\n    \"钙(毫克)\": \"7mg\",\n    \"磷(毫克)\": \"46mg\",\n    \"镁(毫克)\": \"24mg\",\n    \"氯 (毫克)\": null,\n    \"铁(毫克)\": \"0.4mg\",\n    \"锌(毫克)\": \"0.3mg\",\n    \"铜(毫克)\": \"0.09mg\",\n    \"锰(毫克)\": \"0.1mg\",\n    \"碘(微克)\": \"1.2μg\",\n    \"硒(微克)\": \"0.5μg\",\n    \"氟(毫克)\": null,\n    \"汞(ppm)\": null,\n    \"原花青素(毫克)\": null,\n    \"大豆异黄酮(毫克)\": null,\n    \"叶黄素+玉米黄素(微克)\": null,\n    \"杨梅黄酮(毫克)\": null,\n    \"玉米黄酮(毫克)\": null,\n    \"嘌呤(毫克)\": \"13mg\"\n  },\n  {\n    \"热量(千卡)\": \"32kcal\",\n    \"热量(千焦)\": \"134kJ\",\n    \"碳水化合物(克)\": \"8.1g\",\n    \"膳食纤维(克)\": \"2.4g\",\n    \"果糖(克)\": null,\n    \"蛋白质(克)\": \"1.0g\",\n    \"脂肪(克)\": \"0.2g\",\n    \"饱和脂肪(酸)(克)\": null,\n    \"不饱和脂肪酸(克)\": null,\n    \"反式脂肪(酸)(克)\": null,\n    \"单不饱和脂肪酸(克)\": null,\n    \"多不饱和脂肪酸(克)\": null,\n    \"n-3脂肪酸(克)\": null,\n    \"胆固醇(毫克)\": null,\n    \"叶酸(微克)\": \"20μg\",\n    \"维生素A(微克)\": \"342μg\",\n    \"维生素B1(硫胺素)(毫克)\": null,\n    \"维生素B2(核黄素)(毫克)\": \"0.02mg\",\n    \"维生素B6(吡哆醇)(毫克)\": \"0.22mg\",\n    \"维生素B12(钴胺素)(微克)\": null,\n    \"维生素C(抗坏血酸)(毫克)\": \"9.0mg\",\n    \"胡萝卜素(微克)\": \"4107\",\n    \"维生素D(微克)\": null,\n    \"维生素E(毫克)\": \"0.31mg\",\n    \"维生素K(微克)\": null,\n    \"烟酸(烟酰胺/尼克酸)(毫克)\": null,\n    \"泛酸(毫克)\": \"0.27mg\",\n    \"胆碱(毫克)\": null,\n    \"生物素(微克)\": \"3.1μg\",\n    \"钠(毫克)\": \"121mg\",\n    \"钾(毫克)\": \"119mg\",\n    \"钙(毫克)\": \"27mg\",\n    \"磷(毫克)\": \"38mg\",\n    \"镁(毫克)\": \"18mg\",\n    \"氯 (毫克)\": null,\n    \"铁(毫克)\": \"0.3mg\",\n    \"锌 (毫克)\": \"0.22mg\",\n    \"铜(毫克)\": \"0.07mg\",\n    \"锰(毫克)\": \"0.08mg\",\n    \"碘(微克)\": \"1.2μg\",\n    \"硒(微克)\": \"0.6μg\",\n    \"氟(毫克)\": null,\n    \"汞(ppm)\": null,\n    \"原花青素(毫克)\": \"256.36mg\",\n    \"大豆异黄酮(毫克)\": null,\n    \"叶黄素+玉米黄素(微克)\": null,\n    \"杨梅黄酮(毫克)\": null,\n    \"玉米黄酮(毫克)\": null,\n    \"嘌呤(毫克)\": \"2mg\"\n  },\n  {\n    \"热量(千卡)\": \"107kcal\",\n    \"热量(千焦)\": \"448kJ\",\n    \"碳水化合物(克)\": \"17.8g\",\n    \"膳食纤维(克)\": \"4.7g\",\n    \"果糖(克)\": null,\n    \"蛋白质(克)\": \"3.3g\",\n    \"脂肪(克)\": \"2.5g\",\n    \"饱和脂肪(酸)(克)\": \"0.7g\",\n    \"不饱和脂肪酸(克)\": null,\n    \"反式脂肪(酸)(克)\": null,\n    \"单不饱和脂肪酸(克)\": null,\n    \"多不饱和脂肪酸(克)\": null,\n    \"n-3脂肪酸(克)\": null,\n    \"胆固醇(毫克)\": null,\n    \"叶酸(微克)\": \"27μg\",\n    \"维生素A(微克)\": \"18μg\",\n    \"维生素B1(硫胺素)(毫克)\": \"0.13mg\",\n    \"维生素B2(核黄素)(毫克)\": \"0.1mg\",\n    \"维生素B6(吡哆醇)(毫克)\": \"0.2mg\",\n    \"维生素B12(钴胺素)(微克)\": null,\n    \"维生素C(抗坏血酸)(毫克)\": \"5.4mg\",\n    \"胡萝卜素(微克)\": null,\n    \"维生素D(微克)\": null,\n    \"维生素E(毫克)\": \"0.75mg\",\n    \"维生素K(微克)\": null,\n    \"烟酸(烟酰胺/尼克酸)(毫克)\": \"1.84mg\",\n    \"泛酸(毫克)\": null,\n    \"胆碱(毫克)\": null,\n    \"生物素(微克)\": null,\n    \"钠(毫克)\": \"2mg\",\n    \"钾(毫克)\": \"269mg\",\n    \"钙(毫克)\": \"3mg\",\n    \"磷(毫克)\": \"84mg\",\n    \"镁(毫克)\": \"34mg\",\n    \"氯(毫克)\": null,\n    \"铁(毫克)\": \"0.6mg\",\n    \"锌(毫克)\": \"0.6mg\",\n    \"铜(毫克)\": null,\n    \"锰(毫克)\": null,\n    \"碘(微克)\": null,\n    \"硒(微克)\": null,\n    \"氟(毫克)\": null,\n    \"汞(ppm)\": null,\n    \"原花青素(毫克)\": null,\n    \"大豆异黄酮(毫克)\": null,\n    \"叶黄素+玉米黄素(微克)\": null,\n    \"杨梅黄酮(毫克)\": null,\n    \"玉米黄酮(毫克)\": null,\n    \"嘌呤(毫克)\": null\n  },\n  {\n    \"热量(千卡)\": \"20kcal\",\n    \"热量(千焦)\": \"84kJ\",\n    \"蛋白质(克)\": \"4.8g\",\n    \"脂肪(克)\": \"0.1g\",\n    \"碘(微克)\": \"1.3μg\",\"钠\":\"7247mg\"\n  },\n  {\n    \"热量(千卡)\": \"16kcal\",\n    \"热量(千焦)\": \"67kJ\",\n    \"碳水化合物(克)\": \"2.9g\",\n    \"钠(克)\": \"1032mg\",\n    \"蛋白质(克)\": \"0.8g\"\n  },\n  {\n    \"热量(千卡)\": \"126kcal\",\n    \"热量(千焦)\": \"527kJ\",\n    \"碳水化合物(克)\": \"13.4g\",\n    \"膳食纤维(克)\": null,\n    \"果糖(克)\": null,\n    \"蛋白质(克)\": \"7.5g\",\n    \"脂肪(克)\": \"4.6g\",\n    \"钠\":\"3930mg\"\n  },\n  {\n    \"钠\":\"39311mg\",\"氯\":\"59900mg\",\"碘\":\"3500mg\",\"硒\":\"1mg\"\n  },\n  {\n    \"热量(千卡)\": \"397kcal\",\n    \"热量(千焦)\": \"1661kJ\",\n    \"碳水化合物(克)\": \"99.3g\",\n    \"维生素B1(硫胺素)(毫克)\": \"0.03mg\",\n    \"维生素B2(核黄素)(毫克)\": \"0.03mg\",\n    \"钠(毫克)\": \"3mg\",\n    \"钾(毫克)\": \"1mg\",\n    \"钙(毫克)\": \"23mg\",\n    \"镁(毫克)\": \"2mg\",\n    \"铁(毫克)\": \"1.4mg\",\n    \"锌(毫克)\": \"0.21mg\",\n    \"铜(毫克)\": \"0.03mg\"\n  },\n  {\n    \"热量(千卡)\": \"38kcal\",\n    \"热量(千焦)\": \"159kJ\",\n    \"碳水化合物(克)\": \"3.1g\", \"蛋白质\": \"0.4g\"\n  },\n  {\n    \"热量(千卡)\": \"40kcal\",\n    \"热量(千焦)\": \"167kJ\",\n    \"碳水化合物(克)\": \"9.0g\",\n    \"膳食纤维(克)\": \"0.9g\",\n    \"蛋白质(克)\": \"1.1g\",\n    \"脂肪(克)\": \"0.2g\",\n    \"叶酸(微克)\": \"16μg\",\n    \"维生素A(微克)\": \"2μg\",\n    \"维生素B1(硫胺素)(毫克)\": \"0.03mg\",\n    \"维生素B2(核黄素)(毫克)\": \"0.03mg\",\n    \"维生素B6(吡哆醇)(毫克)\": \"0.12mg\",\n    \"维生素C(抗坏血酸)(毫克)\": \"8.0mg\",\n    \"胡萝卜素(微克)\": \"20μg\",\n    \"维生素E(毫克)\": \"0.14mg\",\n    \"烟酸(烟酰胺/尼克酸)(毫克)\": \"0.3mg\",\n    \"泛酸(毫克)\": \"0.12mg\",\n    \"生物素(微克)\": \"1.0μg\",\n    \"钠(毫克)\": \"4mg\",\n    \"钾(毫克)\": \"147mg\",\n    \"钙(毫克)\": \"24mg\",\n    \"磷(毫克)\": \"39mg\",\n    \"镁(毫克)\": \"15mg\",\n    \"氯(毫克)\": \"54mg\",\n    \"铁(毫克)\": \"0.6mg\",\n    \"锌(毫克)\": \"0.23mg\",\n    \"铜(毫克)\": \"0.05mg\",\n    \"锰(毫克)\": \"0.14mg\",\n    \"碘(微克)\": \"1.2μg\",\n    \"硒(微克)\": \"0.9μg\",\n    \"原花青素(毫克)\": \"4.0mg\"\n  }]";
        var obj = JsonConvert.DeserializeObject<List<Dictionary<string, string>>>(list);

        foreach (var item in obj)
        {
            var mp = new Dictionary<string, string>();
            foreach (var (key, value) in item)
            {
                if (value is null or "") continue;
                mp[key] = value;
            }

            Console.WriteLine(JsonConvert.SerializeObject(mp));
        }
    }

    private static void test4()
    {
        for (var i = 13000000000; i <= 19900000000; i++)
            if (Regex.IsMatch(i.ToString(), RegexHelper.MobileExact))
                Console.WriteLine(i);
    }

    private static void test5()
    {
        var result = "..11..1......1.";

        // 使用正则表达式匹配数字和小数点
        var pattern = @"\d+(\.\d*)?|\.\d+";

        // 匹配字符串中的所有数字和小数点
        var match = Regex.Match(result, pattern);

        // 输出匹配结果中的第一个小数点及其前面的数字
        Console.WriteLine(match.Value);
    }

    private static void test6()
    {
        var json = File.ReadLines(@"..\..\..\NewFile2.txt").ToList();
        var newList = new List<string>();
        var map = new List<List<string>>();
        var all = new List<List<List<string>>>();
        var names = new List<string>();
        for (var i = 0; i < json.Count; i++)
        {
            var item = json[i];
            if (item.Contains("岁~"))
            {
                var l = json[i - 1].Split('\t');
                var r = json[i].Split('\t');
                int ll = 0, rr = 0;
                try
                {
                    ll = int.Parse(l[0].Replace("岁~", ""));
                }
                catch
                {
                    // ignored
                }

                try
                {
                    rr = int.Parse(r[0].Replace("岁~", ""));
                }
                catch
                {
                    // ignored
                }

                if (rr == 101) Console.WriteLine(rr);

                if (ll == 0 || ll > rr)
                {
                    ll = 1;
                    var list2 = new List<string>();
                    list2.Add("1岁~");
                    for (var k = 1; k < l.Length; k++) list2.Add("0");
                    l = list2.ToArray();
                }
                else if (rr == 0)
                {
                    rr = 101;
                }

                for (var j = ll; j < rr; j++)
                {
                    newList.Add($"{j}\t" + string.Join('\t', l.Skip(1)));
                    map.Add(l.Skip(1).ToList());
                }
            }
            else
            {
                newList.Add(item);
                var list = item.Split('\t');
                map.Add(list.Skip(1).ToList());
                if (list.Length == 1) names.Add(list[0]);
            }
        }

        var ind = 0;

        List<List<string>> list4 = null;

        foreach (var list in map)
            if (list.Count == 0)
            {
                list4 = new List<List<string>>();
                all.Add(list4);
            }
            else
            {
                list4.Add(list);
            }

        var dics = new List<Dictionary<string, List<string>>>();
        foreach (var list in all)
        {
            var dic = new Dictionary<string, List<string>>();

            for (var i = 0; i < list[0].Count; i++) dic[list[0][i]] = new List<string>();

            for (var j = 1; j < list.Count; j++)
            for (var i = 0; i < list[j].Count; i++)
                dic[list[0][i]].Add(list[j][i]);

            dics.Add(dic);
        }

        var dicss = new Dictionary<string, Dictionary<string, string>>();
        for (var i = 0; i < dics.Count; i++)
        {
            var dic = dics[i];
            var dic2 = new Dictionary<string, string>();
            foreach (var (key, value) in dic)
            {
                string dic3 = "";
                var list1 = new List<string>();
                for (var k = 0; k < value.Count / 2; k++) list1.Add(value[k]);

                var list2 = new List<string>();
                for (var k = value.Count / 2; k < value.Count; k++) list2.Add(value[k]);


                foreach (var s1 in list1.Where(s1 => s1 != "0"))
                    goto ret;

                foreach (var s1 in list2.Where(s1 => s1 != "0"))
                    goto ret;

                continue;


                ret:
                for (var j = 0; j < list1.Count; j++)
                {
                    if (list1[j] == "0")
                    {
                        list1[j] = "";
                    }
                }

                for (var j = 0; j < list2.Count; j++)
                {
                    if (list2[j] == "0")
                    {
                        list2[j] = "";
                    }
                }

                var str1 = string.Join(',', list1);
                var str2 = string.Join(',', list2);
                str2 = str1 == str2 ? "=" : str2;

                var newList1 = new List<string>();
                var newList11 = new List<byte>();
                var newList2 = new List<string>();
                var newList22 = new List<byte>();

                for (int k = 1, count = 1; k < list1.Count; k++)
                {
                    if (list1[k] == list1[k - 1])
                    {
                        count++;
                    }
                    else
                    {
                        newList1.Add(list1[k - 1]);
                        newList11.Add((byte)count);
                        count = 1;
                    }

                    if (k == list1.Count - 1)
                    {
                        newList1.Add(list1[k]);
                        newList11.Add((byte)count);
                    }
                }

                if (str2 != "=")
                {
                    for (int k = 1, count = 1; k < list2.Count; k++)
                    {
                        if (list2[k] == list2[k - 1])
                        {
                            count++;
                        }
                        else
                        {
                            newList2.Add(list2[k - 1]);
                            newList22.Add((byte)count);
                            count = 1;
                        }

                        if (k == list2.Count - 1)
                        {
                            newList2.Add(list2[k]);
                            newList22.Add((byte)count);
                        }
                    }
                }

                var result = string.Join(',', newList1) + "@" + string.Join(',', newList11);
                result = result + "|" +
                         (str2 == "=" ? "=" : (string.Join(',', newList2)) + "@" + string.Join(',', newList22));

                dic2[key] = result;
            }

            dicss[names[i]] = dic2;
        }

        var s = string.Join('\n', newList);


        dicss.ToJson();

        HashSet<string> set = new HashSet<string>();
        var newDic = new Dictionary<string, Dictionary<string, string>>();
        foreach (var (key, value) in dicss)
        foreach (var (key1, value1) in value)
            set.Add(key1);

        var indDic = new Dictionary<string, int>();
        {
            int k = 0;
            foreach (var s1 in set)
            {
                indDic[s1] = k++;
            }
        }
        
        foreach (var (key, value) in dicss)
        {
            newDic[key]=new Dictionary<string, string>();
            foreach (var (key1, value1) in value)
            {
                newDic[key][indDic[key1].ToString()] = value1;
                set.Add(key1);
            }
        }

        newDic["index"] = new Dictionary<string, string> { ["index"] = string.Join(',', set) };

        newDic.ToJson();

    }
}