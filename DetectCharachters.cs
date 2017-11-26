using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace DetectCharachters
{
   public class DetectCharachters    
   {
       Regex r_emoji;
       Regex r_unreadable;
       Encoding utf8encoding;
       Encoding ansiencoding;

       public DetectCharachters()
       {
           r_emoji = new Regex(@"[\uD800-\uDBFF][\uDC00-\uDFFF]", RegexOptions.Compiled);
           r_unreadable = new Regex(@"[\uFFFD]", RegexOptions.Compiled);
           utf8encoding = Encoding.UTF8;
           ansiencoding = Encoding.GetEncoding(1252);
       }

       public bool isUnreadable(string data)
       {
           bool success_flag_ = false;
           if (r_unreadable.Matches(data).Count > 0) { success_flag_ = true; }
           return success_flag_;
       }

       public bool hasEmoji(string data)
       {
           bool success_flag_ = false;
           if (r_emoji.Matches(data).Count > 0) { success_flag_ = true; }
           return success_flag_;
       }

       public string repairSentence(string sentence)
       {
           //Convert the bytes as the bytes have been damaged while saving
           byte[] ansibytes = Encoding.Convert(utf8encoding, ansiencoding, utf8encoding.GetBytes(sentence));
           //Read the string from repaired bytes
           var result = new string(utf8encoding.GetChars(ansibytes));
           return result;
       }

   }
}
