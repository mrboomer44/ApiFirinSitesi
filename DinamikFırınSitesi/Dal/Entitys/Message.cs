namespace AkademiqDinamikFırınSitesiApi.Dal.Entitys
{
    public class Message
    {
        public int MessageId { get; set; }
        public string Name { get; set; }
        public string Mail { get; set; }
        public string Subject { get; set; }
        public string MessageContent { get; set; }
        public bool Read { get; set; } = false;
    }
}
