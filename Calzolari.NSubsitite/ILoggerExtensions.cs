using ExpectedObjects;
using NSubstitute;
using System;

namespace Microsoft.Extensions.Logging
{
    public static class ILoggerExtensions
    {
        public static void ReceivedMatchingArgs(this ILogger logger, LogLevel logLevel, string formattedMessage)
        {
            logger.Received().Log(Arg.Is(logLevel),
                                  Arg.Is<EventId>(0),
                                  Arg.Is<object>(x => x.ToString() == formattedMessage),
                                  Arg.Is<Exception>(x => x == null),
                                  Arg.Any<Func<object, Exception, string>>());
        }

        public static void ReceivedMatchingArgs(this ILogger logger, LogLevel logLevel, Exception exception, string formattedMessage)
        {
            if (exception == null)
            {
                logger.ReceivedMatchingArgs(logLevel, formattedMessage);
            }

            var expectedException = exception.ToExpectedObject();

            logger.Received().Log(Arg.Is(logLevel),
                                  Arg.Is<EventId>(0),
                                  Arg.Is<object>(x => x.ToString() == formattedMessage),
                                  Arg.Is<Exception>(x => expectedException.Equals(x)),
                                  Arg.Any<Func<object, Exception, string>>());
        }

        public static void DidNotReceiveMatchingLogArgs(this ILogger logger)
        {
            logger.DidNotReceive().Log(Arg.Any<LogLevel>(),
                                       Arg.Any<EventId>(),
                                       Arg.Any<object>(),
                                       Arg.Any<Exception>(),
                                       Arg.Any<Func<object, Exception, string>>());
        }
    }
}