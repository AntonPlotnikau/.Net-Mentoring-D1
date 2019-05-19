c:

cd C:\Program Files (x86)\Log Parser 2.2

logparser "SELECT loglevel, message from D:\.Net-Mentoring-D1\9.Logging\Task3\logs.xml GROUP BY loglevel, message" -i:xml

PAUSE