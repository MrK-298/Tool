using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class Question
    {
        public ObjectId Id { get; set; }
        public string questionText { get; set; }
    }
}
