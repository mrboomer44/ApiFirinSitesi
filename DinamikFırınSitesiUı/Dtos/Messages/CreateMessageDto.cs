namespace DinamikFırınSitesiUı.Dtos.Messages
{
    public class CreateMessageDto
    {
        public string Name { get; set; }
        public string Mail { get; set; }
        public string Subject { get; set; }
        public string MessageContent { get; set; }
        public bool Read { get; set; }
    }
}
