namespace FunctionApp1.Domain
{
    public class QueueTrigger1Handler : IHandler<QueueTrigger1Request, QueueTrigger1Response>
    {
        public QueueTrigger1Response Handle(
            QueueTrigger1Request queueTrigger1Request)
        {
            var queueTrigger1Response =
                new QueueTrigger1Response
                {
                    Body = queueTrigger1Request.Body,
                    Recipients = queueTrigger1Request.Recipients,
                    Subject = queueTrigger1Request.Subject
                };

            return queueTrigger1Response;
        }
    }
}
