using System;
using System.Globalization;
using System.Security.Cryptography.X509Certificates;

namespace LeetCode.Solutions
{
    public class Questions
    {
        public bool ContainsDuplicate(int[] nums)
        {
            return nums.Distinct().Count() != nums.Count();
        }

        public bool IsAnagram(string s, string t)
        {
            if (s.Length != t.Length) 
                return false;

            while (t.Count() != 0)
            {
                string index = t[0].ToString();
                t = t.Replace(index, "");
                s = s.Replace(index, "");

                if (s.Length != t.Length) 
                    return false;
            }

            return true;
        }

        public int[] ConcatenationArray(int[] nums)
        {
            var ars = new int[nums.Length * 2];
            nums.CopyTo(ars, 0);
            nums.CopyTo(ars, nums.Length);
            return ars;
        }

        public int[] ReplaceElements(int[] arr)
        {
            if (arr.Length == 1)
                return new int[] { -1 };

            var higherIndex = 0;
            var value = HigherValue(arr, 1);

            for (int i = 0; i < arr.Count() -1; i++)
            {
                
                if (higherIndex <= i)
                {
                    value = HigherValue(arr, i + 1);
                    higherIndex = value[1];
                }

                arr[i] = value[0];
                arr[i + 1] = -1;
            }

            return arr;
        }

        private int[] HigherValue(int[] arr, int index)
        {
            var higher = new int[2] { -1, 1 };
            for (int i = index; i < arr.Count(); i++)
            {                
                if (higher[0] < arr[i])
                {
                    higher[0] = arr[i];
                    higher[1] = i;
                }                    
            }

            return higher;
        }

        public bool IsSubsequence(string s, string t)
        {
            var lastIndex = -1;
            for (int i = 0; i < s.Count(); i++)
            {
                var index = t.IndexOf(s[i]) + 1;

                if (index == 0)
                    return false;

                t = t.Substring(index, t.Length - index);

                if (lastIndex > index)
                    return false;
            }
            return true;
        }

        public int LengthOfLastWord(string s)
        {
            while(s.IndexOf("  ") > -1)
            {
                s = s.Replace("  ", " ");
            }
            
            var words = s.Split(" ");
            var ret = (words[words.Length - 1] == string.Empty ? words[words.Length - 2].Length : words[words.Length - 1].Length);
            return ret;
        }

        public int[] TwoSum(int[] nums, int target)
        {
            var count = nums.Count() - 1;

            for (int i = 0; i < count; i++)
            {
                for (int x = count; x > 0; x--)
                {                                 
                    if (nums[i] + nums[x] == target && i != x)
                        return new int[] {i, x };
                }
            }

            return new int[0];
        }

        public IList<IList<string>> GroupAnagrams(string[] strs)
        {
            IList<IList<string>> gropsAnagrans = new List<IList<string>>();

            for (int i = 0; i < strs.Count(); i++)
            {
                var find = false;

                if (gropsAnagrans.Count() > 0)
                {
                    foreach (var group in gropsAnagrans) 
                    { 
                        foreach(var item in group)
                        {
                            if (IsAnagram(item, strs[i]))
                            {
                                group.Add(strs[i]);
                                find = true;                                
                            }

                            break;
                        }

                        if (find)
                            break;
                    }                    
                }

                if (!find)
                    gropsAnagrans.Add(new List<string>() { strs[i] });
            }

            return gropsAnagrans;
        }

        public int[] TopKFrequent(int[] nums, int k)
        {
            return nums.ToLookup(x => x).ToDictionary(g => g.Key, g => g.Count()).OrderBy(x => x.Value).Reverse().Take(k).Select(y => y.Key).ToArray();
        }

        public int[] ProductExceptSelf(int[] nums)
        {
            var ans = new int[nums.Length];
            var isntfirstTime = new bool[nums.Length];

            var leftProduct = 1;
            var rigthProduct = 1;

            for (int i = 1, x = nums.Count() - 2; i < nums.Count(); ++i, --x)
            {
                leftProduct *= nums[i - 1];
                rigthProduct *= nums[x + 1];

                if (!isntfirstTime[i])
                {
                    isntfirstTime[i] = true;
                    ans[i] = leftProduct;
                }
                else
                    ans[i] *= leftProduct;

                if (!isntfirstTime[x])
                {
                    isntfirstTime[x] = true;
                    ans[x] = rigthProduct;
                }
                else
                    ans[x] *= rigthProduct;
            }

            return ans;
        }

        public int[] SortedSquares(int[] nums)
        {

            var x = 0;
            var newArray = new int[nums.Length];

            while (x < nums.Length)
            {

                newArray[x] = nums[x] * nums[x];
                x++;
            }

            Array.Sort(newArray);

            return newArray;
        }
    }
}