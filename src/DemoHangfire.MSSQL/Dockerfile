FROM mcr.microsoft.com/mssql/server
WORKDIR /app

ARG SA_PASSWD

ENV ACCEPT_EULA="Y"
ENV SA_PASSWORD=${SA_PASSWD}

COPY DemoHangfire.MSSQL .

ENTRYPOINT [ "/bin/bash", "initDb.sh" ]
CMD [ "/opt/mssql/bin/sqlservr" ]