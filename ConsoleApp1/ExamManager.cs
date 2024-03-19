using MongoDB.Bson;
using MongoDB.Driver;

namespace ConsoleApp1
{
    public class ExamManager
    {
        private readonly IMongoCollection<Exam> _examCollection;
        public ObjectId examId = ObjectId.Parse("65f99ba5ea8b4587aebef865");
        public ObjectId questionId;
        public ExamManager()
        {
            var client = new MongoClient("mongodb+srv://khoinguyen29082002:khoibia123@hoangkhoi.9ehzu5m.mongodb.net/");
            var database = client.GetDatabase("WebTiengAnh");
            _examCollection = database.GetCollection<Exam>("Exam");
        }

        public void AddQuestionToExam(Question question)
        {
            var filter = Builders<Exam>.Filter.Eq("_id",examId);
            var update = Builders<Exam>.Update.Push("Questions", question);
            _examCollection.UpdateOne(filter, update);
            questionId = question.Id;
        }
        public void AddAnswersToQuestion(List<Answer> answers)
        {
            var filter = Builders<Exam>.Filter.And(
                Builders<Exam>.Filter.Eq("_id", examId),
                Builders<Exam>.Filter.ElemMatch(x => x.Questions, q => q.Id == questionId)
            );

            var update = Builders<Exam>.Update.Push("Questions.$.Answers", answers);

            _examCollection.UpdateOne(filter, update);
        }
    }
}
