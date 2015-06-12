using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Testy
{
    class Question
    {
        public int ID { get; set; }
        public  String Text { get; set; }
        public String Answer1 { get; set; }
        public String Answer2 { get; set; }
        public String Answer3 { get; set; }
        public String Answer4 { get; set; }
        public Question(int ID, String Text, String Answer1, String Answer2, String Answer3, String Answer4)
        {
            this.ID = ID;
            this.Text = Text;
            this.Answer1 = Answer1;
            this.Answer2 = Answer2;
            this.Answer3 = Answer3;
            this.Answer4 = Answer4;
        }
    }
}
